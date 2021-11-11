using System.Web.Optimization;
using WebHelpers.Mvc5;

namespace BMSA.App.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Bundles/css")
                .Include("~/Content/css/bootstrap.min.css", new CssRewriteUrlTransformAbsolute())
                .Include("~/Content/css/bootstrap-select.css")
                .Include("~/Content/css/bootstrap-datepicker3.min.css")
                .Include("~/Content/css/font-awesome.min.css", new CssRewriteUrlTransformAbsolute())
                .Include("~/Content/css/icheck/blue.min.css", new CssRewriteUrlTransformAbsolute())
                .Include("~/Content/css/AdminLTE.css", new CssRewriteUrlTransformAbsolute())
                .Include("~/Content/css/skins/skin-blue.css"));

            bundles.Add(new ScriptBundle("~/Bundles/js")
                .Include("~/Content/js/plugins/jquery/jquery-3.3.1.js")
                .Include("~/Content/js/plugins/bootstrap/bootstrap.js")
                .Include("~/Content/js/plugins/fastclick/fastclick.js")
                .Include("~/Content/js/plugins/slimscroll/jquery.slimscroll.js")
                .Include("~/Content/js/plugins/bootstrap-select/bootstrap-select.js")
                .Include("~/Content/js/plugins/moment/moment.js")
                .Include("~/Content/js/plugins/datepicker/bootstrap-datepicker.js")
                .Include("~/Content/js/plugins/icheck/icheck.js")
                .Include("~/Content/js/plugins/validator/validator.js")
                .Include("~/Content/js/plugins/inputmask/jquery.inputmask.bundle.js")
                .Include("~/Content/js/adminlte.js")
                .Include("~/Content/js/init.js"));


            bundles.Add(new StyleBundle("~/Bundles/thirdPartyCss")
                .Include("~/Content/DataTables/css/jquery.dataTables.min.css")
                .Include("~/Content/DataTables/css/rowReorder.dataTables.css")
                .Include("~/Content/DataTables/css/responsive.dataTables.css")
                .Include("~/Content/jquery.fancybox.css")
                .Include("~/Content/toastr.css"));


            bundles.Add(new ScriptBundle("~/Bundles/thirdPartyjs")
                .Include("~/Scripts/DataTables/jquery.dataTables.min.js")
                .Include("~/Scripts/DataTables/dataTables.rowReorder.js")
                .Include("~/Scripts/DataTables/dataTables.responsive.js")
                .Include("~/Scripts/bootbox.js")
                .Include("~/Scripts/toastr.js")
                .Include("~/Scripts/Site.js"));

#if DEBUG
            BundleTable.EnableOptimizations = false;
#else
            BundleTable.EnableOptimizations = true;
#endif
        }
    }
}
