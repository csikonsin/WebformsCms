using System;

using System.Web.UI;
using System.Web.UI.WebControls;
using WebformsCms.Src;

namespace WebformsCms.Module.Menu
{
    public partial class Menu : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var manager = new MenusManager();

            var data = manager.GetMenusByParentControlId(this.ID);

            foreach(var menu in data)
            {
                if (menu.RelativeUrl == null) menu.RelativeUrl = "/";
            }

            if (data.Count == 0)
            {
                var c = (Client.MenuAdd)DefaultModuleFactory.GetControl(null, "~/Module/Client/MenuAdd.ascx");
                c.ParentControlId = this.ID;
                ph.Controls.Add(c);
                return;
            }

            Menus.Visible = true;
            Menus.ItemDataBound += Menus_ItemDataBound;
            Menus.DataSource = data;
            Menus.DataBind();
        }

        private void Menus_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
       
            var data = (Domain.Menus)e.Item.DataItem;

            if (Authentication.Instance.IsAdminEdit && data.ParentId == null)
            {
                var anchorPh = (PlaceHolder)e.Item.FindControl("anchorPh");
                if (anchorPh == null) return;

                var c = (Client.MenuAdd)DefaultModuleFactory.GetControl(null, "~/Module/Client/MenuAdd.ascx");
                c.ParentMenuId = data.Id;
                c.ParentControlId = this.ID;
                anchorPh.Controls.Add(c);
                return;
            }
        }
    }
}