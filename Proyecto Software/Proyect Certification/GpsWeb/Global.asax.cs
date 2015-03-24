using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace GpsWeb
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            if (HttpContext.Current != null && HttpContext.Current.Session != null)
            {
                string lang = "es-MX";
                if (Session["Idioma"] != null)
                {
                    lang = Session["Idioma"].ToString();
                }
                System.Globalization.CultureInfo unCI = System.Globalization.CultureInfo.CreateSpecificCulture(lang);
                System.Threading.Thread.CurrentThread.CurrentCulture = unCI;
                System.Threading.Thread.CurrentThread.CurrentUICulture = unCI;
            }

        }

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}