using CakeGrandOrder.ViewModels;

namespace CakeGrandOrder.Views;

public partial class DefaultCakesPage : ContentPage
{
	public DefaultCakesPage()
	{
		InitializeComponent();
        BindingContext = new DefaultCakesViewModel();
    }
}