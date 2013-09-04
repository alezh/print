using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Xml;
using WindowsFormsApplication1.DeliveryPrintService;

namespace WindowsFormsApplication1
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
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
       // string path = AppDomain.CurrentDomain.BaseDirectory + @"\PathConfig.xml"; //读取PahConfig.xml的路径
        string address = string.Empty;
        string serviceName = string.Empty;
        string format = string.Empty;
        string shopSource = string.Empty;
        string printName = string.Empty;
       
        /// <summary>
        /// 清空文本
        /// </summary>
        public void ClearText()
        {
            this.txtPassword.Text = "";
            this.txtUserName.Text = "";
        }
        /// <summary>
        /// 验证登录
        /// </summary>
        /// <returns></returns>
        public bool CheckLogin()
        {
            string userName = txtUserName.Text.Trim();
            string password = txtPassword.Text.Trim();
            //验证用户名
            if (userName == null || userName.Equals(""))
            {
                MessageBox.Show("用户名不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txtUserName.Focus();
                return false;
            }
            //验证密码
            if (password == null || password.Equals(""))
            {
                MessageBox.Show("密码不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txtPassword.Focus();
                return false;
            }
            return true;
        }
        /// <summary>
        /// 关闭登录窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("是否要关掉当前窗体？", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                Process.GetCurrentProcess().Kill();
            }
            else
            {
                e.Cancel = true;
            }
        }
        /// <summary>
        /// 系统设置菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmSystemSet_Click(object sender, EventArgs e)
        {
            FrmSystemSet fs = new FrmSystemSet();
            fs.Show();
            this.Hide();
        }

        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPassword.Focus();
            }  
        }


        private void Login()
        {
            try
            {
                //验证
                if (CheckLogin())
                {
                    //address = ReadConfig(); //获取地址
                    XmlDocument doc = new XmlDocument();
                    doc.Load("PathConfig.xml");
                    XmlNode node = doc.SelectSingleNode("Config/Path");
                    XmlNode node1 = doc.SelectSingleNode("Config/ServiceName");
                    XmlNode node2 = doc.SelectSingleNode("Config/Format");
                    XmlNode node3 = doc.SelectSingleNode("Config/ShopSource");
                    XmlNode node4 = doc.SelectSingleNode("Config/PrintName");

                    string serverPath = node.InnerText;
                    serviceName = node1.InnerText;
                    format = node2.InnerText;
                    address = serverPath;
                    shopSource = node3.InnerText;
                    printName = node4.InnerText;

                    DeliveryPrintService.DeliveryPrintService MyService = new DeliveryPrintService.DeliveryPrintService();
                    MyService.Credentials = System.Net.CredentialCache.DefaultCredentials;
                    MyService.Url = "http://" + address + "/DeliveryPrintService.asmx";

                    DeliveryPrintService.myheader myheader = new myheader();
                    myheader.username = "nmlch-2012-byken";

                    MyService.myheaderValue = myheader;

                    ////使用cookie
                    //CookieContainer cookie=new CookieContainer();

                    //MyService.CookieContainer = cookie;

                    string strUserName = StringTools.EncodingForString(this.txtUserName.Text.Trim());
                    string strPassword = StringTools.EncodingForString(this.txtPassword.Text.Trim());
                    DataTable dt = MyService.LoginCheck(strUserName, strPassword);

                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            string UserRole = dt.Rows[0]["UserRole"].ToString();
                            string Seller_ID = dt.Rows[0]["Seller_ID"].ToString();
                            string Seller_Iid = dt.Rows[0]["Seller_Iid"].ToString();
                            if(!UserRole.Equals("Q"))
                            {
                                DeliveryPrint dp = new DeliveryPrint();

                                foreach (DataRow dr in dt.Rows)
                                {
                                    dp.Account = dr["Account"].ToString();
                                    dp.CPersonCode = dr["cPersonCode"].ToString();
                                    //dp.CDepCode = dr["cDepCode"].ToString();
                                }
                                dp.Userrole = "打印";
                                dp.Address = address;
                                dp.Seller_ID = Seller_ID;
                                dp.Seller_Iid = Seller_Iid;
                                dp.ServiceName = serviceName;
                                // dp.Format = format;
                                dp.ShipSource = shopSource;
                                dp.PrintName = printName;
                                dp.Show();
                                this.Hide();
                            }
                            else
                            {
                                DeliveryPrint dp = new DeliveryPrint();

                                foreach (DataRow dr in dt.Rows)
                                {
                                    dp.Account = dr["Account"].ToString();
                                    dp.CPersonCode = dr["cPersonCode"].ToString();
                                    dp.CDepCode = dr["cDepCode"].ToString();
                                }
                                dp.Userrole = "查询";
                                dp.Address = address;
                                dp.Seller_ID = Seller_ID;
                                dp.Seller_Iid = Seller_Iid;
                                //dp.ServiceName = serviceName;
                                //// dp.Format = format;
                                //dp.ShipSource = shopSource;
                                //dp.PrintName = printName;
                                dp.Show();
                                dp.Show();
                                this.Hide();

                            }
                            
                        }
                        else
                        {
                            MessageBox.Show("用户名和密码输入的不正确！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            ClearText();
                            this.txtUserName.Focus();

                        }
                    }
                    else
                    {
                        MessageBox.Show("用户名和密码输入的不正确，或该用户已经登录！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ClearText();
                        this.txtUserName.Focus();

                    }
                }

            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
                //throw;
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.Focus();
                btnLogin_Click_1(sender, e);

            }  
        }

        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            Login();
        }

        private void FrmLogin_Resize(object sender, EventArgs e)
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

        private void 解锁用户ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form nf = new Form_jiesuo();
            nf.Show();
            nf.Activate();
       
        }

        private void 新增用户与激活ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_user user = new Form_user();
            user.Show();
            user.Activate();
        }
        
        ///// <summary>
        ///// 读取PathConfig.xml文件
        ///// </summary>
        ///// <returns></returns>
        //public string ReadConfig()
        //{
        //    XmlDocument doc = new XmlDocument();
        //    doc.Load(path);
        //    XmlNode node = doc.SelectSingleNode("Config/Path");
        //    XmlNode node1 = doc.SelectSingleNode("Config/ServiceName");
        //    XmlNode node2 = doc.SelectSingleNode("Config/Format");
        //    string serverPath = node.InnerText;
        //     serviceName = node1.InnerText;
        //   format = node2.InnerText;

        //    return serverPath;
        //}

       
    }
}
