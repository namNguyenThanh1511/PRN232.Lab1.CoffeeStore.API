namespace PRN232.Lab2.CoffeeStore.Services.Constants
{
    public interface IRegexStorage
    {
        const string EMAIL_PATTERN = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        const string PHONE_PATTERN = @"^(0|\+84)(3[2-9]|5[6|8|9]|7[0|6-9]|8[1-9]|9[0-4|6-9])[0-9]{7}$";

    }
}
