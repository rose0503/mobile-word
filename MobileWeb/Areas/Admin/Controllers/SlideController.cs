using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileWeb.Areas.Admin.Controllers
{
    public class SlideController : BaseController
    {
        // GET: Admin/Slide
        
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new SlideDao();
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
        
        
        public ActionResult Create(Slide model, HttpPostedFileBase fileUpload)
        {
            var dao = new SlideDao();
            var p = new Slide();
            try
            {
                // Your code...
                // Could also be before try if you know the exception occurs in SaveChanges

                if (ModelState.IsValid)
                {
                    
                    p.Image = model.Image;
                    p.Link = model.Link;
                    p.DisplayOrder = model.DisplayOrder;
                    p.CreateDate = DateTime.Now;
                    p.Status = model.Status = true;

                    //Lưu tên file
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    //Lưu đường dẫn của file
                    var path = Path.Combine(Server.MapPath("~/Data"), fileName);

                    //Kiểm tra hình ảnh đã tồn tại chưa
                    if (System.IO.File.Exists(path))
                    {
                        ModelState.AddModelError("", "Hình ảnh đã tồn tại");
                    }
                    else
                    {
                        fileUpload.SaveAs(path);
                    }

                    p.Image = model.Image = fileUpload.FileName;

                    int id = dao.Insert(p);
                    if (id > 0)
                    {
                        SetAlert("Thêm Slide thành công", "success");
                        return RedirectToAction("Index", "Menu");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Thêm slide không thành công");

                    }

                }
            }
            catch (DbEntityValidationException e)
            {
                ModelState.AddModelError("", "Thêm slide không thành công");
                throw;
            }
            return View("Create",model);
        }
       
        ///////

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var pc = new SlideDao().ViewDetail(id);
            return View(pc);
                   
        }

        [HttpPost]
        public ActionResult Edit(Slide model)
        {
            if (ModelState.IsValid)
            {
                var dao = new SlideDao();

                var result = dao.Update(model);
                if (result)
                {
                    SetAlert("Sửa Slide thành công", "success");
                    return RedirectToAction("Index", "Menu");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật Slide không thành công");
                }
            }
            
            return View("Edit");
        }



        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new SlideDao().Delete(id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new SlideDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}