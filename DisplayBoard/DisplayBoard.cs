using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Xml.Linq;

namespace DisplayBoard
{    
    public partial class DisplayBoard : Form
    {        
        static XDocument XMLdoc = XDocument.Load(Application.StartupPath + @"\Parameter\Display.xml");
        string fontStr = XMLdoc.Descendants("form").FirstOrDefault().Attribute("font").Value;
        string DBline= XMLdoc.Descendants("lineNo").Descendants("lineNoValue").FirstOrDefault().Attribute("database").Value;//数据库的线别（查询数据时用到）
        string inputName; //input名字
        string outputName;//output名字
        List<string> dayShif = new List<string>();//白班字符串列表
        List<string> nightShif = new List<string>();//夜班字符串列表
        List<string> nowShifList;//现在班次的列表
        bool dayOrNight;//true到了白班时间段，false到了夜班时间段        
        double retestCount;//1次+2次重测个数
        double inputCount;//每个时间段input的叠加数
        int yesterdayInt = 0;//夜班部分时间是前一天的，获取夜班有多少个前一天的时间个数
        DateTime startTimeShif;//班次的开始时间（计算NG数和重测数的开始时间）
        string[] process_cd;//工位数组（用于查所有工位的NG数和重测数）
        //TPC: 引用DT计算类
        double dtRef = 0; // 百分比参考值固定
        DTService DT = new DTService();
        //ENDTPC: 引用DT计算类

        public DateTime dtpNow
        {
            get { return dtpDisplayTime.Value; }
        }

        public DisplayBoard()
        {
            InitializeComponent();            
            #region 样式
            Text = Text + "-" + Application.ProductVersion.ToString();
            //背景
            var varStr = XMLdoc.Descendants("form").FirstOrDefault().Attribute("backColor").Value;
            BackColor = ColorTranslator.FromHtml(varStr);
            dgvTarget.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml(varStr);
            dgvTarget.DefaultCellStyle.BackColor = ColorTranslator.FromHtml(varStr);
            dgvTarget.BackgroundColor = ColorTranslator.FromHtml(varStr);
            dgvTitle.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml(varStr);
            dgvTitle.DefaultCellStyle.BackColor = ColorTranslator.FromHtml(varStr);
            dgvTitle.BackgroundColor = ColorTranslator.FromHtml(varStr);
            dgvDisplay.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml(varStr);
            dgvDisplay.DefaultCellStyle.BackColor = ColorTranslator.FromHtml(varStr);
            dgvDisplay.BackgroundColor = ColorTranslator.FromHtml(varStr);
            //标题
            varStr = XMLdoc.Descendants("title").FirstOrDefault().Value;
            lblTitle.Text = DBHelper.DBremark.ToUpper() + " " + varStr;
            var varInt = Convert.ToInt16(XMLdoc.Descendants("title").FirstOrDefault().Attribute("size").Value);
            lblTitle.Font = new Font(fontStr, varInt);
            varStr = XMLdoc.Descendants("title").FirstOrDefault().Attribute("foreColor").Value;
            lblTitle.ForeColor = ColorTranslator.FromHtml(varStr);
            var varIntArr = Array.ConvertAll(XMLdoc.Descendants("title").FirstOrDefault().Attribute("location").Value.Split(','), int.Parse);
            lblTitle.Location = new Point(varIntArr[0], varIntArr[1]);
            //下拉框
            //string[] name = { "Cover Shield", "External Flex Attach", "Cover Assy", "Base Shield", "Internal Flex Attach", "Base Assey", "Magnet-Plate", "Magnet Insert", "Inner Mount", "L-Frame", "L-Bumper", "Tub Welding", "Cage Bumper", "CLD Attach", "Inner Parts Loading/Welding", "Inner Flexure Glue", "Docking", "SoftStop", "Base Welding", "Tab(E&W) welding", "Hotbar+Insulation Tape", "Tab(S) Welding", "Pre-bend+Label", "VMT", "TRAP4", "Final", "OK2SHIP" };
            IEnumerable<XElement> names = XMLdoc.Descendants("dgvTarget").Descendants(DBHelper.DBremark).Elements();//.FirstOrDefault();            
            foreach (var var in names)
            { cboInline.Items.Add(var.Name.LocalName.Replace("_", " ")); }
            //线别组别
            varInt = Convert.ToInt16(XMLdoc.Descendants("lineNo").FirstOrDefault().Attribute("size").Value);
            lblLineNoStr.Font = new Font(fontStr, varInt);
            varIntArr = Array.ConvertAll(XMLdoc.Descendants("lineNo").FirstOrDefault().Attribute("location").Value.Split(','), int.Parse);
            lblLineNoStr.Location = new Point(varIntArr[0], varIntArr[1]);
            //线别组别的值
            varStr = XMLdoc.Descendants("lineNo").Descendants("lineNoValue").FirstOrDefault().Value;
            lblLineNoValue.Text = DBline + "-" + varStr;
            varInt = Convert.ToInt16(XMLdoc.Descendants("lineNo").Descendants("lineNoValue").FirstOrDefault().Attribute("size").Value);
            lblLineNoValue.Font = new Font(fontStr, varInt);
            varStr = XMLdoc.Descendants("lineNo").Descendants("lineNoValue").FirstOrDefault().Attribute("foreColor").Value;
            lblLineNoValue.ForeColor = ColorTranslator.FromHtml(varStr);
            varIntArr = Array.ConvertAll(XMLdoc.Descendants("lineNo").Descendants("lineNoValue").FirstOrDefault().Attribute("location").Value.Split(','), int.Parse);
            lblLineNoValue.Location = new Point(varIntArr[0], varIntArr[1]);
            //预计目标
            varInt = Convert.ToInt16(XMLdoc.Descendants("target").FirstOrDefault().Attribute("size").Value);
            lblTarget.Font = new Font(fontStr, varInt);
            varIntArr = Array.ConvertAll(XMLdoc.Descendants("target").FirstOrDefault().Attribute("location").Value.Split(','), int.Parse);
            lblTarget.Location = new Point(varIntArr[0], varIntArr[1]);
            //预计目标的值
            int index = dgvTarget.Rows.Add();
            dgvTarget.Rows[index].Cells["Items_1"].Value = "Target";
            dgvTarget.Rows[index].Cells["Items_1"].Style.ForeColor = Color.Black;
            dgvTarget.Rows[index].Cells["Items_1"].Style.Font = new Font("Arial",10f);//Arial, 10pt
            #endregion
        }

