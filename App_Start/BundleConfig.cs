using System.Web;
using System.Web.Optimization;

namespace GoldenHeart
{
    public class BundleConfig
    {
        //Дополнительные сведения об объединении см. по адресу: http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-3.1.1.min.js",
                        "~/Scripts/jquery.mask.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/update.css",
                        "~/Content/style.css",
                        "~/Content/animation.css"));

            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                        "~/Scripts/angular.min.js",
                        "~/Scripts/angular.js",
                        "~/Scripts/action.js",
                        "~/Scripts/jquery.scrolly.js",
                        "~/Scripts/wow.min.js"));
        }
    }
}
