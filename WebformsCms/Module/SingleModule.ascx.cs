using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebformsCms.Src;

namespace WebformsCms.Module
{
    public partial class SingleModule : ModuleUserControl
    {
        public ModuleUserControl Control { get; set; }
        public bool Root { get; set; } = false;

        public SingleModule()
        {

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadPlaceholders();
        }

        public void LoadPlaceholders()
        {
            ph.Controls.Add(Control);

            if (!Authentication.Instance.IsAdminEdit() || Root)
            {
                return;
            }

            var c = (Client.ModuleEditAdd)DefaultModuleFactory.GetControl(null, "~/Module/Client/ModuleEditAdd.ascx");
            c.Data = Control.Data;
            commands.Controls.Add(c);
        }
    }
}