        /// <summary>
        /// 目标值填入dgv中
        /// </summary>
        void fillTarget()
        {
            var var= XMLdoc.Descendants("dgvTarget").Descendants(DBHelper.DBremark).Descendants(cboInline.Text.Replace(" ","_")).FirstOrDefault();
            string[] strArr = var.Value.Split(',');
            dgvTarget.Rows[0].Cells["HeadCount_1"].Value = strArr[0];
            dgvTarget.Rows[0].Cells["DesignUPH_1"].Value = strArr[1];
            //CTBUPH
            double dou = Convert.ToDouble(dgvTarget.Rows[0].Cells["DesignUPH_1"].Value)*0.92;
            dgvTarget.Rows[0].Cells["CTBUPH_1"].Value = RoundUp(dou, 0);
            //UPPH
            dou = Convert.ToDouble(dgvTarget.Rows[0].Cells["CTBUPH_1"].Value)
              / Convert.ToDouble(dgvTarget.Rows[0].Cells["HeadCount_1"].Value);
            if (double.IsInfinity(dou) || double.IsNaN(dou))
            { dgvTarget.Rows[0].Cells["UPPH_1"].Value = "-"; }
            else
            { dgvTarget.Rows[0].Cells["UPPH_1"].Value = RoundUp(dou, 0); }
            //DT            
            dgvTarget.Rows[0].Cells["DT_1"].Value = strArr[2];

            //TPC : 赋值DT百分比参考值
            dtRef = Convert.ToDouble(strArr[2].Replace("%", ""));
            //ENDTPC : 赋值DT百分比参考值

            //RecheckRate            
            dgvTarget.Rows[0].Cells["RecheckRate_1"].Value = strArr[3];
            //Tossing            
            dgvTarget.Rows[0].Cells["Tossing_1"].Value = strArr[4];
            //OpteraterEfficiency            
            dgvTarget.Rows[0].Cells["OpteraterEfficiency_1"].Value = strArr[5];
            //TotalEfficiency
            dou = (1 - Convert.ToDouble(dgvTarget.Rows[0].Cells["DT_1"].Value.ToString().Replace("%", "")) / 100)
               * (1 - Convert.ToDouble(dgvTarget.Rows[0].Cells["RecheckRate_1"].Value.ToString().Replace("%", "")) / 100)
               * (1 - Convert.ToDouble(dgvTarget.Rows[0].Cells["Tossing_1"].Value.ToString().Replace("%", "")) / 100);
            dgvTarget.Rows[0].Cells["TotalEfficiency_1"].Value = RoundUp(dou * 100, 0) + "%";
            //Yield            
            dgvTarget.Rows[0].Cells["Yield_1"].Value = strArr[6];
        }

        /// <summary>
        /// 获取白班和夜班时间段
        /// </summary>
        void getTimeSpan()
        {
            for (int i = 1; ; i++)
            {
                string str = "timeSpan" + i.ToString();
                try { str = XMLdoc.Descendants("dayShif").Descendants(str).FirstOrDefault().Value; }
                catch { break; }
                dayShif.Add(str);
            }
            for (int i = 1; ; i++)
            {
                string str = "timeSpan" + i.ToString();
                try { str = XMLdoc.Descendants("nightShif").Descendants(str).FirstOrDefault().Value; }
                catch { break; }
                nightShif.Add(str);
            }
        }

        /// <summary>
        /// 获取当前应该显示白班还是夜班
        /// </summary>
        void getShowShif()
        {
            string startStr = dayShif[0].Split(',')[0];
            string endStr = nightShif[0].Split(',')[0];
            DateTime start = Convert.ToDateTime(dtpNow.ToString("yyyy/MM/dd ") + startStr);
            DateTime end = Convert.ToDateTime(dtpNow.ToString("yyyy/MM/dd ") + endStr);

            if (start <= dtpNow && dtpNow < end)
            { dayOrNight = true; }
            else
            { dayOrNight = false; }                
        }

