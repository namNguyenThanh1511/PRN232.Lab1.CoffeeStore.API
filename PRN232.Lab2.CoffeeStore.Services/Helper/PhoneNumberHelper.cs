namespace PRN232.Lab2.CoffeeStore.Services.Helper
{
    public static class PhoneNumberHelper
    {
        //normalize phone number 
        public static string NormalizePhoneNumber(string phoneNumber)
        {
            // Remove all non-digit characters
            var digits = new string(phoneNumber.Where(char.IsDigit).ToArray());
            return digits;
        }

    }
}
