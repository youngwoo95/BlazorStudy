using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace Demo2.Models
{
    public static class ExtMethod
    {
        // 참고
        /*
        public static int TotPrice(this ShopCart shopCart) 
        {
            int tot = 0;
            foreach(Product prod in shopCart.ProdList)
            {
                tot += prod.Price;
            }

            return tot;
        }
        */

        public static int TotPrice(this IEnumerable<Product> ProductEnum)
        {
            int tot = 0;
            foreach(Product prod in ProductEnum)
            {
                tot += prod.Price;
            }

            return tot;
        }
    }
}