        private void DisplayBoard_Load(object sender, EventArgs e)
        {
            tmrFlash.Interval = Convert.ToInt16(XMLdoc.Descendants("flash").FirstOrDefault().Value) * 1000;
            //获取白班夜班的时间list<string>
            getTimeSpan();

            #region 夜班部分时间是前一天的，获取夜班有多少个前一天的时间个数
            List<DateTime> timeList = new List<DateTime>();
            foreach (var var in nightShif)
            {
                string[] time = var.Split(',');
                timeList.Add(Convert.ToDateTime(time[0]));
                timeList.Add(Convert.ToDateTime(time[1]));
            }
            DateTime start = timeList[0];
            foreach (var var in timeList)
            {
                DateTime end = var;
                if (start > end)
                { break; }
                start = end;
                yesterdayInt++;
            }
            #endregion

            cboInline.SelectedIndex = 0;
            dgvDisplay.ClearSelection();            
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            #region 显示
            dgvTarget.ClearSelection();
            dgvDisplay.ClearSelection();            
            dtpShowTime.Value = dtpNow;
            #endregion
            #region 恢复初始值
            retestCount = 0;
            inputCount = 0;
            #endregion
            //获取设置的时间应该展示白班还是夜班（dayOrNight = true是白班）
            getShowShif();
            //重新布局，保存人数
            writeDGV_Config();

            //显示DGV
            int writeRowCount = 0;
            for (; writeRowCount < nowShifList.Count; writeRowCount++)
            {
                string[] time = nowShifList[writeRowCount].Split(',');
                DateTime start = Convert.ToDateTime(dtpNow.ToString("yyyy/MM/dd ")+time[0]);                
                DateTime end = Convert.ToDateTime(dtpNow.ToString("yyyy/MM/dd ") + time[1]);
                //如果是夜班（有部分是前一天），开始和结束的时间要-1或+1天
                if (!dayOrNight)
                {
                    DateTime d0 = Convert.ToDateTime(dayShif[0].Split(',')[0]);//白班开始时间8：00
                    int totalMinutes_UntiDayShif = d0.Hour * 60 + d0.Minute;
                    int totalMinutes_Now = dtpNow.Hour * 60 + dtpNow.Minute;
                    //0:00到早班开始前时间段
                    if (totalMinutes_Now < totalMinutes_UntiDayShif)
                    {
                        if (writeRowCount * 2 + 1 <= yesterdayInt)
                        {
                            start = start.AddDays(-1);
                        }
                        if (writeRowCount * 2 + 2 <= yesterdayInt)
                        {
                            end = end.AddDays(-1);
                        }
                    }
                    else
                    {
                        if (writeRowCount * 2 + 1 > yesterdayInt)
                        {
                            start = start.AddDays(1);
                        }
                        if (writeRowCount * 2 + 2 > yesterdayInt)
                        {
                            end = end.AddDays(1);
                        }
                    }
                }
                if (writeRowCount==0)
                    startTimeShif = start;
                if (start <= dtpNow)
                {
                    //在时间段内使用实际时间
                    if (dtpNow < end)
                    {
                        end = dtpNow;
                    }
                    //表格通常的行
                    writeDGV_General(start, end, writeRowCount);
                }
                else { break; }
            }
            //表格"Total"的行
            writeDGV_Total();
            //表格异常数据上色
            paint();
            // TPC: 调用DT方法
            string assy = cboInline.Text;
            writeDT(assy);
            // ENDTPC: 调用方法
        }

        private void TmrFlash_Tick(object sender, EventArgs e)
        {
            dtpDisplayTime.Value = DateTime.Now;
            BtnSave_Click(sender, e);
        }

        void writeDGV_Config()
        {
            //保存人数
            List<object> headCount = new List<object>();
            for (int i=0;i< dgvDisplay.Rows.Count-1; i++ )
            {
                object obj = dgvDisplay.Rows[i].Cells["HeadCount_2"].Value;
                headCount.Add(obj);
            }
            dgvDisplay.Rows.Clear();

            int index;
            if (dayOrNight) { nowShifList = dayShif; }
            else { nowShifList = nightShif; }
            
            foreach (string str in nowShifList)
            {
                string[] timeStr = str.Split(',');
                index = dgvDisplay.Rows.Add();
                dgvDisplay.Rows[index].Cells[0].Value = timeStr[0];
                dgvDisplay.Rows[index].Cells[1].Value = timeStr[1];
                dgvDisplay.Rows[index].Cells[0].Style.ForeColor = Color.Black;
                dgvDisplay.Rows[index].Cells[1].Style.ForeColor = Color.Black;
                try { dgvDisplay.Rows[index].Cells["HeadCount_2"].Value = headCount[index]; }
                catch { }
            }

            //写“合计”行
            index = dgvDisplay.Rows.Add();
            dgvDisplay.Rows[index].Cells[1].Value = "Total";
            dgvDisplay.Rows[index].Cells[1].Style.ForeColor = Color.Black;
        }

