using MoneyTracker.Dal;
using MoneyTracker.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace MoneyTracker
{
	public partial class App : Application
	{
        // *********************************************************************
        // TODO: wrap in a global Navigation Service (for example purposes only)
        // https://wolfprogrammer.com/2016/07/22/navigation-using-mvvm-light/
        public static NavigationPage NavigationPage { get; private set; }
        private static RootPage RootPage;
        public static bool MenuIsPresented
        {
            get
            {
                return RootPage.IsPresented;
            }
            set
            {
                RootPage.IsPresented = value;
            }
        }
        // *********************************************************************


        static ExpenseDatabase database;

        public App ()
		{
			InitializeComponent();

            var menuPage = new MenuPage();
            NavigationPage = new NavigationPage(new HomePage());
            RootPage = new RootPage();
            RootPage.Master = menuPage;
            RootPage.Detail = NavigationPage;
            MainPage = RootPage;
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}

        public static ExpenseDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new ExpenseDatabase(DependencyService.Get<IFileHelper>().GetLocalFilePath("ExpenseSQLite.db3")); ;
                }
                return database;
            }
        }
    }
}
