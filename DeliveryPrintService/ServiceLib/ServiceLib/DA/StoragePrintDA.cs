using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Data;
using ServiceLib.Util;
using System.Data.SqlClient;

namespace ServiceLib.DA
{
    public class StoragePrintDA
    {
        private const string LoginCheckSQL = "SELECT count(1) FROM ProviderUser where cPersonCode=@cPersonCode and Password=@Password  and IsDelete=0";
      

        private const string Cdlcode = "@cdlcode";
        private const string CPersonCode = "@cPersonCode";
        private const string Password = "@Password";
        private const string Delivery = "@delivery";
        private const string CsoCode = "@cSOCode";

        /// <summary>
        /// 获取打印确认发货后的汇总数据
        /// </summary>
        /// <param name="address"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public DataTable GetHuiZ(int address,DateTime dt)
        {

            if(address==1)
            {
                try
                {
                    string sql = "select Seller_nick,Pcode,Buyer_nick,a.Tid,Created,PaymentTime,a.Payment,a.Num,freight,Consignee,Phone,Tel,Provinces,City,District,ExpressWay,DeliveryNo,b.color,b.[size],Address from dbo.ProductOrderInfo a,dbo.ProductOrderDetil b where a.tid=b.tid and addTime>'" + dt.ToString() + "'  and (paccount not like '%退款%') and (paccount not like '%废弃%') and (paccount not like '%继续拍%') and a.isclose=1 order by hdname asc";


                    return SqlHelper.ExecuteDataset(SqlHelper.Double12Con, CommandType.Text, sql).Tables[0];


                }
                catch (Exception ex)
                {
                    //Logger.Error("get all part error", ex.Message);
                    return null;
                }
            }
            else
            {
                try
                {
                    string sql = "select Seller_nick,Pcode,Buyer_nick,a.Tid,Created,PaymentTime,a.Payment,a.Num,freight,Consignee,Phone,Tel,Provinces,City,District,ExpressWay,DeliveryNo,b.color,b.[size] from dbo.ProductOrderInfo a,dbo.ProductOrderDetil b where a.tid=b.tid and addTime>'" + dt.ToString() + "'  and (paccount not like '%退款%') and (paccount not like '%废弃%') and (paccount not like '%继续拍%') and a.isclose=1 order by hdname asc";


                    return SqlHelper.ExecuteDataset(SqlHelper.Double12Con, CommandType.Text, sql).Tables[0];


                }
                catch (Exception ex)
                {
                    //Logger.Error("get all part error", ex.Message);
                    return null;
                }
            }
        }

