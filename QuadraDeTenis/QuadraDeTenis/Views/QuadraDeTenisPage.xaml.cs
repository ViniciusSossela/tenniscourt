using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MvvmHelpers;
using Xamarin.Forms;
using Xamarinos.AdMob.Forms;

namespace QuadraDeTenis
{
	public partial class QuadraDeTenisPage : ContentPage
	{
		QuadraViewModel viewModel;

		public QuadraDeTenisPage()
		{
			InitializeComponent();

			//var myBanner = new AdBanner();

			////Set Your AdMob Key
			//myBanner.AdID = "ca-app-pub-1460185125045371/6114685443";



		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			viewModel = new QuadraViewModel(this);
			viewModel.GetStoresCommand.Execute(null);
			BindingContext = viewModel;
			//await CrossAdmobManager.Current.Show();
		}

		private void btnDetails_Clicked(object sender, EventArgs e)
		{
			var b = (Button)sender;
			var t = b.CommandParameter;
			Device.OpenUri(new Uri($"tel:{t}"));
		}
	}
}
