using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class CategoryDao
    {
        MobileDbContext db = null;
        public CategoryDao()
        {
            db = new MobileDbContext();
        }
        public List<Category> ListAll()
        {
            return db.Category.Where(x => x.Status == true).ToList();
        }
        public Category ViewDetail(long id)
        {
            return db.Category.Find(id);
        }

        //allds
        public List<Category> ListAllCC()
        {
            return db.Category.Where(x => x.Status == true).OrderBy(x => x.CreateDate).ToList();
        }
        //finfid
        public Category ViewDetaill(long id)
        {
            return db.Category.Find(id);
        }


        //Create
        public long Insert(Category entity)
        {
            db.Category.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }


        //Edit
        public bool Update(Category model)
        {
            try
            {
                var product = db.Category.Find(model.ID);
                product.Name = model.Name;
                product.ModifiedDate = DateTime.Now;
                product.Status = true;
                
                product.IsDelete = false;
                product.ShowOnHome = false;
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
                var product = db.Category.Find(id);
                db.Category.Remove(product);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        //timkiem
        public IEnumerable<Category> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Category> model = db.Category;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.Name.Contains(searchString));
            }

            return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
        }

        //
        public bool ChangeStatus(long id)
        {
            var product = db.Category.Find(id);
            product.Status = !product.Status;
            db.SaveChanges();
            return product.Status;
        }

        //alldsmuc
        public List<Category> ListAllPC()
        {
            return db.Category.Where(x => x.Status == true).ToList();
        }

        //ktraten
        public bool CheckProductName(string ProName)
        {
            return db.Category.Count(x => x.Name == ProName) > 0;
        }
    }
}
