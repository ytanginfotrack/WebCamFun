using System;
using System.IO;
using System.Threading.Tasks;
using System.Web.Mvc;
using Amazon.S3;
using Amazon.S3.Model;
using WebCamFun.Hubs;

namespace WebCamFun.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDashboardHub _dashboardHub;

        public HomeController(IDashboardHub dashboardHub)
        {
            _dashboardHub = dashboardHub;
            _dashboardHub.Run();
        }

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

        

        
    }
}