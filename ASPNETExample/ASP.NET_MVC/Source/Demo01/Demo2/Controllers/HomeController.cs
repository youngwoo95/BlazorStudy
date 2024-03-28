using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Demo2.Models;
using System.Text;

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
                    new Product() { Name = "라면", Cat = "식료품", Price = 5000 },
                    new Product() { Name = "야구공", Cat = "스포츠", Price = 10000 },
                    new Product() { Name = "테니스공", Cat = "스포츠", Price = 5000 }
                }
            };

            // var : 명시적으로 변수의 형식을 지정하지 않아도 지역 변수를 선언 하늗네, 이러한 기능을
            // 암시적 형식(Implicit Typing), 묵시적 형식, 형식 추론(Type Inference) 이라고 한다.

            /*
            var myProduct = new Product
            {
                Name = "테니스공",
                Cat = "스포츠",
                Price = 5000
            };
            
             myProduct와 같은 개체를 익명형식 개체라고 한다.
             익명형식 개체는 이니셜라이저에 의해서 정의된 속성만 값을 얻거나 설정할 수 있다.
            */

            // 델리게이트 표현
            /*
            Func<Product, bool> catFilter = delegate (Product prod)
            {
                return prod.Cat == "스포츠";
            };
            */

            // 람다식 표현
            /*
            Func<Product, bool> catFilter = prod => prod.Cat == "스포츠";

            int total = 0;
            foreach(Product prod in products.Filter(catFilter))
            {
                total += prod.Price;
            }
            */

            int total = 0;
            foreach(Product prod in products.Filter(prod=> prod.Cat == "스포츠" && prod.Price >= 20000))
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

        public ViewResult CreateImplicitObj()
        {
            var myProduct = new[]
            {
                new { Name = "참치", Cat = "식료품"},
                new { Name = "양말", Cat = "의류"},
                new { Name = "오렌지", Cat="과일"}
            };

            StringBuilder result = new StringBuilder();

            foreach(var item in myProduct)
            {
                result.Append(item.Name).Append(" ");
            };

            return View("Result", (object)result.ToString());
        }
        
        public ViewResult QueryProducts()
        {
            Product[] products =
            {
                new Product { Name = "축구화", Cat = "스포츠", Price = 100000},
                new Product { Name = "패딩점퍼", Cat = "의류", Price = 300000},
                new Product { Name = "농구공", Cat="스포츠", Price = 30000},
                new Product { Name = "세탁기", Cat="가전", Price = 1500000}
            };

            // 쿼리 결과를 저장하기 위한 배열 선언
            //Product[] queryProducts = new Product[3];

            // 정렬
            //Array.Sort(products, (prod1, prod2) =>
            //{
            //    return Comparer<int>.Default.Compare(prod1.Price, prod2.Price);
            //});

            // 세개의 항목의 결과를 얻어오기
            //Array.Copy(products, queryProducts, 3);

            // 결과를 작성

            //StringBuilder result = new StringBuilder();

            //foreach (Product prod in queryProducts)
            //{
            //    result.AppendFormat($"가격 : {prod.Price}");
            //}

            // LINQ를 이용한 쿼리
            var queryProducts = from pd in products
                                orderby pd.Price descending
                                select new
                                {
                                    pd.Name,
                                    pd.Price
                                };

            // 결과물 작성
            int count = 0;
            StringBuilder result = new StringBuilder();

            foreach (var item in queryProducts)
            {
                result.AppendFormat($"가격 : {item.Price}");
                if (++count == 3)
                {
                    break;
                }
            }

            return View("Result", (object)result.ToString());
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