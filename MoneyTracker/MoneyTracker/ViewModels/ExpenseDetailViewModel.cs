using MoneyTracker.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace MoneyTracker.ViewModels
{
    public class ExpenseDetailViewModel
    {
        public Expense Expense { get; set; }

        public ExpenseDetailViewModel(Expense expense)
        {
            Expense = expense;
        }

        public ImageSource DecodedImage
        {
            get
            {
                if (Expense.ReceiptImageBase64 == null)
                    return null;

                byte[] imageBytes = Convert.FromBase64String(Expense.ReceiptImageBase64);
                return ImageSource.FromStream(() => new MemoryStream(imageBytes));
            }
        }
    }
}
