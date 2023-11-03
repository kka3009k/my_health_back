using Microsoft.AspNetCore.Components;

namespace MyHealth.Cabinet.Services
{
    public class NavigationService
    {
        private NavigationManager _navigationManager;

        private string _previosPage = "/";
        private string _currentPage = "/";

        public NavigationService(NavigationManager pNavigationManager)
        {
            _navigationManager = pNavigationManager;
        }

        public void NavigateTo(string pPath)
        {
            _previosPage = _currentPage;
            _currentPage = pPath;
            _navigationManager.NavigateTo(_currentPage);
        }

        public void Back()
        {
            NavigateTo(_previosPage);
        }
    }
}
