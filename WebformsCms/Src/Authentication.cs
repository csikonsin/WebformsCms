using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebformsCms.Models;

namespace WebformsCms.Src
{

    public class Authentication
    {
        private static Authentication instance;
        private Authentication() { }

        public static Authentication Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Authentication();
                }
                return instance;
            }
        }

        public bool IsAuthenticated {
            get {
                return HttpContext.Current.User.Identity.IsAuthenticated;
            }
        }

        public bool IsAdmin
        {
            get
            {
                if (!HttpContext.Current.User.Identity.IsAuthenticated) return false;

                var context = HttpContext.Current.GetOwinContext();
                var manager = context.GetUserManager<ApplicationUserManager>();

                var userId = HttpContext.Current.User.Identity.GetUserId();

                var roleStore = new RoleStore<IdentityRole>(context.Get<ApplicationDbContext>());
                var roleManager = new RoleManager<IdentityRole>(roleStore);

                if (!roleManager.RoleExists("Administrator"))
                {
                    context.Get<ApplicationDbContext>().Roles.Add(new IdentityRole()
                    {
                        Name = "Administrator"
                    });
                    context.Get<ApplicationDbContext>().SaveChanges();
                }

                var isAdmin = manager.IsInRole(userId, "Administrator");

                return isAdmin;
            }
        }

        private bool _edit = false;
        public  bool IsAdminEdit
        {
            get
            {
                if (!IsAuthenticated) return false;
                if (!IsAdmin) return false;                
                return _edit;
            }   
            set
            {
                _edit = value;
            }            
        }

        public int GetUserId()
        {
            if (!IsAuthenticated) return 0;

            return 1;
        }
    }
}