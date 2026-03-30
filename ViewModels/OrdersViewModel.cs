using CakeGrandOrder.Models;
using CakeGrandOrder.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CakeGrandOrder.ViewModels
{
    internal class OrdersViewModel : ViewModelBase
    {
        #region Get Set
        private ObservableCollection<Order> orderList;
        public ObservableCollection<Order> OrderList
        {
            get { return orderList; }
            set
            {
                if (value != null)
                {
                    orderList = value;
                    OnPropertyChanged();
                }
            }
        }
        private ObservableCollection<Cake> cakeList;
        public ObservableCollection<Cake> CakeList
        {
            
            get {
                return cakeList;
                 
                    /*ObservableCollection<Cake> cakes = new ObservableCollection<Cake>();
                    for (int i = 0; i < orderList.Count; i++)
                    {
                        for (int j = 0; j < orderList[i].Cakes.Count; j++)
                        {
                            cakes.Add(orderList[i].Cakes[j]);
                        }
                    }
                    return (ObservableCollection<Cake>)cakes;
                */
            }
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
        public OrdersViewModel()
        {
            InitAsync();
            OrderDefaultCakeCommand = new Command<Cake>(async (cake) => await OrderCake(cake));
        }
        #endregion

        #region Methods

        public async Task InitAsync()
        {
            List<Order> tempOrders = new List<Order>(await AppService.GetInstance().GetOrderCakesAsync());
            OrderList = new ObservableCollection<Order>(tempOrders);

            ObservableCollection<Cake> cakes = new ObservableCollection<Cake>();
            for (int i = 0; i < OrderList.Count; i++)
            {
                for (int j = 0; j < OrderList[i].Cakes.Count; j++)
                {
                    cakes.Add(OrderList[i].Cakes[j]);
                }
            }
            CakeList = (ObservableCollection<Cake>)cakes;
        }
        Order order = new Order();
        public async Task OrderCake(Cake cake)
        {
            order.Cakes.Add(cake);
            await AppService.GetInstance().CreateCakeOrder(order);
            await Shell.Current.GoToAsync("//CartPage");
        }
        #endregion

    }
}

 
      