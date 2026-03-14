using CakeGrandOrder.Models;
using CakeGrandOrder.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
namespace CakeGrandOrder.ViewModels;

    internal class CartViewModel : ViewModelBase
    {
        #region Get Set
        private ObservableCollection<Cake> cakeList;
        public ObservableCollection<Cake> CakeList
        {
            get { return cakeList; }
            set
            {
                if (value != null)
                {
                    cakeList = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region Commands
        public ICommand OrderCart { get; set; }

        #endregion
        #region Constructor
        public CartViewModel()
        {
            InitAsync();
        OrderCart = new Command(async () => await CakeOrderCart());

        }
        #endregion

        #region Methods

        private async Task InitAsync()
        {
            CakeList = new ObservableCollection<Cake>(await AppService.GetInstance().GetCakesAsync());
        }
        private async Task CakeOrderCart()
        {
            
            AppService.GetInstance();
            await Shell.Current.GoToAsync("//");
        }
        #endregion
    }

