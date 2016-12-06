using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace QuadraDeTenis.UITests
{
	[TestFixture(Platform.Android)]
	[TestFixture(Platform.iOS)]
	public class Tests
	{
		IApp app;
		Platform platform;

		public Tests(Platform platform)
		{
			this.platform = platform;
		}

		[SetUp]
		public void BeforeEachTest()
		{
			app = AppInitializer.StartApp(platform);
		}

		[Test]
		public void WelcomeTextIsDisplayed()
		{
			AppResult[] results = app.WaitForElement(c => c.Marked("Welcome to Xamarin Forms!"));
			app.Screenshot("Welcome screen.");

			Assert.IsTrue(results.Any());
		}

		[Test]
		public void NewTest()
		{
			app.Tap(x => x.Text("Call"));
			app.Screenshot("Tapped on view with class: AppCompatButton");
			app.Tap(x => x.Text("Detalheee"));
			app.Screenshot("Tapped on view with class: FormsTextView");
		}

	}
}
