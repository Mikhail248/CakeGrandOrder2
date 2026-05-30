using CakeGrandOrder.Services;


namespace CakeGrandOrder
{
    public partial class AppShellAuth : Shell
    {
        public AppShellAuth()
        {
            InitializeComponent();

            
        }
        private async void MenuItem_Logout_Clicked(object sender, EventArgs e)
        {
            bool answer = await Application.Current.MainPage.DisplayAlert(
        "Logout",
        "Are you sure you want to logout?",
        "Yes",
        "No");

            if (!answer)
                return;
            AppService.GetInstance().Logout();
            ((App)Application.Current).SetUnauthenticatedShell();
        }
    }
}
