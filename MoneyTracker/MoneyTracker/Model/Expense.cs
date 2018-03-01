using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MoneyTracker.Model
{
    public class Expense
    {
        public int ID { get; set; }
        public string Location { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public decimal Total { get; set; }
        public decimal TotalWithTip { get; set; }
        public bool SplitBill { get; set; }
        public decimal SplitPortion { get; set; }
        public decimal SplitPortionWithTip { get; set; }
        public string ReceiptImageBase64 { get; set; }
    }
}
