using System;
using System.Collections.Generic;
using System.Web.UI;
using WebformsCms.Data;
using WebformsCms.Modules.Heading;
using WebformsCms.Module.Text;
using WebformsCms.Module.Image;
using WebformsCms.Module.Login;
using WebformsCms.Module.Register;

namespace WebformsCms.Src
{
    public enum ModuleType
    {
        Text = 0,
        Heading = 1,
        Image = 2,
        Login = 3,
        Register = 4,
        Logout = 5
    }




    public interface IModulesManager
    {
        List<ModuleUserControl> GetMenuModules(int menuId, int parentId = 0);
        int AddModule(int menuId, ModuleType type, int parentId = 0);
    }

    public class ModulesManager : IModulesManager
    {
        public static Dictionary<string, int> GetModuleTypes()
        {
            var types = new Dictionary<string,int>();

            var values = Enum.GetValues(typeof(ModuleType));

            foreach(int value in values)
            {
                var type = (ModuleType)value;
                types.Add(type.ToString(), value);
            }

            return types;
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
                case ModuleType.Logout:
                    factory = new DefaultModuleFactory("~/Module/Login/Logout.ascx");
                    break;
                default:                    
                    throw new Exception("Unkown module type!");
            }

            return factory?.GetControl(module);
        }


        private int GetNextPosition(int menuId, int parentId)
        {
            int position = 100;

            using (var session = new DataSession())
            {
                var modulesRepo = new ModulesRepository(session.UnitOfWork);

                position = modulesRepo.GetNextPosition(menuId, parentId);                
            }

            return position;
        }

        public void AddModule(ModuleType type, int parentId)
        {
            AddModule(GetMenuIdFromModuleId(parentId), type, parentId);
        }

        private int GetMenuIdFromModuleId(int parentId)
        {
            int menuId = 0;

            using (var session = new DataSession())
            {
                var modulesRepo = new ModulesRepository(session.UnitOfWork);

                menuId = modulesRepo.GetMenuIdFromModuleId(parentId);
            }

            return menuId;
        }

        public void DeleteModule(int menuId, int moduleId)
        {
            using (var session = new DataSession())
            {
                var modulesRepo = new ModulesRepository(session.UnitOfWork);

                var module = modulesRepo.GetSingle(moduleId);

                if (module.MenuId == menuId)
                {
                    modulesRepo.Delete(module);
                }                          
            }
        }

        public int AddModule(int menuId, ModuleType type, int parentId = 0)
        {
            int newId;

            var module = new Domain.Modules()
            {
                MenuId = menuId,
                ModuleType = (int)type,
                Position = GetNextPosition(menuId, parentId),
                CreatedAt = DateTime.Now,
                CreatedById = Authentication.Instance.GetUserId()                
            };
            if (parentId > 0) module.ParentId = parentId;


            using (var session = new DataSession())
            {
                var modulesRepo = new ModulesRepository(session.UnitOfWork);

                newId = modulesRepo.Save(module);
            }
            return newId;
        }


        public List<ModuleUserControl> GetMenuModules(int menuId, int parentId = 0)
        {
            var controls = new List<ModuleUserControl>();

            using (var session = new DataSession())
            {
                var modulesRepo = new ModulesRepository(session.UnitOfWork);

                var modules = modulesRepo.GetMenuModules(menuId);

                int index = 0;
                foreach (var module in modules)
                {
                    int moduleParentId = 0;
                    int.TryParse(Convert.ToString(module.ParentId), out moduleParentId);
                    if (moduleParentId != parentId) continue;
                    var control = GetControlFromModuleData(module);
                    control.ID = control.GetType().Name + "_index";
                    if (control != null) controls.Add(control);
                }
            }
            return controls;
        }
    }


    public abstract class ModuleUserControl : UserControl
    {
        public Domain.Modules Data { get; set; }

        public abstract void Initialize(bool serverRendering = false);
      
    }
}