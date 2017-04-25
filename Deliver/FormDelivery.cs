using CrystalDecisions.CrystalReports.Engine;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utilities;

namespace Deliver
{
    public partial class FormDelivery : Form
    {
        string str_api = "http://abc.xxczd.com/index.php/api/api";
        const string token = "chzpdx2014mn1989";
        string str_OrderInfo = "/sendOrderView";
        string str_OrderEdit = "/sendOrderEdit";
        string str_ProductAdd = "/newProductAdd";
        string str_SearchProduct = "/searchProduct";
        OrderInfoResult orderinfo;
        string printType, deliveName, sName, lName, tel, printModel, imgPath;
        ReportClass rpt;

        public FormDelivery(ReportClass rpt, int id, string printType, string deliveName, string sName, string lName, string tel, string printModel, string imgPath)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterParent;
            InitInfo(id);
            this.printType = printType;
            this.sName = sName;
            this.deliveName = deliveName;
            this.lName = lName;
            this.tel = tel;
            this.rpt = rpt;
            this.printModel = printModel;
            this.imgPath = imgPath;
        }

        private void InitInfo(int id)
        {
            orderinfo = OrderInfo(id);
            lblCName.Text = orderinfo.Data.user_name;
            lblTime.Text = orderinfo.Data.time;
            lblTotal.Text = orderinfo.Data.total_price.ToString();
            OrderProductList = orderinfo.Data.product_list;
        }

        private List<OrderProduct> OrderProductList
        {
            get { return orderProductBindingSource.DataSource as List<OrderProduct>; }
            set
            {
                if (value == null)
                {
                    orderProductBindingSource.Clear();
                }
                else
                {
                    orderProductBindingSource.DataSource = value;
                    dgvData.Refresh();

                    foreach (DataGridViewRow row in dgvData.Rows)
                    {
                        if (!string.IsNullOrWhiteSpace(row.Cells[colbalance_color.Name].Value.ToString()))
                        {
                            var val = row.Cells[colbalance_color.Name].Value.ToString();
                            if (val == "1")
                            {
                                row.Cells[colbalance.Name].Style.BackColor = Color.Red;
                            }
                        }
                    }
                }
            }
        }

        private OrderInfoResult OrderInfo(int id)
        {
            var postdata = string.Format("token={0}&sessionId={1}&id={2}", token, User.SessionID, id);
            var htmlstr = Html.Post(str_api + str_OrderInfo, postdata);
            return JsonConvert.DeserializeObject<OrderInfoResult>(htmlstr);
        }

        private void OrderEdit()
        {
            var f = OrderProductList.Where(w => w.isChange);

            foreach (var product in f)
            {
                var postdata = string.Format("token={0}&sessionId={1}&id={2}&val={3}", token, User.SessionID, product.id, product.real_num);
                var htmlstr = Html.Post(str_api + str_OrderEdit, postdata);
                JsonConvert.DeserializeObject<OrderInfo>(htmlstr);
            }
        }

        private void btnAutoPrint_Click(object sender, EventArgs e)
        {
            OrderEdit();
            DataVerifier dv = new DataVerifier();
            dv.Check(string.IsNullOrWhiteSpace(deliveName), "没有送货单名称");
            dv.CheckIfBeforePass(string.IsNullOrWhiteSpace(sName), "没有收款账号");
            dv.CheckIfBeforePass(string.IsNullOrWhiteSpace(lName), "没有联系人");
            dv.CheckIfBeforePass(orderinfo.Data == null, "没有可打印的数据");
            if (dv.Pass)
            {
                using (Printer print = new Printer())
                {
                    PrintData info = new PrintData { FName = deliveName, SName = sName, LName = lName, Tel = tel, Img = imgPath, OrderData = orderinfo.Data };
                    print.Enqueue(info, rpt, printType, printModel);
                    print.OncePrint();
                }
            }
            dv.ShowMsgIfFailed();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataVerifier dv = new DataVerifier();
            dv.Check(string.IsNullOrWhiteSpace(txtID.Text), "请输入商品编码");
            dv.CheckIfBeforePass(string.IsNullOrWhiteSpace(txtNum.Text), "请输入实配数量");
            dv.CheckIfBeforePass(string.IsNullOrWhiteSpace(txtReaNum.Text), "请输入拍单量");
            dv.CheckIfBeforePass(string.IsNullOrWhiteSpace(txtPrice.Text), "请输入单价");
            if(dv.Pass)
            {
                var result = SaveProduct();
                dv.Check(result.Status == "40000", result.Message);
                if (dv.Pass)
                {
                    var total = Convert.ToDecimal(result.Data.price) * Convert.ToDecimal(result.Data.real_num);
                    OrderProductList.Add(new OrderProduct
                    {
                        index = OrderProductList.Max(m => m.index) + 1,
                        id = result.Data.id,
                        name = result.Data.name,
                        num = result.Data.num,
                        real_num = result.Data.real_num,
                        price = result.Data.price,
                        unit = result.Data.unit,
                        total = (float)total,
                        balance = result.Data.balance
                    });
                    lblTotal.Text = (orderinfo.Data.total_price + (float)total).ToString();
                    dgvData.DataSource = null;
                    dgvData.DataSource = orderProductBindingSource;
                }
            }
            dv.ShowMsgIfFailed();
        }

        private SearchResult SearchProduct()
        {
            var postdata = string.Format("token={0}&sessionId={1}&uid={2}&val={3}", token, User.SessionID, orderinfo.Data.uid, txtPName.Text);
            var htmlstr = Html.Post(str_api + str_SearchProduct, postdata);
            return JsonConvert.DeserializeObject<SearchResult>(htmlstr);
        }

        private AddProductResult SaveProduct()
        {
            var postdata = string.Format("token={0}&sessionId={1}&o_id={2}&p_id={3}&num={4}&price={5}&real_num={6}", token, User.SessionID, orderinfo.Data.id, txtID.Text, txtNum.Text, txtPrice.Text, txtReaNum.Text);
            var htmlstr = Html.Post(str_api + str_ProductAdd, postdata);
            return JsonConvert.DeserializeObject<AddProductResult>(htmlstr);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataVerifier dv = new DataVerifier();
            dv.Check(string.IsNullOrWhiteSpace(txtPName.Text), "请输入商品名称");
            if (dv.Pass)
            {
                var p = SearchProduct();
                txtID.Text = p.Data[0].id.ToString();
            }
            dv.ShowMsgIfFailed();
        }
    }

    public class AddProductResult : Result
    {
        public AddProduct Data;
    }

    public class AddProduct
    {
        public string id { get; set; }
        public string name { get; set; }
        public string price { get; set; }
        public string num { get; set; }
        public string real_num { get; set; }
        public string unit { get; set; }
        public float balance { get; set; }
    }

    public class SearchResult : Result
    {
        public List<SearchData> Data; 
    }

    public class SearchData
    {
        public int id { get; set; }
        public string name { get; set; }
        public float rank_price { get; set; }
        public int time { get; set; }
    }
}
