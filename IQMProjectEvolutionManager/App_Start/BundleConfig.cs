using System.Web.Optimization;

namespace IQMProjectEvolutionManager
{
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
            // Library scripts.
            bundles.Add(new ScriptBundle("~/bundles/libraries").Include("~/Scripts/libraries/*.js"));
            bundles.Add(new ScriptBundle("~/bundles/libraries/jquery").Include("~/Scripts/libraries/jquery/*.js", "~/Scripts/*.js"));
            bundles.Add(new ScriptBundle("~/bundles/libraries/jqueryui").Include("~/Scripts/libraries/jquery/ui/*.js"));
            bundles.Add(new ScriptBundle("~/bundles/libraries/jqueryval").Include("~/Scripts/libraries/jquery/validate/*.js"));
            bundles.Add(new ScriptBundle("~/bundles/libraries/modernizr").Include("~/Scripts/libraries/modernizr/*.js"));
            bundles.Add(new ScriptBundle("~/bundles/libraries/flot").Include("~/Scripts/libraries/flot/jquery.flot.js").Include("~/Scripts/libraries/flot/jquery.rezise.js").Include("~/Scripts/libraries/flot-labels/*.js"));
            bundles.Add(new ScriptBundle("~/bundles/libraries/knockout").Include("~/Scripts/libraries/knockout/*.js"));

            // System Scripts
            bundles.Add(new ScriptBundle("~/bundles/Core").Include("~/Scripts/Core/*.js"));
            bundles.Add(new ScriptBundle("~/bundles/Core/System").Include("~/Scripts/Core/System/*.js"));

            // Styles
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/stylesheets/*.css", "~/Content/stylesheets/Views/*.css"));
            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include("~/Content/themes/base/*.css"));
            bundles.Add(new StyleBundle("~/Content/themes/custom-theme/css").Include("~/Content/themes/custom-theme/jquery-ui-1.10.3.custom.css"));
        }
    }
}
