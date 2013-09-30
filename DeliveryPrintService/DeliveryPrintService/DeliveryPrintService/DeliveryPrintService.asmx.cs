using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Data;
using ServiceLib.BC;
using ServiceLib.Util;
using System.Configuration;
using ServiceLib.Entity;
using System.Web.Services.Protocols;


namespace DeliveryPrintService
{

    // define a soap header by deriving from the soapheader base class.
    public class myheader : SoapHeader
    {
        public string username;
        public string password;
    }
    /// <summary>
    /// Service1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class DeliveryPrintService : System.Web.Services.WebService
    {

        public myheader NSoapVar;


        //double12
        [WebMethod(Description = "获取没有快递方式的订单")]
        [SoapHeader("NSoapVar")]
        public DataTable GetNoKdTable(string account, int count)
        {
            try
            {
                if (!CheckSoapHead())
                {
                    return null;
                }
                return new StoragePrintBC().GetNoKdTable(account, count);
            }
            catch (Exception)
            {
                return null;
                //throw;
            }
           
        }

        //double12
        [WebMethod(Description = "更新快递方式")]
        [SoapHeader("NSoapVar")]
        public bool UpKdFs(string tid, string kdfs)
        {
            try
            {
                if (!CheckSoapHead())
                {
                    return false;
                }
                return new StoragePrintBC().UpKdFs(tid, kdfs);

            }
            catch (Exception)
            {
                return false;
                //throw;
            }


        }


        ////double12
        //[WebMethod(Description = "获取订单明细颜色尺码信息")]
        //public List<string >  GetColorSize(string id)
        //{

        //    return new DeliveryPrintBC().GetColorSize(id);
        //}

        //double12
        [WebMethod(Description = "用户解锁")]
        [SoapHeader("NSoapVar")]
        public bool RealseLockuser(string ll)
        {
            try
            {
                if (!CheckSoapHead())
                {
                    return false;
                }
                //string strShippingMethods = StringTools.DecodingForString(ShippingMethods);
                //return new StoragePrintBC().RealseLock(ll);

                bool ok = false;

                string r = StringTools.DecodingForString(ll);


                if (Application["DeliveryPrintService" + r] != null)
                {
                    Application["DeliveryPrintService" + r] = null;
                    ok = true;
                }


                return ok;
            }
            catch (Exception)
            {
                return false;
                //throw;
            }

        }

        //double12
        [WebMethod(Description = "数据解锁")]
        [SoapHeader("NSoapVar")]
        public bool RealseLock(string ll)
        {
            try
            {
                if (!CheckSoapHead())
                {
                    return false;
                }
                //string strShippingMethods = StringTools.DecodingForString(ShippingMethods);
                return new StoragePrintBC().RealseLock(ll);
            }
            catch (Exception)
            {
                 return false;
                //throw;
            }

            //bool ok = false;

            //string r = StringTools.DecodingForString(ll);


            //if (Application["DeliveryPrintService" + r] != null)
            //{
            //    Application["DeliveryPrintService" + r] = null;
            //    ok = true;
            //}


            //return ok;

        }


        //double12
        [WebMethod(Description = "返回打印人员的打印数量，供参考")]
        [SoapHeader("NSoapVar")]
        public DataTable GetPrintGroupBy()
        {
            try
            {
                if (!CheckSoapHead())
                {
                    return null;
                }
                //string strShippingMethods = StringTools.DecodingForString(ShippingMethods);
                return new StoragePrintBC().GetPrintGroupBy();
            }
            catch (Exception)
            {
                return null;
                //throw;
            }
        }


        //double12
        [WebMethod(Description = "返回部分已经打印的订单，供参考")]
        [SoapHeader("NSoapVar")]
        public DataTable GetPrintPart(int from,string orderunm,string kddh,string mjid,string caozuo,DateTime shijian,string hdname,DateTime ddtime,bool pt,bool dt)
        {
            try
            {
                if (!CheckSoapHead())
                {
                    return null;
                }
                //string strShippingMethods = StringTools.DecodingForString(ShippingMethods);
                return new StoragePrintBC().GetPrintPart(from, orderunm, kddh, mjid, caozuo, shijian, hdname, ddtime, pt, dt);
            }
            catch (Exception)
            {
                return null;
                //throw;
            }
        }

        //double12
        [WebMethod(Description = "获取订单表头，只返回快递单号")]
        [SoapHeader("NSoapVar")]
        public string GetOrderInfo2(string id)
        {
            try
            {
                if (!CheckSoapHead())
                {
                    return null;
                }
                return new DeliveryPrintBC().GetOrderInfo2(id);
            }
            catch (Exception)
            {
                return null;
                //throw;
            }
        }

        //double12
        [WebMethod(Description = "获取订单表头部分信息")]
        [SoapHeader("NSoapVar")]
        public DataSet GetOrderInfoPart(string id)
        {
            try
            {
                if (!CheckSoapHead())
                {
                    return null;
                }
                return new DeliveryPrintBC().GetOrderInfoPart(id);
            }
            catch (Exception)
            {
                return null;
                //throw;
            }
        }

        //double12
       [WebMethod(Description = "获取订单表头")]
        [SoapHeader("NSoapVar")]
        public DataSet GetOrderInfo(string id)
        {
            try
            {
                if (!CheckSoapHead())
                {
                    return null;
                }
                return new DeliveryPrintBC().GetOrderInfo(id);
            }
            catch (Exception)
            {
                return null;
                //throw;
            }
        }

        // 新增功能 alezh
       [WebMethod(Description = "获取已发货数量颜色尺码")]
       [SoapHeader("NSoapVar")]
       public DataTable Getkucun(string sellename, string hdname, string time, string isclose)
       {
           try
           {
               if (!CheckSoapHead())
               {
                   return null;
               }
               return new DeliveryPrintBC().Getchushou(sellename, hdname, time, isclose);
           }
           catch (Exception)
           {
               return null;
               //throw;
           }
       }

       // 新增功能 alezh
       [WebMethod(Description = "获取ERP库存")]
       [SoapHeader("NSoapVar")]
       public DataTable kucun(string code)
       {
           try
           {
               if (!CheckSoapHead())
               {
                   return null;
               }
               return new DeliveryPrintBC().kucun(code);
           }
           catch (Exception)
           {
               return null;
               //throw;
           }
       }


       // 新增功能 alezh
       [WebMethod(Description = "获取打印数量信息")]
       [SoapHeader("NSoapVar")]
       public string[] GetorderNum(string sellename, string hdname, string time)
       {
           try
           {
               if (!CheckSoapHead())
               {
                   return null;
               }
               return new DeliveryPrintBC().GetNum(sellename, hdname, time);
           }
           catch (Exception)
           {
               return null;
               //throw;
           }
       }

       //double12
       [WebMethod(Description = "返回需要打印的订单表头列表<br>发运方式列表id:ShippingMethods<br>订单号:orderNo")]
       [SoapHeader("NSoapVar")]
       public DataTable GetOrderDeliveryPrintStateNew(string account, string ShippingMethods, string orderNo, string category, string salestype, int count, string sellernick,string hdname)
       {
           try
           {
               if (!CheckSoapHead())
               {
                   return null;
               }
               //string strShippingMethods = StringTools.DecodingForString(ShippingMethods);
               return new StoragePrintBC().GetInvoice(account, ShippingMethods, orderNo, category, salestype, count, sellernick, hdname);
           }
           catch (Exception)
           {
               return null;
               //throw;
           }
       }



        [WebMethod(Description = "快递单号:delivery<br>销售订单号:csoCode")]
       [SoapHeader("NSoapVar")]
        public bool DispatchListsInsert(string delivery, string csoCode,bool cf)
        {
            try
            {
                if (!CheckSoapHead())
                {
                    return false;
                }
                return new DeliveryPrintBC().GetInvoiceAndInsertSQL(delivery, csoCode, cf);
            }
            catch (Exception)
            {
                return false;
                //throw;
            }
        }



        [WebMethod(Description = "检查快递单号是否重复")]
        [SoapHeader("NSoapVar")]
        public bool CheckDeliveryNo(string deliveryNo)
        {
            try
            {
                if (!CheckSoapHead())
                {
                    return false;
                }
                return new DeliveryPrintBC().CheckDeliveryNo(deliveryNo);
            }
            catch (Exception)
            {
                return false;
                //throw;
            }
        }

        [WebMethod(Description = "根据快递单号取订单")]
        [SoapHeader("NSoapVar")]
        public DataTable GetByDeliveryNo(string deliveryNo)
        {
            try
            {
                if (!CheckSoapHead())
                {
                    return null;
                }
                return new DeliveryPrintBC().GetByDeliveryNo(deliveryNo);
            }
            catch (Exception)
            {
                return null;
                //throw;
            }
        }


        //开启session
        [WebMethod(Description = "登录")]
        [SoapHeader("NSoapVar")]
       public DataTable LoginCheck(string userNum, string password)
        {
            try
            {
                if (!CheckSoapHead())
                {
                    return null;
                }
                string strUserNum = StringTools.DecodingForString(userNum);
                DataTable dt = new DataTable();

                //检查是否已经登录
                if (Application["DeliveryPrintService" + strUserNum] != null)
                {
                    dt = null;
                }
                else
                {
                    string strPassword = StringTools.MD5Encode(StringTools.DecodingForString(password)).ToUpper();
                    dt = new DeliveryPrintBC().LoginCheck(strUserNum, strPassword);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                //保存session
                                Application["DeliveryPrintService" + strUserNum] = "1";
                            }

                        }

                    }

                }


