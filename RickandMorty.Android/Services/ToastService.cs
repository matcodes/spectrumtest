using System;
using Android.App;
using Android.Widget;
using RickandMorty.Droid.Services;
using RickandMorty.Interfaces;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(ToastService))]
namespace RickandMorty.Droid.Services
{
    public class ToastService : IToastService
    {
        private static Toast _toastInstance;

        public void ShowToast(string message, bool isLongToast = false)
        {
            var toastLength = isLongToast
                ? ToastLength.Long
                : ToastLength.Short;

            MainThread.BeginInvokeOnMainThread(() =>
            {
                _toastInstance?.Cancel();
                _toastInstance = Toast.MakeText(Android.App.Application.Context,
                    message, toastLength);
                _toastInstance?.Show();
            });
        }
    }
}
