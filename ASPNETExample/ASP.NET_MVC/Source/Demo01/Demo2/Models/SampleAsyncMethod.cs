using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Threading.Tasks;

namespace Demo2.Models
{
    public class SampleAsyncMethod
    {
        // 웹사이트에 접속한 페이지의 Content길이를 반환해주는 메소드(기존의 비동기 작업 방식)
        //public static Task<long?> GetPageLength() 
        //{
        //    HttpClient client = new HttpClient();

        //    var hTask = client.GetAsync("http://www.naver.com"); // 리턴값이 Task<HttpResponseMessage>

        //    // HTTP 요청이 완료되는 동안 처리해야할 작업을 기술.

        //    return hTask.ContinueWith((Task<HttpResponseMessage> ConAction) =>
        //    {
        //        return ConAction.Result.Content.Headers.ContentLength;
        //    });
        //}

        // async, await 키워드를 이용한 비동기 작업 처리
        public async static Task<long?> GetPageLength()
        {
            HttpClient client = new HttpClient();

            var httpMsg = await client.GetAsync("http://www.naver.com");
            // Http요청 작업이 완료되는 동안 처리해야 할 작업을 기술

            return httpMsg.Content.Headers.ContentLength;
        }

        // async, await는 3가지 리턴타입
        // void, Task, Task<T>
        // void 는 이벤트 핸들러를 사용하기 위해서 사용(메소드 내부에서 UnHandled Exception이 발생하면 프로세스를 다운시키는 문제가 있어서 사용을 자제)
        // Task 는 리턴값이 없는 비동기 메서드를 위해서 사용
        // Task<T> 리턴값 T를 갖는 비동기 메소드를 위해 사용

        // C#7 에서는 위의 3가지 리턴 타입에 대한 제약을 넘어서 커스텀 리턴 타입을 허용하고 있다.
        
        /*  커스텀 리턴 타입의 예
           .Net Framework에서 ValueTask<T>라는 타입이 제공
           
            비동기 메소드에서 흔히 사용하는 Task, Task<T> 리턴 타입은 비동기 작업 처리가 필요 없는 상수값을 리턴할 때도
            Task 객체를 항상 만든다. 따라서, 성능 저하를 일으킨다.    

            이런 점을 보완하기 위해서 ValueTask<T>를 이용한다.
            
            ValueTask<T> 타입은 값 T와 Task<T> 결합된 형태라고 볼 수 있다.
            비동기 메서드 내에서 동기 처리 작업이 필요할 때는 Task 객체를 생성하지 않고 직접 값 T를 리턴한다. 비동기 처리가
            필요할 경우에는 Task 객체를 생성하여 사용한다.
            
            즉, ValueTask<T> 타입은 비동기 메서드 내에 동기처리방식과 비동기 처리 방식을 혼용하고 있을 때 유용한 타입이다.

            
        */

    }
}

/*
    [ 비동기 메서드 ]
    백그라운드에서 작업을 수행하다가 작업이 완료되었을 경우 통보를 해주기 때문에 백그라운드 작업이 실행되는 동안
    다른 작업을 처리할 수 있다.
    
 */