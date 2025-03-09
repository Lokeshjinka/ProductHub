
namespace BAL.Constants
{
    public class ApiConstant
    {
        public static readonly int IdGenerationMaxAttempts = 100;
        public static readonly int MinInstanceId = 10;
        public static readonly int MaxInstanceId = 99;
        public static readonly string TwoDigitFormat = "D2";
        public static readonly string ProductNamePattern = @"^(?=.*[a-zA-Z])[a-zA-Z0-9 ]*$";
    }
    public static class ApiMessages
    {
        public static readonly string API001 = "Failed to generate a unique ID after multiple attempts.";
        public static readonly string API002 = "NodeId must be between 10 and 99.";
        public static readonly string API003 = "No products available.";
        public static readonly string API004 = "Product added successfully.";
        public static readonly string API005 = "Failed to add product. Please try again later.";
        public static readonly string API006 = "Product with ID {0} not found.";
        public static readonly string API007 = "Successfully deleted product with ID {0}.";
        public static readonly string API008 = "Failed to delete the product with ID {0}. Please try again later.";
        public static readonly string API009 = "Product body content is null or empty.";
        public static readonly string API010 = "ProductName is null or empty.";
        public static readonly string API011 = "Product name can only contain letters and numbers. ";
        public static readonly string API012 = "Invalid stock value, it must be greater than zero.";
        public static readonly string API013 = "Successfully updated the product with ID {0}.";
        public static readonly string API014 = "Failed to delete the product with ID {0}.";
        public static readonly string API015 = "Product stock value is null or empty.";
        public static readonly string API016 = "Failed to decrement stock for product with ID {0}. Not enough stock.";
        public static readonly string API017 = "Successfully decremented the stock value of product with ID {0} by {1}.";
        public static readonly string API018 = "Unable to decrement stock for product with ID {0}. Please try again later.";
        public static readonly string API019 = "Successfully incremented the stock value of product with ID {0} by {1}.";
        public static readonly string API020 = "Unable to increment stock for product with ID {0}. Please try again later.";
        public static readonly string API021 = "{0}. Please try again later.";
    }
}
