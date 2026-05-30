using Android.Webkit;
using CakeGrandOrder.Models;
using CakeGrandOrder.ViewModels;
using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Auth.Repository;
using Firebase.Database;
using Firebase.Database.Query;
using Microsoft.Maui.ApplicationModel.Communication;
using Microsoft.Maui.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
///using static Android.Provider.ContactsContract.CommonDataKinds;


namespace CakeGrandOrder.Services
{
    class AppService
    {

        List<Cake> Cakes;
        string uid="";
        FirebaseAuthClient? auth;
        FirebaseClient? client;
        public AuthCredential? loginAuthUser;


        static private AppService instance;
        static public AppService GetInstance()
        {
            if (instance == null)
            {
                instance = new AppService();
            }
            return instance;
        }
        public AppService()
        {
            Init();
        }
        public void Init()
        {
            var config = new FirebaseAuthConfig()
            {
                ApiKey = "AIzaSyBeyB-AhHFBUA8Ql1I-HUKLjW7IbpqdYsI",
                AuthDomain = "grandcakeorder.firebaseapp.com",
                Providers = new FirebaseAuthProvider[]

              {
          new EmailProvider() 
              },
                UserRepository = new FileUserRepository("appUserData")   
            };
            auth = new FirebaseAuthClient(config);

            client =
              new FirebaseClient(@"https://grandcakeorder-default-rtdb.europe-west1.firebasedatabase.app", //כתובת מסד הנתונים
              new FirebaseOptions
              {
                  AuthTokenAsyncFactory = () => Task.FromResult(auth.User.Credential.IdToken)// מזהה ההתחברות של המשתמש עם השרת, הנתון נשמר במכשיר
              });
        }

        public async Task<bool> TryRegister(string userNameString, string passwordString, string fullName)
        {
            
            if (!await ConnectivityService.GetInstance().CheckConnectivityAndAlert("registration"))
            {
                return false;
            }

            try
            {
                
                var respond = await auth.CreateUserWithEmailAndPasswordAsync(userNameString, passwordString);
                

                uid = respond.User.Uid;
                loginAuthUser = respond.AuthCredential;

                await client
                    .Child("users")
                    .Child(uid)
                    .PutAsync(new
                    {
                        FullName = fullName,
                        Email = respond.User.Info.Email
                    });

                return true;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    ex.Message,
                    "OK");

                return false;

            }
        }

        public async Task<bool> TryLogin(string userNameString, string passwordString)
        {
            if (userNameString == null || passwordString == null)
            {
                return false;
            }
            if (!await ConnectivityService.GetInstance().CheckConnectivityAndAlert("login"))
            {
                return false;
            }

            try
            {
                var authUser = await auth.SignInWithEmailAndPasswordAsync(userNameString, passwordString);
                loginAuthUser = authUser.AuthCredential;
                uid = auth.User.Uid;
                string fullName = await client
                    .Child("users")
                    .Child(uid)
                    .Child("FullName")
                    .OnceSingleAsync<string>();

                Cakes = await GetCakesAsync();
                return true;
            }
            catch (FirebaseAuthException ex)
            {
                return false;
            }
        }


        public bool Logout()
        {

            try
            {
                auth.SignOut();
                loginAuthUser = null;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Cake>?> GetCakesAsync()
        {
            if (!await ConnectivityService.GetInstance().CheckConnectivityAndAlert("loading cakes"))
            {
                return new List<Cake>();
            }
            try
            {
                var results = await client
                  .Child("Cakes")
                  .OnceAsync<Cake>();
                List<Cake> cakes = results.Select(fbItm => new Cake() { Id = fbItm.Key, CakeName = fbItm.Object.CakeName, Price = fbItm.Object.Price, Fat = fbItm.Object.Fat, Sugar = fbItm.Object.Sugar, CakeBase = fbItm.Object.CakeBase, CakeFilling = fbItm.Object.CakeFilling, BaseNum = fbItm.Object.BaseNum, Construction = fbItm.Object.Construction, Top = fbItm.Object.Top }).ToList();
                return cakes;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<Order>?> GetOrderCakesAsync()
        {
            if (!await ConnectivityService.GetInstance().CheckConnectivityAndAlert("loading orders"))
            {
                return new List<Order>();
            }
            try
            {
                var results = await client
                  .Child("users")
                  .Child(uid)
                  .Child("orders")

                  .OnceAsync<Order>();
                List<Order> orders = results.Select(fbItm => new Order() { Id = fbItm.Key, Date = fbItm.Object.Date, Cakes = fbItm.Object.Cakes }).ToList();
                ///fbItm => new Cake() { Id = fbItm.Key, CakeName = fbItm.Object.CakeName, Price = fbItm.Object.Price, Fat = fbItm.Object.Fat, Sugar = fbItm.Object.Sugar, CakeBase = fbItm.Object.CakeBase, CakeFilling = fbItm.Object.CakeFilling, BaseNum = fbItm.Object.BaseNum, Construction = fbItm.Object.Construction, Top = fbItm.Object.Top }
                Console.WriteLine();
                return orders;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> CreateCakeOrder(Order order)
        {
            if (!await ConnectivityService.GetInstance().CheckConnectivityAndAlert("adding order"))
            {
                return false;
            }
            try
            {
                var result = await client
                  .Child("users")
                  .Child(uid)
                  .Child("orders")
                  .PostAsync(order);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
        public async Task<bool> AddToCart(Cake cake)
        {
            if (!await ConnectivityService.GetInstance().CheckConnectivityAndAlert("adding cake to cart"))
            {
                return false;
            }
            try
            {
                var result = await client
                  .Child("users")
                  .Child(uid)
                  .Child("cart")
                  .PostAsync(cake);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
        
        public async Task<List<Cake>?> GetCartAsync()
        {
            if (!await ConnectivityService.GetInstance().CheckConnectivityAndAlert("loading cart"))
            {
                return new List<Cake>();
            }
            try
            {
                var results = await client
                  .Child("users")
                  .Child(uid)
                  .Child("cart")
                  .OnceAsync<Cake>();
                List<Cake> cakes = results.Select(fbItm => new Cake() { Id = fbItm.Key, CakeName = fbItm.Object.CakeName, Price = fbItm.Object.Price, Fat = fbItm.Object.Fat, Sugar = fbItm.Object.Sugar, CakeBase = fbItm.Object.CakeBase, CakeFilling = fbItm.Object.CakeFilling, BaseNum = fbItm.Object.BaseNum, Construction = fbItm.Object.Construction, Top = fbItm.Object.Top }).ToList();
                return cakes;                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task CleanCartAsync()
        {
            if (!await ConnectivityService.GetInstance().CheckConnectivityAndAlert("cleaning cart"))
            {}
            try
            {
                await client
            .Child("users")
            .Child(uid)
            .Child("cart")
            .DeleteAsync();

                Console.WriteLine("Deleted");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}