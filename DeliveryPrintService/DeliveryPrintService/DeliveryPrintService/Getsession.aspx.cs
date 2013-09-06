using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace DeliveryPrintService
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {    //获取taobao API session
            if (!IsPostBack)
            {
                TextBox1.Text = string.Empty;
                string appKey = string.Empty;
                string parameters = string.Empty;
                string session = string.Empty;
                string sign = string.Empty;

                if (!string.IsNullOrEmpty(Request.QueryString["top_appkey"]))
                {
                    appKey = Request.QueryString["top_appkey"];
                }

                if (!string.IsNullOrEmpty(Request.QueryString["top_parameters"]))
                {
                    parameters = Request.QueryString["top_parameters"];
                }

                if (!string.IsNullOrEmpty(Request.QueryString["top_session"]))
                {
                    session = Request.QueryString["top_session"];
                }

                if (!string.IsNullOrEmpty(Request.QueryString["top_sign"]))
                {
                    sign = Request.QueryString["top_sign"];
                }
                string remark = string.Format("AppKey:{0};Parameters:{1};Session:{2};Sign:{3};", appKey, parameters,
                                              session,
                                              sign);

                string sql1 = string.Format(@"insert into errlog (title,remark) values ('session','{0}')", session);

                string sql =
                    string.Format(
                        @"update TaoBaoShopAPI set session_id = '{0}' ,ChangeTime = getdate() ,IsValid = 1, 
                            FailureTime = null where app_key = '{1}'",
                        session, appKey);



                TextBox1.Text = session;

            }
        }

      
    }
}
