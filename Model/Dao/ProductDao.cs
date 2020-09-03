using Model.EF;
using Model.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ProductDao
    {
        MobileDbContext db = null;
        public ProductDao()
        {
            db = new MobileDbContext();
        }


        public List<Product> ListNewProduct(int top)
        {
            return db.Product.Where(x=>x.Status == true).OrderByDescending(x => x.CreateDate).Take(top).ToList();
        }

        public List<Product> ListFeatureProduct(int top)
        {
            return db.Product.Where(x => x.TopHot != null && x.TopHot > DateTime.Now && x.Status== true).OrderByDescending(x => x.CreateDate).Take(top).ToList();
        }

        public List<Product> ListRelatedProducts(long productId)
        {
            var product = db.Product.Find(productId);
            return db.Product.Where(x => x.ID != productId && x.CategoryID == product.CategoryID).ToList();
        }

        public List<string> ListName(string keyword)
        {
            return db.Product.Where(x => x.Name.Contains(keyword)).Select(x => x.Name).ToList();
        }
        public Product ViewDetail(long id)
        {
            return db.Product.Find(id);
        }

        //public List<ProductViewModel> ListByCategoryId(long categoryID, ref int totalRecord, int pageIndex = 1, int pageSize = 2)
        //{
        //    totalRecord = db.Product.Where(x => x.CategoryID == categoryID).Count();
        //    //var model = db.Product.Where(x => x.CategoryID == categoryID);
        //    var model = (from a in db.Product
        //                 join b in db.ProductCategory
        //                 on a.CategoryID equals b.ID
        //                 where a.CategoryID == categoryID
        //                 select new ProductViewModel()
        //                 {
        //                     CateMetaTitle = b.MetaTitle,
        //                     CateName = b.Name,
        //                     CreateDate = a.CreateDate,
        //                     ID = a.ID,
        //                     Image = a.Image,
        //                     Name = a.Name,
        //                     MetaTitle = a.MetaTitle,
        //                     Price = a.Price
        //                 });
        //    //).AsEnumerable().Select(x => new ProductViewModel()
        //    //{
        //    //    CateMetaTitle = x.MetaTitle,
        //    //    CateName = x.Name,
        //    //    CreatedDate = x.CreatedDate,
        //    //    ID = x.ID,
        //    //    Images = x.Images,
        //    //    Name = x.Name,
        //    //    MetaTitle = x.MetaTitle,
        //    //    Price = x.Price
        //    //});
        //    model.OrderByDescending(x => x.CreateDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
        //    return model.ToList();
        //}

        //allproduct
        public List<ProductViewModel> ListAllProduct( ref int totalRecord, int pageIndex = 1, int pageSize = 4)
        {
            totalRecord = db.Product.Where( x => x.Status == true).Count();
            var model = (from a in db.Product
                         join b in db.ProductCategory
                         on a.CategoryID equals b.ID
                         where a.CategoryID == b.ID && a.Status == true
                         select new
                         {
                             CateMetaTitle = b.MetaTitle,
                             CateName = b.Name,
                             CreatedDate = a.CreateDate,
                             ID = a.ID,
                             Images = a.Image,
                             Name = a.Name,
                             MetaTitle = a.MetaTitle,
                             Price = a.Price,
                             PromotionPrice =a.PromotionPrice
                         }).AsEnumerable().Select(x => new ProductViewModel()
                         {
                             CateMetaTitle = x.MetaTitle,
                             CateName = x.Name,
                             CreateDate = x.CreatedDate,
                             ID = x.ID,
                             Image = x.Images,
                             Name = x.Name,
                             MetaTitle = x.MetaTitle,
                             Price = x.Price,
                             PromotionPrice=x.PromotionPrice
                         });
            model.OrderByDescending(x => x.CreateDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return model.ToList();
        }

        

        public List<ProductViewModel> ListByCategoryId(long categoryID, ref int totalRecord, int pageIndex = 1, int pageSize = 10)
        {
            totalRecord = db.Product.Where(x => x.CategoryID == categoryID).Count();
            var model = (from a in db.Product
                         join b in db.ProductCategory
                         on a.CategoryID equals b.ID
                         where a.CategoryID == categoryID
                         select new
                         {
                             CateMetaTitle = b.MetaTitle,
                             CateName = b.Name,
                             CreatedDate = a.CreateDate,
                             ID = a.ID,
                             Images = a.Image,
                             Name = a.Name,
                             MetaTitle = a.MetaTitle,
                             Price = a.Price,
                             PromotionPrice = a.PromotionPrice
                         }).AsEnumerable().Select(x => new ProductViewModel()
                         {
                             CateMetaTitle = x.MetaTitle,
                             CateName = x.Name,
                             CreateDate = x.CreatedDate,
                             ID = x.ID,
                             Image = x.Images,
                             Name = x.Name,
                             MetaTitle = x.MetaTitle,
                             Price = x.Price,
                             PromotionPrice = x.PromotionPrice
                         });
            model.OrderByDescending(x => x.CreateDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return model.ToList();
        }
        public List<ProductViewModel> Search(string keyword, ref int totalRecord, int pageIndex = 1, int pageSize = 2)
        {
            totalRecord = db.Product.Where(x => x.Name == keyword).Count();
            var model = (from a in db.Product
                         join b in db.ProductCategory
                         on a.CategoryID equals b.ID
                         where a.Name.Contains(keyword)
                         select new
                         {
                             CateMetaTitle = b.MetaTitle,
                             CateName = b.Name,
                             CreatedDate = a.CreateDate,
                             ID = a.ID,
                             Images = a.Image,
                             Name = a.Name,
                             MetaTitle = a.MetaTitle,
                             Price = a.Price,
                             PromotionPrice = a.PromotionPrice
                         }).AsEnumerable().Select(x => new ProductViewModel()
                         {
                             CateMetaTitle = x.MetaTitle,
                             CateName = x.Name,
                             CreateDate = x.CreatedDate,
                             ID = x.ID,
                             Image = x.Images,
                             Name = x.Name,
                             MetaTitle = x.MetaTitle,
                             Price = x.Price,
                             PromotionPrice = x.PromotionPrice
                         });
            model.OrderByDescending(x => x.CreateDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return model.ToList();
        }


        //Create
        public long Insert(Product entity)
        {
            db.Product.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }


        //Edit
        public bool Update(Product model)
        {
            try
            {
                var product = db.Product.Find(model.ID);
                product.Name = model.Name;

                
                //product.MetaTitle = model.MetaTitle;
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
                product.ModifiedDate = DateTime.Now;
                //product.Status = true;
                //product.IncludeVAT = true;
                //product.IsDelete = false;
                //product.ViewCount = 0;
                product.TopHot = model.TopHot;
                                
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //logging
                return false;
            }

        }

        //delete
        public bool Delete(int id)
        {
            try
            {
                var product = db.Product.Find(id);
                db.Product.Remove(product);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public IEnumerable<Product> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Product> model = db.Product;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.Name.Contains(searchString));
            }

            return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
        }

        //
        public bool ChangeStatus(long id)
        {
            var product = db.Product.Find(id);
            product.Status = !product.Status;
            db.SaveChanges();
            return product.Status;
        }

        public List<ProductCategory> ListAll()
        {
            return db.ProductCategory.Where(x => x.Status == true).ToList();
        }

        public bool CheckProductName(string ProName)
        {
            return db.Product.Count(x => x.Name == ProName) > 0;
        }
    }
}
