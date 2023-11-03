using MyHealth.Cabinet.Services;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace MyHealth.Cabinet.MiddleWares
{
    public class CustomHttpHadler : HttpClientHandler
    {
        private readonly UserStateService _userState;

        public CustomHttpHadler(UserStateService pUserState)
        {
            _userState = pUserState;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userState.GetAuthData(cancellationToken);
                request.Headers.Authorization = new AuthenticationHeaderValue("bearer", user.AccessToken);
                request.Headers.Add("User-Id", user.UserID?.ToString());
            }
            catch
            {
                if (UserStateService.StaticState != null)
                {
                    var user = UserStateService.StaticState.AuthData;
                    request.Headers.Authorization = new AuthenticationHeaderValue("bearer", user.AccessToken);
                    request.Headers.Add("User-Id", user.UserID?.ToString());
                }
            }

            var response = await base.SendAsync(request, cancellationToken);
            return await CheckResponse(response, cancellationToken);
        }

        private Task<HttpResponseMessage> CheckResponse(HttpResponseMessage response,
            CancellationToken cancellationToken)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                UserStateService.OnUnauthorized?.Invoke(cancellationToken);
            return Task.FromResult(response);
        }
    }
}
