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

namespace CakeGrandOrder.ViewModels
{
    internal class OrdersViewModel : ViewModelBase
    {
        #region Get Set
        private ObservableCollection<Order> orderList;
        private ObservableCollection<bool> baseList2;
        public ObservableCollection<bool> BaseList2
        {
            get { return baseList2; }
            set
            {
                if (value != null)
                {
                    baseList2 = value;
                    OnPropertyChanged();
                }
            }
        }
        private ObservableCollection<bool> baseList3;
        public ObservableCollection<bool> BaseList3
        {
            get { return baseList3; }
            set
            {
                if (value != null)
                {
                    baseList3 = value;
                    OnPropertyChanged();
                }
            }
        }
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
        public ICommand ImageCakeCommand { get; set; }

        #endregion
        #region Constructor
        public OrdersViewModel()
        {
            InitAsync();
            
            OrderDefaultCakeCommand = new Command<Cake>(async (cake) => await OrderCake(cake));
            ImageCakeCommand = new Command<Cake>(async(cake) => await CakeImage(cake));
        }
        #endregion

        #region Methods
        public async Task InitAsync()
        {

            List<Order> tempOrders = new List<Order>(await AppService.GetInstance().GetOrderCakesAsync());
            OrderList = new ObservableCollection<Order>(tempOrders);
            ObservableCollection<bool> tempbases2 = new ObservableCollection<bool>();
            ObservableCollection<bool> tempbases3 = new ObservableCollection<bool>();
            ObservableCollection<Cake> tempcakes = new ObservableCollection<Cake>();
            for (int i = 0; i < OrderList.Count; i++)
            {
                for (int j = 0; j < OrderList[i].Cakes.Count; j++)
                {
                    tempcakes.Add(OrderList[i].Cakes[j]);
                    if (OrderList[i].Cakes[j].BaseNum >= 2)
                    {
                        tempbases2.Add(true);
                    }
                    else
                    {
                        tempbases2.Add(false);
                    }
                    if (OrderList[i].Cakes[j].BaseNum == 3)
                    {
                        tempbases3.Add(true);
                    }
                    else
                    {
                        tempbases3.Add(false);
                    }
                }
            }
            CakeList = new ObservableCollection<Cake> (tempcakes);
            BaseList2 = new ObservableCollection<bool>(tempbases2);
            BaseList3 = new ObservableCollection<bool>(tempbases3);
        }
        Order order = new Order();
        public async Task OrderCake(Cake cake)
        {
            order.Cakes.Add(cake);
            await AppService.GetInstance().CreateCakeOrder(order);
            await Shell.Current.GoToAsync("//CartPage");
        }
        public async Task CakeImage(Cake cake)
        {
            List<string> tempImage = new List<string>();
            for (int j = 0; j < cake.BaseNum; j++)
            {
                string cakebase = cake.CakeBase.ToString();
                tempImage.Add("base" + cakebase + ".png");
                string cakefilling = cake.CakeFilling.ToString();
                tempImage.Add("filling" + cakefilling + ".png");
            }
            string top = cake.Top.ToString();
            tempImage.Add("top" + top + ".png");
        }
        #endregion

    }
}

 
      