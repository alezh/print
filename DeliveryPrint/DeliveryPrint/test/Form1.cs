using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sd = "";
            string s = "2013-09-26-拖鞋-J13412|柠檬绿茶居家日用旗舰店";
           string  code = s.Substring(s.LastIndexOf("|") - 6, 6);
            int kl = s.LastIndexOf("|");
            int DD = s.LastIndexOf("#");
            int ds = s.LastIndexOf("&");
            s = code.Substring(kl + 1, ds - kl - 1).Trim();
            

            //string pattern = @"^[A-Z]+\d+[A-Z]+$";//正则式子  字母开头 字母和数字重复多次 字母结束  


            //Match m = Regex.Match(this.textBox1.Text.Trim(), pattern);   // 匹配正则表达式，把this.textBox1.Text跟pattern正则对比  

            //if (!m.Success)   // 判断输入的是不是英文和数字，不是进入  
            //{
            //    MessageBox.Show("错误");
            //}
            //else
            //{
            ////    MessageBox.Show("正确");
            //}



        //    string ss = "123456789123";

        //    MessageBox.Show(ss.Length.ToString());
            //string s = "sldjflsd,sjdlfkj,sdlfkjl,";
            //textBox1.Text = s.Remove(s.LastIndexOf(','));
            //textBox1.Text = button1.GetType().ToString();

            //DeliveryPrintService.DeliveryPrintService MyService = new DeliveryPrintService.DeliveryPrintService();
            //MyService.Credentials = System.Net.CredentialCache.DefaultCredentials;
            //MyService.Url = "http://192.168.0.2:8088/daying/DeliveryPrintService.asmx";

            //string[] ll= MyService.GetHdNameAll();
            //textBox1.Text = ll[0];


            //string k = "浙江省杭州市富阳花坞南路4号";
            //if(k.EndsWith("市"))
            //{
            //    textBox1.Text = k.Substring(0, k.LastIndexOf("市"));
            //}
            //else
            //{
            //    textBox1.Text = "ffff";
            //}

            //DateTime DateTime1=Convert.ToDateTime("2011-12-8 0:20:00"); //设置要求的减的时间  

            //textBox1.Text = DateTime1.Date.ToString();
        //DateTime2 = DateTime.Now;//现在时间  
            //DateTime1 = Convert.ToDateTime("2011-12-8 0:20:00"); //设置要求的减的时间  
        //        string dateDiff = null;
        //        TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
        //        TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
        //        TimeSpan ts = ts1.Subtract(ts2);//.Duration();
          
        //        //显示时间  
        //    textBox1.Text = ts.Minutes.ToString()+"\r\n";
                    
        //            //ts.Days.ToString() + "天"
        //            //    + ts.Hours.ToString() + "小时"
        //            //    + ts.Minutes.ToString() + "分钟"
        //            //    + ts.Seconds.ToString() + "秒"; 

            //string s = "上海市";
            //textBox1.Text = s.Length.ToString(); //s.Remove(s.LastIndexOf("市"));


            //StartThread();
        }


        //private void StartThread()
        //{
        //    var wc1 = new WaitCallback(
        //        o => ExeThread());
        //    for (int i = 0; i < 20;i++ )
        //    {
        //        ThreadPool.QueueUserWorkItem(wc1,"name:"+i.ToString());
        //    }
            

        //}

        private void ExeThread()
        {
            string tt=System.Threading.Thread.CurrentThread.Name;
            
            var ac = new Action<string>(Showtext);

            this.Invoke(ac, tt);
            
        }



        private void Showtext(string ii)
        {
            textBox1.Text += ii+"\r\n";
        }
    }
}