        //double input = 0, retest = 0;
        /// <summary>
        /// 写DGV普通行
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="rowInt">要写的行</param>
        void writeDGV_General(DateTime startTime, DateTime endTime, int rowInt)
        {            
            DataTable dt = new DataTable();
            StringBuilder sql = new StringBuilder();

            #region sql
            //sql.Clear();
            sql.AppendLine("with P as");
            sql.AppendLine("(select proc_uuid,process_cd");
            sql.AppendLine("from m_process");
            sql.AppendLine(string.Format("where process_cd in('{0}','{1}','{2}')", inputName, outputName, String.Join("','", process_cd)));
            sql.AppendLine(string.Format("and line_cd='{0}')", DBline));

            sql.AppendLine("--input数量");
            sql.AppendLine("select *from");
            sql.AppendLine(string.Format("(select count(distinct a1.serial_cd) from t_insp_{0} a1,P", DBHelper.DBremark));
            sql.AppendLine(string.Format("where a1.process_at>='{0}'::timestamp and a1.process_at<'{1}'::timestamp",
                startTime.ToString("yyyy-MM-dd HH:mm"), endTime.ToString("yyyy-MM-dd HH:mm")));
            sql.AppendLine("and a1.proc_uuid=P.proc_uuid");
            sql.AppendLine(string.Format("and P.process_cd='{0}') aa,", inputName));

            sql.AppendLine("--output数量");
            sql.AppendLine(string.Format("(select count(distinct a1.serial_cd) from t_insp_{0} a1,P", DBHelper.DBremark));
            sql.AppendLine(string.Format("where a1.process_at>='{0}'::timestamp and a1.process_at<'{1}'::timestamp",
                startTime.ToString("yyyy-MM-dd HH:mm"), endTime.ToString("yyyy-MM-dd HH:mm")));
            sql.AppendLine("and a1.proc_uuid=P.proc_uuid");
            sql.AppendLine(string.Format("and P.process_cd='{0}'", outputName));
            sql.AppendLine("and a1.judge_text = '0') bb,");

            sql.AppendLine("--NG数量");
            //sql.AppendLine(string.Format("(SELECT count(distinct serial_cd) FROM t_insp_{0} a1,P", DBHelper.DBremark));
            //sql.AppendLine(string.Format("where a1.process_at>='{0}'::timestamp and a1.process_at<'{1}'::timestamp",
            //    startTime.ToString("yyyy-MM-dd HH:mm"), endTime.ToString("yyyy-MM-dd HH:mm")));
            //sql.AppendLine("and a1.proc_uuid=P.proc_uuid");
            //sql.AppendLine(string.Format("and P.process_cd in('{0}','{1}','{2}')", inputName, outputName, String.Join("','", process_cd)));
            //sql.AppendLine("and a1.judge_text='1') cc,");
            sql.AppendLine("(select count(*)");
            sql.AppendLine("from(");
            sql.AppendLine("select judge_text ,row_number()over(partition by process_cd,serial_cd order by updated_at desc) rank");
            sql.AppendLine(string.Format("from t_insp_{0} a1,P", DBHelper.DBremark));
            sql.AppendLine(string.Format("where a1.process_at>='{0}'::timestamp and a1.process_at<'{1}'::timestamp"
                , startTime.ToString("yyyy-MM-dd HH:mm"), endTime.ToString("yyyy-MM-dd HH:mm")));
            sql.AppendLine("and a1.proc_uuid=P.proc_uuid");
            sql.AppendLine(string.Format("and P.process_cd in('{0}','{1}','{2}')", inputName, outputName, String.Join("','", process_cd)));
            sql.AppendLine(") list");
            sql.AppendLine("where rank=1");
            sql.AppendLine("and judge_text='1') cc,");

            sql.AppendLine("--NG1次或2次后,重测通过次数");
            sql.AppendLine("(select sum(ng) from(");
            sql.AppendLine("SELECT COUNT(*) AS ng FROM(");
            sql.AppendLine("SELECT serial_cd,datatype_id,line_cd,process_cd,process_at AS latest_process_at,judge_text AS latest_judge_text,test_count,fail_count,COUNT(*) FILTER (WHERE ROW_NUMBER = '1') OVER (PARTITION BY line_cd,process_cd) AS total_count");
            sql.AppendLine("FROM(");
            sql.AppendLine("SELECT a.process_at,a.serial_cd,a.datatype_id,b.line_cd,b.process_cd,a.judge_text,");
            sql.AppendLine("ROW_NUMBER() OVER (PARTITION BY a.serial_cd,b.process_cd ORDER BY a.process_at ASC) +");
            sql.AppendLine("ROW_NUMBER() OVER (PARTITION BY a.serial_cd,b.process_cd ORDER BY a.process_at DESC) - 1 AS test_count,");
            sql.AppendLine("COUNT(*) FILTER (WHERE a.judge_text = '1') OVER (PARTITION BY a.serial_cd,b.process_cd) AS fail_count,");
            sql.AppendLine("ROW_NUMBER() OVER (PARTITION BY a.serial_cd,b.process_cd ORDER BY a.process_at DESC)");
            sql.AppendLine(string.Format("FROM t_insp_{0} AS a", DBHelper.DBremark));
            sql.AppendLine("LEFT JOIN m_process AS b ON a.proc_uuid = b.proc_uuid");
            sql.AppendLine(string.Format("WHERE a.process_at >= '{0}'", startTimeShif.ToString("yyyy-MM-dd HH:mm")));
            sql.AppendLine(string.Format("AND a.process_at < '{0}'", endTime.ToString("yyyy-MM-dd HH:mm")));
            sql.AppendLine(string.Format("AND process_cd in ('{0}','{1}','{2}')", inputName, outputName, String.Join("','", process_cd)));
            sql.AppendLine(") AS m");
            sql.AppendLine("WHERE ROW_NUMBER = '1'");
            sql.AppendLine(") AS abc");
            sql.AppendLine("WHERE latest_judge_text = '0'");
            sql.AppendLine("AND ((fail_count = '1'AND test_count = '2') OR (fail_count = '2'AND test_count = '3'))");
            sql.AppendLine(string.Format("AND line_cd='{0}'", DBline));
            sql.AppendLine("GROUP BY line_cd,total_count");
            sql.AppendLine(")test)dd");
            #endregion

            //测试工位，数据在另外的数据库
            if (cboInline.Text == "VMT" || cboInline.Text == "TRAP4")
            {
                new DBHelper().ExecuteDataTable(2, sql.ToString(), ref dt);
            }
            else { new DBHelper().ExecuteDataTable(1, sql.ToString(), ref dt); }
            
            var var = dt.Rows[0][3].ToString();//如果是null转成0，其他1、2、3...不需要改变
            if (var == "")
            {
                dt.Rows[0][3] = 0;
            }


            //Working Time_2(plan)
            dgvDisplay.Rows[rowInt].Cells["WorkingTime_2"].Value = RoundUp((endTime - startTime).TotalMinutes, 0);
            //InputQty_2(plan)
            var vardou = Convert.ToDouble(dgvDisplay.Rows[rowInt].Cells["WorkingTime_2"].Value) / 60
                   * Convert.ToDouble(dgvTarget.Rows[0].Cells["CTBUPH_1"].Value);
            dgvDisplay.Rows[rowInt].Cells["InputQty_2"].Value = RoundUp(vardou, 0);            
            //Working Time_
            dgvDisplay.Rows[rowInt].Cells["WorkingTime_"].Value = dgvDisplay.Rows[rowInt].Cells["WorkingTime_2"].Value;//计划时间-DT时间(暂缓开发)
            //Input Qty.
            dgvDisplay.Rows[rowInt].Cells["InputQty_"].Value = dt.Rows[0][0];
            //Output Qty.
            dgvDisplay.Rows[rowInt].Cells["OutputQty_2"].Value = dt.Rows[0][1];
            //Input Gap
            dgvDisplay.Rows[rowInt].Cells["InputGap_2"].Value = Convert.ToInt16(dgvDisplay.Rows[rowInt].Cells["InputQty_"].Value)
                - Convert.ToInt16(dgvDisplay.Rows[rowInt].Cells["InputQty_2"].Value);
            //Input UPH
            vardou = (Convert.ToDouble(dgvDisplay.Rows[rowInt].Cells["InputQty_"].Value)
                / Convert.ToDouble(dgvDisplay.Rows[rowInt].Cells["WorkingTime_2"].Value) * 60);           
            if (double.IsInfinity(vardou) || double.IsNaN(vardou))
            { dgvDisplay.Rows[rowInt].Cells["InputUPH_2"].Value = "-"; }
            else
            { dgvDisplay.Rows[rowInt].Cells["InputUPH_2"].Value = RoundUp(vardou, 0); }
            //Input UPPH
            vardou = Convert.ToDouble(dgvDisplay.Rows[rowInt].Cells["InputUPH_2"].Value)
                / Convert.ToDouble(dgvDisplay.Rows[rowInt].Cells["HeadCount_2"].Value);
            if (double.IsInfinity(vardou) || double.IsNaN(vardou))
            { dgvDisplay.Rows[rowInt].Cells["InputUPPH_2"].Value = "-"; }
            else
            { dgvDisplay.Rows[rowInt].Cells["InputUPPH_2"].Value = RoundUp(vardou, 0); }
            //Output UPH
            vardou = Convert.ToDouble(dgvDisplay.Rows[rowInt].Cells["OutputQty_2"].Value)
                / Convert.ToDouble(dgvDisplay.Rows[rowInt].Cells["WorkingTime_2"].Value) * 60;
            if (double.IsInfinity(vardou) || double.IsNaN(vardou))
            { dgvDisplay.Rows[rowInt].Cells["OutputUPH_2"].Value = "-"; }
            else
            { dgvDisplay.Rows[rowInt].Cells["OutputUPH_2"].Value = RoundUp(vardou, 0); }
            //Efficiency
            vardou = (Convert.ToDouble(dgvDisplay.Rows[rowInt].Cells["InputUPH_2"].Value)
                / Convert.ToDouble(dgvTarget.Rows[0].Cells["DesignUPH_1"].Value));
            if (double.IsInfinity(vardou) || double.IsNaN(vardou))
            { dgvDisplay.Rows[rowInt].Cells["Efficiency_2"].Value = "-"; }
            else
            { dgvDisplay.Rows[rowInt].Cells["Efficiency_2"].Value = RoundUp(vardou * 100, 1) + "%"; }
            //NG Qty.
            dgvDisplay.Rows[rowInt].Cells["NGQty_2"].Value = dt.Rows[0][2];
            //First Pass Yield Rate
            vardou = Convert.ToDouble(dgvDisplay.Rows[rowInt].Cells["OutputQty_2"].Value)
                / (Convert.ToDouble(dgvDisplay.Rows[rowInt].Cells["OutputQty_2"].Value)
                + Convert.ToDouble(dgvDisplay.Rows[rowInt].Cells["NGQty_2"].Value));
            if (double.IsInfinity(vardou) || double.IsNaN(vardou))
            { dgvDisplay.Rows[rowInt].Cells["FirstPassYieldRate_2"].Value = "-"; }
            else
            { dgvDisplay.Rows[rowInt].Cells["FirstPassYieldRate_2"].Value = RoundUp(vardou*100, 1) + "%"; }
            //Retest rate(1 + 2nd time)
            //从SFC中收集各工站测试2次+3次的次数/Input数量（各工站的重测率之和）
            retestCount = Convert.ToDouble(dt.Rows[0][3]);//最后一行合计用            
            inputCount += Convert.ToDouble(dt.Rows[0][0]);
            //改成累加
            vardou = Convert.ToDouble(dt.Rows[0][3]) / inputCount;
            if (double.IsInfinity(vardou) || double.IsNaN(vardou))
            { dgvDisplay.Rows[rowInt].Cells["RetestRate_2"].Value = "-"; }
            else
            { dgvDisplay.Rows[rowInt].Cells["RetestRate_2"].Value = RoundUp(vardou * 100, 1) + "%"; }
            //Operator loss
            try
            {
                vardou = 1 - Convert.ToDouble(dgvDisplay.Rows[rowInt].Cells["Efficiency_2"].Value.ToString().Replace("%", "")) / 100
                    //- Convert.ToDouble(dgvDisplay.Rows[rowInt].Cells["DT_2"].Value.ToString().Replace("%", ""))/100
                    - Convert.ToDouble(dgvDisplay.Rows[rowInt].Cells["RetestRate_2"].Value.ToString().Replace("%", "")) / 100;
                if (double.IsInfinity(vardou) || double.IsNaN(vardou))
                { dgvDisplay.Rows[rowInt].Cells["OperatorLoss_2"].Value = "-"; }
                else
                { dgvDisplay.Rows[rowInt].Cells["OperatorLoss_2"].Value = RoundUp(vardou * 100, 1) + "%"; }
            }
            catch { dgvDisplay.Rows[rowInt].Cells["OperatorLoss_2"].Value = "-"; }
        }

