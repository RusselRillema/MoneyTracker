using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracker.Dal
{
    public interface IFileHelper
    {
        string GetLocalFilePath(string fileName);
    }
}
