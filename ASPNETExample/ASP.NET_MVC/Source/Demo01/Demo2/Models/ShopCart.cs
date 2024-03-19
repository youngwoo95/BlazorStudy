using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo2.Models
{
    public class ShopCart : IEnumerable<Product>
    {
        public List<Product> ProdList { get; set; }

        public IEnumerator<Product> GetEnumerator()
        {
            return ProdList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}