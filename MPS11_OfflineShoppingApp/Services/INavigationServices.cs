using System;
using System.Collections.Generic;
using System.Text;

namespace MPS11_OfflineShoppingApp.Services
{
    public interface INavigationService
    {
        Task GoToAsync(
            string route,
            IDictionary<string, object>? parameters = null);

        Task GoBackAsync();
    }

}
