using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class CartDetailView
    {
        public long ID { set; get; }
        public string Name { get; set; }

        public string Color { get; set; }
        public decimal? Price { get; set; }
        
        public decimal? PromotionPrice { get; set; }

        public int? Quantity { set; get; }

        public long? ProductID { get; set; }
        public long? UserID { get; set; }
        public long OrderID { get; set; }

        public string UserName { get; set; }

        public string Address { get; set; }

        
        public string Email { get; set; }

        
        public string Phone { get; set; }

        public int Status { get; set; }

        public decimal? PriceToTal { get; set; }
        public DateTime? CreateDate { set; get; }



    }
}
