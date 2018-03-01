using MoneyTracker.Model;
using MoneyTracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoneyTracker.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ExpenseDetail : ContentPage
	{
		public ExpenseDetail (Expense expense)
        {
            BindingContext = new ExpenseDetailViewModel(expense);
            Title = "Expense Detail";
            InitializeComponent();
        }
	}
}