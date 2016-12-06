using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MvvmHelpers;
using Xamarin.Forms;

namespace QuadraDeTenis
{
	public partial class QuadraDeTenisPage : ContentPage
	{
		QuadraViewModel viewModel;

		public QuadraDeTenisPage()
		{
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			viewModel = new QuadraViewModel(this);
			viewModel.GetStoresCommand.Execute(null);
			BindingContext = viewModel;



		}

		private void btnDetails_Clicked(object sender, EventArgs e)
		{
			var b = (Button)sender;
			var t = b.CommandParameter;
			Device.OpenUri(new Uri($"tel:{t}"));
		}
	}
}
