﻿using System;
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
            LoadModules();
        }

        private void LoadModules()
        {
            var repData = new List<ModuleUserControl>();
            repModules.ItemDataBound += RepModules_ItemDataBound;

            if (Root)
            {
                var c = new DefaultModuleFactory($"~/Content/templates/{WebSettings.Instance.Settings.Name}/layout/layout.ascx").GetControl(null);
                c.ID = "Layout";
                repData.Add(c);

                var editor = new DefaultModuleFactory("~/Module/Client/ModuleEditor.ascx").GetControl(null);
                Cms.Controls.Add(editor);
                repModules.DataSource = repData;
                repModules.DataBind();

                if (Authentication.IsAdmin)
                {
                    var toggle = (Client.AdminEditToggle)new DefaultModuleFactory("~/Module/Client/AdminEditToggle.ascx").GetControl(null);
                    Cms.Controls.Add(toggle);
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
                var c = (Client.ModuleEditAdd)new DefaultModuleFactory("~/Module/Client/ModuleEditAdd.ascx").GetControl(null);
                c.Data = new Domain.Modules()
                {
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