using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

// HomeController는 모든 웹프로그램의 시작점이다.
// Request 요청을 가장먼저 받는곳이 Controller이다.
namespace Demo01.Controllers
{
    // 모든 컨트롤러는 System.Web.Mvc.Controller에서 파생된다.  
    public class HomeController : Controller
    {
        // GET: Home

        // 액션 메서드에서 ViewResult외에도 RedirectResult 또는 HttpUnauthorizedResult라는 Result 객체를 사용할 수 있다.
        // 이와 같은 객체들을 Action Result라고 한다. 이 개체들은 ActionResult 클래스에서 파생된 객체들이다.

        // RedirectResult : 브라우저가 다른 URL로 재요청 하게된다.
        // HttpUnauthorizedResult : 사용자 로그인을 하도록 하게 한다.
        /*
        public ViewResult Index()
        {
            // ViewResult 객체 리턴
            return View(); // View() 메서드는 ViewResult 객체를 생성하는 메서드
        }
        */

        public ViewResult Index()
        {
            int hour = DateTime.Now.Hour;
            ViewBag.Insa = hour < 12 ?  "좋은 아침" : "즐거운 오후";

            return View();
        }


        // Home, / 모두다 Index()로 라우팅처리된다.
        //public string Index()
        //{
        //    return "Hello World!!!";
        //}


        /*
        public ActionResult Index()
        {
            return View();
        }
        */



    }
}
