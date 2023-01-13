using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Web.UI;
using Page = System.Web.UI.Page;
using System.IO;

namespace Audi.Controllers
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
        public ActionResult Confirm()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Politicas()
        {
            return View();
        }
        public ActionResult Cookies()
        {
            return View();
        }

        public ActionResult SenEmail()
        {
            string recipient = Request["to"];
            string subject = Request["subject"];
            string body = Request["body"];

            WebMail.SmtpServer = "smtp.gmail.com";
            WebMail.SmtpPort = 587;
            WebMail.SmtpUseDefaultCredentials = true;
            WebMail.EnableSsl = true;
            WebMail.UserName= "ikerfrancos008@gmail.com";
            WebMail.Password = "opueskludrgxaxdh";
            WebMail.Send(to: recipient, subject: subject, body: body, isBodyHtml: true);
            Response.Redirect("/Home/Confirm"); 
            return View();
        }

      
    }
}