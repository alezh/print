using System;
using System.Linq;
using System.Text;
using System.Data;
using ServiceLib.Util;
using System.Data.SqlClient;
using ServiceLib.Entity;
using System.Data.Linq;
using System.Collections.Generic;

namespace ServiceLib.DA
{
    class DeliveryPrintDA
    {
        //private const string LoginCheckSQL = "SELECT * FROM ProviderUser with(nolock) where cPersonCode=@cPersonCode and Password=@Password  and IsDelete=0";

        private const string LoginCheckSQL = "SELECT * FROM ProviderUser with(nolock) where Account=@Account and Password=@Password  and IsDelete=0";

        private const string CPersonCode = "@cPersonCode";
        private const string Password = "@Password";
        const string Account = "@Account";
        //private const string Delivery = "@delivery";
        //private const string Cdlcode = "@cdlcode";
        //private const string Ifquantity = "@iFQuantity";
        //private const string Iquantity = "@iquantity";
        //private const string Cfree1 = "@cfree1";
        //private const string Cfree2 = "@cfree2";
        //private const string Cbarcode = "@cBarCode";
        //private const string Cposition = "@cposition";
        //private const string CSoCode = "@cSoCode";
        //private const string CBatch="@cBatch";
        public const string DeliveryNo = "@DeliveryNo";
        public const string OderNo = "@OrderNo";
        private DataClasses1DataContext dataContext;

        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kddh"></param>
        /// <returns></returns>

        public DataTable GetByDeliveryNo(string kddh)
        {
            string sql = string.Format("select * from ProductOrderInfo with(nolock) where DeliveryNo='{0}'", kddh);
            return  SqlHelper.ExecuteDataset(SqlHelper.Double12Con, CommandType.Text, sql).Tables[0];
            
        }

        /// <summary>
        /// 查看发货数量  alezh
        /// </summary>
        /// <param name="sellename"></param>
        /// <param name="code"></param>
        /// <param name="hdname"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public DataTable Getkucun(string sellename,  string hdname, string time, string isclose)
        {
            string sql = string.Format(@"select productCode,b.Num,b.color,b.[size]  into ##maic3 from dbo.ProductOrderInfo a,dbo.ProductOrderDetil b where a.tid=b.tid and 
                                         addTime>'{0}' and seller_nick='{1}' 
                                         and  hdname='{2}' and 
                                         (paccount not like '%退款%') and (paccount not like '%废弃%') and (paccount not like '%代发%') and a.isclose={3} order by hdname asc",
                                                                                                                                                       time,
                                                                                                                                                       sellename,                                                                                                                                                       
                                                                                                                                                       hdname,
                                                                                                                                                       isclose);

            sql += "  select  productCode as '货号' ,sum(Num)as '数量',color as '颜色',[size] as '尺码' from ##maic3 where color=color and size=size GROUP BY productCode,color,size drop table ##maic3";
            

            return SqlHelper.ExecuteDataset(SqlHelper.Double12Con, CommandType.Text, sql).Tables[0];
        }

        //public DataTable G(string sellename, string hdname, string time, string isclose)
        //{
        //    dataContext = new DataClasses1DataContext();
        //    var G = from j in dataContext.ProductOrderInfo
        //            join k in dataContext.ProductOrderDetil
        //            on j.Tid equals k.tid
        //            where j.addTime > Convert.ToDateTime(time) && j.Seller_nick == sellename && j.hdname == hdname &&
        //            j.Paccount.Contains("退款") && j.Paccount.Contains("废弃") && j.Paccount.Contains("代发") && j.IsClose.Value == true
        //            select j;
        //}


