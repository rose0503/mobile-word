using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileWeb.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        // GET: Admin/Category
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new CategoryDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);

            ViewBag.SearchString = searchString;

            return View(model);
            //return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category pc)
        {
            if (ModelState.IsValid)
            {
                var dao = new CategoryDao();
                if (dao.CheckProductName(pc.Name))
                {
                    ModelState.AddModelError("", "Tên danh mục đã tồn tại");
                }
                else
                {
                    var p = new Category();
                    p.Name = pc.Name;
                    p.MetaTitle = pc.MetaTitle;
                    p.DisplayOrder = pc.DisplayOrder = 1;
                    p.CreateDate = DateTime.Now;
                    p.Status = true;
                    p.ShowOnHome = false;
                    p.IsDelete = false;
                    long id = dao.Insert(p);
                    if (id > 0)
                    {
                        SetAlert("Thêm danh mục thành công", "success");
                        return RedirectToAction("Index", "Category");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Thêm danh mục không thành công");

                    }
                }

            }
            return View("Create");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var pc = new CategoryDao().ViewDetail(id);
            return View(pc);
        }
        [HttpPost]
        public ActionResult Edit(Category pc)
        {
            if (ModelState.IsValid)
            {
                var dao = new CategoryDao();
                pc.ModifiedDate = DateTime.Now;
                var result = dao.Update(pc);
                if (result)
                {
                    SetAlert("Sửa danh mục thành công", "success");
                    return RedirectToAction("Index", "Category");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật danh mục không thành công");
                }
            }
            return View("Edit");
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new CategoryDao().Delete(id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new CategoryDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}