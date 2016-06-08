using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASPWarehouse.ViewModels;
using DAL.Interfaces;

namespace ASPWarehouse.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IWarehouseUOW _warehouseUow;

        public HomeController(IWarehouseUOW warehouseUow)
        {
            _warehouseUow = warehouseUow;
        }

        public ActionResult Index()
        {
            var vm = new HomeIndexViewModel()
            {
                Article = _warehouseUow.Articles.FindArticleByName("HomeIndex")
            };
            return View(vm);
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
    }
}