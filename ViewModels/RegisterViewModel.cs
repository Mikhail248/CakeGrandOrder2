using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CakeGrandOrder.ViewModels
{
    internal class RegisterViewModel : ViewModelBase
    {

        #region Get Set
        private string userName;
        public string UserName
        {
            get { return userName; }
            set
            {
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
            set
            {
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
        public ICommand LinkToLoginPageCommand { get; set; }
        public ICommand ResetCommand { get; set; }
        #endregion

        #region Constructor
        public RegisterViewModel()
        {
            TogglePasswordCommand = new Command(ToggleViewPassword);
            LinkToLoginPageCommand = new Command(async () => await LinkToLoginPage());
            ResetCommand = new Command(ResetField);
        }
        #endregion

        #region Methods
        private void ToggleViewPassword()
        {
            ShowPassword = !ShowPassword;
        }
        private async Task LinkToLoginPage()
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
        private void ResetField()
        {
            UserName = "";
            UserPassword = "";
        }
        #endregion
    }
}
