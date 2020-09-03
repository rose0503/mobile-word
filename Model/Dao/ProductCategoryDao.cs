using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ProductCategoryDao
    {
        MobileDbContext db = null;
        public ProductCategoryDao()
        {
            db = new MobileDbContext();
        }
        //allds
        public List<ProductCategory> ListAll()
        {
            return db.ProductCategory.Where(x => x.Status == true).OrderBy(x => x.DisplayOrder).ToList();
        }
        //finfid
        public ProductCategory ViewDetail(long id)
        {
            return db.ProductCategory.Find(id);
        }


        //Create
        public long Insert(ProductCategory entity)
        {
            db.ProductCategory.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }


        //Edit
        public bool Update(ProductCategory model)
        {
            try
            {
                var product = db.ProductCategory.Find(model.ID);
                product.Name = model.Name;
                product.ModifiedDate = DateTime.Now;
                product.Status = true;
                product.ShowOnHome = false;
                product.IsDelete = false;
                
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
                var product = db.ProductCategory.Find(id);
                db.ProductCategory.Remove(product);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        //timkiem
        public IEnumerable<ProductCategory> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<ProductCategory> model = db.ProductCategory;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.Name.Contains(searchString));
            }

            return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
        }

        //
        public bool ChangeStatus(long id)
        {
            var product = db.ProductCategory.Find(id);
            product.Status = !product.Status;
            db.SaveChanges();
            return product.Status;
        }

        //alldsmuc
        public List<ProductCategory> ListAllPC()
        {
            return db.ProductCategory.Where(x => x.Status == true).ToList();
        }

        //ktraten
        public bool CheckProductName(string ProName)
        {
            return db.ProductCategory.Count(x => x.Name == ProName) > 0;
        }
    }
}
