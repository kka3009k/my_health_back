using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;

namespace MyHealth.Cabinet.Services
{
    public class SpinnerService
    {
        public event Action<CancellationToken?> OnShow;
        public event Action<CancellationToken?> OnHide;

        private IToastService _toastService;

        public SpinnerService(IToastService pToastService)
        {
            _toastService = pToastService;
        }

        public async Task Show()
        {
            OnShow?.Invoke(CancellationToken.None);
        }

        public async Task Hide()
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
            catch (Exception ex)
            {
                _toastService.ShowError(ex.ToString());
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
            catch (Exception ex)
            {
                _toastService.ShowError(ex.ToString());
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
}
