using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Serilog;
using WhatPetASPC.App_Start;
namespace WhatPetASPC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.File("C:/Logs/log.txt", outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();
            Log.Information("Starting application");
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            // Load the static data
            DataSetup.CostCategories.ClearTable();
            DataSetup.CostCategories.CSVImport();
            DataSetup.PetClass.ClearTable();
            DataSetup.PetClass.CSVImport();
            DataSetup.Species.ClearTable();
            DataSetup.Species.CSVImport();
            DataSetup.PetTypes.ClearTable();
            DataSetup.PetTypes.CSVImport();
            DataSetup.Questions.ClearTable();
        }
    }
}