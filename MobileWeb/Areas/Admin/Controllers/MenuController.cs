using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileWeb.Areas.Admin.Controllers
{
    public class MenuController : BaseController
    {
        // GET: Admin/Menu
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new MenuDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);

            ViewBag.SearchString = searchString;

            return View(model);
            //return View();
        }


        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Menu model)
        {
            if (ModelState.IsValid)
            {
                
                var dao = new MenuDao();
                if (dao.CheckProductName(model.Text))
                {
                    ModelState.AddModelError("", "Tên menu đã tồn tại");
                }
                else
                {
                    var p = new Menu();
                    p.Text = model.Text;
                    p.Link = model.Link;
                    p.DisplayOrder = model.DisplayOrder;
                    p.Target = model.Target = "_self";
                    p.Status = true;
                    p.TypeID = model.TypeID;                    
                    long id = dao.Insert(p);
                    if (id > 0)
                    {
                        SetAlert("Thêm menu thành công", "success");
                        return RedirectToAction("Index", "Menu");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Thêm menu không thành công");

                    }
                }
            }
            SetViewBag();
            return View("Create");
        }
        public void SetViewBag(int? selectedId = null)
        {
            var dao = new MenuTypeDao();
            ViewBag.TypeID = new SelectList(dao.ListAllMT(), "ID", "Name", selectedId);
        }

        ///////

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var dao = new MenuDao();
            var content = dao.ViewDetail(id);

            SetViewBag(content.TypeID);

            return View(content);
        }

        [HttpPost]
        public ActionResult Edit(Menu model)
        {
            if (ModelState.IsValid)
            {
                var dao = new MenuDao();
                
                var result = dao.Update(model);
                if (result)
                {
                    SetAlert("Sửa menu thành công", "success");
                    return RedirectToAction("Index", "Menu");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật menu không thành công");
                }
            }
            SetViewBag(model.TypeID);
            return View("Edit");
        }



        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new MenuDao().Delete(id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new MenuDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}