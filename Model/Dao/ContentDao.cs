using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ContentDao
    {
        MobileDbContext db = null;
        public static string USER_SESSION = "USER_SESSION";
        public ContentDao()
        {
            db = new MobileDbContext();
        }

        public Content GetByID(long id)
        {
            return db.Content.Find(id);
        }

        //allds
        public List<Content> ListAll()
        {
            return db.Content.Where(x => x.Status == true).OrderBy(x => x.CreateDate).ToList();
        }
        //finfid
        public Content ViewDetail(long id)
        {
            return db.Content.Find(id);
        }


        //Create
        public long Insert(Content entity)
        {
            db.Content.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }


        //Edit
        public bool Update(Content model)
        {
            try
            {
                var product = db.Content.Find(model.ID);
                product.Name = model.Name;
                product.ModifiedDate = DateTime.Now;
                product.Status = true;
                product.Image = model.Image;
                product.Description = model.Description;
                product.IsDelete = false;
                product.Detail = model.Detail;
                product.CategoryID = model.CategoryID;
              
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
                var product = db.Content.Find(id);
                db.Content.Remove(product);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        //timkiem
        public IEnumerable<Content> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Content> model = db.Content;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.Name.Contains(searchString));
            }

            return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
        }

        //
        public bool ChangeStatus(long id)
        {
            var product = db.Content.Find(id);
            product.Status = !product.Status;
            db.SaveChanges();
            return product.Status;
        }

        //alldsmuc
        public List<Content> ListAllPC()
        {
            return db.Content.Where(x => x.Status == true).ToList();
        }

        //ktraten
        public bool CheckProductName(string ProName)
        {
            return db.Content.Count(x => x.Name == ProName) > 0;
        }
    }
}
