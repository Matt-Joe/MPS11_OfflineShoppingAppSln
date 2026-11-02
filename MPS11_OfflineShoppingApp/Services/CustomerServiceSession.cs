using MPS11_OfflineShoppingApp.Models;

namespace MPS11_OfflineShoppingApp.Services
{
    public class CustomerSessionService
    {
        public Customer? SelectedCustomer { get; private set; }

        public void SetCustomer(Customer customer)
        {
            SelectedCustomer = customer;
        }

        public void ClearCustomer()
        {
            SelectedCustomer = null;
        }
    }
}
