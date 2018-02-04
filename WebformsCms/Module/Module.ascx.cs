using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using WebformsCms.Src;

namespace WebformsCms.Module
{
    public partial class Module : System.Web.UI.UserControl
    {
        private Authentication Authentication = Authentication.Instance;

        public bool Root { get; set; }

        public int ModuleId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            var repData = new List<ModuleUserControl>();
            repModules.ItemDataBound += RepModules_ItemDataBound;

            if (Root)
            {
                var c = (ModuleUserControl)LoadControl($"~/Content/templates/{WebSettings.Instance.Settings.Name}/layout/layout.ascx");
                repData.Add(c);
                Cms.Controls.Add(DefaultModuleFactory.GetControl(null, "~/Module/Client/ModuleEditor.ascx"));
                repModules.DataSource = repData;
                repModules.DataBind();

                if (Authentication.IsAdmin)
                {
                    Cms.Controls.Add((Client.AdminEditToggle)DefaultModuleFactory.GetControl(null, "~/Module/Client/AdminEditToggle.ascx"));
                }

                return;
            }

            int menuId;
            int.TryParse(Request.QueryString["menuid"], out menuId);

            if (menuId == 0)
            {
                Cms.Controls.Add(new Literal() { Text = "You need to create a menu first!" });
                return;
            }

            var manager = new ModulesManager();

            var controls = manager.GetMenuModules(menuId, ModuleId);

            foreach (var control in controls)
            {
                repData.Add(control);
            }

         

            if (Authentication.Instance.IsAdminEdit)
            {
                var c = (Client.ModuleEditAdd)DefaultModuleFactory.GetControl(null, "~/Module/Client/ModuleEditAdd.ascx");
                c.Data = new Domain.Modules() {
                    MenuId = menuId
                };
                Add.Controls.Add(c);
            }

            repModules.DataSource = repData;
            repModules.DataBind();
        }

        private void RepModules_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (!(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)) return;

            var single = (SingleModule)e.Item.FindControl("singlemodule");

            single.Root = Root;
            single.Control = (ModuleUserControl)e.Item.DataItem; 
        }
    }


}