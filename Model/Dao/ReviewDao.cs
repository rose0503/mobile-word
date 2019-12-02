using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ReviewDao
    {
        MobileDbContext db = null;
        public ReviewDao()
        {
            db = new MobileDbContext();
        }
        public List<Review> ListReview(long reviewId)
        {
            var r = db.Review.Find(reviewId);
            return db.Review.ToList();
        }

    }
}
