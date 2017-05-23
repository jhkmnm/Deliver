namespace Deliver
{
    partial class Form1
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
            this.ddlPageSize = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.ddlPrinter = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.txtSendName = new System.Windows.Forms.TextBox();
            this.txtAccount = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.txtLinkMan = new System.Windows.Forms.TextBox();
            this.txtTel = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.chkRemember = new System.Windows.Forms.CheckBox();
            this.label20 = new System.Windows.Forms.Label();
            this.ddlPrintType = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.ddlAutoPrint = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.btnAutoPrint = new System.Windows.Forms.Button();
            this.btnImportCode = new System.Windows.Forms.Button();
            this.pbCode = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpSendDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDown = new Utilities.UserControls.TextIntegerOnly();
            this.txtTop = new Utilities.UserControls.TextIntegerOnly();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.ucPagerEx1 = new Utilities.UserControls.UcPagerEx();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.coliD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colroutename = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colsendorder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colcname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colisprinted = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colaction = new System.Windows.Forms.DataGridViewButtonColumn();
            this.sendOrderBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sendOrderBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // ddlPageSize
            // 
            this.ddlPageSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlPageSize.Font = new System.Drawing.Font("宋体", 18F);
            this.ddlPageSize.FormattingEnabled = true;
            this.ddlPageSize.Location = new System.Drawing.Point(451, 93);
            this.ddlPageSize.Name = "ddlPageSize";
            this.ddlPageSize.Size = new System.Drawing.Size(102, 32);
            this.ddlPageSize.TabIndex = 25;
            this.ddlPageSize.SelectedIndexChanged += new System.EventHandler(this.ddlPageSize_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(413, 103);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 12);
            this.label9.TabIndex = 26;
            this.label9.Text = "纸张:";
            // 
            // ddlPrinter
            // 
            this.ddlPrinter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlPrinter.Font = new System.Drawing.Font("宋体", 18F);
            this.ddlPrinter.FormattingEnabled = true;
            this.ddlPrinter.Location = new System.Drawing.Point(87, 93);
            this.ddlPrinter.Name = "ddlPrinter";
            this.ddlPrinter.Size = new System.Drawing.Size(320, 32);
            this.ddlPrinter.TabIndex = 22;
            this.ddlPrinter.SelectedIndexChanged += new System.EventHandler(this.ddlPrinter_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(34, 102);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 12);
            this.label7.TabIndex = 22;
            this.label7.Text = "打印机:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(10, 17);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(71, 12);
            this.label16.TabIndex = 25;
            this.label16.Text = "送货单名称:";
            // 
            // txtSendName
            // 
            this.txtSendName.Font = new System.Drawing.Font("宋体", 18F);
            this.txtSendName.Location = new System.Drawing.Point(87, 5);
            this.txtSendName.Name = "txtSendName";
            this.txtSendName.Size = new System.Drawing.Size(218, 35);
            this.txtSendName.TabIndex = 26;
            // 
            // txtAccount
            // 
            this.txtAccount.Font = new System.Drawing.Font("宋体", 18F);
            this.txtAccount.Location = new System.Drawing.Point(429, 5);
            this.txtAccount.Name = "txtAccount";
            this.txtAccount.Size = new System.Drawing.Size(399, 35);
            this.txtAccount.TabIndex = 28;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(340, 17);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(83, 12);
            this.label17.TabIndex = 27;
            this.label17.Text = "收款账号名称:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(22, 58);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(59, 12);
            this.label18.TabIndex = 29;
            this.label18.Text = "联 系 人:";
            // 
            // txtLinkMan
            // 
            this.txtLinkMan.Font = new System.Drawing.Font("宋体", 18F);
            this.txtLinkMan.Location = new System.Drawing.Point(87, 45);
            this.txtLinkMan.Name = "txtLinkMan";
            this.txtLinkMan.Size = new System.Drawing.Size(218, 35);
            this.txtLinkMan.TabIndex = 30;
            // 
            // txtTel
            // 
            this.txtTel.Font = new System.Drawing.Font("宋体", 18F);
            this.txtTel.Location = new System.Drawing.Point(429, 45);
            this.txtTel.Name = "txtTel";
            this.txtTel.Size = new System.Drawing.Size(194, 35);
            this.txtTel.TabIndex = 32;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(364, 58);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(59, 12);
            this.label19.TabIndex = 31;
            this.label19.Text = "联系电话:";
            // 
            // chkRemember
            // 
            this.chkRemember.AutoSize = true;
            this.chkRemember.Font = new System.Drawing.Font("宋体", 18F);
            this.chkRemember.Location = new System.Drawing.Point(661, 48);
            this.chkRemember.Name = "chkRemember";
            this.chkRemember.Size = new System.Drawing.Size(173, 28);
            this.chkRemember.TabIndex = 33;
            this.chkRemember.Text = "记住以上设置";
            this.chkRemember.UseVisualStyleBackColor = true;
            this.chkRemember.CheckedChanged += new System.EventHandler(this.chkRemember_CheckedChanged);
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.Black;
            this.label20.Location = new System.Drawing.Point(12, 85);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(816, 1);
            this.label20.TabIndex = 34;
            this.label20.Text = "label20";
            // 
            // ddlPrintType
            // 
            this.ddlPrintType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlPrintType.Font = new System.Drawing.Font("宋体", 18F);
            this.ddlPrintType.FormattingEnabled = true;
            this.ddlPrintType.Location = new System.Drawing.Point(87, 131);
            this.ddlPrintType.Name = "ddlPrintType";
            this.ddlPrintType.Size = new System.Drawing.Size(131, 32);
            this.ddlPrintType.TabIndex = 35;
            this.ddlPrintType.SelectedIndexChanged += new System.EventHandler(this.ddlPrintType_SelectedIndexChanged);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(22, 141);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(59, 12);
            this.label21.TabIndex = 36;
            this.label21.Text = "打印模式:";
            // 
            // ddlAutoPrint
            // 
            this.ddlAutoPrint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlAutoPrint.Font = new System.Drawing.Font("宋体", 18F);
            this.ddlAutoPrint.FormattingEnabled = true;
            this.ddlAutoPrint.Location = new System.Drawing.Point(318, 131);
            this.ddlAutoPrint.Name = "ddlAutoPrint";
            this.ddlAutoPrint.Size = new System.Drawing.Size(131, 32);
            this.ddlAutoPrint.TabIndex = 37;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(253, 141);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(59, 12);
            this.label22.TabIndex = 38;
            this.label22.Text = "自动打印:";
            // 
            // btnAutoPrint
            // 
            this.btnAutoPrint.Font = new System.Drawing.Font("宋体", 18F);
            this.btnAutoPrint.Location = new System.Drawing.Point(537, 131);
            this.btnAutoPrint.Name = "btnAutoPrint";
            this.btnAutoPrint.Size = new System.Drawing.Size(164, 34);
            this.btnAutoPrint.TabIndex = 39;
            this.btnAutoPrint.Text = "启动自动打印";
            this.btnAutoPrint.UseVisualStyleBackColor = true;
            this.btnAutoPrint.Click += new System.EventHandler(this.btnAutoPrint_Click);
            // 
            // btnImportCode
            // 
            this.btnImportCode.Font = new System.Drawing.Font("宋体", 18F);
            this.btnImportCode.Location = new System.Drawing.Point(559, 93);
            this.btnImportCode.Name = "btnImportCode";
            this.btnImportCode.Size = new System.Drawing.Size(142, 34);
            this.btnImportCode.TabIndex = 40;
            this.btnImportCode.Text = "上传二维码";
            this.btnImportCode.UseVisualStyleBackColor = true;
            this.btnImportCode.Click += new System.EventHandler(this.btnImportCode_Click);
            // 
            // pbCode
            // 
            this.pbCode.Location = new System.Drawing.Point(713, 91);
            this.pbCode.Name = "pbCode";
            this.pbCode.Size = new System.Drawing.Size(115, 115);
            this.pbCode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbCode.TabIndex = 41;
            this.pbCode.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 186);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 42;
            this.label3.Text = "送货日期:";
            // 
            // dtpSendDate
            // 
            this.dtpSendDate.Checked = false;
            this.dtpSendDate.CustomFormat = "yyyy-MM-dd";
            this.dtpSendDate.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpSendDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpSendDate.Location = new System.Drawing.Point(87, 177);
            this.dtpSendDate.Name = "dtpSendDate";
            this.dtpSendDate.ShowCheckBox = true;
            this.dtpSendDate.Size = new System.Drawing.Size(168, 31);
            this.dtpSendDate.TabIndex = 43;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(12, 170);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(689, 1);
            this.label1.TabIndex = 44;
            this.label1.Text = "label1";
            // 
            // txtDown
            // 
            this.txtDown.Font = new System.Drawing.Font("宋体", 15.75F);
            this.txtDown.IsDecimal = false;
            this.txtDown.IsNegativeNumbers = false;
            this.txtDown.Location = new System.Drawing.Point(377, 177);
            this.txtDown.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.txtDown.Name = "txtDown";
            this.txtDown.Size = new System.Drawing.Size(67, 33);
            this.txtDown.TabIndex = 45;
            // 
            // txtTop
            // 
            this.txtTop.Font = new System.Drawing.Font("宋体", 15.75F);
            this.txtTop.IsDecimal = false;
            this.txtTop.IsNegativeNumbers = false;
            this.txtTop.Location = new System.Drawing.Point(451, 177);
            this.txtTop.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.txtTop.Name = "txtTop";
            this.txtTop.Size = new System.Drawing.Size(69, 33);
            this.txtTop.TabIndex = 46;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(288, 185);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 47;
            this.label2.Text = "配送顺序范围:";
            // 
            // btnSearch
            // 
            this.btnSearch.Font = new System.Drawing.Font("宋体", 18F);
            this.btnSearch.Location = new System.Drawing.Point(607, 176);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(84, 34);
            this.btnSearch.TabIndex = 48;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // ucPagerEx1
            // 
            this.ucPagerEx1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ucPagerEx1.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ucPagerEx1.Location = new System.Drawing.Point(36, 630);
            this.ucPagerEx1.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.ucPagerEx1.Name = "ucPagerEx1";
            this.ucPagerEx1.PageIndex = 1;
            this.ucPagerEx1.PageSize = 15;
            this.ucPagerEx1.PreviousPage = 0;
            this.ucPagerEx1.RecordCount = 0;
            this.ucPagerEx1.Size = new System.Drawing.Size(769, 61);
            this.ucPagerEx1.TabIndex = 49;
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToDeleteRows = false;
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvData.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvData.ColumnHeadersHeight = 41;
            this.dgvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.coliD,
            this.colroutename,
            this.colsendorder,
            this.colcname,
            this.colisprinted,
            this.colaction});
            this.dgvData.DataSource = this.sendOrderBindingSource;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvData.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvData.Location = new System.Drawing.Point(3, 218);
            this.dgvData.Name = "dgvData";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvData.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvData.RowHeadersWidth = 21;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dgvData.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvData.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dgvData.RowTemplate.Height = 55;
            this.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvData.Size = new System.Drawing.Size(831, 419);
            this.dgvData.TabIndex = 7;
            this.dgvData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvData_CellContentClick);
            this.dgvData.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvData_CellPainting);
            // 
            // coliD
            // 
            this.coliD.DataPropertyName = "ID";
            this.coliD.HeaderText = "订单号";
            this.coliD.Name = "coliD";
            this.coliD.Width = 80;
            // 
            // colroutename
            // 
            this.colroutename.DataPropertyName = "route_name";
            this.colroutename.HeaderText = "线路名称";
            this.colroutename.Name = "colroutename";
            this.colroutename.Width = 150;
            // 
            // colsendorder
            // 
            this.colsendorder.DataPropertyName = "send_order";
            this.colsendorder.HeaderText = "配送顺序";
            this.colsendorder.Name = "colsendorder";
            this.colsendorder.Width = 150;
            // 
            // colcname
            // 
            this.colcname.DataPropertyName = "cname";
            this.colcname.HeaderText = "客户名称";
            this.colcname.Name = "colcname";
            this.colcname.Width = 250;
            // 
            // colisprinted
            // 
            this.colisprinted.DataPropertyName = "is_printed";
            this.colisprinted.HeaderText = "是否打印";
            this.colisprinted.Name = "colisprinted";
            this.colisprinted.Visible = false;
            // 
            // colaction
            // 
            this.colaction.DataPropertyName = "action";
            this.colaction.HeaderText = "操作";
            this.colaction.Name = "colaction";
            this.colaction.ReadOnly = true;
            this.colaction.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colaction.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colaction.Width = 80;
            // 
            // sendOrderBindingSource
            // 
            this.sendOrderBindingSource.DataSource = typeof(Deliver.SendOrder);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(493, 141);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 12);
            this.label4.TabIndex = 50;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 688);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtDown);
            this.Controls.Add(this.txtTop);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpSendDate);
            this.Controls.Add(this.pbCode);
            this.Controls.Add(this.btnImportCode);
            this.Controls.Add(this.btnAutoPrint);
            this.Controls.Add(this.ddlAutoPrint);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.ddlPrintType);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.chkRemember);
            this.Controls.Add(this.txtTel);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.txtLinkMan);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.ddlPageSize);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtAccount);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.txtSendName);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.ddlPrinter);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.ucPagerEx1);
            this.Name = "Form1";
            this.Text = "称重打印";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pbCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sendOrderBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.ComboBox ddlPageSize;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox ddlPrinter;
        private System.Windows.Forms.Label label7;        
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtSendName;
        private System.Windows.Forms.TextBox txtAccount;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtLinkMan;
        private System.Windows.Forms.TextBox txtTel;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.CheckBox chkRemember;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ComboBox ddlPrintType;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ComboBox ddlAutoPrint;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Button btnAutoPrint;
        private System.Windows.Forms.Button btnImportCode;
        private System.Windows.Forms.PictureBox pbCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpSendDate;
        private System.Windows.Forms.Label label1;
        private Utilities.UserControls.TextIntegerOnly txtDown;
        private Utilities.UserControls.TextIntegerOnly txtTop;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSearch;
        private Utilities.UserControls.UcPagerEx ucPagerEx1;
        private System.Windows.Forms.BindingSource sendOrderBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn coliD;
        private System.Windows.Forms.DataGridViewTextBoxColumn colroutename;
        private System.Windows.Forms.DataGridViewTextBoxColumn colsendorder;
        private System.Windows.Forms.DataGridViewTextBoxColumn colcname;
        private System.Windows.Forms.DataGridViewTextBoxColumn colisprinted;
        private System.Windows.Forms.DataGridViewButtonColumn colaction;
        private System.Windows.Forms.Label label4;
    }
}

