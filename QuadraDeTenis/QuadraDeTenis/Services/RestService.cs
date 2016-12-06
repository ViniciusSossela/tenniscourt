using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.Geolocator;

namespace QuadraDeTenis
{
	public class RestService
	{
		HttpClient client;

		static RestService instance;

		public RestService()
		{
			client = new HttpClient();
			client.MaxResponseContentBufferSize = 256000;
		}

		public static RestService Instance()
		{
			instance = instance ?? new RestService();
			return instance;
		}

		public async Task<RootObject> GetPlaces()
		{

			try
			{
				var locator = CrossGeolocator.Current;
				locator.DesiredAccuracy = 50;
				var position = await locator.GetPositionAsync(timeoutMilliseconds: 10000);

				string latlong = $"{position.Latitude.ToString().Replace(",", ".")},{position.Longitude.ToString().Replace(",", ".")}";
				string url = $"https://maps.googleapis.com/maps/api/place/nearbysearch/json?location={latlong}&key=AIzaSyDfasHI57GB1RXPVUCkilCWFOb7UoINeVE&radius=10000&keyword=quadra+de+tênis";
				var uri = new Uri(string.Format(url, string.Empty));

				var response = await client.GetAsync(uri);
				if (response.IsSuccessStatusCode)
				{
					var content = await response.Content.ReadAsStringAsync();
					return JsonConvert.DeserializeObject<RootObject>(content);
				}
				else
				{
					return null;
				}
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		public async Task<PlaceDetails> GetPlaceDetails(string idPlace)
		{

			try
			{
				string url = $"https://maps.googleapis.com/maps/api/place/details/json?reference={idPlace}&key=AIzaSyDfasHI57GB1RXPVUCkilCWFOb7UoINeVE";
				var uri = new Uri(string.Format(url, string.Empty));

				var response = await client.GetAsync(uri);
				if (response.IsSuccessStatusCode)
				{
					var content = await response.Content.ReadAsStringAsync();
					return JsonConvert.DeserializeObject<PlaceDetails>(content);
				}
				else
				{
					return null;
				}
			}
			catch (Exception ex)
			{
				return null;
			}
		}
	}



	public class Location
	{
		public double lat { get; set; }
		public double lng { get; set; }
	}

	public class Northeast
	{
		public double lat { get; set; }
		public double lng { get; set; }
	}

	public class Southwest
	{
		public double lat { get; set; }
		public double lng { get; set; }
	}

	public class Viewport
	{
		public Northeast northeast { get; set; }
		public Southwest southwest { get; set; }
	}

	public class Geometry
	{
		public Location location { get; set; }
		public Viewport viewport { get; set; }
	}

	public class OpeningHours
	{
		public bool open_now { get; set; }
		public List<object> weekday_text { get; set; }
	}

	public class Photo
	{
		public int height { get; set; }
		public List<string> html_attributions { get; set; }
		public string photo_reference { get; set; }
		public int width { get; set; }
	}

	public class Result
	{
		public Geometry geometry { get; set; }
		public string icon { get; set; }
		public string id { get; set; }
		public string name { get; set; }
		public OpeningHours opening_hours { get; set; }
		public List<Photo> photos { get; set; }
		public string place_id { get; set; }
		public double rating { get; set; }
		public string reference { get; set; }
		public string scope { get; set; }
		public List<string> types { get; set; }
		public string vicinity { get; set; }
	}

	public class RootObject
	{
		public List<object> html_attributions { get; set; }
		public List<Result> results { get; set; }
		public string status { get; set; }
	}





	public class AddressComponent
	{
		public string long_name { get; set; }
		public string short_name { get; set; }
		public List<string> types { get; set; }
	}

	public class GeometryDetail
	{
		public Location location { get; set; }
	}

	public class ResultDetail
	{
		public List<AddressComponent> address_components { get; set; }
		public string adr_address { get; set; }
		public string formatted_address { get; set; }
		public string formatted_phone_number { get; set; }
		public GeometryDetail geometry { get; set; }
		public string icon { get; set; }
		public string id { get; set; }
		public string international_phone_number { get; set; }
		public string name { get; set; }
		public string place_id { get; set; }
		public string reference { get; set; }
		public string scope { get; set; }
		public List<string> types { get; set; }
		public string url { get; set; }
		public int utc_offset { get; set; }
		public string vicinity { get; set; }
		public string website { get; set; }
	}

	public class PlaceDetails
	{
		public List<object> html_attributions { get; set; }
		public ResultDetail result { get; set; }
		public string status { get; set; }
	}
}
