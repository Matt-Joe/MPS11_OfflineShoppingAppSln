using MPS11_OfflineShoppingApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPS11_OfflineShoppingApp.Views;
public class BaseView<TViewModel> : ContentPage
    where TViewModel : BaseViewModel
{
    protected TViewModel ViewModel =>
        BindingContext as TViewModel
        ?? throw new InvalidOperationException(
            $"The BindingContext must be of type {typeof(TViewModel).Name}");

    public BaseView(TViewModel viewModel)
    {
        BindingContext = viewModel;
    }
}
