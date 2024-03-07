using Demo01.Models;
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

        /*
        HTTP GET 요청 : 브라우저의 링크 클릭시 요청방식

        HTTP POST 요청 : Html.BeginForm() --> 메서드를 통해서 렌더링 된 폼은 POST로 요청되도록 하고 있다.
        */

        /* GET 방식으로 넘어왔을 때 처리 */
        [HttpGet]
        public ViewResult TestForm()
        {
            return View();
        }

        /* POST 방식을 넘어왔을 때 처리*/
        
        [HttpPost]
        public ViewResult TestForm(MemberResponse memberResponse)
        {
            return View("demoView", memberResponse); // demoView를 찾아서 렌더링을 한 후 memberResponse 객체를 전달하라는 의미
        }
        


        /*
        컨트롤러의 Index() 메서드에서 View() 메서드를 호출하면

        MVC 프레임워크는 Views/Home/Index.cshtml파일(뷰파일)을 찾는다.
        그 다음에는 Razor 뷰엔진 에게 Index.cshtml 파일을 파싱할 것을 요청을 한다.

        Razor 는 해당 파일을 분석 하면서 Razor 표현식을 찾아 처리한다.

        그리고 그 결과의 HTML을 생성하여 브라우저로 전송한다.

        컨트롤러(Controller)는 특정 데이터를 뷰로 전달해주는 작업을 하고, 
        뷰(View)는 전달 받은 데이터를 HTML로 렌더링하는 작업을 한다.
        */




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
