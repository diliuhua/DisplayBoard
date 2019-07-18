using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Xml.Linq;
using System.Windows.Forms;
using DisplayBoard.Util;

namespace DisplayBoard
{
    class DTService
    {
        /// <summary>
        /// 获取DT的百分比和分钟数
        /// </summary>
        /// <param name="inputName"></param>
        /// <param name="outputName"></param>
        /// <param name="process_cd"></param>
        /// <param name="DBline"></param>
        /// <param name="dayOrNight"></param>
        /// <param name="nowShifList"></param>
        /// <returns></returns>
        public List<double[]> GetDTMin(string inputName, string outputName, string[] process_cd, string DBline, bool dayOrNight, List<string> nowShifList, string assy)
        {
            List<DateTime[]> timeSpan = ConvertTimeSpan(dayOrNight, nowShifList); // 获取格式化后的DateTime
            if (timeSpan.Count == 0) return null;
            //string a = cboInline.Text;
            List<DateTime> rangeDate = GetRangeDate(dayOrNight, nowShifList); // 查询班次的范围
            //DataTable dt = QueryDT(inputName, outputName, process_cd, DBline, rangeDate, assy); // 获取数据
            //Dictionary<string, List<DtData>> dictProcDt = GetDtDataByProc(dt); // 获取分组的Proc的DT
            //List<DtData> dtDataList = GetDtDataAll(dictProcDt, timeSpan); // 对重复时间进行整合后的DT
            List<DtData> dtDataList = GetDtData(DBline, rangeDate, assy);
            List<double[]> dtData = ComputeDT(dtDataList, timeSpan); // 计算
            return dtData;
        }

        /// <summary>
        /// 数据库查询出数据
        /// </summary>
        /// <param name="inputName"></param>
        /// <param name="outputName"></param>
        /// <param name="process_cd"></param>
        /// <param name="DBline"></param>
        /// <param name="dayOrNight"></param>
        /// <param name="nowShifList"></param>
        /// <returns></returns>
        //private DataTable QueryDT(string inputName, string outputName, string[] process_cd, string DBline, List<DateTime> rangeDate)
        //{
        //    string process_cd_in = string.Format("'{0}', '{1}', '{2}'", inputName, outputName, String.Join("','", process_cd));

        //    // 查询数据
        //    //测试用
        //    string sql = string.Format(@"
        //        SELECT AA.proc_uuid, process_at, judge_text, 
        //      ROW_NUMBER() OVER (PARTITION BY AA.proc_uuid ORDER BY process_at) AS rid
        //        FROM(		 
        //         SELECT proc_uuid,process_cd
        //         FROM m_process
        //         WHERE process_cd in('AE-1') 
        //         AND line_cd='L03'
        //        )PP
        //        LEFT JOIN t_insp_kk07 AA ON AA.proc_uuid = pp.proc_uuid
        //        WHERE process_at >= '2018-06-30 20:00' and  process_at < '2018-07-01 05:00' 
        //    ");

        //    //string sql = string.Format(@"
        //    //    SELECT AA.proc_uuid, process_at, judge_text, 
        //    //  ROW_NUMBER() OVER (PARTITION BY AA.proc_uuid ORDER BY process_at) AS rid
        //    //    FROM(		 
        //    //     SELECT proc_uuid,process_cd
        //    //     FROM m_process
        //    //     WHERE process_cd in({0}) 
        //    //     AND line_cd='{1}' 
        //    //    )PP
        //    //    LEFT JOIN t_insp_{2} AA ON AA.proc_uuid = pp.proc_uuid
        //    //    WHERE process_at >= '{3}' and  process_at < '{4}' 
        //    //", process_cd_in, DBline, DBHelper.DBremark, rangeDate[0].ToString("yyyy-MM-dd HH:mm"), rangeDate[1].ToString("yyyy-MM-dd HH:mm"));

        //    DataTable dt = new DataTable();
        //    Console.WriteLine(sql);
        //    new DBHelper().ExecuteDataTable(1, sql, ref dt);
        //    return dt;
        //}

