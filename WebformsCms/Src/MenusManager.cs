using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebformsCms.Src
{
    public class MenusManager
    {
        public List<Domain.Menus> GetMenusByParentControlId(string id)
        {
            var menus = new List<Domain.Menus>();

            using (var session = new Data.DataSession())
            {
                var repo = new Data.MenusRepository(session.UnitOfWork);

                menus = repo.GetMenusByParentControlId(id);
            }

            return menus;
        }

        public int AddEditMenu(int menuId, string parentControlId, string name, string relativeUrl, bool homepage, int parentId = 0)
        {
            using (var session = new Data.DataSession())
            {
                var repo = new Data.MenusRepository(session.UnitOfWork);

                Domain.Menus menu = null;

                if(menuId > 0)
                {
                    menu = repo.GetSingle(menuId);
                    if(menu != null)
                    {
                        menu.Name = name;
                        menu.RelativeUrl = relativeUrl;
                        if(homepage)menu.Homepage = homepage;
                        repo.Save(menu);
                        return menu.Id;
                    }
                }

                menu = new Domain.Menus()
                {
                    CreatedAt = DateTime.Now,
                    CreatedById = Authentication.Instance.GetUserId(),
                    Name = name,
                    RelativeUrl = relativeUrl
                    
                };
                if (homepage) menu.Homepage = homepage;
                if (parentControlId != "") menu.ParentControlId = parentControlId;
                if (parentId != 0) menu.ParentId = parentId;

                return repo.Save(menu);
            }
        }

        private Domain.Menus GetMenuFromRawUrl(string rawUrl)
        {
            Domain.Menus menu = null;

            using (var session = new Data.DataSession())
            {
                var repo = new Data.MenusRepository(session.UnitOfWork);

                var menus = repo.GetAll();

                foreach(var m in menus)
                {
                    if(rawUrl == "/" && m.Homepage.HasValue && m.Homepage.Value)
                    {
                        menu = m;
                        break;
                    }

                    if (m.RelativeUrl == null) continue;

                    if(m.RelativeUrl == rawUrl || "/" + m.RelativeUrl == rawUrl)
                    {
                        menu = m;
                        break;
                    }

                }
            }

            return menu;
        }

        public string RewriteRawUrl(string rawUrl)
        {           

            var menu = GetMenuFromRawUrl(rawUrl);

            if (menu == null)
            {
                return rawUrl;
            }

            int menuId = menu.Id;

            string rewrite = $"/Default.aspx?menuid={menuId}";

            return rewrite;
        }
    }
}