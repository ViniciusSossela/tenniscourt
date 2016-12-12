using System;
using System.Threading.Tasks;
using MvvmHelpers;
using Xamarin.Forms;
using System.Linq;
using System.Collections.Generic;

namespace QuadraDeTenis
{
	public class QuadraViewModel : ViewModelBase
	{
		public string Texto { get; set; }
		public ObservableRangeCollection<QuadraListView> quadras { get; set; }
		//readonly IService quadraService;

		public QuadraViewModel(Page page) : base(page)
		{
			//quadraService = DependencyService.Get<IService>();
			Texto = "Quadras de Tênis";
			quadras = new ObservableRangeCollection<QuadraListView>();
		}


		private Command getStoresCommand;
		public Command GetStoresCommand
		{
			get
			{
				return getStoresCommand ??
					(getStoresCommand = new Command(async () => await ExecuteGetStoresCommand(), () => { return !IsBusy; }));
			}
		}

		Command callCommand;
		public Command CallCommand
		{
			get
			{
				return callCommand ?? (callCommand = new Command(() =>
				{
					Device.OpenUri(new Uri("tel:054-99199924"));

					//page.Navigation.PushAsync(new QuadraDetalhePage(null));

				}));
			}
		}

		private async Task ExecuteGetStoresCommand()
		{

			if (IsBusy)
				return;
			
			IsBusy = true;

			GetStoresCommand.ChangeCanExecute();

			try
			{

				//var stores = await quadraService.GetQuadrasAsync();
				var places = await RestService.Instance().GetPlaces();

				if (places != null && places.results.Any())
				{
					var a = places.results.Select(async q => new Quadra
					{
						descricao = q.name,
						endereco = q.vicinity,
						rating = q.rating > 0 ? $"Avaliação: {q.rating}" : "Sem Avaliação",
						details = await RestService.Instance().GetPlaceDetails(q.reference),
						imageUrl =
						q.photos != null ? $"https://maps.googleapis.com/maps/api/place/photo?maxwidth=400&photoreference={q.photos.First().photo_reference}&key=AIzaSyDfasHI57GB1RXPVUCkilCWFOb7UoINeVE" : "ic_photo_black_48dp.png"
					});

					var results = await Task.WhenAll(a);


					List<QuadraListView> quadrasListView = new List<QuadraListView>();

					for (int i = 0; i < results.Length; i++)
					{
						quadrasListView.Add(new QuadraListView
						{
							First = results.Length >= i ? results[i] : new Quadra(),
							Second = results.Length >= i + 1 ? results[i + 1] : new Quadra()
						});
						i++;
					}

					//IEnumerable<Quadra> a = new List<Quadra> { new Quadra { descricao = "quadra 1" } };

					quadras.ReplaceRange(quadrasListView);
				}

			}
			catch (Exception ex)
			{
				String a = ex.Message;

			}
			finally
			{
				IsBusy = false;
				GetStoresCommand.ChangeCanExecute();
			}


		}
	}
}
