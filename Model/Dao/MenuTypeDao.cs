using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class MenuTypeDao
    {
        MobileDbContext db = null;
        public MenuTypeDao()
        {
            db = new MobileDbContext();
        }

        public List<MenuType> ListAllMT()
        {
            return db.MenuType.ToList();
        }
    }
}
