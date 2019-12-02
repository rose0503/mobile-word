using Model.EF;
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
    }
}
