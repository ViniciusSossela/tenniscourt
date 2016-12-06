using System;
using System.Collections.Generic;

using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using QuadraDeTenis;
using Xamarin.Forms;

[assembly: Dependency(typeof(QuadraService))]
namespace QuadraDeTenis
{
	public class QuadraService : IService
	{
		private IMobileServiceSyncTable<Quadra> quadraTable;
		bool initialized = false;
		public MobileServiceClient MobileService { get; set; }


		public async Task<IEnumerable<Quadra>> GetQuadrasAsync()
		{
			//await this.SyncSaleItems();

			if (!initialized)
				await Init();
			
			await this.quadraTable.PullAsync("allSaleItems", this.quadraTable.CreateQuery());
			return await this.quadraTable.ToListAsync();
		}

		public async Task<Quadra> AddQuadraAsync(Quadra quadra)
		{
			if (!initialized)
				await Init();
			
			await this.quadraTable.InsertAsync(quadra);
			return quadra;
		}

		public async Task Init()
		{
			MobileService = new MobileServiceClient("http://sstenisbackend.azurewebsites.net");
			initialized = true;
			const string path = "syncstore.db";
			var store = new MobileServiceSQLiteStore(path);
			store.DefineTable<Quadra>();
			await MobileService.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());
			quadraTable = this.MobileService.GetSyncTable<Quadra>();
		}
	}
}