        private DataTable QueryDT(string inputName, string outputName, string[] process_cd, string DBline, List<DateTime> rangeDate, string assy)
        {
            //string start_time = rangeDate[0].ToString("yyyy-MM-dd HH:mm:ss");
            //string end_time = rangeDate[1].ToString("yyyy-MM-dd HH:mm:ss");

            string start_time = "2018-07-10 17:00:00";
            string end_time = "2019-07-17 17:00:00";
            assy = "TUB";
            SummaryDataModel summaryDataModel = new SummaryDataModel()
            {
                line_cd = DBline,
                assy_cd = assy,
                start = start_time,
                end = end_time
            };

            string summaryDataModelStr = SerializeObject<SummaryDataModel>(summaryDataModel);
            string url = "http://10.107.171.57:8000/get_status_data_api";
            string res = PostHttp(url, summaryDataModelStr);
            ResObjModel<List<SummaryDataModel>> resObj = DeserializeJsonToObject<ResObjModel<List<SummaryDataModel>>>(res);
            DataTable dt = new DataTable();
            if (resObj.state)
            {
                dt.Columns.Add("proc_uuid");
                dt.Columns.Add("process_at");
                dt.Columns.Add("judge_text");
                dt.Columns.Add("rid");
                foreach (SummaryDataModel item in resObj.data) {
                    DataRow dr = dt.NewRow();
                    dr["line_cd"] = item.line_cd.ToString();
                    dr["assy_cd"] = item.assy_cd.ToString();
                    dr["start"] = item.start.ToString();
                    dr["end"] = item.end.ToString();
                    dt.Rows.Add(dr);
                }
                return dt;
            }
            return dt;
        }

        /// <summary>
        /// Get start-end time list
        /// </summary>
        /// <param name="DBline"></param>
        /// <param name="rangeDate"></param>
        /// <param name="assy"></param>
        /// <returns></returns>
        private List<DtData> GetDtData(string DBline, List<DateTime> rangeDate, string assy)
        {
            List<DtData> dtDataRet = new List<DtData>();
            try
            {
                XDocument XMLdoc = XDocument.Load(Application.StartupPath + @"\Parameter\Display.xml");
                string url = XMLdoc.Descendants("url").FirstOrDefault().Value;
                string start_time = rangeDate[0].ToString("yyyy-MM-dd HH:mm:ss");
                string end_time = rangeDate[1].ToString("yyyy-MM-dd HH:mm:ss"); 
                SummaryDataModel summaryDataModel = new SummaryDataModel()
                {
                    line_cd = DBline,
                    assy_cd = assy,
                    start = start_time,
                    end = end_time
                };
                string summaryDataModelStr = SerializeObject<SummaryDataModel>(summaryDataModel);
                string res = PostHttp(url, summaryDataModelStr);
                ResObjModel<List<DtData>> resObj = DeserializeJsonToObject<ResObjModel<List<DtData>>>(res);
                foreach (DtData item in resObj.data)
                {
                    dtDataRet.Add(new DtData { start = item.start, end = item.end });
                }
                return dtDataRet;
            }
            catch(Exception ex)
            {
                string str = LogHelper.GetExceptionMsg(ex, "獲取API數據失敗！");
                LogHelper.WriteErrorLog(str);
                return dtDataRet;
            }
            
        }

        /// <summary>
        /// 将对象序列化为JSON格式
        /// </summary>
        /// <param name="o">对象</param>
        /// <returns>json字符串</returns>
        public static string SerializeObject<T>(T t)
        {
            string json = JsonConvert.SerializeObject(t);
            return json;
        }

        /// <summary>
        ///  HttpWebRequest模拟GET请求
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetHttp(string url)
        {
            HttpWebRequest httpWebRequest = null;
            HttpWebResponse httpWebResponse = null;
            StreamReader streamReader = null;
            string responseContent = "";

            try
            {
                httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "text/html;charset=UTF-8";
                httpWebRequest.Method = "GET";
                httpWebRequest.Timeout = 10 * 1000;

                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                streamReader = new StreamReader(httpWebResponse.GetResponseStream());
                responseContent = streamReader.ReadToEnd();
                return responseContent;
            }
            finally
            {
                if (httpWebRequest != null) httpWebRequest.Abort();
                if (httpWebResponse != null) httpWebResponse.Close();
                if (streamReader != null) streamReader.Close();
            }
        }

