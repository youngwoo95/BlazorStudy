using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public static IEnumerable<Product> FilterCategory(this IEnumerable<Product> prodEnum, string catParam)
        {
            
            foreach (Product prod in prodEnum)
            {
                Debug.WriteLine("이게탐");
                if(prod.Cat == catParam)
                {
                    yield return prod;
                }
            }
        }

        public static IEnumerable<Product> Filter(this IEnumerable<Product> prodEnum, Func<Product, bool> selParam)
        {
            foreach(Product prod in prodEnum)
            {
                if (selParam(prod))
                {
                    yield return prod;
                }
            }
        }



    }
}