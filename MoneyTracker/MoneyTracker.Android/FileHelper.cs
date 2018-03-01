using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MoneyTracker.Dal;
using MoneyTracker.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace MoneyTracker.Droid
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string fileName)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return Path.Combine(path, fileName);
        }
    }
}