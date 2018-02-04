using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace WebformsCms.Src
{
    public static class BundleHelpers
    {
        public static void RegisterAllStylesInFolder(StyleBundle styleBundle, string virtualPath, List<string> excludes = null)
        {
            excludes = excludes ?? new List<string>();

            var absolutePath = System.Web.HttpContext.Current.Server.MapPath(virtualPath);

            Directory.CreateDirectory(absolutePath);
            var di = new DirectoryInfo(absolutePath);

            var files = di.GetFiles("*.css");

            foreach (FileInfo file in files)
            {
                if (excludes.Contains(file.Name))
                {
                    continue;
                }

                styleBundle.Include($"{virtualPath}/{file.Name}");
            }
        }
    }
}