        /// <summary>
        /// HttpWebRequest模拟POST请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="body"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static string PostHttp(string url, string body, string contentType = "application/json")
        {
            byte[] btBodys = Encoding.UTF8.GetBytes(body);

            HttpWebRequest httpWebRequest = null;
            HttpWebResponse httpWebResponse = null;
            Stream reqStream = null;
            StreamReader streamReader = null;
            string responseContent = "";

            try
            {
                httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = contentType;
                httpWebRequest.Method = "POST";
                httpWebRequest.Timeout = 30 * 1000;
                //httpWebRequest.ContentLength = btBodys.Length;

                reqStream = httpWebRequest.GetRequestStream();
                reqStream.Write(btBodys, 0, btBodys.Length);

                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                streamReader = new StreamReader(httpWebResponse.GetResponseStream());
                responseContent = streamReader.ReadToEnd();

                return responseContent;
            }
            finally
            {
                if (httpWebRequest != null) httpWebRequest.Abort();
                if (httpWebResponse != null) httpWebResponse.Close();
                if (reqStream != null) reqStream.Close();
                if (streamReader != null) streamReader.Close();
            }
        }


        // <summary>
        /// 解析JSON字符串生成对象实体
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json字符串(eg.{"ID":"112","Name":"石子儿"})</param>
        /// <returns>对象实体</returns>
        public static T DeserializeJsonToObject<T>(string json) where T : class
        {
            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(json);
            object o = serializer.Deserialize(new JsonTextReader(sr), typeof(T));
            T t = o as T;
            return t;
        }

        public class ResObjModel<T>
        {
            public bool state { get; set; }
            public string msg { get; set; }
            public T data { get; set; }
        }

        public class SummaryDataModel
        {
            public string line_cd { get; set; }
            public string assy_cd { get; set; }
            public string start { get; set; }
            public string end { get; set; }
        }


        /// <summary>
        /// 计算对应班次的DT
        /// </summary>
        /// <param name="dtDataList"></param>
        /// <param name="timeSpan"></param>
        private List<double[]> ComputeDT(List<DtData> dtDataList, List<DateTime[]> timeSpan)
        {
            // dtMin 与timeSpan的Index是相对应的
            int countTS = timeSpan.Count;
            List<double> dtMinList = InitDtMinList(countTS);

            // 计算各个班次的时长
            List<double> totalTimeSpan = timeSpan.Select<DateTime[], double>((item) =>
            {
                return (item[1] - item[0]).TotalSeconds;
            }).ToList();

            // 遍历每一个proc_uuid
            foreach (DtData dtData in dtDataList)
            {
                DateTime startDT = dtData.start;
                DateTime endDT = dtData.end;
                int startIndex = -1;
                int endIndex = -1;

                // 遍历每一个班次的时间
                // 1. 找到DT的开始时间在那个最近的班次
                for (int i = 0; i < countTS; i++)
                {
                    DateTime endTS = timeSpan[i][1];
                    if (startDT <= endTS)
                    {
                        startIndex = i;
                        break;
                    }
                }

                // 2. 找到DT的结束时间在那个最近的班次
                //    从当前班次继续往下查找
                if (startIndex > -1)
                {
                    for (int k = startIndex; k < countTS; k++)
                    {
                        DateTime endTS = timeSpan[k][1];
                        if (endDT <= endTS)
                        {
                            endIndex = k;
                            break;
                        }
                    }
                }


                // 3. 计算dt发生时间所对应的班次 
                if (endIndex > -1 && startIndex > -1)
                {
                    // 在同一时间段
                    DateTime[] startTS = timeSpan[startIndex];
                    DateTime[] endTS = timeSpan[endIndex];
                    int interval = endIndex - startIndex;

                    if (interval == 0)
                    {
                        // 只有两种情况需要处理：
                        // 1. DT的开始和结束时间都在查询出来的班次
                        // 2. DT的开始时间不在班次， 但是结束时间在班次
                        double sec = 0;
                        if (startDT >= startTS[0])
                        {
                            sec = (endDT - startDT).TotalSeconds;
                        }
                        else if (endDT > startTS[0])
                        {
                            sec = (endDT - startTS[0]).TotalSeconds;
                        }
                        dtMinList[startIndex] += sec;
                    }
                    // 跨时间段
                    else if (interval > 0)
                    {
                        // DT开始的班次时间计算
                        // 1. DT开始时间所在班次的结束时间 - DT开始时间
                        // 2. 特殊情况： 当前班次不连续的情况，导致DT开始时间小于了班次的开始时间
                        dtMinList[startIndex] += startDT < startTS[0] ? totalTimeSpan[startIndex] : (startTS[1] - startDT).TotalSeconds;

                        // DT结束的班次时间计算
                        // DT结束时间 - DT结束时间所在班次的开始时间
                        dtMinList[endIndex] += (endDT - endTS[0]).TotalSeconds;

                        // 跨班次段的计算, 从DT开始的班次的以下个班次到 结束的上一个班次
                        for (int i = startIndex + 1; i < endIndex; i++)
                        {
                            dtMinList[i] += totalTimeSpan[i];
                        }
                    }
                }




            }

            // 计算百分比
            List<double[]> retDt = new List<double[]>();
            for (int i = 0; i < countTS; i++)
            {
                double min= Math.Round(dtMinList[i] / 60, 2);
                double per = Math.Round(dtMinList[i] / totalTimeSpan[i], 4) * 100;
                retDt.Add(new double[2] { min, per });
            }

            return retDt;
        }

