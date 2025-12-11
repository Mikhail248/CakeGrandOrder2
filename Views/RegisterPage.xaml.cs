using CakeGrandOrder.ViewModels;

namespace CakeGrandOrder.Views;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
		BindingContext = new RegisterViewModel();
	}
}