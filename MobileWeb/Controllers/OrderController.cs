using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileWeb.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
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
        public ActionResult Delete(long id)
        {
            
            var user = new OrderDao().ViewDetail(id);
            return View(user);
        }




        //
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            //Do something with the posted viewModel
            
           Order user = new OrderDao().ViewDetail(id);
            var dao = new OrderDao();
            if (user.Status == 1)
            {
                user.Status = 5;
                var result = dao.Update(user);
                if (result)
                {
                    ViewBag.Success = "Hủy đơn thành công";
                    //return RedirectToAction("/");
                }
                else
                {
                    ModelState.AddModelError("", "Hủy đơn không thành công");
                }
            }
            else /*if (user.Status == 2 || user.Status == 3)*/
            {
                ModelState.AddModelError("", "Đơn hàng đã được xác nhận và giao hàng. Bạn không thể hủy trong lúc này!!");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DetailOrder(long id)
        {


            var user = new OrderDao().ViewDetail(id);
            ViewBag.CartProducts = new OrderDao().ListProductOrder(user.ID);
            return View(user);

        }


        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new OrderDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }

        
    }
}