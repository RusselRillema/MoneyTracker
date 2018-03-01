using MoneyTracker.Model;
using MoneyTracker.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MoneyTracker.ViewModels
{
    public class HomePageViewModel
    {
        public ObservableCollection<Expense> Expenses { get; set; } = new ObservableCollection<Expense>();
        public ICommand GoNewExpenseCommand { get; private set; }
        public ICommand ItemSelectedCommand { get; private set; }

        public HomePageViewModel()
        {
            GoNewExpenseCommand = new Command(GoNewExpense);
            ItemSelectedCommand = new Command<Expense>(HandleItemSelected);
            PopulateExpenses();
        }
        private void HandleItemSelected(Expense expense)
        {
            App.NavigationPage.Navigation.PushAsync(new ExpenseDetail(expense));
        }

        void GoNewExpense(object obj)
        {
            App.NavigationPage.Navigation.PushAsync(new NewExpense());
            App.MenuIsPresented = false;
        }
        private async void PopulateExpenses()
        {
            List<Expense> expenses = await App.Database.GetExpensesAsync();

            foreach (Expense expense in expenses)
            {
                Expenses.Add(expense);
            }
        }
    }
}
