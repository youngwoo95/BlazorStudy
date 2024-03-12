using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo2.Models
{
    public class Product
    {
        /// <summary>
        /// 상품의 이름
        /// </summary>
        private string name;

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




    }
}