using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class SlideDao
    {
        MobileDbContext db = null;
        public SlideDao()
        {
            db = new MobileDbContext();
        }
        //allds
        public List<Slide> ListAll()
        {
            return db.Slide.Where(x => x.Status == true).OrderBy(y => y.DisplayOrder).ToList();
        }
        //finfid
        public Slide ViewDetail(long id)
        {
            return db.Slide.Find(id);
        }
               
       
        
        //Create
        public int Insert(Slide entity)
        {

            db.Slide.Add(entity);
            db.SaveChanges();
            return entity.ID;
            
        }


        //Edit
        public bool Update(Slide model)
        {
            try
            {
                var product = db.Slide.Find(model.ID);
                product.Image = model.Image;
                product.DisplayOrder = model.DisplayOrder;
                product.Status = true;

                product.Link = model.Link;
                
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
                var product = db.Slide.Find(id);
                db.Slide.Remove(product);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

//timkiem
        public IEnumerable<Slide> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Slide> model = db.Slide;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Link.Contains(searchString) || x.Link.Contains(searchString));
            }

            return model.OrderByDescending(x => x.DisplayOrder).ToPagedList(page, pageSize);
        }


        public bool ChangeStatus(long id)
        {
            var product = db.Slide.Find(id);
            product.Status = !product.Status;
            db.SaveChanges();
            return product.Status;
        }

       

        ////ktraten
        //public bool CheckProductName(string ProName)
        //{
        //    return db.Slide.Count(x => x.Text == ProName) > 0;
        //}
    }
}
