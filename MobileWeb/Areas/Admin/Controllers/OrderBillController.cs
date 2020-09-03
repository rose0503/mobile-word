using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileWeb.Areas.Admin.Controllers
{
    public class OrderBillController : Controller
    {
        // GET: Admin/OrderBill
        //public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        //{
        //    var dao = new OrderDao();
        //    var model = dao.ListAllPaging(searchString, page, pageSize);

        //    ViewBag.SearchString = searchString;

        //    return View(model);
        //    //return View();
        //}

        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new OrderDao();
            var model = dao.ListAllPagingNew(searchString, page, pageSize);

            ViewBag.SearchString = searchString;

            return View(model);
            //return View();
        }

        public ActionResult IndexFinish(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new OrderDao();
            var model = dao.ListAllPagingFinish(searchString, page, pageSize);

            ViewBag.SearchString = searchString;

            return View(model);
            //return View();
        }

        public ActionResult IndexCancel(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new OrderDao();
            var model = dao.ListAllPagingCancel(searchString, page, pageSize);

            ViewBag.SearchString = searchString;

            return View(model);
            //return View();
        }
        [HttpGet]
        public ActionResult Edit(long id)
        {
            //HttpSessionState ss = HttpContext.Current.Session;
            //HttpContext.Current.Session["test"] = "test";
            //UserLogin session = (MobileWeb.Common.UserLogin)Session[MobileWeb.Common.CommonConstants.USER_SESSION];

            //id = session.UserID ;

            //HttpContext context = HttpContext.CurrentHandler.ProcessRequest.Session
            //var session = HttpContext.CurrentHandler.ProcessRequest.session;
            var user = new OrderDao().ViewDetail(id);
            return View(user);
        }
        [HttpPost]
        public ActionResult Edit(Order user)
        {
            if (ModelState.IsValid)
            {
                var dao = new OrderDao();

                var result = dao.Update(user);
                if (result)
                {
                    ViewBag.Success = "cập nhật thành công";
                    //return RedirectToAction("/");
                }
                else
                {
                    ModelState.AddModelError("", "cập nhật không thành công");
                }

            }
            return View("Edit");
        }


    }
}