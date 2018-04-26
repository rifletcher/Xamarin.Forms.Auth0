using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Auth1.Interfaces;

namespace Auth1
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

	    async void Handle_Clicked(object sender, System.EventArgs e)
	    {
	        DependencyService.Get<ILogin>().LoginAuth0();
	    }
    }
}
