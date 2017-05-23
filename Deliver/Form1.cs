using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Printing;
using Newtonsoft.Json;
using System.Data;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Cache;
using System.ComponentModel;
using System.Drawing;
using System.Configuration;
using CrystalDecisions.CrystalReports.Engine;
using System.Linq;
using Utilities;

namespace Deliver
{
    public partial class Form1 : Form
    {
        string str_api = "http://abc.xxczd.com/index.php/api/api";
        const string token = "chzpdx2014mn1989";
        string str_SendOrderList = "/getSendOrderList";
        string str_OrderInfo = "/sendOrderView";
        ReportClass rpt;
        
        Thread thread;
        Thread threadInitPrint;
        Printer print;
        //ILog logger;
        string pagesize = "";
        string autoprint = "";
        string SendDate = "";
        string Down = "";
        string Top = "";
        List<string> Printed = new List<string>();

        public Form1()
        {
            this.WindowState = FormWindowState.Maximized;
            InitializeComponent();
            InitLog4Net();
            //logger = LogManager.GetLogger(typeof(Form1));
            
            LoadDevice();
            btnAutoPrint.Enabled = true;
            //timer1.Interval = Convert.ToInt32(textBox1.Text) * 1000;
            LoadCount();

            ucPagerEx1.PageChanged += ucPagerEx1_PageChanged;

            print = new Printer();
            print.SendOrderEvent += Print_SendOrderEvent;
            thread = new Thread(print.LoopPrint);
            thread.Start();
        }

        private void Print_SendOrderEvent(string id)
        {
            foreach (DataGridViewRow row in dgvData.Rows)
            {
                if (row.Cells[coliD.Name].Value.ToString() == id)
                {
                    row.Cells[coliD.Name].Style.BackColor = Color.Red;
                    break;
                }
            }
        }

        #region 初始化窗口
        private void LoadDevice()
        {
            #region 打印机
            PrintDocument print = new PrintDocument();
            string sDefault = print.PrinterSettings.PrinterName;//默认打印机名            

            foreach (string sPrint in PrinterSettings.InstalledPrinters)//获取所有打印机名称
            {
                var i = ddlPrinter.Items.Add(sPrint);
                if (sPrint == sDefault)
                    ddlPrinter.SelectedIndex = i;
            }
            #endregion

            #region 串口
            //RegistryKey keyCom = Registry.LocalMachine.OpenSubKey("Hardware\\DeviceMap\\SerialComm");
            //if (keyCom != null)
            //{
            //    string[] sSubKeys = keyCom.GetValueNames();
            //    foreach (string sName in sSubKeys)
            //    {
            //        string sValue = (string)keyCom.GetValue(sName);
            //        ddlProt.Items.Add(sValue);
            //    }
            //    ddlProt.SelectedIndex = 0;
            //}
            #endregion

            Utilities.InitComboBox.InitDorpDownByEnum(ddlPrintType, typeof(PrintModel));
            Utilities.InitComboBox.InitDorpDownByEnum(ddlAutoPrint, typeof(AutoPrint));
            Utilities.InitComboBox.InitDorpDownByEnum(ddlPageSize, typeof(PageSize));
        }

        #endregion        

        private bool InitRpt()
        {
            if (rpt == null)
            {
                if (ddlPageSize.Text == "窄纸")
                {
                    rpt = new Deliver.Report.ReportB();
                }
                else
                {
                    rpt = new Deliver.Report.Report();
                }
                rpt.PrintOptions.PrinterName = ddlPrinter.Text;
                var doc = new System.Drawing.Printing.PrintDocument
                {
                    PrinterSettings =
                    {
                        PrinterName = ddlPrinter.Text
                    }
                };
                bool hassize = false;
                string pageSize = ddlPageSize.Text;
                //if(ddlPageSize.Text == "窄纸")
                //{
                //    pageSize = "";
                //    hassize = true;
                //}
                if (!string.IsNullOrEmpty(pageSize))
                {
                    for (var i = 0; i <= doc.PrinterSettings.PaperSizes.Count - 1; i++)
                    {
                        if (doc.PrinterSettings.PaperSizes[i].PaperName.ToLower() == pageSize)
                        {
                            hassize = true;
                            rpt.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)doc.PrinterSettings.PaperSizes[i].RawKind;
                            break;
                        }
                    }
                }
                if (!hassize)
                {
                    MessageBox.Show("系统内未找到" + pageSize + "的纸张，请先配置纸张");
                    rpt = null;
                    return false;
                }
            }
            return true;
        }

