using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebformsCms.Src;

namespace WebformsCms.Module.Client
{
    public partial class MenuAdd : ModuleUserControl
    {
        public string ParentControlId { get; set; }
        public int ParentMenuId { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}