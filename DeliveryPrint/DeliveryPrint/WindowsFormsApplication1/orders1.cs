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
    public partial class orders1 : Form
    {
        public orders1()
        {
            InitializeComponent();
        }
        DeliveryPrintService.DeliveryPrintService MyService = new DeliveryPrintService.DeliveryPrintService();
        public string address { get; set; }
        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "输入订单号" || !string.IsNullOrEmpty(textBox1.Text))
            {
                if (MyService.orders(textBox1.Text, "", "", "", "", "", "", "1"))
                    MessageBox.Show("成功");
                else
                    MessageBox.Show("失败");
            }
        }

        private void orders1_Load(object sender, EventArgs e)
        {
            DeliveryPrintService.myheader myheader = new DeliveryPrintService.myheader();
            myheader.username = "nmlch-2012-byken";
            MyService.Url = "http://" + address + "/DeliveryPrintService.asmx";
            MyService.myheaderValue = myheader;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox9.Text!="输入订单号"||!string.IsNullOrEmpty(textBox9.Text)||textBox5.Text!="输入详细地址"||!string.IsNullOrEmpty(textBox5.Text))
            {
                if(MyService.orders(textBox9.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text, "2"))
                    MessageBox.Show("成功");
                else
                    MessageBox.Show("失败");
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.Text = "";
        }
    }
}
