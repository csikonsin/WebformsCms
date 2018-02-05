using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using WebformsCms.Module.Login;
using WebformsCms.Module.Register;
using WebformsCms.Module.Text;
using WebformsCms.Modules.Heading;

namespace WebformsCms.Src
{


    public interface IModuleFactory<out T> where T : ModuleUserControl
    {
        T GetControl(Domain.Modules module);
    }

    public  class DefaultModuleFactory : IModuleFactory<ModuleUserControl>
    {
        private string virtualPath;
        public DefaultModuleFactory(string virtualPath)
        {
            this.virtualPath = virtualPath;
        }

        public ModuleUserControl GetControl(Domain.Modules module)
        {
            var userControl = new UserControl();
            var control = (ModuleUserControl)userControl.LoadControl(virtualPath);
            control.Data = module;
            return control;
        }

    }

    public class HeadingFactory : IModuleFactory<Heading>
    {
        public Heading GetControl(Domain.Modules module)
        {
            var userControl = new UserControl();
            var control = (Heading)userControl.LoadControl("~/Module/Heading/Heading.ascx");
            control.Data = module;
            return control;
        }
    }

    public class TextFactory : IModuleFactory<Text>
    {
        public Text GetControl(Domain.Modules module)
        {
            var userControl = new UserControl();
            var control = (Text)userControl.LoadControl("~/Module/Text/Text.ascx");
            control.Data = module;
            return control;
        }
    }

    public class ImageFactory : IModuleFactory<WebformsCms.Module.Image.Image>
    {
        public WebformsCms.Module.Image.Image GetControl(Domain.Modules module)
        {
            var userControl = new UserControl();
            var control = (WebformsCms.Module.Image.Image)userControl.LoadControl("~/Module/Image/Image.ascx");
            control.Data = module;
            return control;
        }
    }

    public class LoginFactory : IModuleFactory<Login>
    {
        public Login GetControl(Domain.Modules module)
        {
            var userControl = new UserControl();
            var control = (Login)userControl.LoadControl("~/Module/Login/Login.ascx");
            control.Data = module;
            return control;
        }
    }

    public class RegisterFactory : IModuleFactory<Register>
    {
        public Register GetControl(Domain.Modules module)
        {
            var userControl = new UserControl();
            var control = (Register)userControl.LoadControl("~/Module/Register/Register.ascx");
            control.Data = module;
            return control;
        }
    }

}