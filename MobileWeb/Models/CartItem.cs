using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileWeb.Models
{
    [Serializable]
    public class CartItem
    {
        public Product Product { set; get; }
        public int Quantity { set; get; }

        public static decimal? TongTienHoaDon(List<CartItem> product)
        {
            decimal? tong = 0;
            foreach (CartItem item in product)
            {
                tong += item.Product.Price * item.Product.Quantily;
            }
            return tong;
        }
    }
}