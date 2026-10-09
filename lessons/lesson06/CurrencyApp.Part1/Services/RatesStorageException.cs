namespace CurrencyApp.Part1.Services;

public class RatesStorageException : Exception
{
    public RatesStorageException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