        /// <summary>
        /// 合计统计行
        /// </summary>
        void writeDGV_Total()
        {
            double sum1 = 0, sum2 = 0, sum3 = 0, sum4 = 0, sum5 = 0, sum6 = 0, sum7 = 0;
            for (int i = 0; i < dgvDisplay.Rows.Count - 1; i++)
            {
                sum1 += Convert.ToInt16(dgvDisplay.Rows[i].Cells["WorkingTime_2"].Value);
                sum2 += Convert.ToInt16(dgvDisplay.Rows[i].Cells["InputQty_2"].Value);
                sum3 += Convert.ToInt16(dgvDisplay.Rows[i].Cells["HeadCount_2"].Value);
                sum4 += Convert.ToInt16(dgvDisplay.Rows[i].Cells["WorkingTime_"].Value);
                sum5 += Convert.ToInt16(dgvDisplay.Rows[i].Cells["InputQty_"].Value);
                sum6 += Convert.ToInt16(dgvDisplay.Rows[i].Cells["OutputQty_2"].Value);

                sum7 += Convert.ToInt16(dgvDisplay.Rows[i].Cells["NGQty_2"].Value);
            }
            int rowInt = dgvDisplay.Rows.Count - 1;
            dgvDisplay.Rows[rowInt].Cells["WorkingTime_2"].Value = sum1.ToString();
            dgvDisplay.Rows[rowInt].Cells["InputQty_2"].Value = sum2.ToString();
            dgvDisplay.Rows[rowInt].Cells["HeadCount_2"].Value = sum3.ToString();
            dgvDisplay.Rows[rowInt].Cells["WorkingTime_"].Value = sum4.ToString();
            dgvDisplay.Rows[rowInt].Cells["InputQty_"].Value = sum5.ToString();
            dgvDisplay.Rows[rowInt].Cells["OutputQty_2"].Value = sum6.ToString();


            //Input Gap
            dgvDisplay.Rows[rowInt].Cells["InputGap_2"].Value = Convert.ToDouble(dgvDisplay.Rows[rowInt].Cells["InputQty_"].Value)
                - Convert.ToDouble(dgvDisplay.Rows[rowInt].Cells["InputQty_2"].Value);
            //Input UPH
            var vardou = (Convert.ToDouble(dgvDisplay.Rows[rowInt].Cells["InputQty_"].Value)
                / Convert.ToDouble(dgvDisplay.Rows[rowInt].Cells["WorkingTime_2"].Value) * 60);
            if (double.IsInfinity(vardou) || double.IsNaN(vardou))
            { dgvDisplay.Rows[rowInt].Cells["InputUPH_2"].Value = "-"; }
            else
            { dgvDisplay.Rows[rowInt].Cells["InputUPH_2"].Value = RoundUp(vardou, 0); }
            //Input UPPH
            vardou = Convert.ToDouble(dgvDisplay.Rows[rowInt].Cells["InputUPH_2"].Value)
                / Convert.ToDouble(dgvDisplay.Rows[rowInt].Cells["HeadCount_2"].Value);
            if (double.IsInfinity(vardou) || double.IsNaN(vardou))
            { dgvDisplay.Rows[rowInt].Cells["InputUPPH_2"].Value = "-"; }
            else
            { dgvDisplay.Rows[rowInt].Cells["InputUPPH_2"].Value = RoundUp(vardou, 0); }
            //Output UPH
            vardou = Convert.ToDouble(dgvDisplay.Rows[rowInt].Cells["OutputQty_2"].Value)
                / Convert.ToDouble(dgvDisplay.Rows[rowInt].Cells["WorkingTime_2"].Value) * 60;
            if (double.IsInfinity(vardou) || double.IsNaN(vardou))
            { dgvDisplay.Rows[rowInt].Cells["OutputUPH_2"].Value = "-"; }
            else
            { dgvDisplay.Rows[rowInt].Cells["OutputUPH_2"].Value = RoundUp(vardou, 0); }
            //Efficiency
            vardou = (Convert.ToDouble(dgvDisplay.Rows[rowInt].Cells["InputUPH_2"].Value)
                / Convert.ToDouble(dgvTarget.Rows[0].Cells["DesignUPH_1"].Value));
            if (double.IsInfinity(vardou) || double.IsNaN(vardou))
            { dgvDisplay.Rows[rowInt].Cells["Efficiency_2"].Value = "-"; }
            else
            { dgvDisplay.Rows[rowInt].Cells["Efficiency_2"].Value = RoundUp(vardou * 100, 1) + "%"; }
            //NG Qty.
            dgvDisplay.Rows[rowInt].Cells["NGQty_2"].Value = sum7.ToString();
            //First Pass Yield Rate
            vardou = Convert.ToDouble(dgvDisplay.Rows[rowInt].Cells["OutputQty_2"].Value)
                / (Convert.ToDouble(dgvDisplay.Rows[rowInt].Cells["OutputQty_2"].Value)
                + Convert.ToDouble(dgvDisplay.Rows[rowInt].Cells["NGQty_2"].Value));
            if (double.IsInfinity(vardou) || double.IsNaN(vardou))
            { dgvDisplay.Rows[rowInt].Cells["FirstPassYieldRate_2"].Value = "-"; }
            else
            { dgvDisplay.Rows[rowInt].Cells["FirstPassYieldRate_2"].Value = RoundUp(vardou * 100, 1) + "%"; }
            //Retest rate(1 + 2nd time)
            //从SFC中收集各工站测试2次+3次的次数/Input数量（各工站的重测率之和）
            vardou = retestCount / Convert.ToDouble(dgvDisplay.Rows[rowInt].Cells["InputQty_"].Value);
            if (double.IsInfinity(vardou) || double.IsNaN(vardou))
            { dgvDisplay.Rows[rowInt].Cells["RetestRate_2"].Value = "-"; }
            else
            { dgvDisplay.Rows[rowInt].Cells["RetestRate_2"].Value = RoundUp(vardou * 100, 1) + "%"; }
            //Operator loss
            try
            {
                vardou = 1 - Convert.ToDouble(dgvDisplay.Rows[rowInt].Cells["Efficiency_2"].Value.ToString().Replace("%", "")) / 100
              //- Convert.ToDouble(dgvDisplay.Rows[rowInt].Cells["DT_2"].Value.ToString().Replace("%", ""))/100
              - Convert.ToDouble(dgvDisplay.Rows[rowInt].Cells["RetestRate_2"].Value.ToString().Replace("%", "")) / 100;
                if (double.IsInfinity(vardou) || double.IsNaN(vardou))
                { dgvDisplay.Rows[rowInt].Cells["OperatorLoss_2"].Value = "-"; }
                else
                { dgvDisplay.Rows[rowInt].Cells["OperatorLoss_2"].Value = RoundUp(vardou * 100, 1) + "%"; }
            }
            catch { dgvDisplay.Rows[rowInt].Cells["OperatorLoss_2"].Value = "-"; }
        }

