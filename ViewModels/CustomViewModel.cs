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

    internal class CustomViewModel : ViewModelBase
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

    private string id;
    public string Id
    { get { return id; } set { if (value != null) { id = value;OnPropertyChanged();} } }
    private string cakeName;
    public string CakeName
    { get { return cakeName; } set { if (value != null) { cakeName = value;OnPropertyChanged();} } }
    private int price = 100;
    public int Price
    { get { return 100; } set { if (value != null) { price = value; OnPropertyChanged(); } } }
    private int fat;
    public int Fat
    { get { return fat; } set { if (value != null) { fat = value; OnPropertyChanged(); } } }
    private int sugar;
    public int Sugar
    { get { return sugar; } set { if (value != null) { sugar = value; OnPropertyChanged(); } } }
    private int cakeBase;
    public int CakeBase
    { get { return cakeBase; } set { if (value != null) { cakeBase = value; OnPropertyChanged(); } } }
    private int cakeFilling;
    public int CakeFilling
    { get { return cakeFilling; } set { if (value != null) { cakeFilling = value; OnPropertyChanged(); } } }
    private int baseNum;
    public int BaseNum
    { get { return baseNum; } set { if (value != null) { baseNum = value; OnPropertyChanged(); } } }
    private int construction;
    public int Construction
    { get { return construction; } set { if (value != null) { construction = value; OnPropertyChanged(); } } }
    private int top;
    public int Top
    { get { return top; } set { if (value != null) { top = value; OnPropertyChanged(); } } }

    #region Commands
    public ICommand OrderCommand { get; set; }

        #endregion
        #region Constructor
        public CustomViewModel()
        {
            InitAsync();
        OrderCommand = new Command<Cake>(async (cake) => await OrderCake());
    }
        #endregion

        #region Methods

        private async Task InitAsync()
        {
            CakeList = new ObservableCollection<Cake>(await AppService.GetInstance().GetCakesAsync());
        }
    public async Task OrderCake()
    {
        Cake cake = new Cake(
            Id,
            CakeName,
            Price,
            Fat,
            Sugar,
            CakeBase,
            CakeFilling,
            BaseNum,
            Construction,
            Top);
        var order = new Order()
        {
            Cakes = new List<Cake> { cake },
            Date = DateTime.Now
        };
        bool tf = await AppService.GetInstance().AddToCart(cake);
        if (tf) order.Cakes.Add(cake);
        await Shell.Current.GoToAsync("//CartPage");
    }
    #endregion
}
