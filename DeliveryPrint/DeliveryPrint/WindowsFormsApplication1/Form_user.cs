﻿using System;
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

        private void button1_Click(object sender, EventArgs e)
        {
            string user = textBox1.Text;
            string url = "http://container.open.taobao.com/container?appkey=12008063";
            if(IE(url));
        }
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
    }
}
