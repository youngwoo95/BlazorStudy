using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo01.Models
{
    /*
        M(Model) : 도메인 모델(Model) 이라고도 한다. MVC 어플리케이션에서 가장 중요한 부분에 해당된다.
        MVC 어플리케이션이 잘 설계되었다는 의미는 데이터 모델 설계가 잘 되었다는 의미이다.
        
        
        

     */


    // MemberResponse 객체는 모델 객체가 된다.
    public class MemberResponse
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; }
        public bool? Attend { get; set; }
    }
}