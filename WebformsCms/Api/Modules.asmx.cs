using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
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
        protected class ModuleTypesModel
        {
            public string name { get; set; }

            public int type { get; set; }
        }


        [ScriptMethod(UseHttpGet = true, ResponseFormat =ResponseFormat.Json)]
        [WebMethod]
        public void GetModuleTypes()
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";

            var types = ModulesManager.GetModuleTypes();

            var t = new List<ModuleTypesModel>();

            var en = types.GetEnumerator();
            while (en.MoveNext())
            {
                t.Add(new ModuleTypesModel()
                {
                    name = en.Current.Key,
                    type = en.Current.Value
                });
            }

            var serializer = new JavaScriptSerializer();
            Context.Response.Write(serializer.Serialize(t));
        }

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
                var control = DefaultModuleFactory.GetControlFromModuleData(module);

                var single = (Module.SingleModule)DefaultModuleFactory.GetControl(module, "~/Module/SingleModule.ascx");

                single.Control = control;

                single.Initialize(true);
                var children = ModulesHelper.FlattenChildren(single);

                var enumerator = children.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    var child = enumerator.Current;

                    if(child is ModuleUserControl)
                    {
                        var moduleChild = (ModuleUserControl)child;
                        moduleChild.Initialize(true);
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
