using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using WindowsFormsApplication1.DeliveryPrintService;

namespace WindowsFormsApplication1
{
    public partial class DeliveryPrint : Form
    {
        DeliveryPrintService.DeliveryPrintService MyService = new DeliveryPrintService.DeliveryPrintService();
        //////返回需要过滤的地区编码，和特殊字符
        ////DataTable RemoveInfo = new DataTable();
        ////DataTable DistrictClassQiYu = new DataTable();

        //是否保存
        bool saveOk = true;
        //一次选择条数
        private int OnceNum = 100;//默认100

        //快递类型
        private string kdlx = "";
        //活动名称
        private string hdname = "";
        private DataTable ll ;
        //打印页数
        private int printPiece = 100;
        /// <summary>
        /// 总权限
        /// </summary>
        public string Seller_ID {get;set;}
        /// <summary>
        /// 分账权限卖家自己建立的打印权限
        /// </summary>
        public string Seller_Iid { get; set; }
        //防止手工设为否后释放锁定

        private bool fzrlock = false;
        private List<string> kk;
        //记录最后打印gridview行序号
        private int rowin = 0;

        #region 窗口事件

        public DeliveryPrint()
        {
            InitializeComponent();

            //控件自适应大小
            int count = Controls.Count * 2 + 2;
            var factor = new float[count];
            int i = 0;
            factor[i++] = Size.Width;
            factor[i++] = Size.Height;
            foreach (Control ctrl in Controls)
            {
                factor[i++] = ctrl.Location.X / (float)Size.Width;
                factor[i++] = ctrl.Location.Y / (float)Size.Height;
                ctrl.Tag = ctrl.Size;
            }
            Tag = factor;

        }


        private void Form1_Load(object sender, EventArgs e)
        {
            //填充活动数据
            gethdname();

            //
            label_printuser.Text = this.Account;

            //
            this.cmbSelect.SelectedItem = this.cmbSelect.Items[0];
            this.cmbShopMethod.SelectedItem = this.cmbShopMethod.Items[0];

            //
            GetPrintNum();

            //
            toolStripStatusLabel1.Text = "您正在使用柠檬绿茶团购打印系统";


            //添加soapheader
            
            DeliveryPrintService.myheader myheader = new myheader();
            myheader.username = "nmlch-2012-byken";

            MyService.myheaderValue = myheader;

            if(userrole.Equals("查询"))
            {
                this.button_download.Enabled = false;
                this.button_jiechu.Enabled = false;
                this.btnPrint.Enabled = false;
                this.btnSave.Enabled = false;
                this.btnSelect.Enabled = false;
            }
        }

        /// <summary>
        /// 填充活动数据
        /// </summary>
        private void gethdname()
        {
            timer1.Enabled = true;
            toolStripStatusLabel1.ForeColor = Color.FromArgb(80, Color.DarkRed);
            toolStripStatusLabel1.Text = "您正在获取活动名称.";
            //填充活动数据
            try
            {

                MyService.Credentials = System.Net.CredentialCache.DefaultCredentials;
                string address = m_Address;
                MyService.Url = "http://" + address + "/DeliveryPrintService.asmx";
                DeliveryPrintService.myheader myheader = new myheader();
                myheader.username = "nmlch-2012-byken";

                MyService.myheaderValue = myheader;
                ll = MyService.GetHdNameAll(Seller_ID);
                kk = new List<string>();

                for (int k = 0;k< ll.Rows.Count;k++ )
                {
                    string x = ll.Rows[k]["hdname"] + "|" + ll.Rows[k]["seller"];
                    kk.Add(x);
                }

                if (ll != null )
                {
                    comboBox_hdname.DataSource = kk;
                }
                else
                {
                    MessageBox.Show("没有取得活动信息，请连接网络并点击“获取活动名称”按钮");
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
                //throw;
            }
            timer1.Enabled = false;
            toolStripStatusLabel1.ForeColor = Color.FromArgb(80, Color.DarkGreen);
            toolStripStatusLabel1.Text = "获取活动名称动作完毕  [时间：" + System.DateTime.Now.ToString("yyyy年MM月dd HH时mm分ss秒") + "]";
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {



            bool ts = true;

            if (dataGridView1.RowCount > 0)
            {

                if (!saveOk)
                {
                    MessageBox.Show("您还有保存当前工作，请保存再关闭。");
                    e.Cancel = true;
                    return;
                }


                ts = CheckDeliveryNoAndOK();
                if (ts == true)
                    ts = CheckPrintOk();

            }



            if (ts == true && MessageBox.Show("是否要关掉当前窗体？", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                this.jiesuo();
                Process.GetCurrentProcess().Kill();

            }
            else
            {
                //MessageBox.Show("您还有未完成的面单打印或输入快递单号工作，请检查并完成后关闭。");
                e.Cancel = true;
            }
        }
        #endregion 窗口事件



        #region 属性

        private string userrole;

        public string Userrole
        {
            get { return userrole; }
            set { userrole = value; }
        }


        private string m_Account;

        public string Account
        {
            get { return m_Account; }
            set { m_Account = value; }
        }


        private string m_cPersonCode;

        //操作员代码
        public string CPersonCode
        {
            get { return m_cPersonCode; }
            set { m_cPersonCode = value; }
        }

        private string m_cDepCode;

        public string CDepCode
        {
            get { return m_cDepCode; }
            set { m_cDepCode = value; }
        }

        private string m_Address;
        /// <summary>
        /// xml文件的路径
        /// </summary>
        public string Address
        {
            get { return m_Address; }
            set { m_Address = value; }
        }
        private string m_ServiceName;
        /// <summary>
        /// 面单打印机名字
        /// </summary>

        public string ServiceName
        {
            get { return m_ServiceName; }
            set { m_ServiceName = value; }
        }
        private string m_Format;
        /// <summary>
        /// 打印格式
        /// </summary>

        public string Format
        {
            get { return m_Format; }
            set { m_Format = value; }
        }

        private string m_ShipSource;
        /// <summary>
        /// 货的来源
        /// </summary>
        public string ShipSource
        {
            get { return m_ShipSource; }
            set { m_ShipSource = value; }
        }

        private string m_PrintName;
        /// <summary>
        /// 清单打印机名字
        /// </summary>

        public string PrintName
        {
            get { return m_PrintName; }
            set { m_PrintName = value; }
        }

       
        #endregion 属性


        #region  获得打印数目信息

        private void GetPrintNum()
        {
            timer1.Enabled = true;
            toolStripStatusLabel1.ForeColor = Color.FromArgb(80, Color.DarkRed);
            toolStripStatusLabel1.Text = "您正在获得打印数目信息.";
            try
            {
                //填充活动数据
                MyService.Credentials = System.Net.CredentialCache.DefaultCredentials;
                string address = m_Address;
                MyService.Url = "http://" + address + "/DeliveryPrintService.asmx";
                int[] ll = MyService.GetPrintNum(m_Account, hdname);
                if (ll != null && ll.Length >= 3)
                {
                    label_allnum.Text = ll[0].ToString();
                    label_oknum.Text = ll[1].ToString();
                    label_mynum.Text = ll[2].ToString();

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
                //throw;
            }
            timer1.Enabled = false;
            toolStripStatusLabel1.ForeColor = Color.FromArgb(80, Color.DarkGreen);
            toolStripStatusLabel1.Text = "获得打印数目信息完毕  [时间：" + System.DateTime.Now.ToString("yyyy年MM月dd HH时mm分ss秒") + "]";
        }

        #endregion


        #region 绑定数据
        public void DataBind(string comboxSelectedValue)
        {
            timer1.Enabled = true;
            toolStripStatusLabel1.ForeColor = Color.FromArgb(80, Color.DarkRed);
            toolStripStatusLabel1.Text = "您正在获得需要打印的订单.";
            try
            {

                //绑定DataGridView数据
                ////string strID = StringTools.EncodingForString(cmbShopMethod.SelectedValue.ToString());
                string orderID = txtOrderNo.Text.Trim();

                DataTable DeliveryGrid = new DataTable();

                MyService.Timeout = 600000;
                Stopwatch aa = new Stopwatch();
                aa.Start();


                //MyService.myheaderValue=
                

                DeliveryGrid = MyService.GetOrderDeliveryPrintStateNew(m_Account, kdlx, orderID, m_ShipSource, "T", OnceNum,
                                                                       "", hdname);


                aa.Stop();
                string time = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff");//HH24小时制 hh12小时

                List<string> sb = new List<string>();
                dataGridView1.Rows.Clear();
                //dataGridView1.DataSource = null;
                //DataGridView drv=new DataGridView();
                //重置当前操作行号
                rowin = 0;

                if (DeliveryGrid != null && DeliveryGrid.Rows.Count > 0)
                {
                    dataGridView1.Enabled = true;//不设置，将导致滚动条错误

                    saveOk = false;//设定没有保存

                    for (int i = 0; i < DeliveryGrid.Rows.Count; i++)
                    {

                        //drv.Rows.Add(i + 1, DeliveryGrid.Rows[i]["OrderNo"], DeliveryGrid.Rows[i]["DeliveryNo"], "否", DeliveryGrid.Rows[i]["Buyer_nick"], DeliveryGrid.Rows[i]["Pcode"], DeliveryGrid.Rows[i]["Num"], DeliveryGrid.Rows[i]["Created"]);
                        dataGridView1.Rows.Add(i + 1, DeliveryGrid.Rows[i]["OrderNo"], DeliveryGrid.Rows[i]["DeliveryNo"], "否", DeliveryGrid.Rows[i]["Buyer_nick"], DeliveryGrid.Rows[i]["Pcode"], DeliveryGrid.Rows[i]["Num"], DeliveryGrid.Rows[i]["Created"]);
                        sb.Add(DeliveryGrid.Rows[i]["OrderNo"].ToString());


                    }
                    //dataGridView1.Refresh();
                    //dataGridView1.DataSource = drv;

                }
                else
                {
                    MessageBox.Show("没有检索到数据。", "提示：");
                    return;

                }

                ////释放资源

                DeliveryGrid.Dispose();

                try
                {

                    //保存下载订单
                    FileInfo IsFile = new FileInfo("dd-" + time + ".txt");
                    if (!IsFile.Exists)
                    {
                        StreamWriter ss = File.CreateText("dd-" + time + ".txt");
                        ss.Close();
                    }
                    var fs = new FileStream("dd-" + time + ".txt", FileMode.Append, FileAccess.Write);
                    var sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
                    foreach (var ss in sb)
                    {
                        sw.WriteLine(ss);
                    }

                    sw.Close();
                    fs.Close();
                }
                catch (Exception)
                {
                    
                    //throw;
                    
                }


                //获取打印信息
                GetPrintNum();

            }
            catch (Exception ee)
            {

                MessageBox.Show(ee.Message);
            }

            timer1.Enabled = false;
            toolStripStatusLabel1.ForeColor = Color.FromArgb(80, Color.DarkGreen);
            toolStripStatusLabel1.Text = "获得需要打印的订单完毕  [时间：" + System.DateTime.Now.ToString("yyyy年MM月dd HH时mm分ss秒") + "]";

        }



        //if ((i + 1) % 5 == 0)
        //{
        //    j++;
        //}




        #endregion 绑定数据



        #region  面单打印按钮

        /// <summary>
        /// 面单打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {


                setalldisable();

                //清空快递单号输入框
                textBox1.Text = "";

                int noprint = 0;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells["Column3"].Value.ToString() == "否")
                        noprint++;
                }

                if (noprint == 0)
                {
                    MessageBox.Show("你已经打印完面单，如有需要重新打印的订单，请将单条或多条设为否。");
                    return;
                }

                //打印页数
                int printPiece = txtPrintPiece.Text.Trim().Equals("") ? noprint : int.Parse(txtPrintPiece.Text.Trim());

                //if (!string.IsNullOrEmpty(txtPrintPiece.Text))
                //{
                //    printPiece = int.Parse(txtPrintPiece.Text);
                //}
                //else
                //{
                //    printPiece = dataGridView1.Rows.Count;
                //}

                //打印
                //if (cmbShopMethod.SelectedIndex == 0 | cmbShopMethod.SelectedIndex == 1)
                //{
                //    ShenTong(printPiece);
                //}
                if (cmbShopMethod.SelectedIndex == 0)
                { ZhongTong(printPiece); }
                else if (cmbShopMethod.SelectedIndex == 2)
                { POST(printPiece); }
                else if (cmbShopMethod.SelectedIndex == 1)
                {
                    EMS(printPiece);
                }
                //else if (cmbShopMethod.SelectedIndex == 3)
                //{
                //    ZhongTong(printPiece);
                //}
                setallenable();
            }
        }
        #endregion 面单打印按钮


        /// <summary>
        /// 解锁用户
        /// </summary>
        private void jiesuo()
        {
            string strUserName = StringTools.EncodingForString(m_cPersonCode);
            MyService.Credentials = System.Net.CredentialCache.DefaultCredentials;
            string address = m_Address;
            MyService.Url = "http://" + address + "/DeliveryPrintService.asmx";
            MyService.RealseLockuser(strUserName);
        }



