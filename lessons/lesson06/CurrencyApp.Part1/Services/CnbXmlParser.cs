using System.IO;
using System.Xml.Serialization;
using CurrencyApp.Part1.Models;

namespace CurrencyApp.Part1.Services;

public static class CnbXmlParser
{
    private static readonly XmlSerializer Serializer = new(typeof(DailyRates));

    public static DailyRates Load(string filePath)
    {
        // TODO (refactoring): Exceptions should be handled here, as close to their source as possible
        // (IOException, UnauthorizedAccessException, InvalidOperationException from XmlSerializer)
        // and rethrown as a custom exception the UI can catch instead of a general Exception.
        using FileStream stream = File.OpenRead(filePath);
        return (DailyRates?)Serializer.Deserialize(stream)
            ?? throw new InvalidOperationException("XML document is empty.");
    }
}
