using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebformsCms.Src;

namespace WebformsCms.Module.Login
{
    public partial class Logout : ModuleUserControl
    {
        public override void Initialize(bool serverRendering = false)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var context = HttpContext.Current.GetOwinContext();
            var authManager = context.Authentication;
            authManager.SignOut(context.Authentication.GetAuthenticationTypes().Select(o => o.AuthenticationType).ToArray());

            IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);

        }
    }
}