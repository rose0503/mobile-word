using Model.EF;
using System;
using System.Collections.Generic;
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

        public List<Slide> ListAll()
        {
            return db.Slide.Where(x => x.Status == true).OrderBy(y => y.DisplayOrder).ToList();
        }
    }
}
