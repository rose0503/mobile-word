using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class MenuDao
    {
        MobileDbContext db = null;
        public MenuDao()
        {
            db = new MobileDbContext();
        }

        ////public List<MenuType> ListAllMT()
        ////{
        ////    return db.MenuType.ToList();
        ////}

        public List<Menu> ListByGroupId(int groupId)
        {
            return db.Menu.Where(x => x.TypeID == groupId && x.Status == true).OrderBy(x => x.DisplayOrder).ToList();
        }

        public List<Menu> ListAll()
        {
            return db.Menu.Where(x => x.Status == true).ToList();
        }
        public Menu ViewDetail(long id)
        {
            return db.Menu.Find(id);
        }

        //allds
        public List<Menu> ListAllCC()
        {
            return db.Menu.Where(x => x.Status == true).OrderBy(x => x.DisplayOrder).ToList();
        }
        //finfid
        public Menu ViewDetaill(long id)
        {
            return db.Menu.Find(id);
        }


        //Create
        public long Insert(Menu entity)
        {
            db.Menu.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }


        //Edit
        public bool Update(Menu model)
        {
            try
            {
                var product = db.Menu.Find(model.ID);
                product.Text = model.Text;
                product.DisplayOrder = model.DisplayOrder;
                product.Status = true;

                product.Target = model.Target = "_self";
                product.TypeID = model.TypeID;
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
                var product = db.Menu.Find(id);
                db.Menu.Remove(product);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        //timkiem
        public IEnumerable<Menu> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Menu> model = db.Menu;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Text.Contains(searchString) || x.Text.Contains(searchString));
            }

            return model.OrderByDescending(x => x.DisplayOrder).ToPagedList(page, pageSize);
        }

        //
        public bool ChangeStatus(long id)
        {
            var product = db.Menu.Find(id);
            product.Status = !product.Status;
            db.SaveChanges();
            return product.Status;
        }

        //alldsmuc
        public List<Menu> ListAllPC()
        {
            return db.Menu.Where(x => x.Status == true).ToList();
        }

        //ktraten
        public bool CheckProductName(string ProName)
        {
            return db.Menu.Count(x => x.Text == ProName) > 0;
        }
    }
}
