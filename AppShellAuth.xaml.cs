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
            AppService.GetInstance().Logout();
            ((App)Application.Current).SetUnauthenticatedShell();
        }
    }
}