        /// <summary>
        /// 获取说有的DTtime
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private Dictionary<string, List<DtData>> GetDtDataByProc(DataTable dt)
        {
            Dictionary<string, List<DtData>> dictProcDt = new Dictionary<string, List<DtData>>();
            bool isRun = true;

            // 遍历所有数据
            foreach (DataRow row in dt.Rows)
            {
                int rid = Convert.ToInt32(row["rid"]);
                int judge_text = Convert.ToInt32(row["judge_text"]);

                // 根据rid的排序创建不同的proc_uuid的字典
                if (Convert.ToInt32(row["rid"]) == 1)
                {
                    List<DtData> dtDataList = new List<DtData>();
                    dictProcDt.Add(row["proc_uuid"].ToString(), dtDataList);
                    isRun = true;
                }

                // 当发生错误的时候记录
                if (isRun && judge_text == 1)
                {
                    DateTime start = Convert.ToDateTime(row["process_at"].ToString());
                    dictProcDt[row["proc_uuid"].ToString()].Add(new DtData { start = start});
                    isRun = false;
                    continue;
                }

                // 当机器重新运行的时候
                if (!isRun && judge_text == 0)
                {
                    DtData dtData = dictProcDt[row["proc_uuid"].ToString()].Last();
                    dtData.end = Convert.ToDateTime(row["process_at"].ToString());
                    isRun = true;
                }
            }

            return dictProcDt;
        }

        /// <summary>
        /// 处理DT的数据（不同的proc可能在同一个时间段发生DT，所以重复的DT要进行合并）
        /// </summary>
        /// <returns></returns>
        private List<DtData> GetDtDataAll(Dictionary<string, List<DtData>> dictProcDt, List<DateTime[]> timeSpan)
        {
            DateTime endTS = timeSpan.Last()[1];
            List<DtData> dtDataTmp = new List<DtData>();
            List<DtData> dtDataRet = new List<DtData>();

            // 1. 合并所有的proc的DtData
            foreach (KeyValuePair<string, List<DtData>> kv in dictProcDt)
            {
                // 1. 处理每一个proc最后一个DT结束时间可能为NULL， 机器一直没有运行起来
                // 这种情况的end 就是班次的最后时间
                List<DtData> dtDataProc = kv.Value;
                if (dtDataProc.Count > 0)
                {
                    DtData endDt = dtDataProc.Last();
                    if (endDt.end.Year == 1)
                    {
                        endDt.end = endTS;
                    }
                }
                dtDataTmp.AddRange(kv.Value);
            }

            // 2. 处理DtData的时间段重复问题
            //先将所有的dt按照错误时间点进行升序排列
            List<DtData> dtDataAllOrder = dtDataTmp.OrderBy(item => item.start).ToList();

            // 合并所有的重复时间段
            if (dtDataAllOrder.Count > 0)
            {
                DateTime startTmp = dtDataAllOrder[0].start;
                DateTime endTmp = dtDataAllOrder[0].start;

                foreach (DtData item in dtDataAllOrder)
                {
                    // 当下一个DT的开始时间大于上一个的结束时间
                    // 说明 一个DT已经结束了，不存在重复时间段
                    if (item.start > endTmp)
                    {
                        dtDataRet.Add(new DtData { start = startTmp, end = endTmp });
                        startTmp = item.start;
                        endTmp = item.end;
                    }
                    else
                    {
                        if (item.end > endTmp)
                        {
                            endTmp = item.end;
                        }
                    }
                }
                // 最后一组数据需要处理添加
                dtDataRet.Add(new DtData { start = startTmp, end = endTmp });
            }
            return dtDataRet;
        }

