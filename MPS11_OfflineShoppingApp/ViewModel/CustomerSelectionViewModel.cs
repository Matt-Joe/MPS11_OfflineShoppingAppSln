
using MPS11_OfflineShoppingApp.Models;
using MPS11_OfflineShoppingApp.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MPS11_OfflineShoppingApp.ViewModels
{
    public class CustomerSelectionViewModel : BaseViewModel
    {
        private readonly DatabaseService databaseService;
        private readonly CustomerSessionService customerSessionService;
        private readonly INavigationService navigationService;

        private ObservableCollection<Customer> customers = new();
        private Customer? selectedCustomer;
        private bool isLoading;

        public ObservableCollection<Customer> Customers
        {
            get => customers;
            set => SetProperty(ref customers, value);
        }

        public Customer? SelectedCustomer
        {
            get => selectedCustomer;
            set
            {
                if (SetProperty(ref selectedCustomer, value)
                    && value != null)
                {
                    _ = SelectCustomerAsync(value);
                }
            }
        }

        public bool IsLoading
        {
            get => isLoading;
            set => SetProperty(ref isLoading, value);
        }

        public CustomerSelectionViewModel(
            DatabaseService databaseService,
            CustomerSessionService customerSessionService,
            INavigationService navigationService)
        {
            this.databaseService = databaseService;
            this.customerSessionService = customerSessionService;
            this.navigationService = navigationService;

            _ = LoadCustomersAsync();
        }

        private async Task LoadCustomersAsync()
        {
            try
            {
                IsLoading = true;

                List<Customer> customerList =
                    await databaseService.GetCustomersAsync();

                Customers = new ObservableCollection<Customer>(
                    customerList);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading customers: {ex}");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task SelectCustomerAsync(Customer customer)
        {
            try
            {
                customerSessionService.SetCustomer(customer);

                await navigationService.GoToAsync(
                    "product-catalogue");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error selecting customer: {ex}");
            }
        }
    }
}
