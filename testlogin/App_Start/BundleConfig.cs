using System.Web;
using System.Web.Optimization;

namespace testlogin
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/logincss").Include(
                      "~/Content/common/css/xcConfirm.css",
                      "~/Content/css/login.css"));

            bundles.Add(new StyleBundle("~/Content/admincss").Include(
                      "~/Content/common/css/fonts.css",
                      "~/Content/common/css/bootstrap.css",
                      "~/Content/common/css/ggtooltip.css",
                      "~/Content/common/css/loaders.css",
                      "~/Content/css/packagetools.css",
                      "~/Content/common/css/xcConfirm.css",
                      "~/css/packagetools.css",
                      "~/Content/common/css/common.css"
                      ));

            bundles.Add(new ScriptBundle("~/Content/adminjs").Include(
                      "~/Content/common/js/jquery-1.7.1.min.js",
                      "~/Content/common/js/bootstrap.min.js",
                      "~/Content/common/js/jquery.base64.js"
                      //"~/Content/common/js/jquery.cookie.js"
                      //"~/Content/common/js/common.js",
                      //"~/Content/common/js/mark.js",
                      //"~/Content/common/js/ggtooltip.js",
                      //"~/Content/common/js/gg.app.ui.js"
                      ));

        }
    }
}
