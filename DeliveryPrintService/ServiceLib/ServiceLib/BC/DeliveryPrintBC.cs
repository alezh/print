using System;
using System.Collections.Generic;
using System.Text;
using ServiceLib.DA;
using System.Data;
using ServiceLib.Entity;
using ServiceLib.Util;

namespace ServiceLib.BC
{
    public class DeliveryPrintBC
    {
        DeliveryPrintDA da = new DeliveryPrintDA();

       

        //public List<string > GetColorSize(string id)
        //{
        //    return da.GetColorSize(id);
        //    ;
        //}


        public  DataTable GetByDeliveryNo(string  kddh)
        {
            return da.GetByDeliveryNo(kddh);

        }

        #region 获取DispatchList记录并insert

        /// <summary>
        /// 获取DispatchList记录并insert
        /// </summary>
        /// <param name="delivery">发货单号</param>
        /// <param name="cDLCode">订单号</param>
        /// <returns></returns>
        public bool GetInvoiceAndInsertSQL(string delivery, string csoCode,bool cf)
        {
            return da.InsertDispatchLists(delivery, csoCode,cf);
            //}
            //else
            //    return false;
        }

        #endregion

        #region 登录

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public DataTable LoginCheck(string userName, string passWord)
        {
            return da.LoginCheck(userName, passWord);

        }

        #endregion

        public bool reg(string user, string pass, string session)
        {
            return da.reglock(user,pass,session);
        }

        public bool orders(string tid, string Provinces, string City, string District, string Address, string Consignee, string Phone, string flod)
        {
            return da.orders(tid, Provinces, City, District, Address, Consignee, Phone, flod);
        }

        public bool Deupinhd(string hdname, string seller, string code, string title, string Seller_ID, string cfg, string oldhdname, string oldseller, string odlcode, string oldtitle)
        {
            return da.Deupinhd(hdname, seller, code, title, Seller_ID, cfg, oldhdname, oldseller, odlcode, oldtitle);
        }




        public DataTable Getchushou(string sellename, string hdname, string time, string isclose)
        {
            return da.Getkucun(sellename, hdname, time, isclose);
        }



        public DataTable kucun(string code)
        {
            return da.kucun(code);
        }

        public string[] GetNum(string sellename, string hdname, string time)
        {
           return da.GetorderNum(sellename, hdname, time);
        }


        public string GetOrderInfo2(string id)
        {

            return da.GetOrderInfo2(id);
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

            return da.GetOrderInfo(id);
        }

        #endregion



        public DataSet GetOrderInfoPart(string id)
        {

            return da.GetOrderInfoPart(id);
        }













     

          #region 检查快递单号是否已经存在
        /// <summary>
        /// 检查快递单号是否已经存在
        /// </summary>
        /// <param name="deliveryNo">快递单号</param>
        /// <returns></returns>
        public bool CheckDeliveryNo(string deliveryNo)
        {
            return da.CheckDeliveryNo(deliveryNo);
        }
          #endregion

       



       

    }
}
