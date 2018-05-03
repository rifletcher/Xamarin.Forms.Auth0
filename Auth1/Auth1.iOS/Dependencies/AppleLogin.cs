using System.Text;
using Auth1.iOS.Dependencies;
using Auth1.Interfaces;
using Xamarin.Forms;
using Auth0.OidcClient;
using UIKit;

[assembly: Dependency(typeof(AppleLogin))]
namespace Auth1.iOS.Dependencies
{

    public class AppleLogin : ILogin
    {

        private Auth0Client _client;

        public UIViewController Controller { get; set; }

        public async void LoginAuth0()
        {
            _client = new Auth0Client(new Auth0ClientOptions
            {
                Domain = "channelyou.eu.auth0.com",
                ClientId = "mXz8kEuaIIWmEMXumX7BJnnsHkrokYw6",
                Controller = Controller
            });

            var loginResult = await _client.LoginAsync(null);

            var sb = new StringBuilder();

            if (loginResult.IsError)
            {
                sb.AppendLine("An error occurred during login:");
                sb.AppendLine(loginResult.Error);
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