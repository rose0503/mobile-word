using Model.EF;
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
        public ProductCategory ViewDetail(long id)
        {
            return db.ProductCategory.Find(id);
        }
    }
}
