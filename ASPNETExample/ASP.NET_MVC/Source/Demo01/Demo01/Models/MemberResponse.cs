using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Demo01.Models
{
    /*
        M(Model) : 도메인 모델(Model) 이라고도 한다. MVC 어플리케이션에서 가장 중요한 부분에 해당된다.
        MVC 어플리케이션이 잘 설계되었다는 의미는 데이터 모델 설계가 잘 되었다는 의미이다.
        
        
        

     */


    // MemberResponse 객체는 모델 객체가 된다.
    public class MemberResponse
    {
        [Required(ErrorMessage = "당신의 이름을 입력하세요")]
        public string Name { get; set; }

        [Required(ErrorMessage = "이메일을 입력하세요")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "이메일 형식에 맞지 않습니다.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "전화번호를 입력하세요")]
        public string Tel { get; set; }

        [Required(ErrorMessage = "출석여부를 선택해주세요")]
        public bool? Attend { get; set; }
    }
}