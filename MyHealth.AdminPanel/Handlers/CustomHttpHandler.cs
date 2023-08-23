using MyHealth.AdminPanel.Services;
using System.Net.Http.Headers;

namespace MyHealth.AdminPanel.Handlers
{
    public class CustomHttpHandler : HttpClientHandler
    {
        private UserStateService _userStateService;

        public CustomHttpHandler(UserStateService pUserStateService)
        {
            _userStateService = pUserStateService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _userStateService.GetTokenAsync(cancellationToken);

            if (!string.IsNullOrEmpty(token))
                request.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);

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