        #region ShenTong 面单打印
        /// <summary>
        /// ShenTong 面单打印
        /// </summary>
        /// <param name="printPiece"></param>
        public void ShenTong(int printPiece)
        {
            timer1.Enabled = true;
            toolStripStatusLabel1.ForeColor = Color.FromArgb(80, Color.DarkRed);
            toolStripStatusLabel1.Text = "您正在打印申通面单.";
            try
            {
                MyService.Credentials = System.Net.CredentialCache.DefaultCredentials;
                string address = m_Address;
                MyService.Url = "http://" + address + "/DeliveryPrintService.asmx";
                List<ShenTongInfo> listCollection = new List<ShenTongInfo>();
                listCollection.Clear();
                int j = 0;

                //发运方式
                //var shippingMethods = "申通";
                //StringBuilder sbb=new StringBuilder();


                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells["Column3"].Value.ToString() == "否")
                    {
                        j++;
                        if (j <= printPiece)
                        {
                            DataTable info = MyService.GetOrderInfoPart(dataGridView1.Rows[i].Cells["Column1"].Value.ToString()).Tables[0];




                            if (info != null)
                            {
                                int rw = info.Rows.Count;

                                if (rw > 0)
                                {


                                    ShenTongInfo shenTong = new ShenTongInfo();
                                    //shenTong.salseType = "T";//销售类型// info.Rows[0]["SalesType"].ToString();
                                    shenTong.shopName = "柠檬绿茶";
                                    //shenTong.MergerOrderID = Int32.Parse(info.Rows[0]["Tid"].ToString());//Int32.Parse(info.Rows[0]["mergerOrderId"].ToString());
                                    shenTong.Consignee = info.Rows[0]["Consignee"].ToString();//收货人

                                    shenTong.Freight = info.Rows[0]["freight"].ToString();


                                    //string clientUserName = info.Rows[0]["GuestName"].ToString();



                                    shenTong.cDCName = info.Rows[0]["Provinces"].ToString();//地区名称 


                                    //返回面单显示的信息
                                    shenTong.ExpressMessage = GetExpressMessage(shenTong.salseType, shenTong.shopName);



                                    shenTong.GetAddress = info.Rows[0]["Provinces"] + " " + info.Rows[0]["City"] + " " + info.Rows[0]["District"] + " " + info.Rows[0]["Address"];


                                    //大字
                                    shenTong.DaZi = dazi(info.Rows[0]["Provinces"].ToString(), info.Rows[0]["City"].ToString(),
                                                         info.Rows[0]["District"].ToString());

                                    //shenTong.OrderNum = dataGridView1.Rows[i].Cells["Column1"].Value.ToString() + "    " + DateTime.Now.ToString("yyyy-MM-dd");
                                    shenTong.RiQi = DateTime.Now.ToString("yyyy-MM-dd");

                                    shenTong.OrderNo = dataGridView1.Rows[i].Cells["Column1"].Value.ToString();
                                    shenTong.Phone = info.Rows[0]["Tel"] + " " + "  " + " " + info.Rows[0]["Phone"];


                                    shenTong.shopName = string.Empty;
                                    shenTong.StockName = string.Empty;
                                    //shenTong.StockCode = info.Rows[0]["Pcode"].ToString();//商品代码

                                    //shenTong.YanSe = info.Rows[0]["Color"].ToString();//颜色
                                    //shenTong.ChiMa = info.Rows[0]["Size"].ToString();//尺码


                                    //shenTong.TotalCount = info.Rows[0]["Num"].ToString();//数量 //MyService.GetTotalCount(dataGridView1.Rows[i].Cells["Column1"].Value.ToString());
                                    listCollection.Add(shenTong);
                                }
                            }
                        }

                    }

                }
                if (listCollection.Count > 0)
                {
                    //MyBound(listCollection, "ShenTong.rpt");
                    //没有保存的数据
                    saveOk = false;
                }
            }
            catch (Exception ee)
            {

                MessageBox.Show(ee.Message);
            }

