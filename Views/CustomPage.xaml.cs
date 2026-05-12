using CakeGrandOrder.ViewModels;

namespace CakeGrandOrder.Views;

public partial class CustomPage : ContentPage
{
	public CustomPage()
	{
		InitializeComponent();
        BindingContext = new CustomViewModel();
    }
}
