using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form_user : Form
    {
        public Form_user()
        {
            InitializeComponent();
            string text = "新密码设置不能太多简单 /r/n点击激活后会出现IE对话框/r/n输入店铺账号与密码点击授权";
            label5.Text = text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string user = textBox1.Text;
            string url = "";
            using (var proc = new Process())
            {
                proc.StartInfo.FileName = url;
                proc.StartInfo.Arguments = null;
                proc.StartInfo.Verb = "open";
                proc.StartInfo.UseShellExecute = true;
                return proc.Start();
            }
        }

        private bool login(string user, string pass)
        {
            string sql = "select * from Account";
        }
        private bool touser(string user)
        {
            string session = "";//session_id
            string app_key = "";//app_key
            string aap_secret = "";//aap_secret
            string sql = "select * from TaoBaoShopAPI where shopname='"+user+"'";
 
        }
    }
}
