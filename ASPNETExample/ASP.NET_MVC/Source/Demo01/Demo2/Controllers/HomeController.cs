using System;
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
            Product product = new Product();
            product.Name = "pd_01"; // 상품의 이름

            string productName = product.Name; // 인스턴스 접근

            // 뷰 생성
            return View("Result", (object)$"상품명: {productName}"); // 두번째 인자를 넘길때 Object로 형변환 해서 넘겨야 한다.
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