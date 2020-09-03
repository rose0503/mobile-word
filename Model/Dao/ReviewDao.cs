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
            List<Review> model = db.Review.Where(x => x.ProductId == reviewId).OrderByDescending(x => x.DatePost).ToList();
            return model;
        }

    }
}
