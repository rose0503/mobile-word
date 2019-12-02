using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class OrderDao
    {
        MobileDbContext db = null;
        public OrderDao()
        {
            db = new MobileDbContext();
        }
        public long Insert(Order order)
        {
            db.Order.Add(order);
            db.SaveChanges();
            return order.ID;
        }
    }
}
