using MoneyTracker.Model;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using Xamarin.Forms;

namespace MoneyTracker.ViewModels
{
    public class NewExpenseViewModel : INotifyPropertyChanged
    {
        Expense _expense;
        public ICommand TakePhotoCommand { get; set; }
        public ICommand SaveExpense { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public string Location
        {
            get
            {
                return _expense.Location;
            }
            set
            {
                _expense.Location = value;
            }
        }
        public string Category
        {
            get
            {
                return _expense.Category;
            }
            set
            {
                _expense.Category = value;
            }
        }
        public DateTime Date
        {
            get
            {
                return _expense.Date;
            }
            set
            {
                _expense.Date = value;
            }
        }
        private string _total { get; set; }
        public string Total
        {
            get
            {
                return _total;
            }
            set
            {
                _total = value;
                decimal.TryParse(value, out decimal t);
                if (_expense.TotalWithTip == 0 || _expense.TotalWithTip == _expense.Total)
                {
                    _totalWithTip = value;
                    _expense.TotalWithTip = t;
                }
                _expense.Total = t;
                InvokeTotalsChanged();
            }
        }
        private string _totalWithTip { get; set; }
        public string TotalWithTip
        {
            get
            {
                return _totalWithTip;
            }
            set
            {
                _totalWithTip = value;
                decimal.TryParse(value, out decimal t);
                _expense.TotalWithTip = t;
                InvokeTotalsChanged();
            }
        }
        public string TotalTip
        {
            get
            {
                return GetTipString(_expense.Total, _expense.TotalWithTip);
            }
        }
        public bool SplitBill
        {
            get
            {
                return _expense.SplitBill;
            }
            set
            {
                _expense.SplitBill = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SplitBill"));
            }
        }
        private string _splitPortion { get; set; }
        public string SplitPortion
        {
            get
            {
                return _splitPortion;
            }
            set
            {
                _splitPortion = value;
                decimal.TryParse(value, out decimal t);
                _expense.SplitPortion = t;
                InvokeSplitPortionsChanged();
            }
        }
        private string _splitPortionWithTip { get; set; }
        public string SplitPortionWithTip
        {
            get
            {
                return _splitPortionWithTip;
            }
            set
            {
                _splitPortionWithTip = value;
                decimal.TryParse(value, out decimal t);
                _expense.SplitPortionWithTip = t;
                InvokeSplitPortionsChanged();
            }
        }
        public string SplitPortionTip
        {
            get
            {
                return GetTipString(_expense.SplitPortion, _expense.SplitPortionWithTip);
            }
        }

        public NewExpenseViewModel()
        {
            TakePhotoCommand = new Command(TakePhoto);
            SaveExpense = new Command(SaveNewExpense);
            _expense = new Expense();
        }

        private void TakePhoto(object obj)
        {
            StartCameraAsync();
        }

        private async void StartCameraAsync()
        {
            await CrossMedia.Current.Initialize();


            if (Plugin.Media.CrossMedia.Current.IsTakePhotoSupported && CrossMedia.Current.IsCameraAvailable)
            {
                var photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions()
                {
                    SaveToAlbum = false,
                    SaveMetaData = false,
                    Directory = "Pictures",
                    Name = "test.jpg"
                });



                if (photo != null)
                {
                    var stream = photo.GetStream();
                    var bytes = new byte[stream.Length];
                    await stream.ReadAsync(bytes, 0, (int)stream.Length);
                    string base64 = System.Convert.ToBase64String(bytes);
                    _expense.ReceiptImageBase64 = base64;
                }
                else
                    return;
            }
            else
            {
                return;
            }
        }

        private void SaveNewExpense(object obj)
        {
            App.Database.SaveExpenseAsync(_expense);
            GoHome();
        }

        private void InvokeTotalsChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Total"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TotalWithTip"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TotalTip"));
        }

        private void InvokeSplitPortionsChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SplitPortion"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SplitPortionWithTip"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SplitPortionTip"));
        }

        private string GetTipString(decimal total, decimal totalWithTip)
        {
            decimal tip = totalWithTip == 0 ? 0 : totalWithTip - total;
            return $"Tip: R{tip} ({GetTipPercent(total, tip)}%)";
        }

        private string GetTipPercent(decimal total, decimal tip)
        {
            if (tip == 0)
                return "0";
            else if (total == 0)
                return "∞";
            else
                return (tip / total * 100).ToString();
        }

        void GoHome()
        {
            App.NavigationPage.Navigation.PopToRootAsync();
            App.MenuIsPresented = false;
        }
    }
}
