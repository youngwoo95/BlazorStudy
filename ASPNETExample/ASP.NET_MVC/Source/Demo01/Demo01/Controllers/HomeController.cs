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
        // Home, / 모두다 Index()로 라우팅처리된다.
        public string Index()
        {
            return "Hello World!!!";
        }


        /*
        public ActionResult Index()
        {
            return View();
        }
        */



    }
}
