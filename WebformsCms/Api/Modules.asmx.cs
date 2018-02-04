using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;
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
                var moduleId = manager.AddModule(menuId, (ModuleType)moduleType, parentId);
                ret = GetModuleHtml(moduleId);
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
        public static string RenderControlToHtml(System.Web.UI.Control ControlToRender)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            System.IO.StringWriter stWriter = new System.IO.StringWriter(sb);
            System.Web.UI.HtmlTextWriter htmlWriter = new System.Web.UI.HtmlTextWriter(stWriter);
            ControlToRender.RenderControl(htmlWriter);
            return sb.ToString();
        }

        [WebMethod]
        public string GetModuleHtml(int moduleId)
        {
            string ret = "";
            try
            {
                Domain.Modules module;
                using(var session = new Data.DataSession())
                {
                    var repo = new Data.ModulesRepository(session.UnitOfWork);
                    module = repo.GetSingle(moduleId);
                }
                var control = ModulesManager.GetControlFromModuleData(module);

                var single = (Module.SingleModule)DefaultModuleFactory.GetControl(module, "~/Module/SingleModule.ascx");

                single.Control = control;
                single.LoadPlaceholders();
                var commands = (PlaceHolder)single.FindControl("commands");
                if (commands != null && commands.Controls.Count>0)
                {
                    var editAdd = (Module.Client.ModuleEditAdd)commands.Controls[0];
                    if (editAdd != null)
                    {
                        editAdd.InitializeAttributes(true);
                    }
                }
                ret = RenderControlToHtml(single);
            }
            catch (Exception ex)
            {
                ret = ex.ToString();
            }

            return ret;
        }
    }
}
