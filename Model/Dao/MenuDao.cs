using Model.EF;
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

        public List<Menu> ListByGroupId(int groupId)
        {
            return db.Menu.Where(x => x.TypeID == groupId && x.Status == true).OrderBy(x => x.DisplayOrder).ToList();
        }
    }
}