        /// <summary>
        /// 不达标显红色
        /// </summary>
        void paint()
        {
            for (int i = 0; i < dgvDisplay.RowCount; i++)
            {
                #region 不达标显突出颜色
                //Head Count
                try
                {
                    int a = Convert.ToInt16(dgvDisplay.Rows[i].Cells["HeadCount_2"].Value);
                    double b = RoundUp(Convert.ToDouble(dgvTarget.Rows[0].Cells["HeadCount_1"].Value), 0);
                    if (a != b)
                        dgvDisplay.Rows[i].Cells["HeadCount_2"].Style.ForeColor = Color.Red;
                    else
                        dgvDisplay.Rows[i].Cells["HeadCount_2"].Style.ForeColor = Color.White;
                }
                catch { dgvDisplay.Rows[i].Cells["HeadCount_2"].Style.ForeColor = Color.Red; }
                //Input Gap
                try
                {
                    int a = Convert.ToInt16(dgvDisplay.Rows[i].Cells["InputGap_2"].Value);
                    if (a <0)
                        dgvDisplay.Rows[i].Cells["InputGap_2"].Style.ForeColor = Color.Red;
                    else
                        dgvDisplay.Rows[i].Cells["InputGap_2"].Style.ForeColor = Color.White;
                }
                catch
                {
                    if (dgvDisplay.Rows[i].Cells["InputGap_2"].Value == "-")
                    { dgvDisplay.Rows[i].Cells["InputGap_2"].Style.ForeColor = Color.White; }
                    else { dgvDisplay.Rows[i].Cells["InputGap_2"].Style.ForeColor = Color.Red; }
                }
                //Input UPH
                try
                {
                    int a = Convert.ToInt16(dgvDisplay.Rows[i].Cells["InputUPH_2"].Value);
                    int b = Convert.ToInt16(dgvTarget.Rows[0].Cells["CTBUPH_1"].Value);
                    if (a < b)
                        dgvDisplay.Rows[i].Cells["InputUPH_2"].Style.ForeColor = Color.Red;
                    else
                        dgvDisplay.Rows[i].Cells["InputUPH_2"].Style.ForeColor = Color.White;
                }
                catch
                {
                    if (dgvDisplay.Rows[i].Cells["InputUPH_2"].Value == "-")
                    { dgvDisplay.Rows[i].Cells["InputUPH_2"].Style.ForeColor = Color.White; }
                    else { dgvDisplay.Rows[i].Cells["InputUPH_2"].Style.ForeColor = Color.Red; }
                }
                //Input UPPH
                try
                {
                    int a = Convert.ToInt16(dgvDisplay.Rows[i].Cells["InputUPPH_2"].Value);
                    int b = Convert.ToInt16(dgvTarget.Rows[0].Cells["UPPH_1"].Value);
                    if (a < b)
                        dgvDisplay.Rows[i].Cells["InputUPPH_2"].Style.ForeColor = Color.Red;
                    else
                        dgvDisplay.Rows[i].Cells["InputUPPH_2"].Style.ForeColor = Color.White;                    
                }
                catch
                {
                    if (dgvDisplay.Rows[i].Cells["InputUPPH_2"].Value == "-")
                    { dgvDisplay.Rows[i].Cells["InputUPPH_2"].Style.ForeColor = Color.White; }
                    else { dgvDisplay.Rows[i].Cells["InputUPPH_2"].Style.ForeColor = Color.Red; }
                }
                //Output UPH
                try
                {
                    int a = Convert.ToInt16(dgvDisplay.Rows[i].Cells["OutputUPH_2"].Value);
                    int b = Convert.ToInt16(dgvTarget.Rows[0].Cells["CTBUPH_1"].Value);
                    if (a < b)
                        dgvDisplay.Rows[i].Cells["OutputUPH_2"].Style.ForeColor = Color.Red;
                    else
                        dgvDisplay.Rows[i].Cells["OutputUPH_2"].Style.ForeColor = Color.White;
                }
                catch
                {
                    if (dgvDisplay.Rows[i].Cells["OutputUPH_2"].Value == "-")
                    { dgvDisplay.Rows[i].Cells["OutputUPH_2"].Style.ForeColor = Color.White; }
                    else { dgvDisplay.Rows[i].Cells["OutputUPH_2"].Style.ForeColor = Color.Red; }
                }
                //Efficiency
                try
                {
                    double a = Convert.ToDouble(dgvDisplay.Rows[i].Cells["Efficiency_2"].Value.ToString().Replace("%", ""));
                    double b = Convert.ToDouble(dgvTarget.Rows[0].Cells["TotalEfficiency_1"].Value.ToString().Replace("%", ""));
                    if (a < b || double.IsInfinity(a) || double.IsNaN(a))
                        dgvDisplay.Rows[i].Cells["Efficiency_2"].Style.ForeColor = Color.Red;
                    else
                        dgvDisplay.Rows[i].Cells["Efficiency_2"].Style.ForeColor = Color.White;
                }
                catch
                {
                    if (dgvDisplay.Rows[i].Cells["Efficiency_2"].Value == "-")
                    { dgvDisplay.Rows[i].Cells["Efficiency_2"].Style.ForeColor = Color.White; }
                    else { dgvDisplay.Rows[i].Cells["Efficiency_2"].Style.ForeColor = Color.Red; }
                }
                //First Pass Yield Rate
                try
                {
                    double a = Convert.ToDouble(dgvDisplay.Rows[i].Cells["FirstPassYieldRate_2"].Value.ToString().Replace("%", ""));
                    double b = Convert.ToDouble(dgvTarget.Rows[0].Cells["Yield_1"].Value.ToString().Replace("%", ""));
                    if (a < b || double.IsInfinity(a) || double.IsNaN(a))
                        dgvDisplay.Rows[i].Cells["FirstPassYieldRate_2"].Style.ForeColor = Color.Red;
                    else
                        dgvDisplay.Rows[i].Cells["FirstPassYieldRate_2"].Style.ForeColor = Color.White;
                }
                catch
                {
                    if (dgvDisplay.Rows[i].Cells["FirstPassYieldRate_2"].Value == "-")
                    { dgvDisplay.Rows[i].Cells["FirstPassYieldRate_2"].Style.ForeColor = Color.White; }
                    else { dgvDisplay.Rows[i].Cells["FirstPassYieldRate_2"].Style.ForeColor = Color.Red; }
                }
                //RetestRate
                try
                {
                    double a = Convert.ToDouble(dgvDisplay.Rows[i].Cells["RetestRate_2"].Value.ToString().Replace("%", ""));
                    double b = Convert.ToDouble(dgvTarget.Rows[0].Cells["RecheckRate_1"].Value.ToString().Replace("%", ""));
                    if (a > b || double.IsInfinity(a) || double.IsNaN(a))
                        dgvDisplay.Rows[i].Cells["RetestRate_2"].Style.ForeColor = Color.Red;
                    else
                        dgvDisplay.Rows[i].Cells["RetestRate_2"].Style.ForeColor = Color.White;
                }
                catch
                {
                    if (dgvDisplay.Rows[i].Cells["RetestRate_2"].Value == "-")
                    { dgvDisplay.Rows[i].Cells["RetestRate_2"].Style.ForeColor = Color.White; }
                    else { dgvDisplay.Rows[i].Cells["RetestRate_2"].Style.ForeColor = Color.Red; }
                }
                //OperatorLoss
                try
                {
                    double a = Convert.ToDouble(dgvDisplay.Rows[i].Cells["OperatorLoss_2"].Value.ToString().Replace("%", ""));
                    double b = Convert.ToDouble(dgvTarget.Rows[0].Cells["Tossing_1"].Value.ToString().Replace("%", ""));
                    if (a > b || double.IsInfinity(a) || double.IsNaN(a))
                        dgvDisplay.Rows[i].Cells["OperatorLoss_2"].Style.ForeColor = Color.Red;
                    else
                        dgvDisplay.Rows[i].Cells["OperatorLoss_2"].Style.ForeColor = Color.White;
                }
                catch
                {
                    if (dgvDisplay.Rows[i].Cells["OperatorLoss_2"].Value == "-")
                    { dgvDisplay.Rows[i].Cells["OperatorLoss_2"].Style.ForeColor = Color.White; }
                    else { dgvDisplay.Rows[i].Cells["OperatorLoss_2"].Style.ForeColor = Color.Red; }
                }
                #endregion
            }
        }

