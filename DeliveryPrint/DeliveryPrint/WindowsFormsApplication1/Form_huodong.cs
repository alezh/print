using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace WindowsFormsApplication1
{
    public partial class Form_huodong : Form
    {
        public Form_huodong()
        {
            InitializeComponent();
            Up = new List<int>();
            Inte = new List<int>();
        }
        public DataTable hd;
        public string Seller_ID;
        public DataTable de;
        private DataTable UpIntSql;

        private string Uhdn = string.Empty;
       private string Usell = string.Empty;
       private string Ucode = string.Empty;
       private string Utitle = string.Empty;
       private string cfg = string.Empty;
       private List<int> Up; //需要更新的行
       private List<int> Inte; //需要添加的行
        DeliveryPrintService.DeliveryPrintService MyService = new DeliveryPrintService.DeliveryPrintService();
        
        public void locd()        {
            dataGridView1.DataSource = null;
            //Thread.Sleep(600);
            de = removeTable(hd);
            dataGridView1.DataSource = de; 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            save();
            //object dt=dataGridView1.DataSource;
            //DataTable dt1 = dt as DataTable;
            //foreach (DataRow row in dt1.Rows)
            //{

            //}
        }



        public DataTable removeTable(DataTable dt)
        {
            int count = dt.Columns.Count;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (dt.Columns[i].ColumnName.Trim() == "Seller_ID")
                {
                    dt.Columns.Remove(dt.Columns[i].ColumnName);
                }
            }
            return dt;
        }


        public void delsql()
        {
       
        }
        /// <summary>
        /// 右键菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0 && e.ColumnIndex>=0)
                {
                    if (dataGridView1.Rows[e.RowIndex].Selected == false)
                    {
                        dataGridView1.ClearSelection();
                        dataGridView1.Rows[e.RowIndex].Selected = true;
                    }
                    
                    if (dataGridView1.SelectedRows.Count == 1)
                    {
                        dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    }                    
                    contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
                }
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            bool O = false;
            string cfg = "2";
            DialogResult dlResult = MessageBox.Show(this,"确定要删除活动吗？", "请确认",
                   MessageBoxButtons.YesNo,
                   MessageBoxIcon.Question,
                   MessageBoxDefaultButton.Button1);
            if (dlResult == DialogResult.Yes)
            {
                string hd = (string)dataGridView1.CurrentRow.Cells[0].Value;    //接受获取选定行的主键的值
                string sell = (string)dataGridView1.CurrentRow.Cells[1].Value;
                string code = (string)dataGridView1.CurrentRow.Cells[2].Value;
                string title = (string)dataGridView1.CurrentRow.Cells[3].Value;
                string sellid = Seller_ID;
                O=MyService.Deupinhd(hd, sell, code, title, sellid, cfg, "", "", "", "");                
            }
            if (O)
            {
                dataGridView1.Rows.Remove(dataGridView1.CurrentRow);//删除焦点所在的那一行后
                MessageBox.Show("成功删除");
                if (dataGridView1.Rows.Count == 0)
                {
                    MessageBox.Show("没有活动，关闭窗口。");
                    Dispose();
                }
            }
            else
            {
                MessageBox.Show("删除已经取消。");
            }
        }

        private void Form_huodong_Load(object sender, EventArgs e)
        {
            DeliveryPrintService.myheader myheader = new DeliveryPrintService.myheader();
            myheader.username = "nmlch-2012-byken";
            MyService.myheaderValue = myheader;
            
        }
        private void UI()
        {
            if (dataGridView1.CurrentRow.Cells[0].Value != DBNull.Value)
                Uhdn = (string)dataGridView1.CurrentRow.Cells[0].Value;
            else
                return;
            if (dataGridView1.CurrentRow.Cells[1].Value != DBNull.Value)
                Usell = (string)dataGridView1.CurrentRow.Cells[1].Value;
            else
                return;
            if (dataGridView1.CurrentRow.Cells[2].Value != DBNull.Value)
                Ucode = (string)dataGridView1.CurrentRow.Cells[2].Value;
            else
                return;
            if (dataGridView1.CurrentRow.Cells[3].Value != DBNull.Value)
                Utitle = (string)dataGridView1.CurrentRow.Cells[3].Value;
            else
                return; 
        }
        private void save()
        {
            //去掉重复
            List<int> a = (from x in Up select x).Distinct().ToList();             
            //区分更新 
            List<int> b = a.Except((from x1 in Inte select x1).Distinct().ToList()).ToList();
            //区分出新增
            List<int> c = Inte.Except(b).ToList();
            foreach (int k in b)
            {
                UpHd(k);
            }
            foreach (int p in c)
            { 

            }
        }
       
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
                        
            if (dataGridView1.CurrentRow.Cells[0].Value != DBNull.Value && dataGridView1.CurrentRow.Cells[1].Value != DBNull.Value && dataGridView1.CurrentRow.Cells[2].Value != DBNull.Value && dataGridView1.CurrentRow.Cells[3].Value != DBNull.Value)
            {
                Up.Add(dataGridView1.CurrentRow.Index);//添加需要更新的行
            }
            else if (dataGridView1.CurrentRow.Cells[0].Value != DBNull.Value &&( dataGridView1.CurrentRow.Cells[1].Value == DBNull.Value || dataGridView1.CurrentRow.Cells[2].Value == DBNull.Value || dataGridView1.CurrentRow.Cells[3].Value == DBNull.Value))
            {
                Inte.Add(dataGridView1.CurrentRow.Index);//添加需要新增的行
            }
            
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                //if (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value != DBNull.Value)
                //    Inte.Add(dataGridView1.CurrentRow.Index); //添加需要新增的行
            }
        }
        /// <summary>
        /// 更新活动
        /// </summary>
        /// <param name="k"></param>
        private void UpHd(int k)
        {
            Uhdn = dataGridView1.Rows[k].Cells[0].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            locd();
        }


    }


        
    

}



//if (cfg == "3"  && dataGridView1.CurrentRow.Cells[1].Value != DBNull.Value && dataGridView1.CurrentRow.Cells[2].Value != DBNull.Value && dataGridView1.CurrentRow.Cells[3].Value != DBNull.Value)
//{
//    return;
//}
//UpIntSql = new DataTable();
//UpIntSql.Columns.Add("活动名称", Type.GetType("System.String"));
//UpIntSql.Columns.Add("店铺名称", Type.GetType("System.String"));
//UpIntSql.Columns.Add("商品货号", Type.GetType("System.String"));
//UpIntSql.Columns.Add("商品全名", Type.GetType("System.String"));
//UpIntSql.Columns.Add("Seller_ID", Type.GetType("System.String"));
//UpIntSql.Columns.Add("cfg", Type.GetType("System.String"));
//DataRow cku = UpIntSql.NewRow();
//        cku["活动名称"] = Uhdn;
//        cku["店铺名称"] = Usell;
//        cku["商品货号"] = Ucode;
//        cku["商品全名"] = Utitle;
//        cku["Seller_ID"] = Seller_ID;
//        cku["cfg"] = cfg;
//DialogResult dlResult = MessageBox.Show(this, "是否现在更新！继续填写按否", "更新确认",
//               MessageBoxButtons.YesNo,
//               MessageBoxIcon.Question,
//               MessageBoxDefaultButton.Button1);
//if (dlResult == DialogResult.Yes)
//{
//    de.Rows.Add(cku);
//}