        /// <summary>
        /// 订单数量查询 alezh
        /// </summary>
        /// <param name="sellename"></param>
        /// <param name="hdname"></param>
        /// <param name="time"></param>
        /// <param name="isclose"></param>
        /// <returns></returns>
        public string[] GetorderNum(string sellename, string hdname, string time)
        {
            //List<ProductOrderInfo> pd = new List<ProductOrderInfo>();
            //List<string> st = new List<string>();
            string[] sr;
            int isprint=0;//打印数量
            int noprint=0;//未打印数量
            int outNum=0;//已经出库
            int count = 0;//总数
            dataContext = new DataClasses1DataContext();
            var td = from x in dataContext.ProductOrderInfo
                                       where x.addTime > Convert.ToDateTime(time) && x.hdname == hdname &&
                                           x.Seller_nick == sellename 
                                       select x;
            foreach (ProductOrderInfo ds in td)
            {
                count++;
                if (ds.IsPrint.Value)
                    isprint++;
                else
                    noprint++;
                if (ds.rongyu3!=null)
                    outNum++;
            }
            sr = new string[] { isprint.ToString(), noprint.ToString(), outNum.ToString(), count.ToString() };
            //st.Add(isprint.ToString());
            //st.Add(noprint.ToString());
            //st.Add(outNum.ToString());
            //st.Add(count.ToString());
            //dataContext.Dispose();
            return sr;
        }


        public DataTable kucun(string code)
        {
            string sql = string.Format(@"select * from TBRobot.dbo.NMLCH_vUseNum with(nolock) where Stockcode='{0}'", code);
            return SqlHelper.ExecuteDataset(SqlHelper.Double12Con, CommandType.Text, sql).Tables[0];
            //DataTable de=new DataTable();
            //de.Columns.Add("Stockcode", Type.GetType("System.String"));
            //de.Columns.Add("Color", Type.GetType("System.String"));
            //de.Columns.Add("Size", Type.GetType("System.String"));
            //de.Columns.Add("Amount", Type.GetType("System.String"));
            //dataContext = new DataClasses1DataContext();
            //var kucun = from x in dataContext.NMLCH_vUseNum                        
            //            where x.Stockcode == code && x.Size == size && x.Color == color.fie
            //            select new
            //            {
            //                x.Stockcode,
            //                x.Color,
            //                x.Size,
            //                x.Amount
            //            };
            //foreach (var c in kucun)
            //{
            //    DataRow cku = de.NewRow();
            //    cku["Stockcode"] = c.Stockcode;
            //    cku["Color"] = c.Color;
            //    cku["Size"] = c.Size;
            //    cku["Amount"] = c.Amount.ToString();
            //    de.Rows.Add(cku);
            //}           
        }



        //double12 添加发货单号
        #region 添加发货单号

        /// <summary>
        /// 添加发货单表体
        /// </summary>
        /// <param name="cDLCode">发货单单号</param>
        /// <returns></returns>

