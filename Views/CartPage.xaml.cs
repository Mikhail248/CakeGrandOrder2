using CakeGrandOrder.ViewModels;
namespace CakeGrandOrder.Views;

public partial class CartPage : ContentPage
{
    CartViewModel vm;
    public CartPage()
	{
        InitializeComponent();
        vm = new CartViewModel();
        BindingContext = vm;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        vm.OnAppearing();
    }
}
