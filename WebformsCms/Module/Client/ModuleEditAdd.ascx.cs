using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebformsCms.Src;

namespace WebformsCms.Module.Client
{
    public partial class ModuleEditAdd : ModuleUserControl
    {        
        protected void Page_Load(object sender, EventArgs e)
        {
            Initialize();
        }

        public override void Initialize(bool serverRendering = false)
        {
            if (serverRendering)
            {
                editadd.Attributes["class"] += " notloaded";
            }

            editadd.Attributes["data-moduleid"] = Convert.ToString(Data.Id);
            editadd.Attributes["data-menuid"] = Convert.ToString(Data.MenuId);
            editadd.Attributes["data-moduletype"] = Convert.ToString(Data.ModuleType);
            editadd.Attributes["data-parentmoduleid"] = Convert.ToString(Data.ParentId);
            editadd.ID = "";
        }
    }
}