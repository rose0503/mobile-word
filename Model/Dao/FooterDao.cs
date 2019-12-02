using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class FooterDao
    {
        MobileDbContext db = null;
        public FooterDao()
        {
            db = new MobileDbContext();
        }
        public Footer GetFooter()
        {
            return db.Footer.SingleOrDefault(x => x.Status == true);
        }
    }
}
