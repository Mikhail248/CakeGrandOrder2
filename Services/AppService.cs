using Firebase.Auth;
using Firebase.Auth.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CakeGrandOrder.Models;
using Firebase.Auth.Repository;
using Firebase.Database;
using Firebase.Database.Query;
using System.Runtime.ConstrainedExecution;
using Microsoft.Maui.ApplicationModel.Communication;
using CakeGrandOrder.ViewModels;
using Microsoft.Maui.Storage;


namespace CakeGrandOrder.Services
{
    class AppService
    {

        List<Cake> Cakes;
        string uid="";
        FirebaseAuthClient? auth;
        FirebaseClient? client;
        public AuthCredential? loginAuthUser; //This is to keep the logged in user credential, so we can logout later


        // SingleTone Pattern
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
            // We need a costructor because of :  _instance = new AppService();
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
          new EmailProvider() //אנחנו נשתמש בשירות חינמי של התחברות עם מייל
              },
                UserRepository = new FileUserRepository("appUserData") //לא חובה, שם של קובץ בטלפון הפרטי שאפשר לשמור בו את מזהה ההתחברות כדי לא הכניס כל פעם את הסיסמא 
            };
            auth = new FirebaseAuthClient(config); //ההתחברות

            client =
              new FirebaseClient(@"https://grandcakeorder-default-rtdb.europe-west1.firebasedatabase.app", //כתובת מסד הנתונים
              new FirebaseOptions
              {
                  AuthTokenAsyncFactory = () => Task.FromResult(auth.User.Credential.IdToken)// מזהה ההתחברות של המשתמש עם השרת, הנתון נשמר במכשיר
              });
        }

        public async Task<bool> TryRegister(string userNameString, string passwordString, string fullName)
        {
            try
            {
                // 1: Create a user in Firebase with an Email and Password.
                var respond = await auth.CreateUserWithEmailAndPasswordAsync(userNameString, passwordString);
                // 2: User was created and also user is also Logged in
                // 3: We Store the Uid of the user
                //fullDetaillsLoggedInUser = new AuthUser()
                //{
                //    Email = respond.User.Info.Email,
                //    Id = respond.User.Uid,
                //    FullName = fullName
                //};
                //// 3: We can continue and add more details about the user but this time in the firebase Database
                //// Example: saving the full name
                //await client
                //    .Child("users")
                //    .Child(fullDetaillsLoggedInUser.Id)
                //    .PutAsync(new
                //    {
                //        fullName = fullName
                //    });

                return true;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    ex.Message,
                    "OK"
                );

                return false;
            }
        }

        public async Task<bool> TryLogin(string userNameString, string passwordString)
        {
            if (userNameString == null || passwordString == null)
            {
                return false;
            }
            try
            {
                var authUser = await auth.SignInWithEmailAndPasswordAsync(userNameString, passwordString);
                loginAuthUser = authUser.AuthCredential;
                // We are logged in. Now go to DataBase and fetch data on user itself. Exampe 1 parameter: fullname
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
                // Authentication failed
                return false;
            }
        }


        public bool Logout()
        {
            try
            {
                auth.SignOut();
                loginAuthUser = null;
             //   fullDetaillsLoggedInUser = null;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Cake>?> GetCakesAsync()
        {
            try
            {
                var results = await client
                  .Child("users")
                  .Child(uid)
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

        public async Task<bool> CreateCakeOrder(Order order)
        {
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
        
        
    }
}