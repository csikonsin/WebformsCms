using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebformsCms.Src;

namespace WebformsCms.Module.Menu
{

    public partial class Menu : UserControl
    {
        protected class MenuViewModel : Domain.Menus
        {
            public string Url { get; set; }
        }

        private List<MenuViewModel> GetMenusByParentControlId(string controlId)
        {
            var manager = new MenusManager();

            var menus = manager.GetMenusByParentControlId(this.ID);

            var vms = new List<MenuViewModel>();

            foreach(var menu in menus)
            {
                var vm = new MenuViewModel()
                {
                    Id = menu.Id,
                    CreatedAt = menu.CreatedAt,
                    CreatedById = menu.CreatedById,
                    Homepage = menu.Homepage,
                    ModifiedAt = menu.ModifiedAt,
                    ModifiedById = menu.ModifiedById,
                    Name = menu.Name,
                    ParentControlId = menu.ParentControlId,
                    ParentId = menu.ParentId,
                    RelativeUrl = menu.RelativeUrl
                };

                if (WebSettings.UseFriendlyUrls)
                {
                    vm.Url = menu.RelativeUrl;
                }else
                {
                    vm.Url = $"/Default.aspx?menuid={menu.Id}";
                }

                vms.Add(vm);
            }

            return vms;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var data = GetMenusByParentControlId(this.ID);

            if (data.Count == 0)
            {
                var c = (Client.MenuAdd)new DefaultModuleFactory("~/Module/Client/MenuAdd.ascx").GetControl(null);
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

                var c = (Client.MenuAdd)new DefaultModuleFactory("~/Module/Client/MenuAdd.ascx").GetControl(null);
                c.ParentMenuId = data.Id;
                c.ParentControlId = this.ID;
                anchorPh.Controls.Add(c);
                return;
            }
        }
    }
}