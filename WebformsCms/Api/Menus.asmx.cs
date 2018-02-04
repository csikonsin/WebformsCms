using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using WebformsCms.Src;

namespace WebformsCms.Api
{
    /// <summary>
    /// Summary description for Menus
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class Menus : System.Web.Services.WebService
    {

        [WebMethod]
        public string MenuAddEdit(int menuId, string parentControlId, string name, string relativeUrl, bool homepage, int parentId)
        {
            string ret = "";

            try
            {
                var manager = new MenusManager();
                ret = Convert.ToString(manager.AddEditMenu(menuId, parentControlId, name, relativeUrl, homepage, parentId));
            }
            catch (Exception ex)
            {
                ret = ex.ToString();
            }

            return ret;
        }
    }
}
