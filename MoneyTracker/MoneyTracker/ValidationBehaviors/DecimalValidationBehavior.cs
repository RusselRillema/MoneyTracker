using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MoneyTracker.ValidationBehaviors
{
    class DecimalValidationBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += Entry_TextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= Entry_TextChanged;
            base.OnDetachingFrom(entry);
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool isValid = decimal.TryParse(e.NewTextValue, out decimal result);
            Entry entry = sender as Entry;
            entry.TextColor = isValid ? Color.Default : Color.Red;
        }
    }
}
