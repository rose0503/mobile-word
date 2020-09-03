using MobileWeb.Common;
using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileWeb.Areas.Admin.Controllers
{
    public class ContentController : BaseController
    {
        // GET: Admin/Content
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new ContentDao();
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
        public ActionResult Create(Content model )
        {
            if (ModelState.IsValid)
            {
                //var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                //model.CreateBy = session.UserName;
                ////var culture = Session[CommonConstants.CurrentCulture];
                ////model.Language = culture.ToString();
                //new ContentDao().Create(model);
                //return RedirectToAction("Index");

                var dao = new ContentDao();
                if (dao.CheckProductName(model.Name))
                {
                    ModelState.AddModelError("", "Tên tin tức đã tồn tại");
                }
                else
                {
                    var p = new Content();
                    p.Name = model.Name;
                    p.MetaTitle = model.MetaTitle;
                    p.Description = model.Description;
                    p.CreateDate = DateTime.Now;
                    p.Status = true;
                    p.Image = model.Image;
                    p.Detail = model.Detail;
                    p.CategoryID = model.CategoryID;
                    p.IsDelete = false;
                    long id = dao.Insert(p);
                    if (id > 0)
                    {
                        SetAlert("Thêm tin tức thành công", "success");
                        return RedirectToAction("Index", "Content");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Thêm tin tức không thành công");

                    }
                }
             }
                SetViewBag();
            return View();
        }
        public void SetViewBag(long? selectedId = null)
        {
            var dao = new CategoryDao();
            ViewBag.CategoryID = new SelectList(dao.ListAll(), "ID", "Name", selectedId);
        }

        ///////

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var dao = new ContentDao();
            var content = dao.GetByID(id);

            SetViewBag(content.CategoryID);

            return View(content);
        }

        [HttpPost]
        public ActionResult Edit(Content model)
        {
            if (ModelState.IsValid)
            {
                var dao = new ContentDao();
                model.ModifiedDate = DateTime.Now;
                var result = dao.Update(model);
                if (result)
                {
                    SetAlert("Sửa tin tức thành công", "success");
                    return RedirectToAction("Index", "Content");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật tin tức không thành công");
                }
            }
            SetViewBag(model.CategoryID);
            return View("Edit");
        }

                       
        
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new ContentDao().Delete(id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new ContentDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}