            timer1.Enabled = false;
            toolStripStatusLabel1.ForeColor = Color.FromArgb(80, Color.DarkGreen);
            toolStripStatusLabel1.Text = "打印申通面单完毕   [时间：" + System.DateTime.Now.ToString("yyyy年MM月dd HH时mm分ss秒") + "]";

        }
        #endregion ShenTong 面单打印



        private string dazi(string provinces, string city, string district)
        {
            string dz = "";
           
            switch (city)
            {
                    //2012.8.15
                case "北京市":
                    dz = provinces + district;
                    break;
                case "上海市":
                    dz = provinces + district;
                   break;
                case "天津市":
                   dz = provinces + district;
                    
                    break;
                case "重庆市":
                    dz = provinces + district;
                   
                    break;
                /////////////////////////////////
                case "南京市":
                    switch (district)
                    {
                        case "江宁区":
                            dz = "江宁";
                            break;
                        case "浦口区":
                            dz = "浦口";
                            break;
                        case "六合区":
                            dz = "六合";
                            break;
                        default:
                            if (district.EndsWith("市") || district.EndsWith("县"))
                            {
                                dz = district;
                            }
                            else
                            {
                                dz = city;
                            }
                            break;
                    }
                    break;
                case "泰州市":
                    if (district.Contains("高港"))
                    {
                        dz = "高港";
                    }
                    else
                    {
                        if (district.EndsWith("市") || district.EndsWith("县"))
                        {
                            dz = district;
                        }
                        else
                        {
                            dz = city;
                        }
                    }
                    break;
                case "常州市":
                    switch (district)
                    {
                        case "戚墅堰区":
                            dz = "戚墅堰";
                            break;
                        case "武进区":
                            dz = "武进";
                            break;
                        default:
                            if (district.EndsWith("市") || district.EndsWith("县"))
                            {
                                dz = district;
                            }
                            else
                            {
                                dz = city;
                            }
                            break;
                    }
                    break;
                case "杭州市":
                    switch (district)
                    {
                        case "萧山区":
                            dz = "萧山";
                            break;
                        case "余杭区":
                            dz = "余杭";
                            break;
                        case "临安区":
                            dz = "临安";
                            break;
                        default:
                            if (district.EndsWith("市") || district.EndsWith("县"))
                            {
                                dz = district;
                            }
                            else
                            {
                                dz = city;
                            }
                            break;
                    }
                    break;
                case "台州市":
                    switch (district)
                    {
                        case "椒江区":
                            dz = "椒江";
                            break;
                        case "黄岩区":
                            dz = "黄岩";
                            break;
                        case "路桥区":
                            dz = "路桥";
                            break;
                        default:
                            if (district.EndsWith("市") || district.EndsWith("县"))
                            {
                                dz = district;
                            }
                            else
                            {
                                dz = city;
                            }
                            break;
                    }
                    break;
                case "宁波市":
                    switch (district)
                    {
                        case "镇海区":
                            dz = "镇海";
                            break;
                        case "北仑区":
                            dz = "北仑";
                            break;
                        default:
                            if (district.EndsWith("市") || district.EndsWith("县"))
                            {
                                dz = district;
                            }
                            else
                            {
                                dz = city;
                            }
                            break;
                    }
                    break;
                case "舟山市":
                    if (district.Contains("定海"))
                    {
                        dz = "定海";
                    }
                    else
                    {
                        if (district.EndsWith("市") || district.EndsWith("县"))
                        {
                            dz = district;
                        }
                        else
                        {
                            dz = city;
                        }
                    }
                    break;
                case "克拉玛依市":
                    if (district.Contains("独山子"))
                    {
                        dz = "新疆独子山";
                    }
                    else
                    {
                        if (district.EndsWith("市") || district.EndsWith("县"))
                        {
                            dz = district;
                        }
                        else
                        {
                            dz = city;
                        }
                    }
                    break;
                case "泰安市":
                    if (district.Contains("宁阳"))
                    {
                        dz = "宁阳";
                    }
                    else
                    {
                        if (district.EndsWith("市") || district.EndsWith("县"))
                        {
                            dz = district;
                        }
                        else
                        {
                            dz = city;
                        }
                    }
                    break;
                case "淮安市":
                    if (district.Contains("楚州"))
                    {
                        dz = "楚州";
                    }
                    else
                    {
                        if (district.EndsWith("市") || district.EndsWith("县"))
                        {
                            dz = district;
                        }
                        else
                        {
                            dz = city;
                        }
                    }
                    break;

                //从此往下逻辑不同，注意:
                case "长沙市":
                    if (district.Contains("宁乡"))
                    {
                        dz = "宁乡";
                    }
                    else
                    {
                        if (district.EndsWith("市"))
                        {
                            dz = district;
                        }
                        else
                        {
                            dz = city;
                        }
                    }
                    break;
                case "常德市":
                    switch (district)
                    {
                        case "石门县":
                            dz = "石门";
                            break;
                        case "澧县":
                            dz = "澧县";
                            break;
                        default:
                            if (district.EndsWith("市"))
                            {
                                dz = district;
                            }
                            else
                            {
                                dz = city;
                            }
                            break;
                    }
                    break;
                case "岳阳市":
                    if (district.Contains("湘阴"))
                    {
                        dz = "湘阴";
                    }
                    else
                    {
                        if (district.EndsWith("市"))
                        {
                            dz = district;
                        }
                        else
                        {
                            dz = city;
                        }
                    }
                    break;
                case "张家界市":
                    if (district.Contains("慈利"))
                    {
                        dz = "慈利";
                    }
                    else
                    {
                        if (district.EndsWith("市"))
                        {
                            dz = district;
                        }
                        else
                        {
                            dz = city;
                        }
                    }
                    break;
                case "汕头市":
                    switch (district)
                    {
                        case "潮南区":
                            dz = "广东潮南";
                            break;
                        case "澄海区":
                            dz = "广东澄海";
                            break;
                        case "潮阳区":
                            dz = "广东潮阳";
                            break;
                        default:
                            if (district.EndsWith("市") || district.EndsWith("县"))
                            {
                                dz = district;
                            }
                            else
                            {
                                dz = city;
                            }
                            break;
                    }
                    break;
                //case "邯郸市":
                //    if (district.Contains("峰峰"))
                //    {
                //        dz = "峰峰";
                //    }

                //    else
                //    {
                //        if (district.EndsWith("市"))
                //        {
                //            dz = district;
                //        }
                //        else
                //        {
                //            dz = city;
                //        }
                //    }
                //    break;
                case "邯郸市":
                    switch (district)
                    {
                        case "峰峰矿区":
                            dz = "北峰峰";
                            break;
                        case "峰峰":
                            dz = "峰峰";
                            break;                      
                        default:
                            if (district.EndsWith("市") || district.EndsWith("县"))
                            {
                                dz = district;
                            }
                            else
                            {
                                dz = city;
                            }
                            break;
                    }
                    break;
                case "南通市":
                    if (district.Contains("海安"))
                    {
                        dz = "海安";
                    }
                    else
                    {
                        if (district.EndsWith("市"))
                        {
                            dz = district;
                        }
                        else
                        {
                            dz = city;
                        }
                    }
                    break;


                case "黄冈市"://反逻辑 减少判断次数 黄梅县、罗田县、团风县、英山县、蕲春县、浠水县（黄冈市下属有7个县，除去红安县，大字都要写到县）
                    switch (district)
                    {
                        case "红安县":
                            dz = "黄冈";
                            break;
                        case "黄州区":
                            dz = "黄冈";
                            break;
                        case "其它区":
                            dz = "黄冈";
                            break;
                        default:
                            if (district.EndsWith("市"))
                            {
                                dz = district;
                            }
                            else
                            {
                                dz = city;
                            }
                            break;
                    }
                    break;
                case "黄石市":
                    if (district.Contains("阳新"))
                    {
                        dz = "阳新";
                    }
                    else
                    {
                        if (district.EndsWith("市"))
                        {
                            dz = district;
                        }
                        else
                        {
                            dz = city;
                        }
                    }
                    break;
                case "荆门市":
                    if (district.Contains("沙洋"))
                    {
                        dz = "沙洋";
                    }
                    else
                    {
                        if (district.EndsWith("市"))
                        {
                            dz = district;
                        }
                        else
                        {
                            dz = city;
                        }
                    }
                    break;
                case "荆州市":
                    switch (district)
                    {
                        case "公安县":
                            dz = "公安";
                            break;
                        case "监利县":
                            dz = "监利";
                            break;
                        default:
                            if (district.EndsWith("市"))
                            {
                                dz = district;
                            }
                            else
                            {
                                dz = city;
                            }
                            break;
                    }
                    break;
                case "咸宁市":
                    switch (district)
                    {
                        case "崇阳县":
                            dz = "崇阳";
                            break;
                        case "通城县":
                            dz = "通城";
                            break;
                        case "通山县":
                            dz = "通山";
                            break;
                        default:
                            if (district.EndsWith("市"))
                            {
                                dz = district;
                            }
                            else
                            {
                                dz = city;
                            }
                            break;
                    }
                    break;
                case "襄樊市":
                    if (district.Contains("谷城"))
                    {
                        dz = "谷城";
                    }
                    else
                    {
                        if (district.EndsWith("市"))
                        {
                            dz = district;
                        }
                        else
                        {
                            dz = "襄阳";
                        }
                    }
                    break;
                case "襄阳市"://2010年11月26日经国务院批复同意，湖北省襄樊市更名为襄阳市
                    if (district.Contains("谷城"))
                    {
                        dz = "谷城";
                    }
                    else
                    {
                        if (district.EndsWith("市"))
                        {
                            dz = district;
                        }
                        else
                        {
                            dz = city;
                        }
                    }
                    break;
                case "孝感市":
                    switch (district)
                    {
                        case "孝昌县":
                            dz = "孝昌";
                            break;
                        case "云梦县":
                            dz = "云梦";
                            break;
                        case "大悟县":
                            dz = "大悟";
                            break;
                        default:
                            if (district.EndsWith("市"))
                            {
                                dz = district;
                            }
                            else
                            {
                                dz = city;
                            }
                            break;
                    }
                    break;

                
                
                //增加深圳
                case "深圳市":
                    switch (district)
                    {
                        case "龙岗区":
                            dz = "广东龙岗";
                            break;
                        case "宝安区":
                            dz = "广东宝安";
                            break;
                        default:

                            dz = city;
                            break;
                    }
                    break;
                default:
                    if (district.EndsWith("市") || district.EndsWith("县"))
                    {
                        dz = district;
                    }
                    else
                    {
                        dz = city;
                    }
                    break;

            }
            //2012.9.14
            switch (provinces)
            {
                case "新疆维吾尔自治区":
                   if (district.EndsWith("市") || district.EndsWith("县"))
                        {
                        dz = "新疆" + district;
                        }
                       else
                        {
                        dz = "新疆" + city;
                        }
                        break;
                case"内蒙古自治区":
                    if (district.EndsWith("市") || district.EndsWith("县"))
                    {
                        dz = "内蒙古" + district;
                    }
                    else
                    {
                        dz = "内蒙古" + city;
                    }
                    break;                   
                case "广西壮族自治区":
                    if (district.EndsWith("市") || district.EndsWith("县"))
                    {
                        dz = "广西" + district;
                    }
                    else
                    {
                        dz = "广西" + city;
                    }
                    break;                   
                case "西藏自治区":
                    if (district.EndsWith("市") || district.EndsWith("县"))
                    {
                        dz = "西藏" + district;
                    }
                    else
                    {
                        dz = "西藏" + city;
                    }
                    break;                   
                case"宁夏回族自治区":
                    if (district.EndsWith("市") || district.EndsWith("县"))
                    {
                        dz = "宁夏" + district;
                    }
                    else
                    {
                        dz = "宁夏" + city;
                    }
                    break;         
            }
            
                if (dz.Length >= 3)
                {  
                    if (dz.EndsWith("市"))
                    {
                        dz = dz.Remove(dz.LastIndexOf("市"));
                    }
                    else if (dz.EndsWith("区"))
                    {
                        dz = dz.Remove(dz.LastIndexOf("区"));
                    }
                    else if (dz.EndsWith("县"))
                    {
                        dz = dz.Remove(dz.LastIndexOf("县"));
                    }
                    else if (dz.EndsWith("自治县"))
                    {
                        dz = dz.Remove(dz.LastIndexOf("自治县"));
                    }
                    else if (dz.EndsWith("自治州"))
                    {
                        dz = dz.Remove(dz.LastIndexOf("自治州"));
                    }              
                }

                if (provinces.StartsWith("北") || provinces.StartsWith("上") || provinces.StartsWith("重") || provinces.StartsWith("天"))
                {
                    
                    dz = provinces  + district;
                } 
                else if (provinces.EndsWith("省"))
                {
                    dz = provinces.Substring(0, provinces.Length - 1) + dz;
                }
                //else if (district.EndsWith("回族自治区") || district.EndsWith("壮族自治区"))
                //{
                //    dz = provinces.Substring(0, provinces.Length - 5) + district;
                //}
                //else if (district.EndsWith("维吾尔自治区"))
                //{
                //    dz = provinces.Substring(0, provinces.Length - 6) + district; 
                //}
                //else if (district.EndsWith("自治区") || district.EndsWith("开发区") || district.EndsWith("其它区"))
                //{
                //    dz = provinces.Substring(0, provinces.Length - 3) + district;
                //}
                //else
                //{
                //    dz = provinces  + dz;
                //}
               
            return dz;

        }

        /// <summary>
        /// 返回申通大字
        /// </summary>
        /// <param name="provinces"></param>
        /// <param name="city"></param>
        /// <param name="district"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        private string dazi2(string provinces, string city, string district)
        {
            string dz = "";
            //string tl = "江宁区,浦口区,高港区,戚墅堰区,武进区,萧山区,椒江区,黄岩区,路桥区,镇海区,北仑区,定海区,锦屏县,宁乡县,石门县,澧县,湘阴县,慈利县,黄梅县,罗田县,团风县,英山县,蕲春县,浠水县,阳新县,沙洋县,公安县,监利县,崇阳县,通城县,通山县,谷城县,孝昌县,云梦县,大悟县";
            //string tl2 = "临澧县";
            switch (city)
            { 
                case"南通市":
                    switch (district)
                    {
                        case "如东县":
                            dz = "如东";
                            break;
                        case"通州区":
                            dz="通州";
                            break;
                        case"如东区":
                            dz="如东";
                            break;
                        case"启东市":
                            dz="启东";
                            break;
                        default:
                            if (city.EndsWith("市") && district.EndsWith("县"))
                            {
                                dz = city + district;
                            }
                            else if (city.EndsWith("市") && !district.EndsWith("县"))
                            {
                                dz = city + district; ;
                            }
                            else if (!city.EndsWith("市") && district.EndsWith("县"))
                            {
                                dz = district;
                            }
                            break;
                    }
                    break;
                case"营口市":
                    switch (district)
                    {
                        case "鲅鱼圈区":
                            dz = "营口鲅鱼圈";
                            break;
                        default:
                            if (city.EndsWith("市") && district.EndsWith("县"))
                            {
                                dz = city + district;
                            }
                            else if (city.EndsWith("市") && !district.EndsWith("县"))
                            {
                                dz = city + district; ;
                            }
                            else if (!city.EndsWith("市") && district.EndsWith("县"))
                            {
                                dz = district;
                            }
                            break;
                    } break;
                case"台州市":
                    switch (district)
                    {
                        case "黄岩区":
                            dz = "黄岩";
                            break;
                        case "椒江区":
                            dz = "椒江";
                            break;
                        case "临海市":
                            dz = "临海";
                            break;
                        case "临海县":
                            dz = "临海";
                            break;
                        case "路桥区":
                            dz = "路桥";
                            break;
                        case "三门县":
                            dz = "三门";
                            break;
                        case "天台县":
                            dz = "天台";
                            break;
                        case "温岭市":
                            dz = "温岭";
                            break;
                        case "仙居县":
                            dz = "仙居";
                            break;
                        case "玉环县":
                            dz = "玉环";
                            break;
                        default:
                            if (city.EndsWith("市") && district.EndsWith("县"))
                            {
                                dz = city + district;
                            }
                            else if (city.EndsWith("市") && !district.EndsWith("县"))
                            {
                                dz = city + district; ;
                            }
                            else if (!city.EndsWith("市") && district.EndsWith("县"))
                            {
                                dz = district;
                            }
                            break;
                    } break;
                case "临海市":
                    switch (district)
                    {
                        case "杜桥镇":
                            dz = "杜桥";
                            break;
                        default:
                            if (city.EndsWith("市") && district.EndsWith("县"))
                            {
                                dz = city + district;
                            }
                            else if (city.EndsWith("市") && !district.EndsWith("县"))
                            {
                                dz = city + district; ;
                            }
                            else if (!city.EndsWith("市") && district.EndsWith("县"))
                            {
                                dz = district;
                            }
                            break;
                    } break;
                case"青岛市":
                    switch (district)
                    {
                        case "黄岛区":
                            dz = "黄岛";
                            break;
                        default:
                            if (city.EndsWith("市") && district.EndsWith("县"))
                            {
                                dz = city + district;
                            }
                            else if (city.EndsWith("市") && !district.EndsWith("县"))
                            {
                                dz = city + district; ;
                            }
                            else if (!city.EndsWith("市") && district.EndsWith("县"))
                            {
                                dz = district;
                            }
                            break;
                    } break;
                case "佳木斯市":
                    switch (district)
                    {
                        case "建三江":
                            dz = "建三江";
                            break;
                        default:
                            if (city.EndsWith("市") && district.EndsWith("县"))
                            {
                                dz = city + district;
                            }
                            else if (city.EndsWith("市") && !district.EndsWith("县"))
                            {
                                dz = city + district; ;
                            }
                            else if (!city.EndsWith("市") && district.EndsWith("县"))
                            {
                                dz = district;
                            }
                            break;
                    } break;
                case "佳木斯":
                    switch (district)
                    {
                        case "建三江":
                            dz = "建三江";
                            break;
                        default:
                            if (city.EndsWith("市") && district.EndsWith("县"))
                            {
                                dz = city + district;
                            }
                            else if (city.EndsWith("市") && !district.EndsWith("县"))
                            {
                                dz = city + district; ;
                            }
                            else if (!city.EndsWith("市") && district.EndsWith("县"))
                            {
                                dz = district;
                            }
                            break;
                    }break;
                case"杭州市":
                    switch (district)
                    {
                        case "萧山区":
                            dz = "萧山";
                            break;
                        default:
                            if (city.EndsWith("市") && district.EndsWith("县"))
                            {
                                dz = city + district;
                            }
                            else if (city.EndsWith("市") && !district.EndsWith("县"))
                            {
                                dz = city + district; ;
                            }
                            else if (!city.EndsWith("市") && district.EndsWith("县"))
                            {
                                dz = district;
                            }
                            break;
                    } break;
                case "绍兴县":
                    switch (district)
                    {
                        case "柯桥镇":
                            dz = "柯桥";
                            break;
                        default:
                            if (city.EndsWith("市") && district.EndsWith("县"))
                            {
                                dz = city + district;
                            }
                            else if (city.EndsWith("市") && !district.EndsWith("县"))
                            {
                                dz = city + district; ;
                            }
                            else if (!city.EndsWith("市") && district.EndsWith("县"))
                            {
                                dz = district;
                            }
                            break;
                    } break;               
            }
            switch (provinces)
            {
                case "广东省":
                    switch (city)
                    {
                        case "中山市":
                            dz = "中山";
                            break;
                        case "珠海市":
                            dz = "珠海";
                            break;
                        default:
                            if (city.EndsWith("市") && district.EndsWith("县"))
                            {
                                dz = city + district;
                            }
                            else if (city.EndsWith("市") && !district.EndsWith("县"))
                            {
                                dz = city + district; ;
                            }
                            else if (!city.EndsWith("市") && district.EndsWith("县"))
                            {
                                dz = district;
                            }
                            break;
                    } break;
                default:
                    if (city.EndsWith("市") && district.EndsWith("县"))
                    {
                        dz = city + district;
                    }
                    else if (city.EndsWith("市") && !district.EndsWith("县"))
                    {
                        dz = city + district; ;
                    }
                    else if (!city.EndsWith("市") && district.EndsWith("县"))
                    {
                        dz = district;
                    }
                    break;
            }






           
                //if (district.EndsWith("市"))
                //{
                //    dz = district.Substring(0, district.LastIndexOf("市"));
                //}
                //else if (district.EndsWith("区") & !district.EndsWith("开发区") & !district.EndsWith("其它区"))
                //{
                //    dz = district.Substring(0, district.LastIndexOf("区"));
                //}
                //else if (district.EndsWith("县") & !district.EndsWith("自治县"))
                //{
                //    dz = district.Substring(0, district.LastIndexOf("县"));
                //}
                //else
                //{
                //    if (city.EndsWith("市"))
                //    {
                //        dz = city.Substring(0, city.LastIndexOf("市"));
                //    }
                //    else if (city.EndsWith("区"))
                //    {
                //        dz = city.Substring(0, city.LastIndexOf("区"));
                //    }
                //    else if (city.EndsWith("州"))
                //    {
                //        dz = city.Substring(0, city.LastIndexOf("州"));
                //    }
                //    else
                //    {
                //        dz = city;

                //    }
                //}
            

            return dz;
        }

        #region ZhongTong 面单打印
        /// <summary>
        /// ShenTong 面单打印
        /// </summary>
        /// <param name="printPiece"></param>
        public void ZhongTong(int printPiece)
        {
            timer1.Enabled = true;
            toolStripStatusLabel1.ForeColor = Color.FromArgb(80, Color.DarkRed);
            toolStripStatusLabel1.Text = "您正在打印申通面单.";
            try
            {
                MyService.Credentials = System.Net.CredentialCache.DefaultCredentials;
                string address = m_Address;
                MyService.Url = "http://" + address + "/DeliveryPrintService.asmx";
                List<ShenTongInfo> listCollection= new List<ShenTongInfo>();
                
                int j = 0;

                //发运方式
                //var shippingMethods = "申通";
                //StringBuilder sbb=new StringBuilder();

                //List<listinfo> infok =  new List<listinfo>();
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                   
                    if (dataGridView1.Rows[i].Cells["Column3"].Value.ToString() == "否")
                    {
                        j++;
                        if (j <= printPiece)
                        {
                            DataTable info = MyService.GetOrderInfoPart(dataGridView1.Rows[i].Cells["Column1"].Value.ToString()).Tables[0];




                            if (info != null)
                            {
                                int rw = info.Rows.Count;

                                if (rw > 0)
                                {


                                    ShenTongInfo ZhongTong = new ShenTongInfo();
                                    //shenTong.salseType = "T";//销售类型// info.Rows[0]["SalesType"].ToString();
                                    ZhongTong.shopName = "柠檬绿茶";
                                    //shenTong.MergerOrderID = Int32.Parse(info.Rows[0]["Tid"].ToString());//Int32.Parse(info.Rows[0]["mergerOrderId"].ToString());
                                    ZhongTong.Consignee = info.Rows[0]["Consignee"].ToString();//收货人

                                    ZhongTong.Freight = info.Rows[0]["freight"].ToString();


                                    //string clientUserName = info.Rows[0]["GuestName"].ToString();



                                    ZhongTong.cDCName = info.Rows[0]["Provinces"].ToString();//地区名称 


                                    //返回面单显示的信息
                                    ZhongTong.ExpressMessage = GetExpressMessage(ZhongTong.salseType, ZhongTong.shopName);



                                    ZhongTong.GetAddress = info.Rows[0]["Provinces"] + " " + info.Rows[0]["City"] + " " + info.Rows[0]["District"] + " " + info.Rows[0]["Address"];


                                    //大字
                                    ZhongTong.DaZi = dazi2(info.Rows[0]["Provinces"].ToString(), info.Rows[0]["City"].ToString(),
                                                         info.Rows[0]["District"].ToString());

                                    //shenTong.OrderNum = dataGridView1.Rows[i].Cells["Column1"].Value.ToString() + "    " + DateTime.Now.ToString("yyyy-MM-dd");
                                    ZhongTong.RiQi = DateTime.Now.ToString("yyyy-MM-dd");

                                    ZhongTong.OrderNo = dataGridView1.Rows[i].Cells["Column1"].Value.ToString();
                                    ZhongTong.Phone = info.Rows[0]["Tel"] + " " + "  " + " " + info.Rows[0]["Phone"];


                                    ZhongTong.shopName = string.Empty;
                                    ZhongTong.StockName = string.Empty;
                                    if (info.Rows.Count>0)
                                    {
                                        ZhongTong.YanSe1 = info.Rows[0]["Color"].ToString();
                                        ZhongTong.ChiMa1 = info.Rows[0]["Size"].ToString();
                                        ZhongTong.Num1 = info.Rows[0]["Num"].ToString();
                                        ZhongTong.StockCode1 = info.Rows[0]["Pcode"].ToString();
                                    }
                                    if (info.Rows.Count>1)
                                    {
                                        ZhongTong.YanSe2 = info.Rows[1]["Color"].ToString();
                                        ZhongTong.ChiMa2 = info.Rows[1]["Size"].ToString();       
                                        ZhongTong.Num2 = info.Rows[1]["Num"].ToString();
                                        ZhongTong.StockCode2 = info.Rows[1]["Pcode"].ToString();
                                    }
                                    if (info.Rows.Count > 2)
                                    {
                                        ZhongTong.YanSe3 = info.Rows[2]["Color"].ToString();
                                        ZhongTong.ChiMa3 = info.Rows[2]["Size"].ToString();
                                        ZhongTong.Num3 = info.Rows[2]["Num"].ToString();
                                        ZhongTong.StockCode3 = info.Rows[2]["Pcode"].ToString();
                                    }
                                    if (info.Rows.Count > 3)
                                    {
                                        ZhongTong.YanSe4 = info.Rows[3]["Color"].ToString();
                                        ZhongTong.ChiMa4 = info.Rows[3]["Size"].ToString();
                                        ZhongTong.Num4 = info.Rows[3]["Num"].ToString();
                                        ZhongTong.StockCode4 = info.Rows[3]["Pcode"].ToString();
                                    } if (info.Rows.Count > 4)
                                    {
                                        ZhongTong.YanSe5 = info.Rows[4]["Color"].ToString();
                                        ZhongTong.ChiMa5 = info.Rows[4]["Size"].ToString();
                                        ZhongTong.Num5 = info.Rows[4]["Num"].ToString();
                                        ZhongTong.StockCode5 = info.Rows[4]["Pcode"].ToString();
                                    } if (info.Rows.Count > 5)
                                    {
                                        ZhongTong.YanSe6 = info.Rows[5]["Color"].ToString();
                                        ZhongTong.ChiMa6 = info.Rows[5]["Size"].ToString();
                                        ZhongTong.Num6 = info.Rows[5]["Num"].ToString();
                                        ZhongTong.StockCode6 = info.Rows[5]["Pcode"].ToString();
                                    }
                                    if (info.Rows.Count > 6)
                                    {
                                        ZhongTong.YanSe7 = info.Rows[6]["Color"].ToString();
                                        ZhongTong.ChiMa7 = info.Rows[6]["Size"].ToString();
                                        ZhongTong.Num7 = info.Rows[6]["Num"].ToString();
                                        ZhongTong.StockCode7 = info.Rows[6]["Pcode"].ToString();
                                    }
                                    if (info.Rows.Count > 7)
                                    {
                                        ZhongTong.YanSe8 = info.Rows[7]["Color"].ToString();
                                        ZhongTong.ChiMa8 = info.Rows[7]["Size"].ToString();
                                        ZhongTong.Num8 = info.Rows[7]["Num"].ToString();
                                        ZhongTong.StockCode8 = info.Rows[7]["Pcode"].ToString();
                                    } if (info.Rows.Count > 8)
                                    {
                                        ZhongTong.YanSe9 = info.Rows[8]["Color"].ToString();
                                        ZhongTong.ChiMa9 = info.Rows[8]["Size"].ToString();
                                        ZhongTong.Num9 = info.Rows[8]["Num"].ToString();
                                        ZhongTong.StockCode9 = info.Rows[8]["Pcode"].ToString();
                                    } if (info.Rows.Count > 9)
                                    {
                                        ZhongTong.YanSe0 = info.Rows[9]["Color"].ToString();
                                        ZhongTong.ChiMa0 = info.Rows[9]["Size"].ToString();
                                        ZhongTong.Num0 = info.Rows[9]["Num"].ToString();
                                        ZhongTong.StockCode0 = info.Rows[9]["Pcode"].ToString();
                                    }

                                    //ZhongTong.StockCode = info.Rows[0]["Pcode"].ToString();//商品代码
                                    //for (int k = 0; k < info.Rows.Count; k++)
                                    //{
                                    //    listinfo al = new listinfo();
                                    //    al.StockCode1 = info.Rows[k]["Pcode"].ToString();//商品代码
                                    //    al.YanSe1 = info.Rows[k]["Color"].ToString();//颜色
                                    //    al.ChiMa1 = info.Rows[k]["Size"].ToString();//尺码 
                                    //    al.num1 = info.Rows[k]["Num"].ToString();// MyService.GetTotalCount(dataGridView1.Rows[i].Cells["Column1"].Value.ToString());】
                                    //    infok.Add(al);
                                    //}
                                    
                                    //ZhongTong.YanSe = info.Rows[0]["Color"].ToString();//颜色
                                    //ZhongTong.ChiMa = info.Rows[0]["Size"].ToString();//尺码
                                    

                                    //ZhongTong.TotalCount = info.Rows[0]["Num"].ToString();//数量 //MyService.GetTotalCount(dataGridView1.Rows[i].Cells["Column1"].Value.ToString());
                                    listCollection.Add(ZhongTong);
                                    //if (listCollection.Count > 0)
                                    //{
                                    //    MyBound(listCollection, infok, "ZhongTong.rpt");

                                    //}
                                }
                            }
                        }

                    }

                }
                if (listCollection.Count > 0)
                {
                    MyBound(listCollection, "ZhongTong.rpt");
                    //没有保存的数据
                    saveOk = false;
                }
            }
            catch (Exception ee)
            {

                MessageBox.Show(ee.Message);
            }

            timer1.Enabled = false;
            toolStripStatusLabel1.ForeColor = Color.FromArgb(80, Color.DarkGreen);
            toolStripStatusLabel1.Text = "打印申通面单完毕   [时间：" + System.DateTime.Now.ToString("yyyy年MM月dd HH时mm分ss秒") + "]";

        }
        #endregion ZhongTong 面单打印

        #region EMS 面单打印

        /// <summary>
        /// EMS 面单打印
        /// </summary>
        /// <param name="printPiece"></param>
        public void EMS(int printPiece)
        {
            timer1.Enabled = true;
            toolStripStatusLabel1.ForeColor = Color.FromArgb(80, Color.DarkRed);

            toolStripStatusLabel1.Text = "您正在打印 EMS 面单.";
            try
            {
                MyService.Credentials = System.Net.CredentialCache.DefaultCredentials;
                string address = m_Address;
                MyService.Url = "http://" + address + "/DeliveryPrintService.asmx";
                List<EmsInfo> listCollection=new List<EmsInfo>();
                int j = 0;
                



                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                   
                    if (dataGridView1.Rows[i].Cells["Column3"].Value.ToString() == "否")
                    {
                        j++;
                        if (j <= printPiece)
                        {
                            DataTable info = MyService.GetOrderInfoPart(dataGridView1.Rows[i].Cells["Column1"].Value.ToString()).Tables[0];

                            if (info != null)
                            {
                                int rw = info.Rows.Count;
                                if (rw > 0)
                                {




                                    EmsInfo emsInfo = new EmsInfo();
                                    emsInfo.salseType = "T";// info.Rows[0]["SalesType"].ToString();
                                    emsInfo.shopName = "柠檬绿茶";
                                    //emsInfo.MergerOrderID = Int32.Parse(info.Rows[0]["Tid"].ToString());//Int32.Parse(info.Rows[0]["mergerOrderId"].ToString());
                                    emsInfo.consignee = info.Rows[0]["Consignee"].ToString();
                                    emsInfo.phone = info.Rows[0]["Tel"] + " " + "   " + " " + info.Rows[0]["Phone"];



                                    //返回面单显示的信息
                                    //emsInfo.ExpressMessage = GetExpressMessage(emsInfo.salseType, emsInfo.shopName);

                                    string shopName = string.Empty;

                                    emsInfo.shopName = shopName;
                                    emsInfo.GetAddress = info.Rows[0]["Provinces"] + " " + info.Rows[0]["City"] + " " + info.Rows[0]["District"] + " " + info.Rows[0]["Address"];
                                    if (info.Rows.Count > 0)
                                    {
                                        emsInfo.YanSe1 = info.Rows[0]["Color"].ToString();
                                        emsInfo.ChiMa1 = info.Rows[0]["Size"].ToString();
                                        emsInfo.Num1 = info.Rows[0]["Num"].ToString();
                                        emsInfo.StockCode1 = info.Rows[0]["Pcode"].ToString();
                                    }
                                    if (info.Rows.Count > 1)
                                    {
                                        emsInfo.YanSe2 = info.Rows[1]["Color"].ToString();
                                        emsInfo.ChiMa2 = info.Rows[1]["Size"].ToString();
                                        emsInfo.Num2 = info.Rows[1]["Num"].ToString();
                                        emsInfo.StockCode2 = info.Rows[1]["Pcode"].ToString();
                                    }
                                    if (info.Rows.Count > 2)
                                    {
                                        emsInfo.YanSe3 = info.Rows[2]["Color"].ToString();
                                        emsInfo.ChiMa3 = info.Rows[2]["Size"].ToString();
                                        emsInfo.Num3 = info.Rows[2]["Num"].ToString();
                                        emsInfo.StockCode3 = info.Rows[2]["Pcode"].ToString();
                                    }
                                    if (info.Rows.Count > 3)
                                    {
                                        emsInfo.YanSe4 = info.Rows[3]["Color"].ToString();
                                        emsInfo.ChiMa4 = info.Rows[3]["Size"].ToString();
                                        emsInfo.Num4 = info.Rows[3]["Num"].ToString();
                                        emsInfo.StockCode4 = info.Rows[3]["Pcode"].ToString();
                                    } if (info.Rows.Count > 4)
                                    {
                                        emsInfo.YanSe5 = info.Rows[4]["Color"].ToString();
                                        emsInfo.ChiMa5 = info.Rows[4]["Size"].ToString();
                                        emsInfo.Num5 = info.Rows[4]["Num"].ToString();
                                        emsInfo.StockCode5 = info.Rows[4]["Pcode"].ToString();
                                    } if (info.Rows.Count > 5)
                                    {
                                        emsInfo.YanSe6 = info.Rows[5]["Color"].ToString();
                                        emsInfo.ChiMa6 = info.Rows[5]["Size"].ToString();
                                        emsInfo.Num6 = info.Rows[5]["Num"].ToString();
                                        emsInfo.StockCode6 = info.Rows[5]["Pcode"].ToString();
                                    }
                                    if (info.Rows.Count > 6)
                                    {
                                        emsInfo.YanSe7 = info.Rows[6]["Color"].ToString();
                                        emsInfo.ChiMa7 = info.Rows[6]["Size"].ToString();
                                        emsInfo.Num7 = info.Rows[6]["Num"].ToString();
                                        emsInfo.StockCode7 = info.Rows[6]["Pcode"].ToString();
                                    }
                                    if (info.Rows.Count > 7)
                                    {
                                        emsInfo.YanSe8 = info.Rows[7]["Color"].ToString();
                                        emsInfo.ChiMa8 = info.Rows[7]["Size"].ToString();
                                        emsInfo.Num8 = info.Rows[7]["Num"].ToString();
                                        emsInfo.StockCode8 = info.Rows[7]["Pcode"].ToString();
                                    } if (info.Rows.Count > 8)
                                    {
                                        emsInfo.YanSe9 = info.Rows[8]["Color"].ToString();
                                        emsInfo.ChiMa9 = info.Rows[8]["Size"].ToString();
                                        emsInfo.Num9 = info.Rows[8]["Num"].ToString();
                                        emsInfo.StockCode9 = info.Rows[8]["Pcode"].ToString();
                                    } if (info.Rows.Count > 9)
                                    {
                                        emsInfo.YanSe0 = info.Rows[9]["Color"].ToString();
                                        emsInfo.ChiMa0 = info.Rows[9]["Size"].ToString();
                                        emsInfo.Num0 = info.Rows[9]["Num"].ToString();
                                        emsInfo.StockCode0 = info.Rows[9]["Pcode"].ToString();
                                    }
                                    emsInfo.OrderNum = dataGridView1.Rows[i].Cells["Column1"].Value.ToString() + "    " + DateTime.Now.ToString("yyyy-MM-dd");
                                    emsInfo.OrderNo = dataGridView1.Rows[i].Cells["Column1"].Value.ToString();
                                    emsInfo.PostCard = info.Rows[0]["PostBack"].ToString();//邮编
                                    //emsInfo.TotalCount = info.Rows[0]["Num"].ToString();
                                    listCollection.Add(emsInfo);
                                
                                }
                            }
                        }
                    }

                }
                if (listCollection.Count > 0)
                    MyBound(listCollection, "EMSB.rpt");
                saveOk = false;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
                //throw;
            }

            timer1.Enabled = false;
            toolStripStatusLabel1.ForeColor = Color.FromArgb(80, Color.DarkGreen);
            toolStripStatusLabel1.Text = "打印 EMSB 面单完毕  [时间：" + System.DateTime.Now.ToString("yyyy年MM月dd HH时mm分ss秒") + "]";


        }
        #endregion


        #region shentong面单打印 MyBound

        /// <summary>
        /// shentong面单打印
        /// </summary>
        /// <param name="list"></param>
        /// <param name="fileName"></param>
        private void MyBound(List<ShenTongInfo> list, string fileName)
        {
            ReportDocument StockObjectsReport = new ReportDocument();
            StockObjectsReport.Load(AppDomain.CurrentDomain.BaseDirectory + fileName);
            StockObjectsReport.SetDataSource(list);
            //ReportDocument subReport = StockObjectsReport.Subreports["listinfodp.rpt"];
            //subReport.SetDataSource(infok);
            System.Drawing.Printing.PrintDocument prtDocument = new System.Drawing.Printing.PrintDocument();

            MessageBox.Show(m_ServiceName);
            if (!string.IsNullOrEmpty(m_ServiceName))
            {
                StockObjectsReport.PrintOptions.PrinterName = m_ServiceName;
                int paperSizeid = PaperSizeGetter.GetPaperSizeId(prtDocument.PrinterSettings.PrinterName, "shentong格式");
                StockObjectsReport.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)(paperSizeid);
                StockObjectsReport.PrintToPrinter(1, false, 0, 0);
                crystalReportViewer1.ReportSource = StockObjectsReport;
                foreach (DataGridViewRow dr in dataGridView1.Rows)
                {
                    foreach (ShenTongInfo al in list)
                    {
                        if (al.OrderNo == dr.Cells["Column1"].Value.ToString())
                        {
                            dr.Cells["Column3"].Value = "是";
                            dr.Cells["Column3"].Style.BackColor = Color.DarkGreen;
                            //生成面单日志

                            //AddLog("23", dr.Cells["Column1"].Value.ToString(), al.MergerOrderID);

                            //记录当前操作行序号
                            rowin = dr.Index;
                        }
                    }
                }
            }

        }

        #endregion shentong面单打印 MyBound



        #region Ems面单打印 MyBound

        /// <summary>
        /// Ems面单打印
        /// </summary>
        /// <param name="list"></param>
        /// <param name="fileName"></param>

        private void MyBound(List<EmsInfo> list, string fileName)
        {
            ReportDocument StockObjectsReport = new ReportDocument();
            StockObjectsReport.Load(AppDomain.CurrentDomain.BaseDirectory + fileName);
            StockObjectsReport.SetDataSource(list);
            System.Drawing.Printing.PrintDocument prtDocument = new System.Drawing.Printing.PrintDocument();
            // StockObjectsReport.PrintOptions.PrinterName = prtDocument.PrinterSettings.PrinterName;
            MessageBox.Show(m_ServiceName);
            if (!string.IsNullOrEmpty(m_ServiceName))
            {
                StockObjectsReport.PrintOptions.PrinterName = m_ServiceName;

                int paperSizeid = PaperSizeGetter.GetPaperSizeId(prtDocument.PrinterSettings.PrinterName, "ems格式");
                StockObjectsReport.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)(paperSizeid);
                StockObjectsReport.PrintToPrinter(1, false, 0, 0);

                foreach (DataGridViewRow dr in dataGridView1.Rows)
                {
                    foreach (EmsInfo al in list)
                    {
                        if (al.OrderNo == dr.Cells["Column1"].Value.ToString())
                        {
                            dr.Cells["Column3"].Value = "是";
                            //生成面单日志
                            //AddLog("23", dr.Cells["Column1"].Value.ToString(), al.MergerOrderID);
                        }
                    }
                }
            }
        }

        #endregion Ems面单打印 MyBound


        #region Ems面单打印2 MyBound

        /// <summary>
        /// Ems2面单打印
        /// </summary>
        /// <param name="list"></param>
        /// <param name="fileName"></param>

        private void MyBound2(List<EmsInfo> list, string fileName)
        {
            ReportDocument StockObjectsReport = new ReportDocument();
            StockObjectsReport.Load(AppDomain.CurrentDomain.BaseDirectory + fileName);
            StockObjectsReport.SetDataSource(list);

            
            System.Drawing.Printing.PrintDocument prtDocument = new System.Drawing.Printing.PrintDocument();
            
            // StockObjectsReport.PrintOptions.PrinterName = prtDocument.PrinterSettings.PrinterName;
            MessageBox.Show(m_ServiceName);
            if (!string.IsNullOrEmpty(m_ServiceName))
            {
                StockObjectsReport.PrintOptions.PrinterName = m_ServiceName;

                int paperSizeid = PaperSizeGetter.GetPaperSizeId(prtDocument.PrinterSettings.PrinterName, "ems格式");
                StockObjectsReport.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)(paperSizeid);
                StockObjectsReport.PrintToPrinter(1, false, 0, 0);

                foreach (DataGridViewRow dr in dataGridView1.Rows)
                {
                    foreach (EmsInfo al in list)
                    {
                        if (al.OrderNo == dr.Cells["Column1"].Value.ToString())
                        {
                            dr.Cells["Column3"].Value = "是";
                            //生成面单日志
                            //AddLog("23", dr.Cells["Column1"].Value.ToString(), al.MergerOrderID);
                        }
                    }
                }
            }
        }

        #endregion Ems面单打印2 MyBound

        #region post 面单打印

        /// <summary>
        /// EMS 面单打印
        /// </summary>
        /// <param name="printPiece"></param>
        public void POST(int printPiece)
        {
            timer1.Enabled = true;
            toolStripStatusLabel1.ForeColor = Color.FromArgb(80, Color.DarkRed);

            toolStripStatusLabel1.Text = "您正在打印 EMS 面单.";
            try
            {
                MyService.Credentials = System.Net.CredentialCache.DefaultCredentials;
                string address = m_Address;
                MyService.Url = "http://" + address + "/DeliveryPrintService.asmx";
                List<EmsInfo> listCollection= new List<EmsInfo>();
                listCollection.Clear();
                int j = 0;
                



                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                   
                   
                    if (dataGridView1.Rows[i].Cells["Column3"].Value.ToString() == "否")
                    {
                        j++;
                        if (j <= printPiece)
                        {
                            DataTable info = MyService.GetOrderInfoPart(dataGridView1.Rows[i].Cells["Column1"].Value.ToString()).Tables[0];

                            if (info != null)
                            {
                                int rw = info.Rows.Count;
                                if (rw > 0)
                                {




                                    EmsInfo emsInfo = new EmsInfo();
                                    emsInfo.salseType = "T";// info.Rows[0]["SalesType"].ToString();
                                    emsInfo.shopName = "柠檬绿茶";
                                    //emsInfo.MergerOrderID = Int32.Parse(info.Rows[0]["Tid"].ToString());//Int32.Parse(info.Rows[0]["mergerOrderId"].ToString());
                                    emsInfo.consignee = info.Rows[0]["Consignee"].ToString();
                                    emsInfo.phone = info.Rows[0]["Tel"] + " " + "   " + " " + info.Rows[0]["Phone"];



                                    //返回面单显示的信息
                                    //emsInfo.ExpressMessage = GetExpressMessage(emsInfo.salseType, emsInfo.shopName);

                                    string shopName = string.Empty;

                                    emsInfo.shopName = shopName;

                                    emsInfo.StockCode = info.Rows[0]["Pcode"].ToString();//商品代码

                                    emsInfo.GetAddress = info.Rows[0]["Provinces"] + " " + info.Rows[0]["City"] + " " + info.Rows[0]["District"] + " " + info.Rows[0]["Address"];

                                    if (info.Rows.Count > 0)
                                    {
                                        emsInfo.YanSe1 = info.Rows[0]["Color"].ToString();
                                        emsInfo.ChiMa1 = info.Rows[0]["Size"].ToString();
                                        emsInfo.Num1 = info.Rows[0]["Num"].ToString();
                                        emsInfo.StockCode1 = info.Rows[0]["Pcode"].ToString();
                                    }
                                    if (info.Rows.Count > 1)
                                    {
                                        emsInfo.YanSe2 = info.Rows[1]["Color"].ToString();
                                        emsInfo.ChiMa2 = info.Rows[1]["Size"].ToString();
                                        emsInfo.Num2 = info.Rows[1]["Num"].ToString();
                                        emsInfo.StockCode2 = info.Rows[1]["Pcode"].ToString();
                                    }
                                    if (info.Rows.Count > 2)
                                    {
                                        emsInfo.YanSe3 = info.Rows[2]["Color"].ToString();
                                        emsInfo.ChiMa3 = info.Rows[2]["Size"].ToString();
                                        emsInfo.Num3 = info.Rows[2]["Num"].ToString();
                                        emsInfo.StockCode3 = info.Rows[2]["Pcode"].ToString();
                                    }
                                    if (info.Rows.Count > 3)
                                    {
                                        emsInfo.YanSe4 = info.Rows[3]["Color"].ToString();
                                        emsInfo.ChiMa4 = info.Rows[3]["Size"].ToString();
                                        emsInfo.Num4 = info.Rows[3]["Num"].ToString();
                                        emsInfo.StockCode4 = info.Rows[3]["Pcode"].ToString();
                                    } if (info.Rows.Count > 4)
                                    {
                                        emsInfo.YanSe5 = info.Rows[4]["Color"].ToString();
                                        emsInfo.ChiMa5 = info.Rows[4]["Size"].ToString();
                                        emsInfo.Num5 = info.Rows[4]["Num"].ToString();
                                        emsInfo.StockCode5 = info.Rows[4]["Pcode"].ToString();
                                    } if (info.Rows.Count > 5)
                                    {
                                        emsInfo.YanSe6 = info.Rows[5]["Color"].ToString();
                                        emsInfo.ChiMa6 = info.Rows[5]["Size"].ToString();
                                        emsInfo.Num6 = info.Rows[5]["Num"].ToString();
                                        emsInfo.StockCode6 = info.Rows[5]["Pcode"].ToString();
                                    }
                                    if (info.Rows.Count > 6)
                                    {
                                        emsInfo.YanSe7 = info.Rows[6]["Color"].ToString();
                                        emsInfo.ChiMa7 = info.Rows[6]["Size"].ToString();
                                        emsInfo.Num7 = info.Rows[6]["Num"].ToString();
                                        emsInfo.StockCode7 = info.Rows[6]["Pcode"].ToString();
                                    }
                                    if (info.Rows.Count > 7)
                                    {
                                        emsInfo.YanSe8 = info.Rows[7]["Color"].ToString();
                                        emsInfo.ChiMa8 = info.Rows[7]["Size"].ToString();
                                        emsInfo.Num8 = info.Rows[7]["Num"].ToString();
                                        emsInfo.StockCode8 = info.Rows[7]["Pcode"].ToString();
                                    } if (info.Rows.Count > 8)
                                    {
                                        emsInfo.YanSe9 = info.Rows[8]["Color"].ToString();
                                        emsInfo.ChiMa9 = info.Rows[8]["Size"].ToString();
                                        emsInfo.Num9 = info.Rows[8]["Num"].ToString();
                                        emsInfo.StockCode9 = info.Rows[8]["Pcode"].ToString();
                                    } if (info.Rows.Count > 9)
                                    {
                                        emsInfo.YanSe0 = info.Rows[9]["Color"].ToString();
                                        emsInfo.ChiMa0 = info.Rows[9]["Size"].ToString();
                                        emsInfo.Num0 = info.Rows[9]["Num"].ToString();
                                        emsInfo.StockCode0 = info.Rows[9]["Pcode"].ToString();
                                    }

                                    emsInfo.OrderNum = dataGridView1.Rows[i].Cells["Column1"].Value.ToString() + "    " + DateTime.Now.ToString("yyyy-MM-dd");
                                    emsInfo.OrderNo = dataGridView1.Rows[i].Cells["Column1"].Value.ToString();
                                    emsInfo.PostCard = info.Rows[0]["PostBack"].ToString();//邮编
                                    //emsInfo.TotalCount = info.Rows[0]["Num"].ToString();// MyService.GetTotalCount(dataGridView1.Rows[i].Cells["Column1"].Value.ToString());
                                    listCollection.Add(emsInfo); 
                                    
                                }
                            }
                        }
                    }

                }
                if (listCollection.Count > 0)
                    MyBound1(listCollection, "POSTB.rpt");
                saveOk = false;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
                //throw;
            }

            timer1.Enabled = false;
            toolStripStatusLabel1.ForeColor = Color.FromArgb(80, Color.DarkGreen);
            toolStripStatusLabel1.Text = "打印 POSTB 面单完毕  [时间：" + System.DateTime.Now.ToString("yyyy年MM月dd HH时mm分ss秒") + "]";


        }
        #endregion
        #region POSTB 面单打印 MyBound

        /// <summary>
        /// POSTB 面单打印
        /// </summary>
        /// <param name="list"></param>
        /// <param name="fileName"></param>

        private void MyBound1(List<EmsInfo> list, string fileName)
        {
            ReportDocument StockObjectsReport = new ReportDocument();
            StockObjectsReport.Load(AppDomain.CurrentDomain.BaseDirectory + fileName);
            StockObjectsReport.SetDataSource(list);
       
            System.Drawing.Printing.PrintDocument prtDocument = new System.Drawing.Printing.PrintDocument();
            // StockObjectsReport.PrintOptions.PrinterName = prtDocument.PrinterSettings.PrinterName;
            MessageBox.Show(m_ServiceName);
            if (!string.IsNullOrEmpty(m_ServiceName))
            {
                StockObjectsReport.PrintOptions.PrinterName = m_ServiceName;

                int paperSizeid = PaperSizeGetter.GetPaperSizeId(prtDocument.PrinterSettings.PrinterName, "shentong格式");
                StockObjectsReport.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)(paperSizeid);
                StockObjectsReport.PrintToPrinter(1, false, 0, 0);

                foreach (DataGridViewRow dr in dataGridView1.Rows)
                {
                    foreach (EmsInfo al in list)
                    {
                        if (al.OrderNo == dr.Cells["Column1"].Value.ToString())
                        {
                            dr.Cells["Column3"].Value = "是";
                            //生成面单日志
                            //AddLog("23", dr.Cells["Column1"].Value.ToString(), al.MergerOrderID);
                        }
                    }
                }
            }
        }

        #endregion Ems面单打印 MyBound










        #region 快递类型选择事件
        /// <summary>
        /// 选择快递单类型下拉列表框的操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cmbShopMethod.SelectedIndex == 0)
            //{
            //    kdlx = "申通汽运";
            //}
            //else if (cmbShopMethod.SelectedIndex == 1)
            //{
            //    kdlx = "申通快递";
            //    //btnSelect.Visible = true;
            //}
            if (cmbShopMethod.SelectedIndex == 0)
            {
                kdlx = "中通快递";
            }
            else if (cmbShopMethod.SelectedIndex == 2)
            {
                kdlx = "邮政国内小包";
            }
            else
            {
                kdlx = "EMS";
                btnSelect.Visible = true;
            }


        }
        #endregion 快递类型选择事件



        #region 快递单号输入
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                //申通
                //if (cmbShopMethod.SelectedIndex == 0 | cmbShopMethod.SelectedIndex == 1 | cmbShopMethod.SelectedIndex == 3)
                if (cmbShopMethod.SelectedIndex == 0 | cmbShopMethod.SelectedIndex == 2)
                {
                    //if (this.CheckPrintOk() == false)
                    //{
                    //    this.textBox1.Text = "";
                    //    MessageBox.Show("请先打印面单，然后再输入快递单号。", "操作提示");

                    //    return;
                    //}
                    //隐藏浏览按钮
                    btnSelect.Visible = false;
                    if (textBox1.Text.Trim().Equals(""))
                    {
                        return;
                    }
                    if (e.KeyChar == '\r')//if(e.KeyChar == (char)Keys.Enter)  //判断是否按下回车键(扫描枪和键盘不同，键盘用keys.enter 扫描枪用e.keychar=='\r')
                    {
                        if(this.textBox1.Text.Trim().Length!=12)
                        {
                            MessageBox.Show("申通面单12位，你输入了错误的订单位数。", "提示：");
                            return;
                        }

                        string pattern = @"^[0-9]+$";//正则式子  
                        

                        Match m = Regex.Match(this.textBox1.Text.Trim(), pattern);   // 匹配正则表达式，把this.textBox1.Text跟pattern正则对比  

                        if (!m.Success)   // 判断输入的是不是英文和数字，不是进入  
                        {
                            MessageBox.Show("请输入数字，不能输入字母", "提示：");
                        }
                        else   //输入的是英文和数字  
                        {
                            string txtData = textBox1.Text;
                            Int64 S = Int64.Parse(txtData);

                            List<string> sbb = new List<string>();

                            string time = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff");//HH24小时制 hh12小时

                            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                            {
                                if (dataGridView1.Rows[i].Cells["Column3"].Value.ToString() == "是" & dataGridView1.Rows[i].Cells["Column2"].Style.BackColor != Color.LightSeaGreen)// & dataGridView1.Rows[i].Cells["Column2"].Value.ToString().Equals("")
                                {
                                    dataGridView1.Rows[i].Cells["Column2"].Value = S;
                                    sbb.Add(dataGridView1.Rows[i].Cells["Column1"].Value.ToString()+","+S);
                                    S++;
                                }
                            }

                            try
                            {
                                if(sbb.Count>0)
                                {
                                    //保存下载订单
                                    FileInfo IsFile = new FileInfo("LR-" + time + ".txt");
                                    if (!IsFile.Exists)
                                    {
                                        StreamWriter ss = File.CreateText("LR-" + time + ".txt");
                                        ss.Close();
                                    }
                                    var fs = new FileStream("LR-" + time + ".txt", FileMode.Append, FileAccess.Write);
                                    var sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
                                    foreach (var ss in sbb)
                                    {
                                        sw.WriteLine(ss);
                                    }

                                    sw.Close();
                                    fs.Close();
                                }

                               
                            }
                            catch (Exception)
                            {

                                //throw;

                            }
                        }

                    }
                }
                //EMS
                else
                {
                    btnSelect.Visible = true;
                }
            }
            catch (Exception ee)
            {

                MessageBox.Show(ee.Message);
            }
        }

        #endregion 快递单号输入




        #region 保存按钮
        /// <summary>
        /// 保存  将快递单号写入数据库表，没有成功写入的写入本地日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnSave_Click(object sender, EventArgs e)
        {

            setalldisable();

            timer1.Enabled = true;

            try
            {
                //清空打印张数
                //txtPrintPiece.Text = "";
                //清空快递单号
                textBox1.Text = "";
                bool result = true;
                MyService.Credentials = System.Net.CredentialCache.DefaultCredentials;
                string address = m_Address;
                MyService.Url = "http://" + address + "/DeliveryPrintService.asmx";
                //if (CheckPrintOk() == false)
                //{
                //    MessageBox.Show("还没有未打印面单，请先打印面单！", "提示：");
                //    return;
                //}
                //if (CheckDeliveryNoAndOK() == false)
                //{
                //    MessageBox.Show("快递单号不能为空，请输入快递单号!", "提示：");
                //    return;

                //}
                if (CheckDeliveryNoAndOK() == false)
                {
                    MessageBox.Show("快递单号不能为空!");
                    return;

                }
                //else
                //{
                //    //检查快递单号是否重复
                //    foreach (DataGridViewRow dr in dataGridView1.Rows)
                //    {
                //        if (dr.Cells["Column3"].Value.ToString() == "是")
                //        {
                //            if (!string.IsNullOrEmpty(dr.Cells["Column2"].Value.ToString().Trim()))
                //            {
                //                bool b = MyService.CheckDeliveryNo(dr.Cells["Column2"].Value.ToString().Trim());
                //                if (b)
                //                {
                //                    MessageBox.Show(dr.Cells["Column1"].Value.ToString() + "快递单号不能重复!");
                //                    return;

                //                }
                //            }
                //        }

                //    }
                //}

                if (saveOk == false)
                {
                    if (MessageBox.Show("确认要保存数据?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) ==
                        DialogResult.Yes)
                    {

                        toolStripStatusLabel1.ForeColor = Color.FromArgb(80, Color.DarkRed);
                        toolStripStatusLabel1.Text = "您正在保存打印信息到服务器.";

                        string time = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff");

                        List<string >  lll=new List<string>();

                        foreach (DataGridViewRow dr in dataGridView1.Rows)
                        {
                            bool cf = false;//是否重复打印
                            if (dr.Cells["Column3"].Value.ToString() == "是")
                            {
                                var ordernum = dr.Cells["Column1"].Value.ToString().Trim();

                                var deliveryNo = dr.Cells["Column2"].Value.ToString().Trim(); //发货单号
                                //if (!deliveryNo.Equals(""))
                                //{


                                string okddh = MyService.GetOrderInfo2(ordernum);

                                if (okddh == null || okddh.Equals(""))
                                {
                                    bool b = MyService.CheckDeliveryNo(deliveryNo);
                                    if (b)
                                    {
                                        MessageBox.Show(dr.Cells["Column1"].Value.ToString() + "快递单号不能重复!");
                                        return;
                                    }
                                    result &= MyService.DispatchListsInsert(deliveryNo, ordernum, cf);
                                }
                                else if (okddh.Equals(deliveryNo)) //没有变更
                                {
                                    continue;
                                }
                                else
                                {
                                    bool b = MyService.CheckDeliveryNo(deliveryNo);
                                    if (b)
                                    {
                                        MessageBox.Show(dr.Cells["Column1"].Value.ToString() + "快递单号不能重复!");
                                        return;
                                    }
                                    cf = true; //重复打印 替换快递单号
                                    if (MessageBox.Show("该订单已经保存快递单号，您确定要更换吗?", "提示", MessageBoxButtons.YesNo,
                                                        MessageBoxIcon.Information) == DialogResult.Yes)
                                    {

                                        result &= MyService.DispatchListsInsert(deliveryNo, ordernum, cf);
                                    }
                                }
                                //}


                                //不成功则重复写入3次
                                if (!result)
                                {
                                    for (int i = 0; i < 3; i++)
                                    {
                                        result = true;
                                        result &= MyService.DispatchListsInsert(deliveryNo,
                                                                                dr.Cells["Column1"].Value.ToString(), cf);
                                        if (result)
                                        {
                                            break;
                                        }
                                    }
                                }
                                if (!result)
                                {
                                    try
                                    {

                                        FileInfo IsFile = new FileInfo("er-" + time + ".txt");
                                        if (!IsFile.Exists)
                                        {
                                            StreamWriter ss = File.CreateText("er-" + time + ".txt");
                                            ss.Close();
                                        }
                                        var fs = new FileStream("er-" + time + ".txt", FileMode.Append, FileAccess.Write);
                                        var sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
                                        sw.WriteLine(dr.Cells["Column2"].Value.ToString() + "," +
                                                     dr.Cells["Column1"].Value.ToString());
                                        sw.Close();
                                        fs.Close();

                                    }
                                    catch (Exception)
                                    {
                                        
                                        //throw;
                                    }
                                }
                                if (result)
                                {
                                    dr.Cells["Column2"].Style.BackColor = Color.LightSeaGreen;

                                    lll.Add(dr.Cells["Column1"].Value.ToString() + "," + dr.Cells["Column2"].Value.ToString());
                                }

                                //-------------
                                //}
                                //else
                                //{
                                //    MessageBox.Show("快递单号不能为空，请输入快递单号!", "提示：");
                                //}


                            }//end 是

                        }//end for


                        ////清除已保存数据
                        //while (dataGridView1.Rows.Count > 0)
                        //{

                        //    if (dataGridView1.Rows[0].Cells["Column3"].Value.ToString().Equals("是") & !dataGridView1.Rows[0].Cells["Column2"].Value.ToString().Equals(""))
                        //    {
                        //        dataGridView1.Rows.Remove(dataGridView1.Rows[0]);


                        //    }


                        //}
                        ////dataGridView1.EndEdit();
                        //dataGridView1.RefreshEdit();
                        //dataGridView1.Refresh();

                        try
                        {
                            if (lll.Count > 0)
                            {
                                //保存订单
                                FileInfo IsFile = new FileInfo("Save-" + time + ".txt");
                                if (!IsFile.Exists)
                                {
                                    StreamWriter ss = File.CreateText("Save-" + time + ".txt");
                                    ss.Close();
                                }
                                var fs = new FileStream("Save-" + time + ".txt", FileMode.Append, FileAccess.Write);
                                var sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
                                foreach (var ss in lll)
                                {
                                    sw.WriteLine(ss);
                                }

                                sw.Close();
                                fs.Close();
                            }


                        }
                        catch (Exception)
                        {

                            //throw;

                        }


                        toolStripStatusLabel1.ForeColor = Color.FromArgb(80, Color.DarkGreen);
                        toolStripStatusLabel1.Text = "保存打印信息到服务器完毕   [时间：" + System.DateTime.Now.ToString("yyyy年MM月dd HH时mm分ss秒") + "]";
                    }


                } //ene saveok

                timer1.Enabled = false;

                //int jj = 0;





                if (CheckPrintOk() & CheckDeliveryNoAndOK())
                {
                    saveOk = true;//保存成功

                    if (MessageBox.Show("是否继续下载订单数据进行打印?", "提示", MessageBoxButtons.YesNo,
                                                            MessageBoxIcon.Information) == DialogResult.Yes)
                    {

                        DataBind(kdlx);

                    }
                    else
                    {
                        //dataGridView1.Rows.Clear();
                        //return;
                    }
                }

                //else
                //{
                //获取打印信息
                GetPrintNum();
                //}

            }
            catch (Exception ee)
            {

                MessageBox.Show(ee.Message);
            }
            setallenable();

        }
        #endregion 保存按钮


        #region 查询订单号按钮
        /// <summary>
        /// 查询订单号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {

            try
            {

                //保存数据
                //CheckDataIsSave();

                ////string strID = StringTools.EncodingForString(cmbShopMethod.SelectedValue.ToString());
                string strOrderNo = txtOrderNo.Text.Trim();
                if (strOrderNo.Equals(""))
                {
                    return;
                }
                DataTable dt = new DataTable();

                MyService.Timeout = 600000;
                Stopwatch st = new Stopwatch();
                st.Start();
                timer1.Enabled = true;

                //
                toolStripStatusLabel1.ForeColor = Color.FromArgb(80, Color.DarkRed);
                toolStripStatusLabel1.Text = "您正在查询单个订单详细信息.";


                dt = MyService.GetOrderInfo(strOrderNo).Tables[0];


                st.Stop();

                //弹出毫秒
                //MessageBox.Show(st.ElapsedMilliseconds.ToString());


                dataGridView2.DataSource = null;
                if (dt != null)
                {
                    this.dataGridView2.DataSource = dt;
                    this.Update_ddmx_btl();
                }
                else
                {
                    MessageBox.Show("没有找到此订单的信息！");
                    //return;
                }
                //释放资源
                dt.Dispose();

                toolStripStatusLabel1.ForeColor = Color.FromArgb(80, Color.DarkGreen);
                toolStripStatusLabel1.Text = "查询单个订单详细信息完毕   [时间：" + System.DateTime.Now.ToString("yyyy年MM月dd HH时mm分ss秒") + "]";
            }
            catch (Exception ee)
            {

                MessageBox.Show(ee.Message);
            }

            timer1.Enabled = false;


        }
        #endregion 查询订单号按钮



        #region 浏览按钮
        /// <summary> 
        /// 浏览
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                //设置用户只能选择.txt格式文件
                ofd.Filter = "*.txt|*.txt";
                if (ofd.ShowDialog(this) == DialogResult.OK)
                {
                    //读取文本文件
                    string[] orders = File.ReadAllLines(ofd.FileName);
                    textBox1.Text = ofd.FileName;
                    //循环所有快递单号，将快递单号填充到控件

                    List<string> sb2=new List<string>();
                    string time = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff");//HH24小时制 hh12小时

                    for (int i = 0; i < orders.Length; i++)
                    {
                        //判断DataGridView是否存在该行，防止DataGridView数据行小于文本文件中的行。
                        if (dataGridView1.Rows.Count > i)
                        {
                            if(orders[i].Length!=13)
                            {
                                MessageBox.Show("EMS面单为13位，请输入正确的位数。","提示：");
                                break;
                            }

                            string pattern = @"^[A-Z]+\d+[A-Z]+$";//正则式子  字母开头 数字重复多次 字母结束


                            Match m = Regex.Match(orders[i], pattern);   // 匹配正则表达式，把this.textBox1.Text跟pattern正则对比  

                            if (!m.Success)   // 判断输入的是不是英文和数字，不是进入  
                            {
                                MessageBox.Show("EMS面单是字母打头，字母结束，请检查格式。","提示：");

                                break;
                            }



                            dataGridView1.Rows[i].Cells["Column2"].Value = orders[i];

                            sb2.Add(dataGridView1.Rows[i].Cells["Column1"].Value.ToString()+","+orders[i]);

                        }
                        else
                        {
                            //如果小于则跳出循环
                            break;
                        }

                        try
                        {
                            if(sb2.Count>0)
                            {

                                //保存下载订单
                                FileInfo IsFile = new FileInfo("LR-ems-" + time + ".txt");
                                if (!IsFile.Exists)
                                {
                                    StreamWriter ss = File.CreateText("LR-ems-" + time + ".txt");
                                    ss.Close();
                                }
                                var fs = new FileStream("LR-ems-" + time + ".txt", FileMode.Append, FileAccess.Write);
                                var sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
                                foreach (var ss in sb2)
                                {
                                    sw.WriteLine(ss);
                                }

                                sw.Close();
                                fs.Close();

                            }

                        }
                        catch (Exception)
                        {

                            //throw;

                        }

                    }
                    //删除.txt文件
                    File.Delete(ofd.FileName);
                }
            }
            catch (Exception ee)
            {

                MessageBox.Show(ee.Message);
            }
        }
        #endregion 浏览按钮









        #region 设置单条为否按钮

        /// <summary>
        /// 设置单条为否
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetSingle_Click(object sender, EventArgs e)
        {
            //循环DataGridView
            foreach (DataGridViewRow dr in dataGridView1.Rows)
            {
                string str = textBox_reprint.Text.Trim();  //获取订单号的值
                //如果那一行等于是，又和输入的快递单号相当的话
                if (dr.Cells["Column3"].Value.ToString() == "是" && str == dr.Cells["Column1"].Value.ToString())
                {
                    //就让这一列变为否
                    dr.Cells["Column3"].Value = "否";
                    dr.Cells["Column3"].Style.BackColor = Color.Red;
                    fzrlock = true;
                    break;
                }
            }

            //this.btnPrint_Click(this, new EventArgs());
        }

        #endregion  设置单条为否按钮





        #region 选择打印条数事件
        private void cmbSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            //100
            if (cmbSelect.SelectedIndex == 0)
            {
                OnceNum = 100;
                printPiece = 100;
                this.txtPrintPiece.Text = "100";
            }
            //200
            else if (cmbSelect.SelectedIndex == 1)
            {
                OnceNum = 200;
                printPiece = 200;
                this.txtPrintPiece.Text = "200";
            }
            //300
            else if (cmbSelect.SelectedIndex == 2)
            {
                OnceNum = 300;
                printPiece = 300;
                this.txtPrintPiece.Text = "300";
            }
            //400
            else if (cmbSelect.SelectedIndex == 3)
            {
                OnceNum = 400;
                printPiece = 400;
                this.txtPrintPiece.Text = "400";
            }
            //500
            else if (cmbSelect.SelectedIndex == 4)
            {
                OnceNum = 500;
                printPiece = 500;
                this.txtPrintPiece.Text = "500";
            }
        }
        #endregion 选择打印条数事件









        #region 返回面单相应信息
        /// <summary>
        /// 返回面单相应信息
        /// </summary>
        /// <param name="salseType"></param>
        /// <param name="shopName"></param>
        /// <returns></returns>
        private string GetExpressMessage(string salseType, string shopName)
        {

            if (salseType == "L" || shopName == "柠檬绿茶")
            {
                //return @"请您签收前，检查包裹外包装！(包裹外包装有损坏或受潮，请拒收并联系店家客服，感谢您的配合)";
                return @"请您签收前，检查包裹外包装完好！（若有破损，请先联系客服或拒收）";
            }
            else
            {
                return @"签收前，请您检查包裹外包装！(包裹外包装有损坏或受潮，请拒收并联系柠檬绿茶客服4006-189-889)";
            }
        }

        #endregion 返回面单相应信息



        /// <summary>
        /// 如果等于是的话，快递单号必须有值
        /// </summary>
        /// <returns></returns>

        private bool CheckDeliveryNoAndOK()
        {
            bool tt = true;
            if (dataGridView1.Rows.Count > 0)
            {

                foreach (DataGridViewRow dr in dataGridView1.Rows)
                {

                    if (dr.Cells["Column3"].Value.ToString() == "是")
                    {
                        if (dr.Cells["Column2"].Value == null || string.IsNullOrEmpty(dr.Cells["Column2"].Value.ToString()))
                        {

                            tt = false;
                        }
                    }
                }

            }
            return tt;
        }

        //检查面单是否打印完
        private bool CheckPrintOk()
        {
            bool tt = true;

            if (dataGridView1.Rows.Count > 0)
            {
                foreach (DataGridViewRow dr in dataGridView1.Rows)
                {

                    if (dr.Cells["Column3"].Value == "否")
                    {

                        return false;
                    }

                }
            }
            return tt;
        }



        #region 检查数据是否保存，并提示
        /// <summary>
        /// 检查数据是否保存，并提示。
        /// </summary>
        private void CheckDataIsSave()
        {
            if (!saveOk)
            {
                return;
            }
            bool ts = false;


            if (dataGridView1.RowCount > 0)
            {
                ts = CheckDeliveryNoAndOK();
                if (ts == true)
                {
                    ts = CheckPrintOk();
                }
                if (!ts)
                {
                    if (MessageBox.Show("当前数据已经改变，您确定保存数据吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        ////定义变量
                        //saveOk = true;
                        //保存数据
                        btnSave_Click(null, EventArgs.Empty);
                    }
                }

            }



        }
        #endregion 检查数据是否保存，并提示
        /// <summary>
        /// 检查是否有需要注意的地区编码
        /// </summary>
        /// <param name="code">地区编码</param>
        /// <param name="itemCode">需要注意的地区编码</param>
        /// <returns>True:有符合的地区编码 False:没有符合</returns>
        private bool CheckAddressCode(string code, string[] itemCode)
        {
            foreach (var s in itemCode)
            {
                if (s == code)
                {
                    return false;
                }
            }
            return true;
        }


        /// <summary>
        /// 检查不需要打出申通汽运的省市区编码
        /// </summary>
        /// <param name="code"></param>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        private bool CheckDistrictClassQiYu(string code, string[] itemCode)
        {
            var tmp = code.Substring(0, 2);
            foreach (var s in itemCode)
            {
                if (s == tmp)
                {
                    return false;
                }
            }
            return true;
        }


        /// <summary>
        /// 检查是否有需要注意的商品名称
        /// </summary>
        /// <param name="stockName">商品名称</param>
        /// <param name="itemSpecialString">需要注意的特殊字符</param>
        /// <returns>True:有符合的商品名称 False:没有符合</returns>
        private static bool CheckStockName(string stockName, string[] itemSpecialString)
        {
            foreach (var s in itemSpecialString)
            {
                if (stockName.IndexOf(s.Trim()) > 0)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 获取活动名称按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_gethdname_Click(object sender, EventArgs e)
        {
            setalldisable();
            gethdname();
            setallenable();
        }


        //活动名称改变
        private void comboBox_hdname_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_hdname.SelectedValue != null)
            {
                hdname = comboBox_hdname.SelectedValue.ToString();
                hdname=hdname.Substring(0, hdname.LastIndexOf("|"));

                //if (hdname != null && !hdname.Equals(""))
                //{
                //    //if (kdlx != null && !kdlx.Equals(""))
                //    //{
                //    //    DataBind(kdlx);
                //    //}
                //}

            }



        }

        private void button_setmulfalse_Click(object sender, EventArgs e)
        {

            string str = txtOrderNo.Text.Trim(); //获取订单号的值
            if (!str.Equals(""))
            {
                int f = 0;

                //循环DataGridView
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {

                    if (dataGridView1.Rows[i].Cells["Column3"].Value.ToString() == "是")
                    {
                        if (dataGridView1.Rows[i].Cells["Column1"].Value.ToString().Equals(str))
                        {
                            f = i;

                            break;
                        }

                    }
                }
                //

                //if(f!=0)
                //{
                for (int i = f; i < dataGridView1.Rows.Count; i++)
                {

                    if (dataGridView1.Rows[i].Cells["Column3"].Value.ToString() == "是")
                    {

                        dataGridView1.Rows[i].Cells["Column3"].Value = "否";
                        dataGridView1.Rows[i].Cells["Column3"].Style.BackColor = Color.Red;

                    }
                }
                fzrlock = true;
                //}

                //

            }

        }

        private void button_getallprintok_Click(object sender, EventArgs e)
        {
            Form_PrintAllOk pro = Form_PrintAllOk.InstanceObject();
            pro.Address = this.m_Address;
            pro.Hdname = this.hdname;
            pro.Show();

            pro.Activate();
        }


        //选择订单号
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 0 && e.RowIndex != -1 && !dataGridView1.Rows[e.RowIndex].IsNewRow)
            {
                string onum = this.dataGridView1.Rows[e.RowIndex].Cells["Column1"].Value.ToString();
                this.txtOrderNo.Text = onum;
                this.textBox_reprint.Text = onum;
                //rowin=
            }
        }

        private void 帮助信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_help fh = Form_help.InstanceObject();
            fh.Show();
            fh.Activate();
        }


        private int statusnum = 26;
        private int statusnumnow = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (statusnumnow < statusnum)
            {
                toolStripStatusLabel1.Text += ".";
                statusnumnow++;
            }
            else
            {
                statusnumnow = 0;
                try
                {
                    toolStripStatusLabel1.Text = toolStripStatusLabel1.Text.Substring(0,
                                                                                              toolStripStatusLabel1.Text.IndexOf("."));
                }
                catch (Exception)
                {

                    //throw;
                }
            }
        }

        private void DeliveryPrint_Resize(object sender, EventArgs e)
        {
            float[] scale = (float[])Tag;
            int i = 2;

            foreach (Control ctrl in Controls)
            {
                ctrl.Left = (int)(Size.Width * scale[i++]);
                ctrl.Top = (int)(Size.Height * scale[i++]);
                ctrl.Width = (int)(Size.Width / (float)scale[0] * ((Size)ctrl.Tag).Width);
                ctrl.Height = (int)(Size.Height / (float)scale[1] * ((Size)ctrl.Tag).Height);

                //每次使用的都是最初始的控件大小，保证准确无误。
            }
        }

        void setalldisable()
        {
            foreach (Control ctrl in Controls)
            {
                if (ctrl.GetType().ToString().Equals("System.Windows.Forms.Button"))
                {
                    ctrl.Enabled = false;
                }

            }
        }


        void setallenable()
        {
            foreach (Control ctrl in Controls)
            {
                ctrl.Enabled = true;
            }
        }

        private void DeliveryPrint_ResizeEnd(object sender, EventArgs e)
        {

        }



        //替换订单明细的标题栏

        private void Update_ddmx_btl()
        {
            if (dataGridView2 != null)
            {
                //dataGridView2.Columns["id"].HeaderText = "序号";
                //dataGridView2.Columns["id"].DisplayIndex = 0;
                dataGridView2.Columns["id"].Visible = false;
                dataGridView2.Columns["Iid"].Visible = false;
                dataGridView2.Columns["pic_path"].Visible = false;
                dataGridView2.Columns["Type"].Visible = false;
                dataGridView2.Columns["Alipay_no"].Visible = false;
                dataGridView2.Columns["Payment"].Visible = false;
                dataGridView2.Columns["Status"].Visible = false;
                dataGridView2.Columns["Seller_rate"].Visible = false;
                dataGridView2.Columns["Buyer_rate"].Visible = false;
                dataGridView2.Columns["IsError"].Visible = false;
                dataGridView2.Columns["IsComplete"].Visible = false;
                dataGridView2.Columns["IsMerger"].Visible = false;
                dataGridView2.Columns["IsModifiedPostage"].Visible = false;
                dataGridView2.Columns["JHLockUser"].Visible = false;
                dataGridView2.Columns["IsClose"].Visible = false;
                dataGridView2.Columns["addTime"].Visible = false;
                dataGridView2.Columns["addType"].Visible = false;
                dataGridView2.Columns["Piecesday"].Visible = false;
                dataGridView2.Columns["paymentmethod"].Visible = false;




                dataGridView2.Columns["Seller_nick"].HeaderText = "店铺";
                dataGridView2.Columns["Seller_nick"].DisplayIndex = 1;




                dataGridView2.Columns["Buyer_nick"].HeaderText = "买家ID";
                dataGridView2.Columns["Buyer_nick"].DisplayIndex = 2;





                dataGridView2.Columns["Title"].HeaderText = "产品标题";
                dataGridView2.Columns["Title"].DisplayIndex = 3;


                dataGridView2.Columns["Price"].HeaderText = "价格";
                dataGridView2.Columns["Price"].DisplayIndex = 4;



                dataGridView2.Columns["Num"].HeaderText = "数量";
                dataGridView2.Columns["Num"].DisplayIndex = 5;


                dataGridView2.Columns["Created"].HeaderText = "交易创建时间";
                dataGridView2.Columns["Created"].DisplayIndex = 6;


                dataGridView2.Columns["Tid"].HeaderText = "订单号";
                dataGridView2.Columns["Tid"].DisplayIndex = 7;


                dataGridView2.Columns["PaymentTime"].HeaderText = "支付时间";
                dataGridView2.Columns["PaymentTime"].DisplayIndex = 8;



                dataGridView2.Columns["Consignee"].HeaderText = "收货人";
                dataGridView2.Columns["Consignee"].DisplayIndex = 9;



                dataGridView2.Columns["Phone"].HeaderText = "电话";
                dataGridView2.Columns["Phone"].DisplayIndex = 10;



                dataGridView2.Columns["Tel"].HeaderText = "联系电话";
                dataGridView2.Columns["Tel"].DisplayIndex = 11;


                dataGridView2.Columns["Provinces"].HeaderText = "省份";
                dataGridView2.Columns["Provinces"].DisplayIndex = 12;


                dataGridView2.Columns["City"].HeaderText = "城市";
                dataGridView2.Columns["City"].DisplayIndex = 13;


                dataGridView2.Columns["District"].HeaderText = "区";
                dataGridView2.Columns["District"].DisplayIndex = 14;


                dataGridView2.Columns["Address"].HeaderText = "地址";
                dataGridView2.Columns["Address"].DisplayIndex = 15;


                dataGridView2.Columns["PostBack"].HeaderText = "邮编";
                dataGridView2.Columns["PostBack"].DisplayIndex = 16;


                dataGridView2.Columns["ExpressWay"].HeaderText = "快递方式";
                dataGridView2.Columns["ExpressWay"].DisplayIndex = 17;


                dataGridView2.Columns["LogisticsCompanies"].HeaderText = "物流公司";
                dataGridView2.Columns["LogisticsCompanies"].DisplayIndex = 18;


                dataGridView2.Columns["AWBNo"].HeaderText = "运单号";
                dataGridView2.Columns["AWBNo"].DisplayIndex = 19;


                dataGridView2.Columns["Remark"].HeaderText = "买家留言";
                dataGridView2.Columns["Remark"].DisplayIndex = 20;


                dataGridView2.Columns["freight"].HeaderText = "运费";
                dataGridView2.Columns["freight"].DisplayIndex = 21;


                dataGridView2.Columns["PaymentTotal"].HeaderText = "实收款";
                dataGridView2.Columns["PaymentTotal"].DisplayIndex = 22;
                dataGridView2.Columns["PaymentTotal"].Visible = false;

                dataGridView2.Columns["buyMailBox"].HeaderText = "支付宝号";
                dataGridView2.Columns["buyMailBox"].DisplayIndex = 23;
                dataGridView2.Columns["buyMailBox"].Visible = false;


                dataGridView2.Columns["salesType"].HeaderText = "销售类型";
                dataGridView2.Columns["salesType"].DisplayIndex = 24;


                dataGridView2.Columns["Paccount"].HeaderText = "打印操作员";
                dataGridView2.Columns["Paccount"].DisplayIndex = 25;

                dataGridView2.Columns["Pcode"].HeaderText = "产品代码";
                dataGridView2.Columns["Pcode"].DisplayIndex = 26;


                dataGridView2.Columns["IsRead"].HeaderText = "是否下载";
                dataGridView2.Columns["IsRead"].DisplayIndex = 27;


                dataGridView2.Columns["IsPrint"].HeaderText = "是否打印";
                dataGridView2.Columns["IsPrint"].DisplayIndex = 28;


                dataGridView2.Columns["Stime"].HeaderText = "下载时间";
                dataGridView2.Columns["Stime"].DisplayIndex = 29;


                dataGridView2.Columns["Ptime"].HeaderText = "打印时间";
                dataGridView2.Columns["Ptime"].DisplayIndex = 30;


                dataGridView2.Columns["DeliveryNo"].HeaderText = "快递单号";
                dataGridView2.Columns["DeliveryNo"].DisplayIndex = 31;


                dataGridView2.Columns["hdname"].HeaderText = "活动名称";
                dataGridView2.Columns["hdname"].DisplayIndex = 32;


                dataGridView2.Columns["cfprint"].HeaderText = "重复打印数";
                dataGridView2.Columns["cfprint"].DisplayIndex = 33;



            }
        }

        private void txtPrintPiece_TextChanged(object sender, EventArgs e)
        {
            if (txtPrintPiece.Text.Trim().Equals(""))
            {
                return;
            }
            string pattern = @"^[0-9]+$";//正则式子  
            string txtData = txtPrintPiece.Text.Trim();

            Match m = Regex.Match(txtData, pattern);   // 匹配正则表达式，把this.textBox1.Text跟pattern正则对比  

            if (!m.Success)   // 判断输入的是不是英文和数字，不是进入  
            {
                MessageBox.Show("请输入数字，不能输入字母", "提示：");
            }
            else   //输入的是英文和数字  
            {

                printPiece = int.Parse(txtData);


            }
        }

        private void button_jiechu_Click(object sender, EventArgs e)
        {
            setalldisable();


            if (dataGridView1 != null && dataGridView1.Rows.Count > 0)
            {
                foreach (DataGridViewRow dr in dataGridView1.Rows)
                {
                    if (dr.Cells["Column3"].Value.ToString().Equals("是"))
                    {
                        MessageBox.Show("你已经开始打印订单，不能释放选择的订单，请把下载的处理完毕。");
                        return;
                    }
                }

                if (fzrlock)
                {
                    MessageBox.Show("你已经重打订单，不能释放选择的订单，请把下载的处理完毕。");
                    return;
                }

                if (MessageBox.Show("您将解除已经下载的并且没有打印订单的锁定状态,解锁的订单可以被其他人打印使用！", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (DataGridViewRow dr in dataGridView1.Rows)
                    {
                        if (dr.Cells["Column3"].Value.ToString().Equals("否") & !dr.Cells["Column1"].Value.ToString().Equals(""))
                        {
                            sb.Append("'" + dr.Cells["Column1"].Value.ToString() + "',");
                        }
                    }

                    if (sb.Length > 0)
                    {
                        MyService.Credentials = System.Net.CredentialCache.DefaultCredentials;
                        string address = m_Address;
                        MyService.Url = "http://" + address + "/DeliveryPrintService.asmx";

                        string s = sb.ToString();
                        string ok = s.Remove(s.LastIndexOf(','));
                        bool b = MyService.RealseLock(ok);
                        if (b)
                        {
                            while (dataGridView1.Rows.Count > 0)
                            {
                                if (dataGridView1.Rows[0].Cells["Column3"].Value.ToString().Equals("否") & !dataGridView1.Rows[0].Cells["Column1"].Value.ToString().Equals(""))
                                {
                                    dataGridView1.Rows.Remove(dataGridView1.Rows[0]);


                                }
                                else
                                {
                                    break;
                                }

                            }
                            dataGridView1.Refresh();


                            MessageBox.Show("释放锁定成功！");
                        }
                        else
                        {
                            MessageBox.Show("释放锁定失败，请确认联网并重新尝试！");
                        }

                    }
                }
            }

            setallenable();

        }

        private void button_download_Click(object sender, EventArgs e)
        {
            setalldisable();


            //if (!CheckPrintOk() &&)
            //{
            //    setallenable();
            //    return;
            //}
            bool ts = false;


            if (dataGridView1.RowCount > 0)
            {

                if (!CheckPrintOk())
                {
                    if (MessageBox.Show("当前订单还没有打印完，是否继续打印面单?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        btnPrint_Click(null, new EventArgs());
                        return;
                    }
                    else
                    {
                        setallenable();
                        return;
                    }
                }


                if (!CheckDeliveryNoAndOK())
                {
                    if (MessageBox.Show("您还没有录入快递单号，是否继续录入?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        ////定义变量
                        //saveOk = true;
                        //保存数据
                        textBox1.Focus();
                        return;
                    }
                    else
                    {
                        setallenable();
                        return;
                    }
                }

                if (!saveOk)
                {
                    if (MessageBox.Show("当前数据已经改变，您确定保存数据吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        ////定义变量
                        //saveOk = true;
                        //保存数据
                        btnSave_Click(null, EventArgs.Empty);
                        return;
                    }
                    else
                    {
                        setallenable();
                        return;
                    }
                }
            }

            txtOrderNo.Clear();
            //
            if (MessageBox.Show("您选择的活动是：“" + hdname + "”，快递类型为：“" + kdlx + "”，请确认！您将下载订单到本机,并保持订单的锁定状态到打印完毕,在此期间其他人不能打印您已经锁定的订单！", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {


                DataBind(kdlx);

                //把打印面单变为可用
                btnPrint.Enabled = true;
                dataGridView1.Columns["Column2"].Visible = true;
                textBox1.Enabled = true;
                btnSelect.Enabled = true;
            }
            //DeliveryPrint_Resize(this,new EventArgs());
            setallenable();

        }

        private void 没点发货汇总ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_NoFaHuo faHuo=new Form_NoFaHuo();
            faHuo.Address = m_Address;
            faHuo.Show();
            faHuo.Activate();

        }

        private void formkucunMenuItem_Click(object sender, EventArgs e)
        {
            Form_kucun kucun = new Form_kucun();
            kucun.huodongname = comboBox_hdname.DataSource;
            kucun.XS();
            kucun.Show();
            kucun.Activate();
        }

        private void 活动信息设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setalldisable();
            gethdname();
            setallenable();
            Form_huodong hdname = new Form_huodong();
            hdname.Seller_ID = Seller_ID;
            hdname.address = m_Address;
            hdname.Show();
            hdname.hd = ll;            
            hdname.Activate();
            hdname.locd();
        }





    }
}
