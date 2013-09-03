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
    }
}