        private void CboInline_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillTarget();
            var var = XMLdoc.Descendants("dgvTarget").Descendants(DBHelper.DBremark).Descendants(cboInline.Text.Replace(" ", "_")).FirstOrDefault();
            inputName = var.Attribute("input").Value;
            outputName = var.Attribute("output").Value;
            process_cd = var.Attribute("process").Value.Split(',');
            dtpDisplayTime.Value = DateTime.Now;
            BtnSave_Click(sender, e);
            lblLineNoValue.Text = lblLineNoValue.Text.Substring(0, 4) + cboInline.Text;
        }

        #region 向上舍入
        /// <summary>
        /// 远离 0 向上舍入
        /// </summary>
        decimal RoundUp(decimal value, sbyte digits)
        {
            if (digits == 0)
            {
                return (value >= 0 ? decimal.Ceiling(value) : decimal.Floor(value));
            }

            decimal multiple = Convert.ToDecimal(Math.Pow(10, digits));
            return (value >= 0 ? decimal.Ceiling(value * multiple) : decimal.Floor(value * multiple)) / multiple;
        }


        /// <summary>
        /// 向上舍入
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="digits">距离小数点的左右，0表示向上整数舍入 </param>
        /// <returns></returns>
        double RoundUp(double value, sbyte digits)
        {
            return decimal.ToDouble(RoundUp(Convert.ToDecimal(value), digits));
        }
        #endregion

