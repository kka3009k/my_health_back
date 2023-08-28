using Radzen;

namespace MyHealth.Admin.Services
{
    public class SpinnerService
    {
        public event Action<CancellationToken?> OnShow;
        public event Action<CancellationToken?> OnHide;

        private NotificationService _notificationService;

        public SpinnerService(NotificationService pNotificationService)
        {
            _notificationService = pNotificationService;
        }

        private void Show()
        {
            OnShow?.Invoke(CancellationToken.None);
        }

        private void Hide()
        {
            OnHide?.Invoke(CancellationToken.None);
        }

        public void Run(Action action)
        {
            Show();
            try
            {
                action();
            }
            finally
            {
                Hide();
            }
        }

        public T Run<T>(Func<T> action)
        {
            T result = default;
            Run(() =>
            {
                result = action();
            });
            return result;
        }

        public async Task RunAsync(Func<Task> action)
        {
            Show();
            try
            {
                await action();
            }
            finally
            {
                Hide();
            }
        }

        public async Task<T> RunAsync<T>(Func<Task<T>> action)
        {
            T result = default;
            await RunAsync(async () =>
            {
                result = await action();
            });
            return result;
        }
    }

    public static class SpinnerServiceConfiguration
    {
        public static IServiceCollection AddSpinner(this IServiceCollection pService)
            => pService.AddScoped<SpinnerService>();
    }
}
