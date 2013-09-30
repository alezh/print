using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace WindowsFormsApplication1
{
    public partial class Form_user : Form
    {
        public Form_user()
        {
            InitializeComponent();
            //string text = "新密码设置不能太多简单 /r/n点击激活后会出现IE对话框/r/n输入店铺账号与密码点击授权";
            label5.Text = "第一行内容<br>第二行内容";
        }
        public string address { get; set; }
        DeliveryPrintService.DeliveryPrintService MyService = new DeliveryPrintService.DeliveryPrintService();
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text.ToString()) && string.IsNullOrEmpty(textBox2.Text.ToString()))
            {
                MessageBox.Show("请将店铺名称与打印密码填写完整。");
            }
            string user = textBox1.Text;
            string url = "http://container.open.taobao.com/container?appkey=12008063";
            IE(url);
            xunhuan();
            
        }
        //打开浏览器
        private Boolean IE(string Url)
        {
            using (var proc = new Process())
            {
                proc.StartInfo.FileName = Url;
                proc.StartInfo.Arguments = null;
                proc.StartInfo.Verb = "open";
                proc.StartInfo.UseShellExecute = true;
                return proc.Start();
            } 
        }
        private void xunhuan()
        {
            bool sd = true;
            while (sd)
            {
                if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text))
                {
                    textBox3.Enabled = true;
                    sd = false;
                }
                else 
                { 
                    textBox3.Enabled = false;
                }                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox3.Text.Length > 20)
            {
                button2.Enabled = true;
            }
            else
            {
                MessageBox.Show("请输入正确的授权码");
            }
            string session = textBox3.Text;
            string loginID = textBox1.Text;
            string password = textBox2.Text;
            
        }

        private void Form_user_Load(object sender, EventArgs e)
        {
            DeliveryPrintService.myheader myheader = new DeliveryPrintService.myheader();
            MyService.Url = "http://" + address + "/DeliveryPrintService.asmx";
            myheader.username = "nmlch-2012-byken";
            MyService.myheaderValue = myheader;
        }
    }
}