        #region TPC:写入DT数据
        /// <summary>
        /// 写入DT数据
        /// </summary>
        void writeDT(string assy)
        {
            try
            {
                // 测试夜班
                //List<double[]> dtData = DT.GetDTMin(inputName, outputName, process_cd, DBline, false, nightShif);

                List<double[]> dtData = DT.GetDTMin(inputName, outputName, process_cd, DBline, dayOrNight, nowShifList, assy);

                if (dtData == null)
                {
                    for (int i = 0; i < dgvDisplay.RowCount; i++)
                    {
                        dgvDisplay.Rows[i].Cells["DT_2"].Value = "-";
                        dgvDisplay.Rows[i].Cells["DTrate_2"].Value = "-";
                    }
                }
                else
                {
                    for (int i = 0; i < dgvDisplay.RowCount; i++)
                    {

                        try
                        {
                            dgvDisplay.Rows[i].Cells["DT_2"].Value = dtData[i][0].ToString();
                            dgvDisplay.Rows[i].Cells["DTrate_2"].Value = dtData[i][1].ToString() + "%";

                            if (dtData[i][1] >= dtRef)
                            {
                                dgvDisplay.Rows[i].Cells["DT_2"].Style.ForeColor = Color.Red;
                                dgvDisplay.Rows[i].Cells["DTrate_2"].Style.ForeColor = Color.Red;
                            }
                        }
                        catch
                        {
                            dgvDisplay.Rows[i].Cells["DT_2"].Value = "-";
                            dgvDisplay.Rows[i].Cells["DTrate_2"].Value = "-";
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        #endregion
    }
}