        /// <summary>
        /// 转化当前班次成为Datetime
        /// </summary>
        /// <param name="dayOrNight"></param>
        /// <param name="nowShifList"></param>
        /// <returns></returns>
        private List<DateTime[]> ConvertTimeSpan(bool dayOrNight, List<string> nowShifList)
        {
            int length = nowShifList.Count;
            DateTime now = DateTime.Now;
            List<DateTime[]> timeSpan = new List<DateTime[]>();

            if (dayOrNight)
            {
                for (int i = 0; i < length; i++)
                {
                    string[] times = nowShifList[i].Split(',');
                    DateTime start = Convert.ToDateTime(times[0]);
                    DateTime end = Convert.ToDateTime(times[1]);

                    // 班次时间还没有到的时候
                    if (start >= now) break;
                    if (end >= now) end = now;

                    //// 测试用start
                    //start = start.AddYears(-1).AddDays(-9);
                    //end = end.AddYears(-1).AddDays(-9);

                    timeSpan.Add(new DateTime[2] { start, end });
                }
            }
            else
            {
                int yesterdayIndex = -1;
                List<DateTime[]> timeSpanTmp = new List<DateTime[]>();

                //1. 把所有的班次转化成DateTime
                for (int i = 0; i < length; i++)
                {
                    string[] times = nowShifList[i].Split(',');
                    DateTime start = Convert.ToDateTime(times[0]);
                    DateTime end = Convert.ToDateTime(times[1]);
                    timeSpanTmp.Add(new DateTime[2] { start, end });

                    // 和一个班次的开始时间做比较， 如果大于下一个班次的时间就算前一天的
                    // 记录下所在的位置
                    if (yesterdayIndex < 0 && i+1 < length)
                    {
                        string[] timesNext = nowShifList[i + 1].Split(',');
                        DateTime startNext = Convert.ToDateTime(timesNext[0]);
                        if (startNext < start)
                        {
                            yesterdayIndex = i;
                        }
                    }
                }

                // 2. 处理前一天的时间范围 -1
                for (int i = 0; i <= yesterdayIndex; i++)
                {
                    DateTime start = timeSpanTmp[i][0];
                    DateTime end = timeSpanTmp[i][1];
                    timeSpanTmp[i][0] = start.AddDays(-1);

                    // 防止最后一组昨天的班次的结束时间是00:00点之后 如： 【23：00，00：30】
                    if (i != yesterdayIndex || start < end)
                    {
                        timeSpanTmp[i][1] = end.AddDays(-1);
                    }
                }

                //3. 获取班次
                for (int i = 0; i < length; i++)
                {
                    
                    DateTime start = timeSpanTmp[i][0];
                    DateTime end = timeSpanTmp[i][1];

                    // 测试用start======================
                    //now = new DateTime(2018, 06, 30, 23, 50, 50);
                    //start = start.AddYears(-1).AddDays(-8);
                    //end = end.AddYears(-1).AddDays(-8);
                    // ======================================

                    if (start >= now) break;
                    if (end >= now) end = now;

                    timeSpan.Add(new DateTime[2] { start, end });
                }
            }

            return timeSpan;
        }

        /// <summary>
        /// 初始化DtMinList
        /// </summary>
        /// <param name="countTS"></param>
        /// <returns></returns>
        private List<double> InitDtMinList(int countTS)
        {
            List<double> dtMinList = new List<double>();
            for (int i = 0; i < countTS; i++)
            {
                dtMinList.Add(0);
            }
            return dtMinList;
        }

        /// <summary>
        /// 获取班次的开始时间与结束时间
        /// </summary>
        /// <param name="dayOrNight"></param>
        /// <param name="nowShifList"></param>
        /// <returns>【start, end】</returns>
        private List<DateTime> GetRangeDate(bool dayOrNight, List<string> nowShifList)
        {
            List<DateTime> rangeDate = new List<DateTime>();

            string startStr = nowShifList[0].Split(',')[0];
            string endStr = nowShifList[nowShifList.Count - 1].Split(',')[0];

            DateTime start = dayOrNight ? Convert.ToDateTime(startStr) : Convert.ToDateTime(startStr).AddDays(-1);
            DateTime end = Convert.ToDateTime(endStr);

            rangeDate.Add(start);
            rangeDate.Add(end);

            return rangeDate;
        }
    }

    class DtData
    {
        public DateTime start { get; set; }
        public DateTime end { get; set; }
    }
}
