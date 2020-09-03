using Model.EF;
using Model.ViewModel;
using PagedList;
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
        //allds
        public List<Order> ListAllNew()
        {
            return db.Order.Where(x => x.Status == 1).OrderBy(x => x.CreateDate).ToList();
        }

        //allds
        public List<Order> ListAllFinish()
        {
            return db.Order.Where(x => x.Status == 4).OrderBy(x => x.CreateDate).ToList();
        }
        //finfid
        public Order ViewDetail(long id)
        {
            return db.Order.Find(id);
        }

        //timkiem  new
        public IEnumerable<Order> ListAllPagingNew(string searchString, int page, int pageSize)
        {
            IQueryable<Order> model = db.Order;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x =>  x.ShipName.Contains(searchString) || x.ShipMobile.Contains(searchString) || x.ShipEmail.Contains(searchString) || x.ShipAddress.Contains(searchString));
            }

            return model.Where(x => x.Status == 1 || x.Status == 2 || x.Status == 3 ).OrderByDescending(x => x.CreateDate ).ToPagedList(page, pageSize);
        }

        //timkiem finish
        public IEnumerable<Order> ListAllPagingFinish(string searchString, int page, int pageSize)
        {
            IQueryable<Order> model = db.Order;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Status == 4 || x.ShipName.Contains(searchString) || x.ShipMobile.Contains(searchString) || x.ShipEmail.Contains(searchString) || x.ShipAddress.Contains(searchString));
            }

            return model.Where(x => x.Status == 4).OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
        }

        //timkiem cancel
        public IEnumerable<Order> ListAllPagingCancel(string searchString, int page, int pageSize)
        {
            IQueryable<Order> model = db.Order;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.ShipName.Contains(searchString) || x.ShipMobile.Contains(searchString) || x.ShipEmail.Contains(searchString) || x.ShipAddress.Contains(searchString));
            }

            return model.Where(x => x.Status == 5 || x.Status ==6).OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
        }
        //Edit
        public bool Update(Order entity)
        {
            try
            {
                var user = db.Order.Find(entity.ID);
                
                user.Status = entity.Status;                
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //logging
                return false;
            }

        }
        public int ChangeStatus(long id)
        {
            var product = db.Order.Find(id);
            product.Status = 5;
            db.SaveChanges();
            return product.Status;
        }

        public List<CartDetailView> ListProductOrder(long orderid)
        {
            var order = db.Order.Find(orderid);
            var model = (from a in db.Product
                         join b in db.OrderDetail
                         on a.ID equals b.ProductID
                         join c in db.Order
                         on b.OrderID equals c.ID
                         where a.ID == b.ProductID && b.OrderID == c.ID && a.Status == true
                         select new
                         {
                             Name = a.Name,
                             Color = a.Color,
                             Price = a.Price,
                             PromotionPrice = a.PromotionPrice,
                             ID = a.ID,
                             CreateDate = a.CreateDate,
                             Quantity = b.Quantity,
                             PriceTotal = b.Price,
                             OrderID = c.ID
                         }).AsEnumerable().Select(x => new CartDetailView()
                         {
                             Name = x.Name,
                             Color = x.Color,
                             Price = x.Price,
                             PromotionPrice = x.PromotionPrice,
                             ID = x.ID,
                             Quantity = x.Quantity,
                             CreateDate = x.CreateDate,
                             OrderID = x.OrderID
                             //PriceTotal = x.Price
                         });
            model.OrderByDescending(x => x.CreateDate);
            return model.ToList();
        }

        public List<CartDetailView> ReportOrder(DateTime? startdate, DateTime? enddate)
        {
            //var order = db.Order.Find(orderid);
            var model = (from a in db.Product
                         join b in db.OrderDetail
                         on a.ID equals b.ProductID
                         join c in db.Order
                         on b.OrderID equals c.ID
                         where a.ID == b.ProductID && b.OrderID == c.ID && c.Status == 4
                         select new
                         {
                             Name = a.Name,
                             Color = a.Color,
                             //Price = a.Price,
                             PromotionPrice = a.PromotionPrice,
                             ID = a.ID,
                             CreateDate = c.CreateDate,
                             Quantity = b.Quantity,
                             PriceToTal = b.Price,
                             OrderID = c.ID,
                             Status = c.Status,
                             UserName = c.ShipName,
                             
                         }).AsEnumerable().Select(x => new CartDetailView()
                         {
                             Name = x.Name,
                             Color = x.Color,
                             PriceToTal = x.PriceToTal,
                             PromotionPrice = x.PromotionPrice,
                             ID = x.ID,
                             Quantity = x.Quantity,
                             CreateDate = x.CreateDate,
                             OrderID = x.OrderID,
                             Status = x.Status,
                             UserName = x.UserName,
                             //PriceTotal = x.Price
                         });
            model.Where(x => x.CreateDate >= startdate && x.CreateDate <= enddate && x.Status == 4).OrderByDescending(x => x.CreateDate);
            return model.ToList();
        }
    }
}
