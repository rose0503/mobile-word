using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
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
        //[ValidateInput(false)]
        public ActionResult Create(Product model)
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
                    product.Image = model.Image;
                    product.Price = model.Price;
                    product.PromotionPrice = model.PromotionPrice;
                    //product.Quantily = model.Quantily;
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
                    product.Status = true;
                    product.IncludeVAT = true;
                    product.ViewCount = 0;
                    product.TopHot = model.TopHot;
                   
                    var result = dao.Insert(product);
                    if (result > 0)
                    {
                        SetAlert("Thêm user thành công", "success");
                        return RedirectToAction("Index", "Product");
                        //ViewBag.Success = "Tạo sản phẩm thành công";
                        //model = new Product();
                        //ModelState.Clear();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Tạo sản phẩm không thành công.");
                    }
                }
            }
            SetViewBag();
            return View("Create");
        }
         
        public void SetViewBag(long? selectedId = null)
        {
            var dao = new ProductDao();
            ViewBag.CategoryID = new SelectList(dao.ListAll(), "ID", "Name", selectedId);
        }


        public JsonResult ChangeStatus(long id)
        {
            var result = new ProductDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}