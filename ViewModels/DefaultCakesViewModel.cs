using CakeGrandOrder.Models;
using CakeGrandOrder.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CakeGrandOrder.ViewModels
{
    internal class DefaultCakesViewModel : ViewModelBase
    {
        #region Get Set
        private List<Cake> cakeList;
        public List<Cake> CakeList
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

        #endregion
        #region Constructor
        public DefaultCakesViewModel()
        {
            InitAsync();
            
            
        }
        #endregion

        #region Methods

        private async Task InitAsync()
        {

            CakeList = await AppService.GetInstance().GetCakesAsync();
        }
        #endregion
}
}
