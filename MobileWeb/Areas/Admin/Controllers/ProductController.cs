using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileWeb.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Admin/Product
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new ProductDao();
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
        public ActionResult Create(Product model, HttpPostedFileBase fileUpload)
        {            
            if (ModelState.IsValid)
            {             

                var dao = new ProductDao();
                if (dao.CheckProductName(model.Name))
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại");
                }
                
                else
                {
                    var product = new Product();
                    product.Name = model.Name;
                    product.MetaTitle = model.MetaTitle;
                    product.Description = model.Description;
                    //product.Image = model.Image;
                    product.Price = model.Price;
                    product.PromotionPrice = model.PromotionPrice;
                    product.Quantily = model.Quantily;
                    product.CategoryID = model.CategoryID;
                    product.Warranty = model.Warranty;
                    product.Size = model.Size;
                    product.Memory = model.Memory;
                    product.Weight = model.Weight;
                    product.Color = model.Color;
                    product.Chip = model.Chip;
                    product.CPU = model.CPU;
                    product.OS = model.OS;
                    product.Ram = model.Ram;
                    product.Battery = model.Battery;
                    product.Screen = model.Screen;
                    product.FontCamera = model.FontCamera;
                    product.RearCameraNo1 = model.RearCameraNo1;
                    product.RearCameraNo2 = model.RearCameraNo2;
                    product.Sim = model.Sim;
                    product.Connect = model.Connect;
                    product.SpecialFeatures = model.SpecialFeatures;
                    product.CreateDate = DateTime.Now;
                    product.Status = model.Status =true;
                    product.IncludeVAT = model.IncludeVAT= true;
                    product.IsDelete =model.IsDelete =  false;
                    product.ViewCount = model.ViewCount=  0;
                    product.TopHot = model.TopHot;

                    //if (fileUpload != null)
                    //{
                    //    string path = Path.Combine(Server.MapPath("~/Data/DataProduct"), Path.GetFileName(fileUpload.FileName));
                    //    fileUpload.SaveAs(path);

                    //}
                    //Lưu tên file
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    //Lưu đường dẫn của file
                    var path = Path.Combine(Server.MapPath("/Data/images"), fileName);

                    //Kiểm tra hình ảnh đã tồn tại chưa
                    if (System.IO.File.Exists(path))
                    {
                        ModelState.AddModelError("", "Hình ảnh đã tồn tại");
                    }
                    else
                    {
                        fileUpload.SaveAs(path);
                    }

                    product.Image = model.Image = fileUpload.FileName;

                    var result = dao.Insert(product);
                    if (result > 0)
                    {
                        SetAlert("Thêm user thành công", "success");
                        return RedirectToAction("Index", "Product");
                        
                    }
                    else
                    {
                        ModelState.AddModelError("", "Tạo sản phẩm không thành công.");
                    }
                }
            }
            SetViewBag();
            return View(model);
        }       
        

        //edit
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var pro = new ProductDao().ViewDetail(id);
            SetViewBag();
            return View(pro);
        }
        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductDao();
                if (fileUpload != null)
                {
                    string path = Path.Combine(Server.MapPath("~/Data/DataProduct"), Path.GetFileName(fileUpload.FileName));
                    fileUpload.SaveAs(path);

                }
                ////image
                ////Lưu tên file
                //var fileName = Path.GetFileName(fileUpload.FileName);
                ////Lưu đường dẫn của file
                //var path = Path.Combine(Server.MapPath("~/Data/DataProduct"), fileName);

                ////Kiểm tra hình ảnh đã tồn tại chưa
                //if (System.IO.File.Exists(path))
                //{
                //    ModelState.AddModelError("", "Hình ảnh đã tồn tại");

                //    //ViewBag.ThongBao = "Hình ảnh đã tồn tại";
                //}
                //else
                //{
                //    fileUpload.SaveAs(path);
                //}
                
                product.Image = fileUpload.FileName;

                
                product.ModifiedDate = DateTime.Now;
                var result = dao.Update(product);
                if (result)
                {
                    SetAlert("Sửa sản phẩm thành công", "success");
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật sản phẩm không thành công");
                }
            }
            SetViewBag();
            return View("Edit");
        }


        //delete
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new ProductDao().Delete(id);

            return RedirectToAction("Index");
        }

        public JsonResult ChangeStatus(long id)
        {
            var result = new ProductDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
        public void SetViewBag(long? selectedId = null)
        {
            var dao = new ProductDao();
            ViewBag.CategoryID = new SelectList(dao.ListAll(), "ID", "Name", selectedId);
        }
    }
}