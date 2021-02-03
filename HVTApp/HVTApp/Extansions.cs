using System;
using System.Linq;
using System.Reflection;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Model;
using Prism.Modularity;

namespace HVTApp
{
    public static class Extansions
    {
        /// <summary>
        /// Добавление модулей на основе ролей.
        /// </summary>
        /// <param name="catalog">Каталог</param>
        /// <param name="moduleType">Тип модуля</param>
        public static void AddModuleByRole(this ModuleCatalog catalog, Type moduleType)
        {
            var attr = (moduleType.GetCustomAttribute<ModuleAccessAttribute>());
            if (attr != null && attr.Roles.Contains(GlobalAppProperties.User.RoleCurrent))
                catalog.AddModule(moduleType);
        }
    }
}