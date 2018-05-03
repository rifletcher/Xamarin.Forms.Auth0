using System;
using Auth1.iOS.Dependencies;
using Auth1.Interfaces;
using UIKit;
using Xamarin.Forms;

namespace Auth1.iOS
{
    public partial class MyViewController : UIViewController
    {
        public MyViewController() : base("MyViewController", null)
        {
            var appleLogin = (AppleLogin)DependencyService.Get<ILogin>();
            appleLogin.Controller = this;

        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.
        }
    }
}