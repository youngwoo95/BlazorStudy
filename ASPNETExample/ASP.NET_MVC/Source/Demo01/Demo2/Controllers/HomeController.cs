﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Demo2.Models;

namespace Demo2.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public string Index()
        {
            return "ASP.NET 프로그래밍을 위한 필수 기능들 알아보기";
        }

        public ViewResult AutoProperty()
        {
            // Product 객체를 생성
            Product product = new Product();
            product.Name = "pd_01"; // 상품의 이름

            string productName = product.Name; // 인스턴스 접근

            // 뷰 생성
            return View("Result", (object)$"상품명: {productName}"); // 두번째 인자를 넘길때 Object로 형변환 해서 넘겨야 한다.
        }

        public ViewResult CreateProduct()
        {
            Product product = new Product();
            
            // 속성값 설정하기
            product.ProductID = 2000;
            product.Cat = "Sports";
            product.Name = "양말";
            product.ProdDesc = "축구용 양말";
            product.Price = 10000;

            return View("Result", (object)String.Format($"카테고리 : {product.Cat}"));
        }

        public ViewResult CreateCollection()
        {
            string[] strArray = { "사과", "수박", "포도", "오렌지" };

            List<int> myList = new List<int>
            {
               100,
               200,
               300,
               400
            };

            Dictionary<string, int> dic = new Dictionary<string, int>
            {
                { "사과", 11},
                { "수박", 22},
                { "포도", 33},
                { "오렌지",44}
            };

            return View("Result", (object)strArray[2]);
        }

        public ViewResult UseExtMethod()
        {
            // 클래스를 이용하는 방법
            ShopCart cart = new ShopCart
            {
                ProdList = new List<Product>
                {
                    new Product() { Name = "참치", Price = 10000 },
                    new Product() { Name = "햄", Price = 12000 },
                    new Product() { Name = "라면", Price = 5000 },
                    new Product() { Name = "맥주", Price = 20000 }
                }
            };
            //return View("Result", (object)String.Format($"총 합계는 : {cart.TotPrice()} 입니다."));

            // 인터페이스를 이용하는 방법
            IEnumerable<Product> ProdList = new ShopCart
            {
                ProdList = new List<Product>
                {
                    new Product() { Name = "참치", Price = 10000 },
                    new Product() { Name = "햄", Price = 12000 },
                    new Product() { Name = "라면", Price = 5000 },
                    new Product() { Name = "맥주", Price = 20000 }
                }
            };

            /*
            int total = 0;
            foreach (var item in ProdList)
            {
                total += item.Price;
            }

            return View("Result", (object)String.Format($"총 합계는 : {total} 입니다."));
            */

            // 배열을 이용하는 방법
            Product[] prodArray =
            {
                new Product() { Name = "참치", Price = 10000 },
                new Product() { Name = "햄", Price = 12000 },
                new Product() { Name = "라면", Price = 5000 },
                new Product() { Name = "맥주", Price = 20000 }
            };

            int total = 0;
            foreach (var item in prodArray)
            {
                total += item.Price;
            }

            return View("Result", (object)String.Format($"총 합계는 : {total} 입니다."));


        }

        public ViewResult UseFilterMethod()
        {
            IEnumerable<Product> products = new ShopCart
            {
                ProdList = new List<Product>
                {
                    new Product() { Name = "축구공", Cat = "스포츠", Price = 30000 },
                    new Product() { Name = "참치", Cat = "식료품", Price = 10000 },
                    new Product() { Name = "농구공", Cat = "스포츠", Price = 50000 },
                    new Product() { Name = "라면", Cat = "식료품", Price = 5000 }
                }
            };

            Func<Product, bool> catFilter = delegate (Product prod)
            {
                return prod.Cat == "스포츠";
            };

            int total = 0;
            foreach(Product prod in products.Filter(catFilter))
            {
                total += prod.Price;
            }


            /*
            foreach(Product prod in products.FilterCategory("스포츠"))
            {
                total += prod.Price;
            }
            */

            return View("Result",(object)String.Format($"필터 합계는 : {total} 입니다."));
        }




    }
}

/*                                                                                  
 [ActionResult]의 파생 클래스

    - ViewResult                    HTML을 리턴                                      View()
    - PartialViewResult             HTML을 리턴                                      PartialView()

    - EmptyResult                   빈결과

    - contentResult                 문자열 리턴                                      Content()

    - FileContentResult
    - FilePathResult                파일을 리턴                                      File()
    - FileStreamResult

    - JavascriptResult              자바스크립트 리턴   JavaScript()

    - JsonResult                    JSON 리턴           Json()

    - RedirectResult                                                                 Redirect()
    - RedirectToRouteResult         새로운 URL 또는 Action으로 Redirect              RedirectToRoute()
 */



/*
public ActionResult Index()
{
    return View();
}
*/