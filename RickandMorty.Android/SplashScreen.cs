using Android.Animation;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Com.Airbnb.Lottie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RickandMorty.Droid
{
    [Activity(Theme = "@style/AppTheme", MainLauncher = true, Label = "RickandMorty", NoHistory =true)]
    public class SplashScreen : Activity, Animator.IAnimatorListener
    {
        LottieAnimationView animationView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SplashScreen);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat)
            {
                Window window = Window;
                window.SetFlags(WindowManagerFlags.Fullscreen, WindowManagerFlags.Fullscreen);
                
                GC.Collect();
            }

            animationView = FindViewById<LottieAnimationView>(Resource.Id.lottie_animationView);
            animationView.AddAnimatorListener(this);
            animationView.RepeatCount = 0;

            animationView.PlayAnimation();
        }

        public void OnAnimationCancel(Animator animation) { }
        public void OnAnimationRepeat(Animator animation) { }
        public void OnAnimationStart(Animator animation) { }

        public void OnAnimationEnd(Animator animation)
        {
            StartActivity(new Intent(this, typeof(MainActivity)));
        }
    }
}