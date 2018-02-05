using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace WebformsCms
{
    public class Global : HttpApplication
    {
        public Global()
        {
            this.AuthenticateRequest += Global_AuthenticateRequest;
        }
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }

        private void Global_AuthenticateRequest(object sender, EventArgs e)
        {
            var rawUrl = HttpContext.Current.Request.RawUrl;

            var ignoreRequests = new List<string> { "browserLink", "Default?menuid=","Default.aspx?menuid=" };

            for (int i = 0; i < ignoreRequests.Count; i++)
            {
                if (rawUrl.IndexOf(ignoreRequests[i]) > -1)
                {
                    return;
                }
            }

            var manager = new Src.MenusManager();

            var rewrite = manager.RewriteRawUrl(rawUrl);

            if (rewrite == rawUrl) return;

            if (Src.WebSettings.UseFriendlyUrls)
            {
                Context.RewritePath(rewrite);
            }else
            {
                Context.Response.Redirect(rewrite);
            }
            
        }
    }
}