using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using form_submission.Models;

namespace form_submission.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public  ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Login5 login)
        {
           

            return View(login);
        }
        public ActionResult Login1(string Username ,string Id,string Gender,string Profession, string Hobbies)
        {
            ViewBag.Username = Request["Username"];
            ViewBag.Id = Request["id"];
            ViewBag.Gender = Request["Gender"];
            ViewBag.Profession = Request["Profession"];
            ViewBag.Hobbies = Request["Hobbies"];

            return View();
        }
        public ActionResult Login2(FormCollection fc)
        {
            ViewBag.Username = fc["Username"];
            ViewBag.Id = fc["id"];
            ViewBag.Gender = fc["Gender"];
            ViewBag.Profession = fc["Profession"];
            ViewBag.Hobbies = fc["Hobbies"];
            return View();
        }
        [HttpGet]
        public ActionResult Signup()
        { 
            return View();
        }
        [HttpPost]
        public ActionResult Signup(Signup Signup)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Login");
            }
            return View(Signup);
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