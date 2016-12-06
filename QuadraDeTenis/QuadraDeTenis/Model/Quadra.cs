using System;
namespace QuadraDeTenis
{
	public class Quadra
	{
		public string Id { get; set; }

		public DateTimeOffset? CreatedAt { get; set; }

		public string descricao { get; set; }

		public string imageUrl { get; set; }

		public string endereco
		{
			get;
			set;
		}

		public string rating
		{
			get;
			set;
		}

		public PlaceDetails details
		{
			get;
			set;
		}

		public bool showCallButton { get { return string.IsNullOrEmpty(details.result.formatted_phone_number) ? false : true; } }
	}

	public class QuadraListView
	{
		public Quadra First
		{
			get;
			set;
		}

		public Quadra Second
		{
			get;
			set;
		}
	}
}
