using CakeGrandOrder.ViewModels;
namespace CakeGrandOrder.Views;

public partial class OrdersPage : ContentPage
{
	public OrdersPage()
	{
		InitializeComponent();
        BindingContext = new OrdersViewModel();
    }
}