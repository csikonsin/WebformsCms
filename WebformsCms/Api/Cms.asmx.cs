using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using WebformsCms.Src;

namespace WebformsCms.Api
{
    /// <summary>
    /// Summary description for Cms
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class Cms : System.Web.Services.WebService
    {

        [WebMethod]
        public string ToggleEdit()
        {
            if (!Authentication.Instance.IsAuthenticated) return "";
            if (!Authentication.Instance.IsAdmin) return "";
            Authentication.Instance.IsAdminEdit = !Authentication.Instance.IsAdminEdit;
            return "";
        }
    }
}
