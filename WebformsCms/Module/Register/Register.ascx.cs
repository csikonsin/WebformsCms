using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebformsCms.Models;
using WebformsCms.Src;

namespace WebformsCms.Module.Register
{
    public partial class Register : ModuleUserControl
    {
        public override void Initialize(bool serverRendering = false)
        {            
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Register_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                return;
            }

            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
            var user = new ApplicationUser() { UserName = Username.Text, Email = Email.Text };
            IdentityResult result = manager.Create(user, Password.Text);
            if (result.Succeeded)
            {
                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                //string code = manager.GenerateEmailConfirmationToken(user.Id);
                //string callbackUrl = IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id, Request);
                //manager.SendEmail(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>.");
                signInManager.SignIn(user, isPersistent: false, rememberBrowser: false);

                var userId = HttpContext.Current.User.Identity.GetUserId();

                var newUser = new Domain.Users()
                {
                    Username = Context.User.Identity.Name,
                    OwinId = userId,
                    CreatedAt = DateTime.Now
                };

                using (var session = new Data.DataSession())
                {
                    var repo = new Data.UsersRepository(session.UnitOfWork);
                    repo.Save(newUser);
                }

                IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
            }
            else
            {
                ErrorMessage.Text = result.Errors.FirstOrDefault();
            }
        }
    }
}