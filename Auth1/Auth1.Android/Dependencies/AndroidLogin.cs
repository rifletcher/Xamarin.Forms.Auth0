using System;
using Android.App;
using Android.Content;
using Auth0.OidcClient;
using Auth1.Droid.Dependencies;
using Auth1.Interfaces;
using IdentityModel.OidcClient;
using Xamarin.Forms;

[assembly: Dependency(typeof(AndroidLogin))]

namespace Auth1.Droid.Dependencies
{
    public class AndroidLogin: ILogin
    {
        public Activity Context { get; set; }
        public Auth0Client client { get; set; }
        public AuthorizeState authorizeState { get; set; }

        public async void LoginAuth0()
        {
            try
            {
                client = new Auth0Client(new Auth0ClientOptions
                {
                    Domain = "channelyou.eu.auth0.com",
                    ClientId = "mXz8kEuaIIWmEMXumX7BJnnsHkrokYw6",
                    Activity = Context
                });
                authorizeState = await client.PrepareLoginAsync();

                // Send the user off to the authorization endpoint
                var uri = Android.Net.Uri.Parse(authorizeState.StartUrl);
                var intent = new Intent(Intent.ActionView, uri);
                intent.AddFlags(ActivityFlags.NoHistory);
                Context.StartActivity(intent);
            }
            catch (Exception ex)
            {
                ;
            }
        }
    }
}