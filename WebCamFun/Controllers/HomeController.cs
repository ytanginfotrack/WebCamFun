using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCamFun.Hubs;

namespace WebCamFun.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public string Echo(string message)
        {
            return message;
        }

        public void FullScreen(string camId, string message)
        {
            var hub = new DashboardHub();

            hub.FullscreenWebCam(camId, message);
        }
    }
}