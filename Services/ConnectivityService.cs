using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeGrandOrder.Services
{
    public class ConnectivityService
    {
        private static ConnectivityService instance;
        private bool wasConnected = true;

        public static ConnectivityService GetInstance()
        {
            if (instance == null)
            {
                instance = new ConnectivityService();
            }
            return instance;
        }

        private ConnectivityService()
        {
            Connectivity.ConnectivityChanged += OnConnectivityChanged;
            wasConnected = Connectivity.NetworkAccess == NetworkAccess.Internet;
        }

        private async void OnConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            bool isConnected = e.NetworkAccess == NetworkAccess.Internet;

            if (!isConnected && wasConnected)
            {
                await ShowDisconnectedAlert();
            }
            else if (isConnected && !wasConnected)
            {
                await ShowReconnectedMessage();
            }

            wasConnected = isConnected;
        }

        private async Task ShowDisconnectedAlert()
        {
            if (Application.Current?.MainPage != null)
            {
                await Application.Current.MainPage.Dispatcher.DispatchAsync(async () =>
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "No Internet Connection",
                        "Your internet connection has been lost. Database operations are unavailable until connection is restored.",
                        "OK"
                    );
                });
            }
        }

        private async Task ShowReconnectedMessage()
        {
            if (Application.Current?.MainPage != null)
            {
                await Application.Current.MainPage.Dispatcher.DispatchAsync(async () =>
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Connected",
                        "Internet connection has been restored.",
                        "OK"
                    );
                });
            }
        }

        private bool IsConnected()
        {
            return Connectivity.NetworkAccess == NetworkAccess.Internet;
        }

        public async Task<bool> CheckConnectivityAndAlert(string operationName = "this operation")
        {
            if (!IsConnected())
            {
                if (Application.Current?.MainPage != null)
                {
                    await Application.Current.MainPage.Dispatcher.DispatchAsync(async () =>
                    {
                        await Application.Current.MainPage.DisplayAlert(
                            "No Internet Connection",
                            $"Cannot perform {operationName} without internet connection. Please check your network settings.",
                            "OK"
                        );
                    });
                }
                return false;
            }
            return true;
        }

        // in case we want to disable the service and stop listening to connectivity changes. Example working offline mode in the app where we don't want to show connectivity alerts
        public void Dispose()
        {
            Connectivity.ConnectivityChanged -= OnConnectivityChanged;
        }
    }
}