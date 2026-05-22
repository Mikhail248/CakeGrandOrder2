using CakeGrandOrder.ViewModels;
namespace CakeGrandOrder.Views;

public partial class CartPage : ContentPage
{
	public CartPage()
	{
        InitializeComponent();
        BindingContext = new CartViewModel();
    }
}
