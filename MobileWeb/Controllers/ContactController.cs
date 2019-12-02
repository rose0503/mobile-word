using Common;
using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace MobileWeb.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            var model = new ContactDao().GetActiveContact();
            return View(model);
        }

        [HttpPost]
        public JsonResult Send(string name, string mobile, string address, string email, string content)
        {
            var feedback = new Feedback();
            feedback.Name = name;
            feedback.Email = email;
            feedback.CreateDate = DateTime.Now;
            feedback.Phone = mobile;
            feedback.Content = content;
            feedback.Address = address;

            var id = new ContactDao().InsertFeedBack(feedback);
            if (id > 0)
            {
                string fb = System.IO.File.ReadAllText(Server.MapPath("~/Content/client/template/feedback.html"));

                fb = fb.Replace("{{CustomerName}}", name);
                fb = fb.Replace("{{Phone}}", mobile);
                fb = fb.Replace("{{Email}}", email);
                fb = fb.Replace("{{Address}}", address);
                fb = fb.Replace("{{Content}}", content);
                var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                //new MailHelper().SendMail(email, "Đơn hàng mới từ cửa hàng điện thoại MobileWord", content);
                new MailHelper().SendMail(toEmail, "Có phản hồi/yêu cầu từ khách hàng tại 'MobileWord'", fb);

                return Json(new
                {
                    status = true
                });
                //send mail
                
            }

            else
                return Json(new
                {
                    status = false
                });

        }
    }
}