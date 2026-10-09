// Part 2 – uncomment when you get to it.

// using System.Net.Http;
// using CurrencyApp.Part1.Models;
//
// namespace CurrencyApp.Part1.Services;
//
// /// <summary>
// /// Downloads the daily exchange rates from the CNB web.
// /// </summary>
// public class CnbClient
// {
//     private const string BaseUrl =
//         "https://www.cnb.cz/cs/financni_trhy/devizovy_trh/kurzy_devizoveho_trhu/denni_kurz.xml";
//
//     // One HttpClient instance for the whole application.
//     private static readonly HttpClient HttpClient = new();
//
//     /// <summary>
//     /// Downloads the rate list for the given date. If no rates were published that day,
//     /// the CNB returns the last valid list – check <see cref="DailyRates.Date"/>.
//     /// </summary>
//     public Task<DailyRates> GetRatesAsync(DateTime date)
//     {
//         // TODO: Build the URL with ?date=dd.MM.yyyy and download it with HttpClient (async).
//         // TODO: Deserialize the stream with XmlSerializer and convert it to DailyRates.
//         throw new NotImplementedException();
//     }
// }