        #region 订单数据
        private List<SendOrder> SendOrderDataList
        {
            get { return sendOrderBindingSource.DataSource as List<SendOrder>; }
            set
            {
                if (value == null)
                {
                    sendOrderBindingSource.Clear();
                }
                else
                {
                    sendOrderBindingSource.DataSource = value;

                    foreach (DataGridViewRow row in dgvData.Rows)
                    {
                        var isprint = row.Cells[colisprinted.Name].Value.ToString();
                        if (!string.IsNullOrWhiteSpace(isprint))
                        {
                            if (isprint == "1")
                            {
                                row.Cells[coliD.Name].Style.BackColor = Color.Red;
                            }
                        }
                        Printed = Read();
                        var id = row.Cells[coliD.Name].Value.ToString();
                        if (Printed.Any(a => a == id))
                        {
                            row.Cells[coliD.Name].Style.BackColor = Color.Red;
                        }

                        var orderinfo = OrderInfo(Convert.ToInt32(id));
                        if(orderinfo.product_list.Any(w => Convert.ToDecimal(w.real_num) <= 0))
                        {
                            row.Cells[colcname.Name].Style.BackColor = Color.Yellow;
                        }
                        else if(orderinfo.product_list.Any(w => w.balance_color == 1))
                        {
                            row.Cells[colcname.Name].Style.BackColor = Color.Red;
                        }
                        else
                        {
                            row.Cells[colcname.Name].Style.BackColor = Color.Green;
                        }
                    }

                    dgvData.Refresh();
                }
            }
        }

        private SendOrder CurrentData
        {
            get { return sendOrderBindingSource.Current as SendOrder; }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            if(chkRemember.Checked)
                SaveCount();
            LoadData(1);
            Cursor = Cursors.Default;
        }

        private void LoadData(int page)
        {
            SendDate = dtpSendDate.Checked ? dtpSendDate.Value.ToString("yyyy-MM-dd") : "";
            Down = txtDown.Text;
            Top = txtTop.Text;
            var orderdata = SearchSendOrderList(page);
            SendOrderDataList = orderdata.Data;
            ucPagerEx1.InitPageInfo(orderdata.Page.Total, orderdata.Page.PageSize);
        }

        private OrderDataResult SearchSendOrderList(int page)
        {
            var postdata = string.Format("token={0}&sessionId={1}&send_date={2}&send_order_down={3}&send_order_top={4}&page={5}", token, User.SessionID, SendDate, Down, Top, page);
            var htmlstr = Html.Post(str_api + str_SendOrderList, postdata);
            return JsonConvert.DeserializeObject<OrderDataResult>(htmlstr);
        }

        private OrderInfo OrderInfo(int id)
        {
            var postdata = string.Format("token={0}&sessionId={1}&id={2}", token, User.SessionID, id);
            var htmlstr = Html.Post(str_api + str_OrderInfo, postdata);
            var result = JsonConvert.DeserializeObject<OrderInfoResult>(htmlstr);
            int i = 1;
            result.Data.product_list.ForEach(f => f.index = i++);
            return result.Data;
        }

        void dgvData_CellContentClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
                return;