        public bool InsertDispatchLists(string cDLCode,string ddh,bool cf)
        {
            SqlConnection conn = new SqlConnection(SqlHelper.Double12Con);
            conn.Open();
            SqlTransaction trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                SqlParameter[] parms = new SqlParameter[]
                { 
                    new SqlParameter("@fhdh", SqlDbType.NVarChar),
                    new SqlParameter("@ddh",SqlDbType.NVarChar)
                };
                parms[0].Value = cDLCode;
                parms[1].Value = ddh;
                
                StringBuilder sb = new StringBuilder();
                if(cf)
                {
                    sb.Append(
                  @"update ProductOrderInfo  set IsPrint=1,Ptime=GETDATE(),DeliveryNo=@fhdh,cfprint=ISNULL(cfprint,0)+1  where Tid=@ddh;");
                    
                }
                else
                {
                    sb.Append(
                     @"update ProductOrderInfo  set IsPrint=1,Ptime=GETDATE(),DeliveryNo=@fhdh  where Tid=@ddh;");
                }
                
       
                SqlHelper.ExecuteNonQuery(conn, trans, CommandType.Text, sb.ToString(), parms);

                trans.Commit();
               
                return true;
                
            }
            catch (Exception ex)
            {
                trans.Rollback();
                Logger.Error("GetInfo", ex.Message);
                //string sql = string.Format("insert into TBRobot.dbo.StorageError (deliveryNo,orderNo,cDLCode,action,errlog) values ('{0}','{1}','{2}','{3}','{4}')", "", "", cDLCode, "InsertDispatchLists", ex.Message.Replace("'", ""));

                //SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        #endregion 

        #region 登录

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>

        /// <returns></returns>
        internal DataTable LoginCheck(string userName, string passWord)
        {
            try
            {
                SqlParameter[] parms = new SqlParameter[]
                { 
                    new SqlParameter(Account, SqlDbType.VarChar),
                    new SqlParameter(Password,SqlDbType.NVarChar)
                };
                parms[0].Value = userName;
                parms[1].Value = passWord;

                DataTable result = SqlHelper.ExecuteDataset(SqlHelper.Double12Con, CommandType.Text, LoginCheckSQL, parms).Tables[0];
                if (result.Rows.Count>0)
                    return result;
                else
                    return null;
            }
            catch (Exception ex)
            {
                Logger.Error("LoginCheck", ex.Message);
                return null;
            }
        }

        #endregion

        //public bool reg(string user, string pass, string session)
        //{
        //    string onesql = "select * from dbo.ProviderUser where Account='" + user + "'";
        //}

        #region 激活

        public bool reglock(string user, string pass, string session)
        {
            string sql = "select * from dbo.ProviderUser where Account='" + user + "'";
            string sql2 = string.Format(@"insert dbo.ProviderUser(Account,[Password],UserRole,Seller_ID,cPersonCode,Seller_Iid)values
                                                ('{0}','{1}','0','00','01010','00')
                                                  declare @id int set @id=SCOPE_IDENTITY()
                                                                       update  dbo.ProviderUser set Seller_ID=id, Seller_Iid=id
                                                                       where id=@id", user, pass);
            if (SqlHelper.ExecuteDataset(SqlHelper.Double12Con, CommandType.Text, sql).Tables[0].Rows == null)
            {
                return SqlHelper.ExecuteNonQuery(SqlHelper.Double12Con, CommandType.Text, sql2) > 0 ? true : false;
            }
            else
                return false;
            string sess = string.Format(@"insert dbo.TaoBaoShopAPI(ShopName,session_id,app_key,aap_secret)values
                                                 ('{0}','{1}','12008063','b3c1dd32f28be83cf7dcf75873650005')",user,session);
            return SqlHelper.ExecuteNonQuery(SqlHelper.Double12Con, CommandType.Text, sess) > 0 ? true : false;
        }
        #endregion

        public bool orders(string tid, string Provinces, string City, string District, string Address, string Consignee, string Phone, string flod)
        {
            string ss = string.Format(@"update  dbo.ProductOrderInfo set Provinces='{0}',City='{1}',District='{2}',[Address]='{3}',Consignee='{4}',Phone='{5}'  where tid='{6}' and IsPrint=0 ", Provinces, City, District, Address, Consignee, Phone, tid);
            if (flod == "1")
            {
                string sql = string.Format(@"update dbo.ProductOrderInfo set isclose=1,IsRead=1,IsPrint=1,paccount='[后台-废弃]' where tid='{0}'", tid);

                return SqlHelper.ExecuteNonQuery(SqlHelper.Double12Con, CommandType.Text, sql) > 0 ? true : false;
            }
            else if (flod == "2")
            {
                return SqlHelper.ExecuteNonQuery(SqlHelper.Double12Con, CommandType.Text, ss) > 0 ? true : false;
            }
            return false;

        }

        /// <summary>
        /// 添加更新删除活动信息  - alezh
        /// </summary>
        /// <param name="hdname"></param>
        /// <param name="seller"></param>
        /// <param name="code"></param>
        /// <param name="title"></param>
        /// <param name="Seller_ID"></param>
        /// <param name="cfg">1更新 2删除 3新增</param>
        /// <returns></returns>
        public bool Deupinhd(string hdname, string seller, string code, string title, string Seller_ID, string cfg, string oldhdname, string oldseller, string odlcode, string oldtitle)
        {
            string Sql = string.Empty;
            if(cfg=="1")
                Sql = string.Format(@"update dbo.HuoDong set hdname='{0}',seller='{1}',pcode='{2}',title='{3}',Seller_ID={4} where hdname='{5}' and seller='{6}' and pcode='{7}' and title='{8}'",
                hdname, seller, code, title, Seller_ID, oldhdname, oldseller, odlcode, oldtitle);
            else if(cfg=="3")
                Sql = string.Format(@"insert into dbo.HuoDong(hdname,seller,pcode,title,Seller_ID)values('{0}','{1}','{2}','{3}',{4})", hdname, seller, code, title, Seller_ID);
            else if(cfg=="2")
                Sql = string.Format(@"delete dbo.HuoDong where hdname='{0}'and seller='{1}'and Seller_ID='{2}' and pcode='{3}'and title='{4}'", hdname, seller, Seller_ID, code, title);
            try
            {
                return SqlHelper.ExecuteNonQuery(SqlHelper.Double12Con, CommandType.Text, Sql) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                Logger.Error("Deupinhd", ex.Message);
                return false;
            }
        }
      

       



        #region 检查快递单号是否已经存在
        /// <summary>
        /// 检查快递单号是否已经存在
        /// </summary>
        /// <param name="deliveryNo">快递单号</param>
        /// <returns></returns>
        public bool CheckDeliveryNo(string deliveryNo)
        {
            string sql = string.Format("select count(1) from ProductOrderInfo with(nolock) where DeliveryNo='{0}'", deliveryNo);
              DataSet ds = SqlHelper.ExecuteDataset(SqlHelper.Double12Con, CommandType.Text,sql);
            bool b = false;
              if (ds.Tables[0].Rows.Count > 0)
              {
                if(Convert.ToInt32( ds.Tables[0].Rows[0][0].ToString())>0)
                {
                    b = true;
                }
              }
              
            return b;
        }
        #endregion 


      
        public string GetOrderInfo2(string id)
        {

            var sql =
                string.Format(
                    @"SELECT  DeliveryNo   FROM    ProductOrderInfo with(nolock)  WHERE   Tid = '{0}'", id);
      

            try
            {

                return SqlHelper.ExecuteScalar(SqlHelper.Double12Con, CommandType.Text, sql).ToString();
        
            }
            catch (Exception ex)
            {
                Logger.Error("GetOrderInfo 2", ex.Message);
                return null;
            }

        }



        //double12
        #region 获取订单表头

        /// <summary>
        /// 获取订单表头
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataSet GetOrderInfo(string id)
        {

            var sql =
                string.Format(
                    @"SELECT  *   FROM    ProductOrderInfo with(nolock)  WHERE   Tid = '{0}'", id);


            try
            {

                DataSet ds = SqlHelper.ExecuteDataset(SqlHelper.Double12Con, CommandType.Text, sql);
                if (ds.Tables[0].Rows.Count <= 0)
                {
                    Logger.Info("hwang", id + "没有任何行" + "@" + DateTime.Now);
                }
                else
                {
                    Logger.Info("hwang", "存在" + id + "@" + DateTime.Now);
                }
                return ds;
            }
            catch (Exception ex)
            {
                Logger.Error("GetOrderInfo", ex.Message);
                return null;
            }

        }

        #endregion


        public DataSet GetOrderInfoPart(string id)
        {

            var sql =
                string.Format(
                    @"SELECT  b.Num,Consignee,Phone,Tel,Provinces,City,District,Address,PostBack,freight,b.productCode as Pcode,Color,Size   FROM    ProductOrderInfo a,dbo.ProductOrderDetil b  with(nolock) WHERE   a.Tid ='{0}' and b.Tid = '{1}'", id, id);

            //2013-04-08 a.num 改为b.num
            try
            {

                DataSet ds = SqlHelper.ExecuteDataset(SqlHelper.Double12Con, CommandType.Text, sql);
                if (ds.Tables[0].Rows.Count <= 0)
                {
                    Logger.Info("hwang", id + "没有任何行" + "@" + DateTime.Now);
                }
                else
                {
                    Logger.Info("hwang", "存在" + id + "@" + DateTime.Now);
                }
                return ds;
            }
            catch (Exception ex)
            {
                Logger.Error("GetOrderInfo", ex.Message);
                return null;
            }

        }
        





    }
}
