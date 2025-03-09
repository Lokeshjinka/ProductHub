using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interfaces
{
    public interface IValidationUtility
    {
        List<string> ValidProductName(string productName);
        List<string> ValidProductStock(int? stockValue);
    }
}
