using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Security.Claims;

namespace BlazorManager.Authentication
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider // 인증 상태 공급자인 AuthenticationStateProvider를 상속받는다. using Microsoft.AspNetCore.Components.Authorization;
    {
        // 세션 저장소이며 : 로그인 사용자 세부정보를 저장하는 곳이다.
        private readonly ProtectedSessionStorage _sessionStorage; // using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

        // 개인 청구 원칙 개체(?)
        // 익명 사용자를 위해 이 개체를 사용한다.
        private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity()); // using System.Security.Claims;

        public CustomAuthenticationStateProvider(ProtectedSessionStorage sessionstorage)
        {
            _sessionStorage = sessionstorage;
        }


        // * AuthenticationStateProvider를 상속받으면
        // [1]. GetAuthenticationStateAsync() 메서드를 재정의 해줘야 한다.
        //      - 애플리케이션이 열리거나 페이지가 새로 고쳐질때 실행된다.
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                await Task.Delay(5000);

                // 사용자 세션이라는 모델 클래스를 만들어야 한다.
                // 보호된 세션 저장소에서 사용자 세션 세부정보를 읽어 볼 수 있다.
                // 따라서 세션 저장 도트는 사용자 세션과 비동기화 된다.
                var userSessionStorageResult = await _sessionStorage.GetAsync<UserSession>("UserSession");// <T> : 제네릭은 모델클래스를 의미하며 매개변수안의 "UserSession" 은 세션의 Key 값을 의미한다.

                // 세션을 정상적으로 가져오면 값을 반환하고 그렇지않으면 null이 반환됨.
                var userSession = userSessionStorageResult.Success ? userSessionStorageResult.Value : null;

                // userSession의 값이 null인 경우 익명 사용자 인증 상태를 반환한다.
                if (userSession == null)
                    return await Task.FromResult(new AuthenticationState(_anonymous));

                // null이 아닐 경우 이름과 역할이라는 두가지 클레임이 포함된 새 클레임 원칙 개체를 만든다.
                // 또한 인증 유형 문자열 값을 제공해야 한다.
                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userSession.UserName),
                    new Claim(ClaimTypes.Role, userSession.Role)
                }, "CustomAuth")); // "CustomAuth" : 인증유형 문자열값
                                   // 아무것도 제공하지 않으면 애플리케이션을 이를 익명 사용자로 간주한다.

                // 새로 생성된 클레임 원칙을 사용하여 인증 상태를 반환한다.
                return await Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
            catch
            {
                return await Task.FromResult(new AuthenticationState(_anonymous));
            }
        }

        // 사용자가 로그인하거나 로그아웃할 때 이 메소드를 호출한다.
        public async Task UpdateAuthenticationState(UserSession userSession)
        {
            // [1]. 클레임 원칙 개체를 선언한다.
            ClaimsPrincipal claimsPrincipal;

            // [2]. 세션 개체가 null 인지 확인한다.
            if (userSession != null)
            {
                // 로그인 했을 경우이다.

                // 세션 저장소에 사용자 세션 값을 할당한다.
                await _sessionStorage.SetAsync("UserSession", userSession);
                // 그 다음 두 개의 클레임 사용자 이름과 역할을 사용하여 새로운 클레임 원칙을 만든다.
                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userSession.UserName),
                    new Claim(ClaimTypes.Role, userSession.Role)
                }));
            }
            else
            {
                // 로그아웃 했을때 UI에서 null을 반환하므로 
                // else 부분은 사용자가 로그아웃 했음을 의미한다.
                // 그러므로 세션 저장소에서 사용자 세션 값을 삭제하면 된다.
                await _sessionStorage.DeleteAsync("UserSession");

                // 그런다음 클레임 원칙을 익명으로 설정한다.
                claimsPrincipal = _anonymous;
            }

            // 마지막으로 Blazor 인증 상태 공급자의 일부인 인증 상태 변경 알림 메서드를 호출한다.
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }


    }
}
