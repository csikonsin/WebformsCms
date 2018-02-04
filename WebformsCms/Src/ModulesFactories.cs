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

    public static class DefaultModuleFactory
    {
        public static ModuleUserControl GetControl(Domain.Modules module, string virtualPath)
        {
            var userControl = new UserControl();
            var control = (ModuleUserControl)userControl.LoadControl(virtualPath);
            control.Data = module;
            return control;
        }

        public static ModuleUserControl GetControlFromModuleData(Domain.Modules module)
        {
            var moduleType = (ModuleType)module.ModuleType;

            IModuleFactory<ModuleUserControl> factory = null;

            switch (moduleType)
            {
                case ModuleType.Text:
                    factory = new TextFactory();
                    break;
                case ModuleType.Heading:
                    factory = new HeadingFactory();
                    break;
                case ModuleType.Image:
                    factory = new ImageFactory();
                    break;
                case ModuleType.Login:
                    factory = new LoginFactory();
                    break;
                case ModuleType.Register:
                    factory = new RegisterFactory();
                    break;
                default:
                    throw new Exception("Unkown module type!");
            }

            return factory?.GetControl(module);
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