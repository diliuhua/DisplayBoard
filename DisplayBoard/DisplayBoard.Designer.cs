namespace DisplayBoard
{
    partial class DisplayBoard
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DisplayBoard));
            this.dtpShowTime = new System.Windows.Forms.DateTimePicker();
            this.lblLineNoValue = new System.Windows.Forms.Label();
            this.lblTarget = new System.Windows.Forms.Label();
            this.tmrFlash = new System.Windows.Forms.Timer(this.components);
            this.btnSave = new System.Windows.Forms.Button();
            this.lblLineNoStr = new System.Windows.Forms.Label();
            this.dgvTitle = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblTitle = new System.Windows.Forms.Label();
            this.dgvTarget = new System.Windows.Forms.DataGridView();
            this.Items_1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HeadCount_1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DesignUPH_1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CTBUPH_1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UPPH_1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DT_1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RecheckRate_1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tossing_1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OpteraterEfficiency_1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalEfficiency_1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Yield_1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvDisplay = new System.Windows.Forms.DataGridView();
            this.Start_2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.End_2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WorkingTime_2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InputQty_2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HeadCount_2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WorkingTime_ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InputQty_ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OutputQty_2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InputGap_2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InputUPH_2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InputUPPH_2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OutputUPH_2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Efficiency_2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NGQty_2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FirstPassYieldRate_2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DT_2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DTrate_2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RetestRate_2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OperatorLoss_2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cboInline = new System.Windows.Forms.ComboBox();
            this.dtpDisplayTime = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTitle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTarget)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDisplay)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpShowTime
            // 
            this.dtpShowTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpShowTime.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            this.dtpShowTime.Enabled = false;
            this.dtpShowTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpShowTime.Location = new System.Drawing.Point(964, 90);
            this.dtpShowTime.Name = "dtpShowTime";
            this.dtpShowTime.Size = new System.Drawing.Size(156, 21);
            this.dtpShowTime.TabIndex = 0;
            // 
            // lblLineNoValue
            // 
            this.lblLineNoValue.AutoSize = true;
            this.lblLineNoValue.Location = new System.Drawing.Point(100, 140);
            this.lblLineNoValue.Name = "lblLineNoValue";
            this.lblLineNoValue.Size = new System.Drawing.Size(59, 12);
            this.lblLineNoValue.TabIndex = 2;
            this.lblLineNoValue.Text = "(Line No)";
            // 
            // lblTarget
            // 
            this.lblTarget.AutoSize = true;
            this.lblTarget.Location = new System.Drawing.Point(20, 180);
            this.lblTarget.Name = "lblTarget";
            this.lblTarget.Size = new System.Drawing.Size(53, 12);
            this.lblTarget.TabIndex = 2;
            this.lblTarget.Text = "Target：";
            // 
            // tmrFlash
            // 
            this.tmrFlash.Enabled = true;
            this.tmrFlash.Interval = 1000;
            this.tmrFlash.Tick += new System.EventHandler(this.TmrFlash_Tick);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(1013, 114);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "立即刷新";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // lblLineNoStr
            // 
            this.lblLineNoStr.AutoSize = true;
            this.lblLineNoStr.Location = new System.Drawing.Point(20, 145);
            this.lblLineNoStr.Name = "lblLineNoStr";
            this.lblLineNoStr.Size = new System.Drawing.Size(65, 12);
            this.lblLineNoStr.TabIndex = 8;
            this.lblLineNoStr.Text = "Line No.：";
            // 
            // dgvTitle
            // 
            this.dgvTitle.AllowUserToAddRows = false;
            this.dgvTitle.AllowUserToDeleteRows = false;
            this.dgvTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTitle.BackgroundColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTitle.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTitle.ColumnHeadersHeight = 40;
            this.dgvTitle.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.dgvTitle.EnableHeadersVisualStyles = false;
            this.dgvTitle.Location = new System.Drawing.Point(12, 210);
            this.dgvTitle.Name = "dgvTitle";
            this.dgvTitle.ReadOnly = true;
            this.dgvTitle.RowHeadersVisible = false;
            this.dgvTitle.RowTemplate.Height = 23;
            this.dgvTitle.Size = new System.Drawing.Size(1104, 41);
            this.dgvTitle.TabIndex = 11;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Time section";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column1.Width = 120;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Plan";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column2.Width = 130;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Actual ";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column3.Width = 605;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Efficiency Loss";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column4.Width = 397;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(378, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(47, 12);
            this.lblTitle.TabIndex = 16;
            this.lblTitle.Text = "(Title)";
            // 
            // dgvTarget
            // 
            this.dgvTarget.AllowUserToAddRows = false;
            this.dgvTarget.AllowUserToDeleteRows = false;
            this.dgvTarget.BackgroundColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTarget.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvTarget.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTarget.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Items_1,
            this.HeadCount_1,
            this.DesignUPH_1,
            this.CTBUPH_1,
            this.UPPH_1,
            this.DT_1,
            this.RecheckRate_1,
            this.Tossing_1,
            this.OpteraterEfficiency_1,
            this.TotalEfficiency_1,
            this.Yield_1});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTarget.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvTarget.EnableHeadersVisualStyles = false;
            this.dgvTarget.Location = new System.Drawing.Point(12, 140);
            this.dgvTarget.Name = "dgvTarget";
            this.dgvTarget.ReadOnly = true;
            this.dgvTarget.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvTarget.RowHeadersVisible = false;
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            this.dgvTarget.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvTarget.RowTemplate.Height = 23;
            this.dgvTarget.Size = new System.Drawing.Size(1104, 60);
            this.dgvTarget.TabIndex = 17;
            // 
            // Items_1
            // 
            this.Items_1.HeaderText = "Items";
            this.Items_1.Name = "Items_1";
            this.Items_1.ReadOnly = true;
            this.Items_1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // HeadCount_1
            // 
            this.HeadCount_1.HeaderText = "Head Count";
            this.HeadCount_1.Name = "HeadCount_1";
            this.HeadCount_1.ReadOnly = true;
            this.HeadCount_1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // DesignUPH_1
            // 
            this.DesignUPH_1.HeaderText = "Design UPH";
            this.DesignUPH_1.Name = "DesignUPH_1";
            this.DesignUPH_1.ReadOnly = true;
            this.DesignUPH_1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // CTBUPH_1
            // 
            this.CTBUPH_1.HeaderText = "CTB UPH";
            this.CTBUPH_1.Name = "CTBUPH_1";
            this.CTBUPH_1.ReadOnly = true;
            this.CTBUPH_1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // UPPH_1
            // 
            this.UPPH_1.HeaderText = "UPPH";
            this.UPPH_1.Name = "UPPH_1";
            this.UPPH_1.ReadOnly = true;
            this.UPPH_1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // DT_1
            // 
            this.DT_1.HeaderText = "DT(≤)";
            this.DT_1.Name = "DT_1";
            this.DT_1.ReadOnly = true;
            this.DT_1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // RecheckRate_1
            // 
            this.RecheckRate_1.HeaderText = "Recheck Rate(≤)";
            this.RecheckRate_1.Name = "RecheckRate_1";
            this.RecheckRate_1.ReadOnly = true;
            this.RecheckRate_1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Tossing_1
            // 
            this.Tossing_1.HeaderText = "Tossing";
            this.Tossing_1.Name = "Tossing_1";
            this.Tossing_1.ReadOnly = true;
            this.Tossing_1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // OpteraterEfficiency_1
            // 
            this.OpteraterEfficiency_1.HeaderText = "Opteration Efficiency(≥)";
            this.OpteraterEfficiency_1.Name = "OpteraterEfficiency_1";
            this.OpteraterEfficiency_1.ReadOnly = true;
            this.OpteraterEfficiency_1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // TotalEfficiency_1
            // 
            this.TotalEfficiency_1.HeaderText = "Total Efficiency(≥)";
            this.TotalEfficiency_1.Name = "TotalEfficiency_1";
            this.TotalEfficiency_1.ReadOnly = true;
            this.TotalEfficiency_1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Yield_1
            // 
            this.Yield_1.HeaderText = "Yield(≥)";
            this.Yield_1.Name = "Yield_1";
            this.Yield_1.ReadOnly = true;
            this.Yield_1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgvDisplay
            // 
            this.dgvDisplay.AllowUserToAddRows = false;
            this.dgvDisplay.AllowUserToDeleteRows = false;
            this.dgvDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDisplay.BackgroundColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDisplay.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvDisplay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDisplay.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Start_2,
            this.End_2,
            this.WorkingTime_2,
            this.InputQty_2,
            this.HeadCount_2,
            this.WorkingTime_,
            this.InputQty_,
            this.OutputQty_2,
            this.InputGap_2,
            this.InputUPH_2,
            this.InputUPPH_2,
            this.OutputUPH_2,
            this.Efficiency_2,
            this.NGQty_2,
            this.FirstPassYieldRate_2,
            this.DT_2,
            this.DTrate_2,
            this.RetestRate_2,
            this.OperatorLoss_2});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDisplay.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgvDisplay.EnableHeadersVisualStyles = false;
            this.dgvDisplay.Location = new System.Drawing.Point(12, 251);
            this.dgvDisplay.Name = "dgvDisplay";
            this.dgvDisplay.RowHeadersVisible = false;
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.White;
            this.dgvDisplay.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvDisplay.RowTemplate.Height = 23;
            this.dgvDisplay.Size = new System.Drawing.Size(1104, 438);
            this.dgvDisplay.TabIndex = 18;
            // 
            // Start_2
            // 
            this.Start_2.HeaderText = "Start";
            this.Start_2.Name = "Start_2";
            this.Start_2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Start_2.Width = 60;
            // 
            // End_2
            // 
            this.End_2.HeaderText = "End";
            this.End_2.Name = "End_2";
            this.End_2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.End_2.Width = 60;
            // 
            // WorkingTime_2
            // 
            this.WorkingTime_2.HeaderText = "Working Time";
            this.WorkingTime_2.Name = "WorkingTime_2";
            this.WorkingTime_2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.WorkingTime_2.Width = 60;
            // 
            // InputQty_2
            // 
            this.InputQty_2.HeaderText = "Input Qty.";
            this.InputQty_2.Name = "InputQty_2";
            this.InputQty_2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.InputQty_2.Width = 70;
            // 
            // HeadCount_2
            // 
            this.HeadCount_2.HeaderText = "Head Count";
            this.HeadCount_2.Name = "HeadCount_2";
            this.HeadCount_2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.HeadCount_2.Width = 50;
            // 
            // WorkingTime_
            // 
            this.WorkingTime_.HeaderText = "Working Time";
            this.WorkingTime_.Name = "WorkingTime_";
            this.WorkingTime_.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.WorkingTime_.Width = 60;
            // 
            // InputQty_
            // 
            this.InputQty_.HeaderText = "Input Qty.";
            this.InputQty_.Name = "InputQty_";
            this.InputQty_.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.InputQty_.Width = 70;
            // 
            // OutputQty_2
            // 
            this.OutputQty_2.HeaderText = "Output Qty.";
            this.OutputQty_2.Name = "OutputQty_2";
            this.OutputQty_2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.OutputQty_2.Width = 70;
            // 
            // InputGap_2
            // 
            this.InputGap_2.HeaderText = "Input Gap";
            this.InputGap_2.Name = "InputGap_2";
            this.InputGap_2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.InputGap_2.Width = 75;
            // 
            // InputUPH_2
            // 
            this.InputUPH_2.HeaderText = "Input UPH";
            this.InputUPH_2.Name = "InputUPH_2";
            this.InputUPH_2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.InputUPH_2.Width = 70;
            // 
            // InputUPPH_2
            // 
            this.InputUPPH_2.HeaderText = "Input UPPH";
            this.InputUPPH_2.Name = "InputUPPH_2";
            this.InputUPPH_2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.InputUPPH_2.Width = 70;
            // 
            // OutputUPH_2
            // 
            this.OutputUPH_2.HeaderText = "OutputUPH";
            this.OutputUPH_2.Name = "OutputUPH_2";
            this.OutputUPH_2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.OutputUPH_2.Width = 70;
            // 
            // Efficiency_2
            // 
            this.Efficiency_2.HeaderText = "Efficiency";
            this.Efficiency_2.Name = "Efficiency_2";
            this.Efficiency_2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Efficiency_2.Width = 70;
            // 
            // NGQty_2
            // 
            this.NGQty_2.HeaderText = "NG Qty.(by M/C)";
            this.NGQty_2.Name = "NGQty_2";
            this.NGQty_2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.NGQty_2.Width = 70;
            // 
            // FirstPassYieldRate_2
            // 
            this.FirstPassYieldRate_2.HeaderText = "First Pass Yield Rate";
            this.FirstPassYieldRate_2.Name = "FirstPassYieldRate_2";
            this.FirstPassYieldRate_2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.FirstPassYieldRate_2.Width = 70;
            // 
            // DT_2
            // 
            this.DT_2.HeaderText = "DT(Min)";
            this.DT_2.Name = "DT_2";
            this.DT_2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DT_2.Width = 50;
            // 
            // DTrate_2
            // 
            this.DTrate_2.HeaderText = "DT%";
            this.DTrate_2.Name = "DTrate_2";
            this.DTrate_2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DTrate_2.Width = 69;
            // 
            // RetestRate_2
            // 
            this.RetestRate_2.HeaderText = "Retest Rate(1+2nd time)";
            this.RetestRate_2.Name = "RetestRate_2";
            this.RetestRate_2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.RetestRate_2.Width = 69;
            // 
            // OperatorLoss_2
            // 
            this.OperatorLoss_2.HeaderText = "Operation Loss";
            this.OperatorLoss_2.Name = "OperatorLoss_2";
            this.OperatorLoss_2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.OperatorLoss_2.Width = 69;
            // 
            // cboInline
            // 
            this.cboInline.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboInline.FormattingEnabled = true;
            this.cboInline.Location = new System.Drawing.Point(20, 50);
            this.cboInline.Name = "cboInline";
            this.cboInline.Size = new System.Drawing.Size(195, 20);
            this.cboInline.TabIndex = 19;
            this.cboInline.SelectedIndexChanged += new System.EventHandler(this.CboInline_SelectedIndexChanged);
            // 
            // dtpDisplayTime
            // 
            this.dtpDisplayTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpDisplayTime.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            this.dtpDisplayTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDisplayTime.Location = new System.Drawing.Point(777, 90);
            this.dtpDisplayTime.Name = "dtpDisplayTime";
            this.dtpDisplayTime.Size = new System.Drawing.Size(156, 21);
            this.dtpDisplayTime.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(960, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 19);
            this.label1.TabIndex = 22;
            this.label1.Text = "显示时间:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(773, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 19);
            this.label2.TabIndex = 23;
            this.label2.Text = "查询时间:";
            // 
            // DisplayBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1128, 701);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboInline);
            this.Controls.Add(this.dgvDisplay);
            this.Controls.Add(this.dgvTarget);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.dgvTitle);
            this.Controls.Add(this.lblLineNoStr);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblLineNoValue);
            this.Controls.Add(this.lblTarget);
            this.Controls.Add(this.dtpDisplayTime);
            this.Controls.Add(this.dtpShowTime);
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DisplayBoard";
            this.Text = "DisplayBoard";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.DisplayBoard_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTitle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTarget)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDisplay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpShowTime;
        private System.Windows.Forms.Label lblLineNoValue;
        private System.Windows.Forms.Label lblTarget;
        private System.Windows.Forms.Timer tmrFlash;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblLineNoStr;
        private System.Windows.Forms.DataGridView dgvTitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridView dgvTarget;
        private System.Windows.Forms.DataGridView dgvDisplay;
        private System.Windows.Forms.ComboBox cboInline;
        private System.Windows.Forms.DataGridViewTextBoxColumn Items_1;
        private System.Windows.Forms.DataGridViewTextBoxColumn HeadCount_1;
        private System.Windows.Forms.DataGridViewTextBoxColumn DesignUPH_1;
        private System.Windows.Forms.DataGridViewTextBoxColumn CTBUPH_1;
        private System.Windows.Forms.DataGridViewTextBoxColumn UPPH_1;
        private System.Windows.Forms.DataGridViewTextBoxColumn DT_1;
        private System.Windows.Forms.DataGridViewTextBoxColumn RecheckRate_1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tossing_1;
        private System.Windows.Forms.DataGridViewTextBoxColumn OpteraterEfficiency_1;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalEfficiency_1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Yield_1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Start_2;
        private System.Windows.Forms.DataGridViewTextBoxColumn End_2;
        private System.Windows.Forms.DataGridViewTextBoxColumn WorkingTime_2;
        private System.Windows.Forms.DataGridViewTextBoxColumn InputQty_2;
        private System.Windows.Forms.DataGridViewTextBoxColumn HeadCount_2;
        private System.Windows.Forms.DataGridViewTextBoxColumn WorkingTime_;
        private System.Windows.Forms.DataGridViewTextBoxColumn InputQty_;
        private System.Windows.Forms.DataGridViewTextBoxColumn OutputQty_2;
        private System.Windows.Forms.DataGridViewTextBoxColumn InputGap_2;
        private System.Windows.Forms.DataGridViewTextBoxColumn InputUPH_2;
        private System.Windows.Forms.DataGridViewTextBoxColumn InputUPPH_2;
        private System.Windows.Forms.DataGridViewTextBoxColumn OutputUPH_2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Efficiency_2;
        private System.Windows.Forms.DataGridViewTextBoxColumn NGQty_2;
        private System.Windows.Forms.DataGridViewTextBoxColumn FirstPassYieldRate_2;
        private System.Windows.Forms.DataGridViewTextBoxColumn DT_2;
        private System.Windows.Forms.DataGridViewTextBoxColumn DTrate_2;
        private System.Windows.Forms.DataGridViewTextBoxColumn RetestRate_2;
        private System.Windows.Forms.DataGridViewTextBoxColumn OperatorLoss_2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DateTimePicker dtpDisplayTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