                return dt;
            }
            catch (Exception)
            {
                return null;
                //throw;
            }
        }
        [WebMethod(Description = "订单修改")]
        [SoapHeader("NSoapVar")]
        public bool orders(string tid, string Provinces, string City, string District, string Address, string Consignee, string Phone, string flod)
        {
            try
            {
                if (!CheckSoapHead())
                {
                    return false;
                }
                return new DeliveryPrintBC().orders(tid, Provinces, City, District, Address, Consignee, Phone, flod);
            }
            catch (Exception)
            {
                return false;
                //throw;
            }
        }

        [WebMethod(Description = "注册")]
        [SoapHeader("NSoapVar")]
        public bool reg(string user, string pass, string session)
        { try
            {
                string strPassword = StringTools.MD5Encode(pass).ToUpper();
                return new DeliveryPrintBC().reg(user, strPassword, session);
            }
            catch (Exception)
           {
               return false;
               //throw;
           }
        }

        [WebMethod(Description = "更新活动")]
        [SoapHeader("NSoapVar")]
        public bool Deupinhd(string hdname, string seller, string code, string title, string Seller_ID, string cfg, string oldhdname, string oldseller, string odlcode, string oldtitle)
        {
            try
            {
                if (!CheckSoapHead())
                {
                    return false;
                }
                return new DeliveryPrintBC().Deupinhd(hdname, seller, code, title, Seller_ID, cfg, oldhdname, oldseller, odlcode, oldtitle);
            }
            catch (Exception)
            {
                return false;
                //throw;
            }
        }



        /// <summary>
        /// 释放锁定用户
        /// </summary>
        /// <param name="a"></param>
        /// <param name="p"></param>
        /// <param name="ruser"></param>
        /// <returns></returns>
        [WebMethod(Description = "")]
        [SoapHeader("NSoapVar")]
        public bool RUser(string a, string p,string ruser)
        {
            try
            {
                if (!CheckSoapHead())
                {
                    return false;
                }
                bool ok = false;
                string strUserNum = StringTools.DecodingForString(a);
                string strpass = StringTools.DecodingForString(p);

                string r = StringTools.DecodingForString(ruser);

                if (strUserNum.Equals("huihailiang") & strpass.Equals("hhl021"))
                {
                    if (Application["DeliveryPrintService" + r] != null)
                    {
                        Application["DeliveryPrintService" + r] = null;
                        ok = true;
                    }
                }


                return ok;
            }
            catch (Exception)
            {
                return false;
                //throw;
            }



        }



        //[WebMethod(Description = "获得所有活动名称")]
        [WebMethod(Description = "获得所有活动名称")]
        [SoapHeader("NSoapVar")]
        public DataTable GetHdNameAll(string Seller_ID)
        {
            ////检查是否已经登录
            //if (Session["DeliveryPrintService"] == null)
            //{
            //    return null;
            //}
            try
            {
                if (!CheckSoapHead())
                {
                    return null;
                }
                return new StoragePrintBC().GetHdNameAll(Seller_ID);
            }
            catch (Exception)
            {

                //throw;
                return null;
            }
            
           
        }


        [WebMethod(Description = "获得打印数目参考信息")]
        [SoapHeader("NSoapVar")]
        public List<int> GetPrintNum(string username,string hdname)
        {
            try
            {
                if (!CheckSoapHead())
                {
                    return null;
                }
                return new StoragePrintBC().GetPrintNum(username, hdname);
            }
            catch (Exception)
            {
                return null;
                //throw;
            }
        }


        [WebMethod(Description = "获得订单打印相关数量")]
        [SoapHeader("NSoapVar")]
        public List<int>  GetTuanNum(DateTime dtime)
        {
            try
            {
                if (!CheckSoapHead())
                {
                    return null;
                }

                return new StoragePrintBC().GetTNum(dtime);
            }
            catch (Exception)
            {
                return null;
                //throw;
            }
        }

        [WebMethod(Description = "获得没有确认发货的订单信息")]
        [SoapHeader("NSoapVar")]
        public DataTable GetWenTiDan(DateTime dtime)
        {
            try
            {
                if (!CheckSoapHead())
                {
                    return null;
                }

                return new StoragePrintBC().GetWTDan(dtime);
            }
            catch (Exception)
            {
                return null;
                //throw;
            }
        }

        [WebMethod(Description = "将问题单关闭")]
        [SoapHeader("NSoapVar")]
        public bool UpWTdan(string tid, string lx)
        {
            try
            {
                if (!CheckSoapHead())
                {
                    return false;
                }

                return new StoragePrintBC().UpWTdan(tid,lx);
            }
            catch (Exception)
            {
                return false;
                //throw;
            }
        }


        [WebMethod(Description = "汇总订单数据")]
        [SoapHeader("NSoapVar")]
        public DataTable GetHuiZong(int address,DateTime dt)
        {
            try
            {
                if (!CheckSoapHead())
                {
                    return null;
                }

                return new StoragePrintBC().GetHuiZ(address,dt);
            }
            catch (Exception)
            {
                return null;
                //throw;
            }
        }

        //UpWTdan(string tid, string lx)

        [WebMethod(Description = "检查订单是不是存在")]
        [SoapHeader("NSoapVar")]
        public bool CheckMergerOrder(string orderNo)
        {
            try
            {
                if (!CheckSoapHead())
                {
                    return false;
                }

                return new StoragePrintBC().CheckMergerOrder(orderNo);
            }
            catch (Exception)
            {
                return false;
                //throw;
            }
        }

      
        bool CheckSoapHead()
        {
            try
            {
                if (NSoapVar.username == "nmlch-2012-byken")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
                //throw;
            }
        }



    }
}