        /// <summary>
        /// 将有问题的订单的关闭状态设为1，避免自动发货程序反复发送
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="lx"></param>
        /// <returns></returns>
        public bool UpWTdan(string tid, string lx)
        {
            try
            {
                string sql = string.Format(@"update dbo.ProductOrderInfo set isclose=1,paccount=paccount+'{0}' where tid='{1}'", lx, tid);


                return SqlHelper.ExecuteNonQuery(SqlHelper.Double12Con, CommandType.Text, sql) > 0 ? true : false;


            }
            catch (Exception ex)
            {
                Logger.Error("update wenti dan error", ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 获得不能确认发货的单号等信息
        /// </summary>
        /// <param name="dtime"></param>
        /// <returns></returns>
         public DataTable GetWTDan(DateTime dtime)
         {
             try
             {
                 string sql = "select Seller_nick,hdname,tid,DeliveryNo,Paccount,Ptime from dbo.ProductOrderInfo  with(nolock) where isprint=1 and isclose=0 and addTime>='"+dtime.ToString()+"'  order by DeliveryNo";


                 return SqlHelper.ExecuteDataset(SqlHelper.Double12Con, CommandType.Text, sql).Tables[0];


             }
             catch (Exception ex)
             {
                 //Logger.Error("get all part error", ex.Message);
                 return null;
             }
         }

        /// <summary>
        /// 返回打印相关数目
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<int> GetTNum(DateTime dt)
        {
            List<int> ll=new List<int>();

            try
            {
                //
                string sqlAllnum = "select COUNT(*) from dbo.ProductOrderInfo with(nolock) where isread=0 and isprint=0 and addTime>='" + dt.ToString() + "'";
                object objAllnum = SqlHelper.ExecuteScalar(SqlHelper.Double12Con, CommandType.Text, sqlAllnum);
                if (objAllnum != null && int.Parse(objAllnum.ToString()) > 0)
                {
                    ll.Add(int.Parse(objAllnum.ToString()));
                }
                else
                {
                    ll.Add(0);
                }

                string sqlAllnum2 = "select COUNT(*) from dbo.ProductOrderInfo with(nolock) where isread=1 and isprint=0 and addTime>='" + dt.ToString() + "'";
                object objAllnum2 = SqlHelper.ExecuteScalar(SqlHelper.Double12Con, CommandType.Text, sqlAllnum2);
                if (objAllnum2 != null && int.Parse(objAllnum2.ToString()) > 0)
                {
                    ll.Add(int.Parse(objAllnum2.ToString()));
                }
                else
                {
                    ll.Add(0);
                }

                string sqlAllnum3 = "select count(*) from dbo.ProductOrderInfo with(nolock) where isprint=1 and isclose=1 and addTime>='" + dt.ToString() + "'";
                object objAllnum3 = SqlHelper.ExecuteScalar(SqlHelper.Double12Con, CommandType.Text, sqlAllnum3);
                if (objAllnum3 != null && int.Parse(objAllnum3.ToString()) > 0)
                {
                    ll.Add(int.Parse(objAllnum3.ToString()));
                }
                else
                {
                    ll.Add(0);
                }


                string sqlAllnum4 = "select count(*)  from dbo.ProductOrderInfo with(nolock) where isprint=1 and isclose=0 and addTime>='" + dt.ToString() + "'";
                object objAllnum4 = SqlHelper.ExecuteScalar(SqlHelper.Double12Con, CommandType.Text, sqlAllnum4);
                if (objAllnum4 != null && int.Parse(objAllnum4.ToString()) > 0)
                {
                    ll.Add(int.Parse(objAllnum4.ToString()));
                }
                else
                {
                    ll.Add(0);
                }

            }
            catch (Exception ex)
            {
                //Logger.Error("update ExpressWay error", ex.Message);
                return null;
            }
            return ll;
        }

        public bool UpKdFs(string tid,string kdfs)
        {
            try
            {
                string sql = string.Format(@"update dbo.ProductOrderInfo set ExpressWay='{0}' where Tid ='{1}' and Isclose=0;",kdfs,tid);
                

                return SqlHelper.ExecuteNonQuery(SqlHelper.Double12Con, CommandType.Text, sql) > 0 ? true : false;


            }
            catch (Exception ex)
            {
                Logger.Error("update ExpressWay error", ex.Message);
                return false;
            }
        }


        public DataTable GetNoKdTable(string account,int count)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                sb.Append(
                    @"set transaction isolation level repeatable read
                    begin tran st2 
                    select top (@oncenum)  id,Tid,ExpressWay,Provinces,City,District,Address,Consignee,Buyer_nick,Num,Pcode,Phone,Tel,PaymentTime  into #mmm2 from double12.dbo.ProductOrderInfo where (((Paccount is null) or (Paccount='')) and (IsRead=0 or (IsRead is null))  and (IsPrint=0 or (IsPrint is null))  and ExpressWay='没有') or (Paccount=@paccount and (IsPrint=0 or (IsPrint is null)) and IsRead=1  and ExpressWay='没有') order by Num,City;
                    update double12.dbo.ProductOrderInfo  set Paccount=@paccount,IsRead=1 from #mmm2 where  double12.dbo.ProductOrderInfo.id=#mmm2.id;
                    select * from #mmm2;
                    drop table #mmm2;
                    commit tran st2
                        ");

                SqlParameter[] parms = new SqlParameter[]
                { 
                    new SqlParameter("@paccount", SqlDbType.NVarChar),
                    new SqlParameter("@oncenum",SqlDbType.Int),
                    
                };
                parms[0].Value = account;
                parms[1].Value = count;

                Logger.Info("no kuaidi", sb.ToString() + "nokuaidi@" + DateTime.Now.ToString());

                return SqlHelper.ExecuteDataset(SqlHelper.Double12Con, CommandType.Text, sb.ToString(), parms).Tables[0];
            }
            catch (Exception ex)
            {
                Logger.Error("GetNoKdTable", ex.Message);
                return null;
            }

        }


        public bool RealseLock(string ll)
        {
            try
            {
                string sql =string.Format(@"update dbo.ProductOrderInfo set Paccount='',Isread=0 where Tid in ({0});",ll);
                //SqlParameter[] parms = new SqlParameter[]
                //{ 
                //    new SqlParameter("@orders", SqlDbType.NText),
                   
                //};
                //parms[0].Value = ll;

                return SqlHelper.ExecuteNonQuery(SqlHelper.Double12Con, CommandType.Text, sql)>0?true:false;


            }
            catch (Exception ex)
            {
                Logger.Error("update lock error", ex.Message);
                return false;
            }
        }


        public DataTable GetPrintGroupBy()
        {
            try
            {
                string sql = "select count(*) as 数量,SUM(ISNULL(cfprint,0)) as 重打数量,ISNULL(Paccount,'未打印') as 操作,hdname as 活动名称 from ProductOrderInfo with(nolock) group by Paccount,hdname";
                

                return SqlHelper.ExecuteDataset(SqlHelper.Double12Con, CommandType.Text, sql).Tables[0];


            }
            catch (Exception ex)
            {
                Logger.Error("get all part error", ex.Message);
                return null;
            }
        }

        #region 获得所有已经打印的订单

        public DataTable GetPrintPart(int fromnum, string orderunm, string kddh, string mjid, string caozuo, DateTime shijian,string hdname,DateTime ddtime,bool pt,bool dt)
        {
            try
            {
                string sql = "select top 300 id as 编号,Seller_nick as 店铺, Buyer_nick as 买家,Tid as 订单号,Provinces as 省份,Paccount as 打印操作员,Pcode as 产品代码,Stime as 打印开始时间,Ptime as 打印结束时间,DeliveryNo as 快递单号,hdname as 活动名称,cfprint as 重复打印数 from ProductOrderInfo with(nolock) where   id >=@fromnum";
                if(!orderunm.Equals(""))
                {
                    sql += " and Tid=@ordernum";
                }
                if(!kddh.Equals(""))
                {
                    sql += " and DeliveryNo=@kddh";
                }
                if(!mjid.Equals(""))
                {
                    sql += " and Buyer_nick=@mjid";
                }
                if(!caozuo.Equals(""))
                {
                    sql += " and Paccount=@caozuo";
                }
                if(pt)
                {
                    sql += " and Ptime>=@shijian";
                }
                if(!hdname.Equals(""))
                {
                    sql += " and hdname=@hdname";
                }
                if(dt)
                {
                    sql += " and Created>=@ddtime";
                }
                SqlParameter[] parms = new SqlParameter[]
                { 
                    new SqlParameter("@fromnum", SqlDbType.Int),
                    new SqlParameter("@ordernum",SqlDbType.NVarChar),
                    new SqlParameter("@kddh",SqlDbType.NVarChar),
                    new SqlParameter("@mjid",SqlDbType.NVarChar),
                    new SqlParameter("@caozuo",SqlDbType.NVarChar),
                    new SqlParameter("@shijian",SqlDbType.DateTime),
                    new SqlParameter("@hdname",SqlDbType.NVarChar),
                    new SqlParameter("@ddtime",SqlDbType.DateTime), 
                };
                parms[0].Value =fromnum;
                parms[1].Value = orderunm;
                parms[2].Value = kddh;
                parms[3].Value = mjid;
                parms[4].Value = caozuo;
                parms[5].Value = shijian;
                parms[6].Value = hdname;
                parms[7].Value = ddtime;
         

                return SqlHelper.ExecuteDataset(SqlHelper.Double12Con, CommandType.Text, sql,parms).Tables[0];
                

            }
            catch (Exception ex)
            {
                Logger.Error("get all part error", ex.Message);
                return null;
            }
        }

        #endregion




        #region 获得打印数目参考

        public List<int> GetPrintNum(string username,string hdname)
        {
            List<int> ll = new List<int>();
            try
            {
                string sql_allnum = "select count(*) from ProductOrderInfo with(nolock) where  hdname='" + hdname + "'";
                object obj_allnum = SqlHelper.ExecuteScalar(SqlHelper.Double12Con, CommandType.Text, sql_allnum);
                if (obj_allnum != null && int.Parse(obj_allnum.ToString()) > 0)
                {
                    ll.Add(int.Parse(obj_allnum.ToString()));
                }
                else
                {
                    ll.Add(0);
                }
                //
                string sqlOknum = "select count(*) from ProductOrderInfo with(nolock) where  hdname='" + hdname + "' and ISNULL(IsPrint,0)=1";
                object objOknum = SqlHelper.ExecuteScalar(SqlHelper.Double12Con, CommandType.Text, sqlOknum);
                if (objOknum != null && int.Parse(objOknum.ToString()) > 0)
                {
                    ll.Add(int.Parse(objOknum.ToString()));
                }
                else
                {
                    ll.Add(0);
                }
                //
                string sql_mynum = "select count(*) from ProductOrderInfo with(nolock) where Paccount='" + username + "' and ISNULL(IsPrint,0)=1 and hdname='" + hdname + "'";
                object obj_mynum = SqlHelper.ExecuteScalar(SqlHelper.Double12Con, CommandType.Text, sql_mynum);
                if (obj_mynum!=null && int.Parse(obj_mynum.ToString())>0)
                {
                    ll.Add(int.Parse(obj_mynum.ToString()));
                }
                else
                {
                    ll.Add(0);
                }
                return ll;
            }
            catch (Exception ex)
            {
                Logger.Error("get print num error", ex.Message);
                return null;

            }
            
        }
        #endregion


        #region 获得所有活动名称

        public DataTable GetHdNameAll(string Seller_ID)
        {
            DataTable dt = new DataTable();
            //List<string > ll=new List<string>();
            try
            {
                string sql = "SELECT hdname,seller,pcode,title,Seller_ID FROM HuoDong with(nolock) where Seller_ID='" + Seller_ID + "'";
               
                dt= SqlHelper.ExecuteDataset(SqlHelper.Double12Con, CommandType.Text, sql).Tables[0];               
                
            }
            catch (Exception ex)
            {
                Logger.Error("get all hdname error", ex.Message);
                
            }
            return dt;
        }
        #endregion


        #region 获取DispatchList记录

        /// <summary>
        /// 获取DispatchList记录
        /// </summary>
        /// <param name="dverifysystime"></param>
        /// <returns></returns>
        public DataTable GetInvoice(string account,string ShippingMethods, string orderNo, string category, string SalesType, int count,string sellnick,string hdname)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                sb.Append(
                    @"set transaction isolation level repeatable read
                    begin tran st 

                    --drop table #mmm;
                    select top (@oncenum)  id,Buyer_nick,Num,Created,Tid as OrderNo,Pcode,DeliveryNo   into #mmm from double12.dbo.ProductOrderInfo where (((Paccount is null) or (Paccount='')) and (IsRead=0 or (IsRead is null))  and (IsPrint=0 or (IsPrint is null)) and ExpressWay=@kdlx and hdname=@hd and ExpressWay!='没有') or (Paccount=@paccount and (IsPrint=0 or (IsPrint is null)) and IsRead=1 and ExpressWay=@kdlx and hdname=@hd and ExpressWay!='没有') order by Num,City;
                    update double12.dbo.ProductOrderInfo  set Paccount=@paccount,IsRead=1,Stime=GETDATE() from #mmm where  double12.dbo.ProductOrderInfo.id=#mmm.id;
                    select * from #mmm;
                    drop table #mmm;

                    commit tran st
                        ");

                SqlParameter[] parms = new SqlParameter[]
                { 
                    new SqlParameter("@paccount", SqlDbType.NVarChar),
                    new SqlParameter("@oncenum",SqlDbType.Int),
                    new SqlParameter("@kdlx",SqlDbType.NVarChar), 
                    new SqlParameter("@hd",SqlDbType.NVarChar)
                    
                };
                parms[0].Value = account;
                parms[1].Value = count;
                parms[2].Value = ShippingMethods;
                parms[3].Value =hdname;
                Logger.Info("hwnag", sb.ToString() + "hwang1@" + DateTime.Now.ToString());

                return SqlHelper.ExecuteDataset(SqlHelper.Double12Con, CommandType.Text, sb.ToString(),parms).Tables[0];
            }
            catch (Exception ex)
            {
                Logger.Error("GetInvoice", ex.Message);
                return null;
            }
        }
        #endregion


      

        #region 检查订单是不是存在

        /// <summary>
        /// 检查订单是不是存在
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>

        public bool CheckMergerOrder(string orderNo)
        {
            try
            {
                string sql = "select count(1) from ProductOrderInfo with(nolock) where Tid='" + orderNo + "'";
                object obj = SqlHelper.ExecuteScalar(SqlHelper.Double12Con, CommandType.Text, sql);
                if (obj.ToString() == "0")
                    return false;
                else
                    return true;
                   
            }
            catch (Exception ex)
            {
                Logger.Error("Check ProductOrderInfo", ex.Message);
                return false;
            }
        }

        #endregion

        //#region 登录
        ///// <summary>
        ///// 登录
        ///// </summary>
        ///// <param name="userName"></param>
        ///// <param name="passWord"></param>
        ///// <param name="cDepCode"></param>
        ///// <returns></returns>
        //public bool LoginCheck(string userName, string passWord)
        //{
        //    try
        //    {
        //        SqlParameter[] parms = new SqlParameter[]
        //        { 
        //            new SqlParameter(CPersonCode, SqlDbType.VarChar),
        //            new SqlParameter(Password,SqlDbType.NVarChar)
        //        };
        //        parms[0].Value = userName;
        //        parms[1].Value = passWord;

        //        string result = SqlHelper.ExecuteScalar(SqlHelper.Double12Con, CommandType.Text, LoginCheckSQL, parms).ToString();
        //        if (result == "0")
        //            return false;
        //        else
        //            return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error("LoginCheck", ex.Message);
        //        return false;
        //    }
        //}

        //#endregion



    }
}
