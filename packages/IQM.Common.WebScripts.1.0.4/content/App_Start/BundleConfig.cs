using System.Web.Optimization;

    /// <summary>
    /// The bundle configuration
    /// </summary>
    public class BundleConfig
    {
        /// <summary>
        /// Registers the bundles.
        /// </summary>
        /// <param name="bundles">The bundles.</param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Libs
            //// bundles.Add(new ScriptBundle("~/bundles/Libs/JQueryCore").Include("~/Scripts/libraries/jquery/jquery-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/Libs/JQueryCore").Include("~/Scripts/libraries/jquery/jquery-1.10.2.js"));
            bundles.Add(new ScriptBundle("~/bundles/Libs/JQueryExt").Include("~/Scripts/libraries/jquery/ext/*.js"));
            bundles.Add(new ScriptBundle("~/bundles/Libs/JQueryUI").Include("~/Scripts/libraries/jquery/ui/jquery-ui-{version}.js", "~/Scripts/libraries/jquery/ui/jquery.flyoutmenu.js", "~/Scripts/libraries/jquery/ui/jquery.tablednd_0_5.js"));
            bundles.Add(new ScriptBundle("~/bundles/Libs/FileUpload").Include("~/Scripts/libraries/file-upload/*.js"));
            bundles.Add(new ScriptBundle("~/bundles/Libs/JSON").Include("~/Scripts/libraries/json/*.js"));
            bundles.Add(new ScriptBundle("~/bundles/Libs/LightBox").Include("~/Scripts/libraries/lightbox/*.js"));
            bundles.Add(new ScriptBundle("~/bundles/Libs/Modernizer").Include("~/Scripts/libraries/modernizr/*.js"));
            bundles.Add(new ScriptBundle("~/bundles/Libs/MvcAjax").Include("~/Scripts/libraries/mvcajax/*.js"));
            
            // Core.
            bundles.Add(new ScriptBundle("~/bundles/Core").Include("~/Scripts/Core/*.js"));
            bundles.Add(new ScriptBundle("~/bundles/Core/System").Include("~/Scripts/Core/System/*.js"));

            // Styles
            bundles.Add(new StyleBundle("~/Content/System").Include("~/Content/Css/Core/System/*.css"));
            bundles.Add(new StyleBundle("~/Content/Libs").Include("~/Content/Css/libraries/*.css"));

            // Themes
            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include("~/Content/themes/base/*.css"));
            bundles.Add(new StyleBundle("~/Content/themes/custom-theme/css").Include("~/Content/themes/custom-theme/*.css"));
        }
    }
