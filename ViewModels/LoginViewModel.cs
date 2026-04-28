using CakeGrandOrder.Services;
using CakeGrandOrder.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CakeGrandOrder.ViewModels
{
    internal class LoginViewModel : ViewModelBase
    {
        #region Get Set
        private string userName;
        public string UserName
        {
            get { return userName; }
            set {
                if (value != null)
                {
                    userName = value;
                    OnPropertyChanged();
                }
            }
        }

        private string userPassword;
        public string UserPassword
        {
            get { return userPassword; }
            set
            {
                if (value != null)
                {
                    userPassword = value;
                    OnPropertyChanged();
                }
            }
        }


        private bool showPassword;
        public bool ShowPassword
        {
            get { return showPassword; }
            set {
                if (value != null)
                {
                    showPassword = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region Commands
        public ICommand TogglePasswordCommand { get; set; }
        public ICommand LinkToRegisterPageCommand { get; set; }
        public ICommand ResetCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        #endregion

        #region Constructor
        public LoginViewModel() {
            UserName = "micha@gmail.com";
            UserPassword = "123456";
            TogglePasswordCommand = new Command(ToggleViewPassword);
            LinkToRegisterPageCommand = new Command(async () => await LinkToRegisterPage());
            ResetCommand = new Command(ResetField);
            LoginCommand = new Command(async () => await Login());
        }
        #endregion

        #region Methods
        private void ToggleViewPassword()
        {
            ShowPassword = !ShowPassword;
        }
        private async Task LinkToRegisterPage()
        {
            await Shell.Current.GoToAsync("//RegisterPage");
        }
        private async Task Login()
        {
            bool succseed = await AppService.GetInstance().TryLogin(UserName, UserPassword);
            if (succseed)
            {
                await Shell.Current.GoToAsync("//OrdersPage");
                ((App)Application.Current).SetAuthenticatedShell();
                ///await Shell.Current.GoToAsync("//DefaultCakesPage");
            }
        }
        private void ResetField()
        {
            UserName = "";
            UserPassword = "";
        }

        }
        #endregion
    }
