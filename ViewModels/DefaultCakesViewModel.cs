using CakeGrandOrder.Models;
using CakeGrandOrder.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CakeGrandOrder.ViewModels
{
    internal class DefaultCakesViewModel : ViewModelBase
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
        public ICommand OrderDefaultCakeCommand { get; set; }

        #endregion
        #region Constructor
        public DefaultCakesViewModel()
        {
            InitAsync();
            OrderDefaultCakeCommand = new Command<Cake>(async (cake) => await OrderCake(cake));
        }
        #endregion

        #region Methods

        public async Task InitAsync()
        {
            CakeList = new ObservableCollection<Cake>(await AppService.GetInstance().GetCakesAsync());

        }
        Order order = new Order();
        public async Task OrderCake(Cake cake)
        {
            order.cakes.Add(cake);
            await AppService.GetInstance().CreateCakeOrder(order);
            await Shell.Current.GoToAsync("//CartPage");
        }
        #endregion
    }
}
