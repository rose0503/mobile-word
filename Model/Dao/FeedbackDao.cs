using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class FeedbackDao
    {
        MobileDbContext db = null;
        public FeedbackDao()
        {
            db = new MobileDbContext();
        }
               
        //allds
        public List<Feedback> ListAll()
        {
            return db.Feedback.Where(x => x.Status == true).OrderBy(x => x.CreateDate).ToList();
        }
        //finfid
        public Feedback ViewDetail(long id)
        {
            return db.Feedback.Find(id);
        }


        //Create
        public long Insert(Feedback entity)
        {
            db.Feedback.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }


        
        //delete
        public bool Delete(int id)
        {
            try
            {
                var product = db.Feedback.Find(id);
                db.Feedback.Remove(product);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        //timkiem
        public IEnumerable<Feedback> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Feedback> model = db.Feedback;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.Email.Contains(searchString) || x.Content.Contains(searchString));
            }

            return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
        }
    }
}
