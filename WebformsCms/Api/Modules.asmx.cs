using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using WebformsCms.Src;

namespace WebformsCms.Api
{
    /// <summary>
    /// Summary description for Modules
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]    
    [System.Web.Script.Services.ScriptService]
    public class Modules : System.Web.Services.WebService
    {

        [WebMethod]
        public string NewModule(int menuId, int moduleType, int parentId)
        {
            string ret = "";

            try
            {
                var manager = new ModulesManager();
                manager.AddModule(menuId, (ModuleType)moduleType, parentId);
            }
            catch (Exception ex)
            {
                ret = ex.ToString();
            }

            return ret;

        }

        [WebMethod]
        public string DeleteModule(int menuId, int moduleId)
        {
            string ret = "";

            try
            {
                var manager = new ModulesManager();
                manager.DeleteModule(menuId, moduleId);
            }
            catch (Exception ex)
            {
                ret = ex.ToString();
            }

            return ret;
        }
    }
}
