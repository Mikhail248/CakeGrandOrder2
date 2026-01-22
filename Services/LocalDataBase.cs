using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CakeGrandOrder.Models;
using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Auth.Repository;
using Firebase.Database;
using Firebase.Database.Query;
using System.Runtime.ConstrainedExecution;
using Microsoft.Maui.ApplicationModel.Communication;
using CakeGrandOrder.ViewModels;
using Microsoft.Maui.Storage;
namespace CakeGrandOrder.CakeGrandOrder.Services
{
    internal class LocalDataBase
    {
        

        private static LocalDataBase instance;
        public static LocalDataBase GetInstance()
        {
            if (instance == null)
            {
                instance = new LocalDataBase();
            }
            return instance;
        }

        /* the data that comes from firebase and will be kept here! */
        List<Cake> cakes;
        List<Predefined> predefinedCakes;
        public LocalDataBase()
        {
           CreateFakeData();
        }
        public void CreateFakeData()
        {
            cakes = new List<Cake>();
            cakes.Add(new Cake() { Id = "1", Fat = 3 });
            cakes.Add(new Cake() { Id = "2", Fat = 3 });

            predefinedCakes = new List<Predefined>();
            {
                predefinedCakes.Add(new Predefined() { Id = "1", name = "Black Forest", tag = Predefined.Tags.Birthday });
                predefinedCakes.Add(new Predefined() { Id = "2", name = "White Samba", tag = Predefined.Tags.Weeding });
            }
        }
        public List<Cake> GetCakes()
        {
            return cakes;
        }
        public List<Predefined> GetPredefinedCakes()
        {
            return predefinedCakes;
        }
    }
}
