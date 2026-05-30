using CakeGrandOrder.Models;
using CakeGrandOrder.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
namespace CakeGrandOrder.ViewModels;

    internal class CartViewModel : ViewModelBase
    {
        #region Get Set

        private ObservableCollection<Cake> cartList;
        public ObservableCollection<Cake> CartList
        {
            get { return cartList; }
            set
            {
                if (value != null)
                {
                cartList = value;
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

        OrderCart = new Command<List<Cake>>(async(a) => await CakeOrderCart());

        }
        #endregion

        #region Methods

        private async Task InitAsync()
        {
        CartList = new ObservableCollection<Cake>(await AppService.GetInstance().GetCartAsync());
        }
        private async Task CakeOrderCart()
        {
        bool answer = await Application.Current.MainPage.DisplayAlert(
    "Confirm",
    "Do you want to order this cart?",
    "Yes",
    "No");

        if (!answer)
        {
            return;
        }
        List<Cake> cakes = await AppService.GetInstance().GetCartAsync();

        var order = new Order()
        {
            Cakes = cakes,
            Date = DateTime.Now
        };
        bool tf = await AppService.GetInstance().CreateCakeOrder(order);

        /*if (tf)
        {
            while (cakes != null)
            {
                order.Cakes.Add(cakes.First());
            }
        }*/

        await AppService.GetInstance().CleanCartAsync();
        }
        #endregion
    }

