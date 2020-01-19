using System.Web.Mvc;
namespace WhatPetASPC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return this.View();
        }
        public ActionResult About()
        {
            this.ViewBag.Message = "WhatPet - Select your ideal pet friend.";
            return this.View();
        }
        public ActionResult Contact()
        {
            this.ViewBag.Message = "Contact Us";
            return this.View();
        }
    }
}