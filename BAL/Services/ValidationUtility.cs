using BAL.Constants;
using BAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BAL.Services
{
    public class ValidationUtility : IValidationUtility
    {
        public List<string> ValidProductName(string productName)
        {
            List<string> errors = new List<string>(); ;
            if(string.IsNullOrEmpty(productName) || string.IsNullOrWhiteSpace(productName))
                errors.Add(ApiMessages.API010);
            else if(!Regex.IsMatch(productName, ApiConstant.ProductNamePattern))
                errors.Add(ApiMessages.API011);
            return errors;
        }
        public List<string> ValidProductStock(int? stockValue)
        {
            List<string> errors = new List<string>();
            if(stockValue == 0 || stockValue < 0)
                errors.Add(ApiMessages.API012);
            return errors;
        }
    }
}
