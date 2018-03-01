using MoneyTracker.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MoneyTracker.ViewModels
{
    public class MenuPageViewModel
    {
        public ICommand GoHomeCommand { get; set; }
        public ICommand GoNewExpenseCommand { get; set; }

        public MenuPageViewModel()
        {
            GoHomeCommand = new Command(GoHome);
            GoNewExpenseCommand = new Command(GoNewExpense);
        }

        void GoHome(object obj)
        {
            App.NavigationPage.Navigation.PopToRootAsync();
            App.MenuIsPresented = false;
        }

        void GoNewExpense(object obj)
        {
            App.NavigationPage.Navigation.PushAsync(new NewExpense());
            App.MenuIsPresented = false;
        }
    }
}
