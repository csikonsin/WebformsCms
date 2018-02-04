using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebformsCms.Src;

namespace WebformsCms.Module.Client
{
    public partial class AdminEditToggle : ModuleUserControl
    {
        public bool IsEdit
        {
            get
            {
                return Authentication.Instance.IsAdminEdit;
            }
        }
        public override void Initialize(bool serverRendering = false)
        {            
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}