            if(e.ColumnIndex == colaction.Index)
            {
                if (InitRpt())
                {
                    FormDelivery form = new FormDelivery(rpt, CurrentData.ID, ddlPrintType.SelectedValue.ToString(), txtSendName.Text, txtAccount.Text, txtLinkMan.Text, txtTel.Text, ddlAutoPrint.SelectedValue.ToString(), pbCode.ImageLocation);
                    form.ShowDialog();
                }
            }
        }

        #endregion
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (rpt != null)
            {
                rpt.Close();
                rpt.Dispose();
            }
            if (thread != null)
                thread.Abort();
        }

        private void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            rpt = null;
        }

        int top = 0;
        int left = 0;
        int height = 0;
        int width1 = 0;

        private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            #region 重绘datagridview表头
            //DataGridView dgv = (DataGridView)(sender);
            //if (e.RowIndex == -1 && (e.ColumnIndex == colaction.Index || e.ColumnIndex == colOwd.Index))
            //{
            //    if (e.ColumnIndex == colaction.Index)
            //    {
            //        top = e.CellBounds.Top;
            //        left = e.CellBounds.Left;
            //        height = e.CellBounds.Height;
            //        width1 = e.CellBounds.Width;
            //    }

            //    int width2 = colOwd.Width;

            //    Rectangle rect = new Rectangle(left, top, width1 + width2, e.CellBounds.Height);
            //    using (Brush backColorBrush = new SolidBrush(e.CellStyle.BackColor))
            //    {
            //        //抹去原来的cell背景
            //        e.Graphics.FillRectangle(backColorBrush, rect);
            //    }
            //    using (Pen pen = new Pen(Color.White))
            //    {
            //        e.Graphics.DrawLine(pen, left + 1, top + 1, left + width1 + width2 - 1, top + 1);
            //    }
            //    using (Pen gridLinePen = new Pen(dgv.GridColor))
            //    {
            //        e.Graphics.DrawLine(gridLinePen, left, top, left + width1 + width2, top);
            //        e.Graphics.DrawLine(gridLinePen, left, top + height - 1, left + width1 + width2, top + height - 1);
            //        e.Graphics.DrawLine(gridLinePen, left, top, left, top + height);
            //        e.Graphics.DrawLine(gridLinePen, left + width1 + width2 - 1, top, left + width1 + width2 - 1, top + height);

            //        //计算绘制字符串的位置
            //        string columnValue = "操作";
            //        SizeF sf = e.Graphics.MeasureString(columnValue, e.CellStyle.Font);
            //        float lstr = (width1 + width2 - sf.Width) / 2;
            //        float rstr = (height / 2 - sf.Height);
            //        //画出文本框
            //        if (columnValue != "")
            //        {
            //            e.Graphics.DrawString(columnValue, e.CellStyle.Font,
            //                                       new SolidBrush(e.CellStyle.ForeColor),
            //                                         left + lstr,
            //                                         top + rstr + 10,
            //                                         StringFormat.GenericDefault);
            //        }
            //    }
            //    e.Handled = true;
            //}
            #endregion
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //timer1.Enabled = false;
            //if(!isPrint)
            //{
                
            //}
            //timer1.Enabled = true;
        }

        private static void InitLog4Net()
        {
            //var logCfg = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config");
            //XmlConfigurator.ConfigureAndWatch(logCfg);
        }

        public void SaveCount()
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings["SendName"].Value = txtSendName.Text;
            configuration.AppSettings.Settings["Account"].Value = txtAccount.Text;
            configuration.AppSettings.Settings["LinkMan"].Value = txtLinkMan.Text;
            configuration.AppSettings.Settings["Tel"].Value = txtTel.Text;
            configuration.AppSettings.Settings["Rembmber"].Value = chkRemember.Checked ? "True" : "False";
            configuration.AppSettings.Settings["ImagePath"].Value = !string.IsNullOrWhiteSpace(pbCode.ImageLocation) ? AppDomain.CurrentDomain.BaseDirectory + "1.bmp":"";
            configuration.Save();
        }

        public void LoadCount()
        {
            if (ConfigurationManager.AppSettings["Rembmber"] == "True")
            {
                txtSendName.Text = ConfigurationManager.AppSettings["SendName"];
                txtAccount.Text = ConfigurationManager.AppSettings["Account"];
                txtLinkMan.Text = ConfigurationManager.AppSettings["LinkMan"];
                txtTel.Text = ConfigurationManager.AppSettings["Tel"];
                pbCode.ImageLocation = ConfigurationManager.AppSettings["ImagePath"];
                chkRemember.Checked = true;                
            }
            txtSendName.Text = ConfigurationManager.AppSettings["SendName"];
        }

        private void btnAutoPrint_Click(object sender, EventArgs e)
        {
            if(btnAutoPrint.Text.Contains("启动"))
            {
                DataVerifier dv = new DataVerifier();
                dv.Check(string.IsNullOrWhiteSpace(txtSendName.Text), "没有送货单名称");
                dv.CheckIfBeforePass(string.IsNullOrWhiteSpace(txtAccount.Text), "没有收款账号");
                dv.CheckIfBeforePass(string.IsNullOrWhiteSpace(txtLinkMan.Text), "没有联系人");

                if (dv.Pass)
                {
                    btnAutoPrint.Text = "暂停自动打印";
                    pagesize = ddlPageSize.SelectedValue.ToString();
                    autoprint = ddlAutoPrint.SelectedValue.ToString();
                    if (InitRpt())
                    {
                        threadInitPrint = new Thread(InitPrintData);
                        threadInitPrint.Start();
                    }
                }
                dv.ShowMsgIfFailed();
            }
            else
            {
                btnAutoPrint.Text = "启动自动打印";
                threadInitPrint.Abort();
            }            
        }

        private void InitPrintData()
        {
            for (int i = 1; i <= ucPagerEx1.PageCount; i++)
            {
                List<SendOrder> orders;
                if (i == 1)
                {
                    orders = SendOrderDataList;
                }
                else
                {
                    var orderdata = SearchSendOrderList(i);
                    orders = orderdata.Data;
                }

                foreach (var order in orders.Where(w => !Printed.Contains(w.ID.ToString())))
                {
                    var printdata = new PrintData { FName = txtSendName.Text, SName = txtAccount.Text, LName = txtLinkMan.Text, Tel = txtTel.Text, Img = pbCode.ImageLocation, OrderData = OrderInfo(order.ID), IsCheck = true };
                    print.Enqueue(printdata, rpt, pagesize, autoprint);
                }
            }

            this.Invoke(new Action(() => { btnAutoPrint.Text = "启动自动打印"; }));
        }

        private void chkRemember_CheckedChanged(object sender, EventArgs e)
        {
            SaveCount();
        }

        private void btnImportCode_Click(object sender, EventArgs e)
        {
            Regex reg = new Regex("(.jpg|.jpeg|.gif|.png|.bmp)$");
            using (OpenFileDialog file = new OpenFileDialog())
            {
                file.Filter = "JPG|*.jpg|JPGE|*.jpeg|GIF|*.gif|PNG|*.png|BMP|*.bmp";
                file.CheckFileExists = true;
                if (file.ShowDialog() == DialogResult.OK)
                {
                    var match = reg.Match(file.SafeFileName);
                    if (!match.Success)
                    {
                        MessageBox.Show("请选择图片文件");
                    }                    
                    pbCode.ImageLocation = file.FileName;
                    Bitmap bmp = new Bitmap(pbCode.ImageLocation);
                    bmp.Save("1.bmp");
                }
            }
        }

        private void ddlPrinter_SelectedIndexChanged(object sender, EventArgs e)
        {
            rpt = null;
        }

        void ucPagerEx1_PageChanged(object sender, EventArgs e)
        {
            LoadData(ucPagerEx1.PageIndex);
        }

        private void ddlPrintType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPrintType.SelectedValue.ToString() == "A")
                btnAutoPrint.Enabled = true;
            else
                btnAutoPrint.Enabled = false;
        }       

        private List<string> Read()
        {
            var ids = new List<string>();
            if(File.Exists("id.txt"))
            {
                var sr = new StreamReader("id.txt", Encoding.Default);
                var line = "";
                while ((line = sr.ReadLine()) != null)
                {
                    ids.Add(line);
                }
                sr.Close();
            }
            return ids;
        }
    }

    public class Printer : IDisposable
    {
        Queue<PrintData> printData = new Queue<PrintData>();
        bool isbusy = false;
        DataSet printdata;
        ReportClass rpt;
        string printType;   //打印纸张
        string printModel;  //打印内容

        public delegate void delsendorder(string id);
        public event delsendorder SendOrderEvent;

        public void SendResult(string id)
        {
            SendOrderEvent?.Invoke(id);
        }

        public void Enqueue(PrintData aj, ReportClass rpt, string printType, string printModel)
        {
            isbusy = true;
            this.rpt = rpt;
            this.printType = printType;
            this.printModel = printModel;
            printData.Enqueue(aj);
            isbusy = false;
        }

        public void LoopPrint()
        {
            while (true)
            {
                if (!isbusy)
                {
                    if (printData.Count > 0)
                    {
                        try
                        {
                            Print(printData.Dequeue());
                        }
                        catch(Exception ex){ }
                    }
                }
            }
        }

        public void OncePrint()
        {
            if (printData.Count > 0)
            {
                try
                {
                    Print(printData.Dequeue());
                }
                catch (Exception ex) { }
            }
        }

        private void Write(string id)
        {
            SendResult(id);
            string path = "id.txt";
            if (!File.Exists(path))
            {
                File.CreateText(path);                
            }
                

            StreamWriter sw = new StreamWriter(path, true);
            sw.WriteLine(id);
            sw.Close();
        }

        #region 宽纸打印
        private bool Print(DataSet ds)
        {
            try
            {
                rpt.SetDataSource(ds.Tables["mainTable"]);
                ReportDocument poRptSub = rpt.Subreports[0];
                DataTable dtSub = ds.Tables["sunTable"];
                poRptSub.SetDataSource(dtSub);
                rpt.PrintToPrinter(1, true, 0, 0);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("打印出错:" + Environment.NewLine + ex.Message + ex.StackTrace);
                return false;
            }
        }

        public bool Print(PrintData info)
        {
            if (printdata == null)
            {
                printdata = new DataSet();
                DataTable mainTable = new DataTable();
                mainTable.TableName = "mainTable";
                mainTable.Columns.AddRange(new[]{
                    new DataColumn("FName", typeof(string)),    //送货单名称
                    new DataColumn("CName", typeof(string)),    //客户名称
                    new DataColumn("DateTime", typeof(string)), //日期
                    new DataColumn("PName", typeof(string)),    //支付宝账号
                    new DataColumn("LName", typeof(string)), //联系人
                    new DataColumn("Img", typeof(string))
                });

                DataTable sunTable = new DataTable();
                sunTable.TableName = "sunTable";
                sunTable.Columns.AddRange(new[]{
                    new DataColumn("ID", typeof(int)),          //序号
                    new DataColumn("PName", typeof(string)),    //商品
                    new DataColumn("Num", typeof(float)),       //拍单数量
                    new DataColumn("Weight", typeof(float)),    //重量
                    new DataColumn("Unit", typeof(string)),     //单位
                    new DataColumn("Price", typeof(decimal)),   //单价
                    new DataColumn("Total", typeof(decimal)),    //金额
                });

                printdata.Tables.Add(mainTable);
                printdata.Tables.Add(sunTable);
            }
            else
            {
                printdata.Tables[0].Rows.Clear();
                printdata.Tables[1].Rows.Clear();
            }

            var rowA = printdata.Tables[0].NewRow();
            rowA["FName"] = info.FName;
            rowA["CName"] = info.OrderData.user_name;
            rowA["DateTime"] = info.OrderData.time;
            rowA["PName"] = info.SName;
            rowA["LName"] = info.LName;
            rowA["Img"] = info.Img;
            printdata.Tables[0].Rows.Add(rowA);
            var Data = new List<OrderProduct>();
            if(info.IsCheck)
            {
                if (!info.OrderData.product_list.Any(w => Convert.ToDecimal(w.real_num) <= 0))
                {
                    if (printModel == "A" && !info.OrderData.product_list.Any(w => w.balance_color == 1))
                        Data = info.OrderData.product_list;
                    else if (printModel == "B" && info.OrderData.product_list.Any(w => w.balance_color == 1))
                        Data = info.OrderData.product_list;
                }
            }
            else
            {
                Data = info.OrderData.product_list;
            }

            if (Data.Count == 0)
                return false;

            foreach(var item in Data)
            {
                var row = printdata.Tables[1].NewRow();
                row["ID"] = item.index;
                row["PName"] = item.name;
                row["Num"] = item.num;
                row["Weight"] = string.IsNullOrWhiteSpace(item.real_num) ? "0": item.real_num;
                row["Unit"] = item.unit;
                row["Price"] = item.price;
                row["Total"] = item.total;
                printdata.Tables[1].Rows.Add(row);
            }

            Write(info.OrderData.id.ToString());

            return Print(printdata);
        }
        #endregion

        #region 窄纸打印
        private StringReader sr;

        public void PrintB(PrintData info)
        {
            StringBuilder sb = new StringBuilder();
            var maxLength = 16;

            var orderinfo = info.OrderData;

            sb.Clear();
            sb.AppendFormat("客户:{0}\n", orderinfo.user_name);
            sb.AppendFormat("日期:{0}\n", orderinfo.time);
            sb.Append("商品         重量         单价         金额\n");
            var mtotal = 0.00;
            var Data = orderinfo.product_list.Where(w => (printModel == "A" && w.balance_color == 0) || (printModel == "B" && w.balance_color == 1));
            foreach (var good in Data)
            {
                if (good.name.Length > maxLength)
                {
                    sb.Append(good.name.Substring(0, maxLength) + "\n");
                    var stra = good.name.Substring(maxLength);
                    if (stra.Length > maxLength)
                    {
                        sb.Append(stra.Substring(0, maxLength) + "\n");
                        stra = stra.Substring(maxLength);
                        sb.Append(stra);
                    }
                    else
                    {
                        sb.Append(stra);
                    }
                }
                else
                {
                    sb.Append(good.name);
                }
                var weight = good.real_num.ToString();
                var price_padLeft = 18;
                var price = good.price.ToString();
                var total_padLeft = 15;
                var total = good.total.ToString();
                mtotal += good.total;
                sb.AppendFormat("            {0}{1}{2}\n", weight, price.PadLeft(price_padLeft - weight.Length, ' '), total.PadLeft(total_padLeft - price.Length, ' '));
            }
            sb.AppendFormat("{0}\n", "合计:" + mtotal.ToString().PadLeft(42, ' '));
            sb.Append("\n");
            sb.AppendFormat("收款账号名称:{0}\n", info.SName);
            sb.AppendFormat("联系人:{0}\n", info.LName);
            sb.AppendFormat("联系电话:{0}\n", info.Tel);
            sb.Append("\n");
            sb.Append("收货人:\n");
            PrintB(sb.ToString());
        }

        private bool PrintB(string str)
        {
            bool result = true;
            try
            {
                sr = new StringReader(str);
                PrintDocument pd = new PrintDocument();
                pd.PrintController = new System.Drawing.Printing.StandardPrintController();

                PaperSize pageSize = new PaperSize("First custom size", getYc(70), 2000);
                pd.DefaultPageSettings.PaperSize = pageSize;
                pd.DefaultPageSettings.Margins.Top = 10;
                pd.DefaultPageSettings.Margins.Left = 0;
                pd.PrinterSettings.PrinterName = rpt.PrintOptions.PrinterName; //pd.DefaultPageSettings.PrinterSettings.PrinterName;//默认打印机
                pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
                pd.Print();
            }
            catch (Exception ex)
            {
                result = false;
            }
            finally
            {
                if (sr != null)
                    sr.Close();
            }
            return result;
        }

        private void pd_PrintPage(object sender, PrintPageEventArgs ev)
        {
            Font titleFont = new Font("Arial", 14, FontStyle.Bold);//打印字体
            Font printFont = new Font("Arial", 8);//打印字体
            float linesPerPage = 0;
            float yPos = 0;
            float count = 0;
            float leftMargin = ev.MarginBounds.Left;
            float topMargin = ev.MarginBounds.Top;
            String line = "";
            linesPerPage = ev.MarginBounds.Height / printFont.GetHeight(ev.Graphics);

            yPos = topMargin + (count * printFont.GetHeight(ev.Graphics));
            ev.Graphics.DrawString("     菜自达送货单\n", titleFont, Brushes.Black,
               leftMargin, yPos, new StringFormat());
            count += 2;

            while (count < linesPerPage && ((line = sr.ReadLine()) != null))
            {
                yPos = topMargin + (count * printFont.GetHeight(ev.Graphics));
                ev.Graphics.DrawString(line, printFont, Brushes.Black,
                   leftMargin, yPos, new StringFormat());
                count += 1;
            }
            if (line != null)
                ev.HasMorePages = true;
            else
                ev.HasMorePages = false;
        }

        private int getYc(double cm)
        {
            return (int)(cm / 25.4) * 100;
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
        #endregion
    }

    #region 实体

    public class Result
    {
        public string Status{ get; set; }
        public string Message{ get; set; }
    }

    public class RegionResult : Result
    {        
        public List<Region> Data{ get; set; }
    }

    public class UserResult : Result
    {
        public Session Data { get; set; }
    }

    public class OrderDataResult : Result
    {
        public List<SendOrder> Data { get; set; }
        public Page Page { get; set; }
    }

    public class Session
    {
        public string SessionID { get; set; }
    }

    public class User
    {
        public static string SessionID;
    }

    public class Region
    {
        public int region_id { get; set; }
        public string region_name { get; set; }
    }

    public class SendOrder
    {
        public int ID { get; set; }
        public string send_order { get; set; }
        public string cname { get; set; }
        public string route_name { get; set; }
        public int is_printed { get; set; }
        public string action { get { return "查看"; } }
    }

    public class OrderInfoResult : Result
    {
        public OrderInfo Data { get; set; }
        public Page Page { get; set; }
    }

    public class PrintData
    {
        public bool IsCheck { get; set; }
        public string FName { get; set; }
        public string SName { get; set; }
        public string LName { get; set; }
        public string Tel { get; set; }
        public string Img { get; set; }

        public OrderInfo OrderData { get; set; }
    }

    public class OrderInfo
    {        
        public int uid { get; set; }
        public string user_name { get; set; }
        public int id { get; set; }
        public string time { get; set; }
        public float total_price { get; set; }
        public List<OrderProduct> product_list { get; set; }
    }

    public class OrderProduct
    {
        private string realnum;
        public bool isChange;
        public bool isInit = true;

        public int index { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string num { get; set; }
        public string real_num {
            get { return realnum; }
            set {
                if(!string.IsNullOrWhiteSpace(realnum) || !isInit)
                {
                    isChange = true;
                }
                realnum = value;
                if (isInit) isInit = !isInit;
            }
        }
        public string unit { get; set; }
        public string price { get; set; }
        public float total { get; set; }
        public float balance { get; set; }
        public int balance_color { get; set; }
    }

    public class Page
    {
        public int Total {get;set;}
        public int PageSize{get;set;}
    }
    #endregion

    public class Html
    {
        public static string Post(string url, string postdata)
        {
            Encoding myEncoding = Encoding.UTF8;
            string sContentType = "application/x-www-form-urlencoded";
            HttpWebRequest req;

            try
            {
                req = WebRequest.Create(url) as HttpWebRequest;
                req.Method = "POST";
                req.Accept = "*/*";
                req.KeepAlive = false;
                req.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);

                byte[] bufPost = myEncoding.GetBytes(postdata);
                req.ContentType = sContentType;
                req.ContentLength = bufPost.Length;
                Stream newStream = req.GetRequestStream();
                newStream.Write(bufPost, 0, bufPost.Length);
                newStream.Close();

                HttpWebResponse res = req.GetResponse() as HttpWebResponse;
                try
                {
                    Encoding encoding = Encoding.UTF8;
                    System.Diagnostics.Debug.WriteLine(encoding);

                    using (Stream resStream = res.GetResponseStream())
                    {
                        using (StreamReader resStreamReader = new StreamReader(resStream, encoding))
                        {
                            return resStreamReader.ReadToEnd();
                        }
                    }
                }
                finally
                {
                    res.Close();
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }

    #region 枚举
    public enum AutoPrint
    {
        [Description("已分无错")]
        A,
        [Description("已分有错")]
        B,
    }

    public enum PrintModel
    {
        [Description("自动打印")]
        A,
        [Description("手动打印")]
        B
    }

    public enum PageSize
    {
        [Description("21x14")]
        A,
        [Description("窄纸")]//7x14
        B
    }
    #endregion
}
