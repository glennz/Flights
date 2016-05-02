using System.Web;
using System.Web.Optimization;

namespace App
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery-ui-1.10.4.custom.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include("~/Scripts/angular.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/flight").Include(
                        "~/Scripts/app/flightInfo/flightInfo.js",
                        "~/Scripts/app/flightInfo/components/section/section.component.js",
                        "~/Scripts/app/flightInfo/components/gates/gates.component.js",
                        "~/Scripts/app/flightInfo/components/flights/flights.component.js",
                        "~/Scripts/app/flightInfo/components/flights/modal/addFlightModal.controller.js",
                        "~/Scripts/app/flightInfo/components/flights/modal/updateFlightModal.controller.js",
                        "~/Scripts/app/flightInfo/components/flights/modal/assignToGateModal.controller.js",
                        "~/Scripts/app/flightInfo/components/message/message.component.js",
                        "~/Scripts/app/flightInfo/components/datetimepicker/datetimepicker.js",
                        "~/Scripts/app/services/flightService.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/ui-bootstrap-tpls-1.3.2.min.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/font-awesome/css/font-awesome.min.css",
                      "~/Content/jquery-ui-1.10.4.custom.min.css"));
        }
    }
}
