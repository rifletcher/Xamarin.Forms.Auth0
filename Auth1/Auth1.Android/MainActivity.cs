using System;
using System.Text;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Auth1.Droid.Dependencies;
using Auth1.Interfaces;
using Xamarin.Forms;

namespace Auth1.Droid
{
    [Activity(Label = "Auth1", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, LaunchMode = LaunchMode.SingleTask)]
    [IntentFilter(
        new[] { Intent.ActionView },
        Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
        DataScheme = "com.companyname.Auth1",
        DataHost = "@string/auth0_domain",
        DataPathPrefix = "/android/com.companyname.Auth1/callback")]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            var androidLogin = (AndroidLogin)DependencyService.Get<ILogin>();
            androidLogin.Context = this;
            LoadApplication(new App());
        }

        protected override async void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
            var androidLogin = (AndroidLogin)DependencyService.Get<ILogin>();
            androidLogin.Context = this;

            var loginResult = await androidLogin.client.ProcessResponseAsync(intent.DataString, androidLogin.authorizeState);

            var sb = new StringBuilder();
            if (loginResult.IsError)
            {
                sb.AppendLine($"An error occurred during login: {loginResult.Error}");
            }
            else
            {
                sb.AppendLine($"ID Token: {loginResult.IdentityToken}");
                sb.AppendLine($"Access Token: {loginResult.AccessToken}");
                sb.AppendLine($"Refresh Token: {loginResult.RefreshToken}");

                sb.AppendLine();

                sb.AppendLine("-- Claims --");
                foreach (var claim in loginResult.User.Claims)
                {
                    sb.AppendLine($"{claim.Type} = {claim.Value}");
                }
            }
        }

    }
}

