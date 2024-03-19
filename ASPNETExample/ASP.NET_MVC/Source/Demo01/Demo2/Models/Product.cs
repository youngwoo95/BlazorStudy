using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo2.Models
{
    public class Product
    {
        
        /*
        private int productID;
        private string cat;
        private string name;
        private string prodDesc;
        private int price;

        public int ProductID
        {
            get
            {
                return productID;
            }
            set
            {
                productID = value;
            }
        }

        public string Cat
        {
            get
            {
                return cat;
            }
            set
            {
                cat = value;
            }
        }

        // 프로퍼티 구현
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        */

        // 자동 구현 프로퍼티(Automatically implemented property / Automatic Property)
        public int ProductID { get; set; }
        public string Cat { get; set; }
        //public string Name { get; set; }
        // 직접 속성 형태를 변경해야 하는 경우
        public string Name { get; set; }
        public string ProdDesc { get; set; }
        public int Price { get; set; }
  
    }
}