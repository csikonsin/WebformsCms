using System.Collections.Generic;
using System.IO;
using System.Web.Optimization;
using System.Web.UI;
using WebformsCms.Src;

namespace WebformsCms
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkID=303951
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterStyleBundle(bundles);

            bundles.Add(new ScriptBundle("~/bundles/WebFormsJs").Include(
                            "~/Scripts/WebForms/WebForms.js",
                            "~/Scripts/WebForms/WebUIValidation.js",
                            "~/Scripts/WebForms/MenuStandards.js",
                            "~/Scripts/WebForms/Focus.js",
                            "~/Scripts/WebForms/GridView.js",
                            "~/Scripts/WebForms/DetailsView.js",
                            "~/Scripts/WebForms/TreeView.js",
                            "~/Scripts/WebForms/WebParts.js"));

            // Order is very important for these files to work, they have explicit dependencies
            bundles.Add(new ScriptBundle("~/bundles/MsAjaxJs").Include(
                    "~/Scripts/WebForms/MsAjax/MicrosoftAjax.js",
                    "~/Scripts/WebForms/MsAjax/MicrosoftAjaxApplicationServices.js",
                    "~/Scripts/WebForms/MsAjax/MicrosoftAjaxTimer.js",
                    "~/Scripts/WebForms/MsAjax/MicrosoftAjaxWebForms.js"));

            // Use the Development version of Modernizr to develop with and learn from. Then, when you’re
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                            "~/Scripts/modernizr-*"));

            ScriptManager.ScriptResourceMapping.AddDefinition(
                "respond",
                new ScriptResourceDefinition
                {
                    Path = "~/Scripts/respond.min.js",
                    DebugPath = "~/Scripts/respond.js",
                });

            bundles.Add(new ScriptBundle("~/bundles/cms").Include(
                "~/Scripts/Cms/common.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/cms-admin").Include(
                "~/Scripts/Cms-Admin/common.js",
                "~/Scripts/Cms-Admin/bundle.js"                
            ));

          
        }

        public static void RegisterStyleBundle(BundleCollection bundles)
        {
            var styleBundle = new StyleBundle($"~/bundles/{WebSettings.Instance.Settings.Name}/css");
            var virtualPath = $"~/Content/css/{WebSettings.Instance.Settings.Name}";

            var manualIncludes = new List<string>(new string[] { "reset.css", "responsive.css" });
            styleBundle.Include($"~/Content/css/{WebSettings.Instance.Settings.Name}/reset.css");


            BundleHelpers.RegisterAllStylesInFolder(styleBundle, virtualPath,manualIncludes);

            styleBundle.Include($"{virtualPath}/responsive.css");

            bundles.Add(styleBundle);

            var cmsBundle = new StyleBundle($"~/bundles/cms/css");
            BundleHelpers.RegisterAllStylesInFolder(cmsBundle, "~/Content/css/cms");
            bundles.Add(cmsBundle);


            BundleTable.EnableOptimizations = !System.Web.HttpContext.Current.IsDebuggingEnabled;

        }


    }
}