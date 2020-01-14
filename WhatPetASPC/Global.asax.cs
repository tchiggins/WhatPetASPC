using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WhatPetASPC.App_Start;
namespace WhatPetASPC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            // Load the static data
            DataSetup.PetClass.ClassTable();
            DataSetup.PetClass.CSVImport();
            DataSetup.Species.ClearTable();
            DataSetup.Species.CSVImport();
            DataSetup.PetTypes.ClearTable();
            DataSetup.PetTypes.CSVImport();
        }
    }
}