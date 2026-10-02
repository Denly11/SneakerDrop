using System.IO;
using System.Windows;
using System.Windows.Controls;
using CurrencyApp.Models;
using CurrencyApp.Services;

namespace CurrencyApp;

public partial class MainWindow : Window
{
    private const int RandomDayMaxDaysBack = 365;

    private readonly RatesStorage _storage = new(Path.Combine(AppContext.BaseDirectory, "rates.json"));
    // private readonly CnbClient _cnbClient = new();

    /// <summary>Rates currently shown in the UI (loaded, downloaded or saved).</summary>
    private DailyRates? _displayedRates;

    /// <summary>True if <see cref="_displayedRates"/> have not been saved yet.</summary>
    private bool _hasUnsavedRates;

    public MainWindow()
    {
        InitializeComponent();
        RefreshSavedDays();
    }

    // ==================================================================
    // Part 1 – local XML, JSON storage, browsing saved days
    // ==================================================================

    /// <summary>
    /// Lets the user pick an XML file, loads it and shows the rates.
    /// </summary>
    private void LoadXmlButton_Click(object sender, RoutedEventArgs e)
    {
        // TODO: Show an OpenFileDialog (Microsoft.Win32) for *.xml files.
        // TODO: Load the file with CnbXmlParser and show it with ShowRates.
        // TODO: Enable SaveButton and update StatusTextBlock. Handle invalid files.
    }

    /// <summary>
    /// Saves the displayed rates to the JSON file and refreshes the list of saved days.
    /// </summary>
    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        // TODO: Save _displayedRates with _storage and select the saved day in the list.
    }

    /// <summary>
    /// Shows the rates of the day selected in the list of saved days.
    /// </summary>
    private void SavedDaysListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // TODO: Show the selected DailyRates (SavedDaysListBox.SelectedItem).
        // TODO: Part 2 – enable DeleteButton.
    }

    /// <summary>
    /// Reloads saved days from the JSON file into the list and optionally selects one of them.
    /// </summary>
    private void RefreshSavedDays(DateTime? dateToSelect = null)
    {
        // TODO: Load all days from _storage and bind them to SavedDaysListBox.ItemsSource.
    }

    /// <summary>
    /// Displays the list of rates in the UI.
    /// </summary>
    /// <param name="requestedDate">Part 2: date the user asked for; show a warning if the CNB returned another one.</param>
    private void ShowRates(DailyRates rates, DateTime? requestedDate = null)
    {
        // TODO: Remember the rates in _displayedRates, fill DateTextBlock and RatesDataGrid.ItemsSource.
        // TODO: Part 2 – fill InfoTextBlock (downloaded at, age, Id), WarningTextBlock and
        //       CurrencyComboBox, then call ApplyFilter instead of setting the grid directly.
    }

    // ==================================================================
    // Part 2 – download from the CNB, DateTime, Random
    // ==================================================================

    /// <summary>
    /// Selects today in the date picker, which starts the download.
    /// </summary>
    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        // TODO: Select DateTime.Today in RatesDatePicker.
    }

    private void PreviousDayButton_Click(object sender, RoutedEventArgs e)
    {
        // TODO: Select the previous workday in RatesDatePicker.
    }

    private void NextDayButton_Click(object sender, RoutedEventArgs e)
    {
        // TODO: Select the next workday, but never a date in the future.
    }

    private void TodayButton_Click(object sender, RoutedEventArgs e)
    {
        // TODO: Select DateTime.Today.
    }

    private void RandomDayButton_Click(object sender, RoutedEventArgs e)
    {
        // TODO: Select a random workday within RandomDayMaxDaysBack days (Random.Shared).
    }

    /// <summary>
    /// Downloads the rates whenever a new date is selected.
    /// </summary>
    private async void RatesDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
    {
        // TODO: Download the rates for RatesDatePicker.SelectedDate (DownloadRatesAsync).
    }

    /// <summary>
    /// Downloads the rates for the given date and shows them. Handles network errors.
    /// </summary>
    private Task DownloadRatesAsync(DateTime date)
    {
        // TODO: Use SetBusy, _cnbClient.GetRatesAsync and ShowRates; catch HttpRequestException etc.
        throw new NotImplementedException();
    }

    /// <summary>
    /// Disables the toolbar and shows the progress bar while downloading.
    /// </summary>
    private void SetBusy(bool isBusy)
    {
        // TODO: Toggle ToolbarPanel.IsEnabled and BusyProgressBar.Visibility.
    }

    // ==================================================================
    // Part 2 – deleting saved days (Guid)
    // ==================================================================

    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        // TODO: Ask for confirmation (MessageBox) and delete the selected day by its Id.
    }

    // ==================================================================
    // Part 2 – filter and converter (Random)
    // ==================================================================

    private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        // TODO: Call ApplyFilter.
    }

    /// <summary>
    /// Shows only the rates whose code or country contains the text of FilterTextBox.
    /// </summary>
    private void ApplyFilter()
    {
        // TODO: Filter _displayedRates.Rates (LINQ) and set RatesDataGrid.ItemsSource.
    }

    private void AmountTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        // TODO: Call UpdateConversion.
    }

    private void CurrencyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // TODO: Call UpdateConversion.
    }

    private void RandomConversionButton_Click(object sender, RoutedEventArgs e)
    {
        // TODO: Select a random currency and fill in a random amount (Random.Shared).
    }

    /// <summary>
    /// Recalculates the converter result from AmountTextBox and CurrencyComboBox.
    /// </summary>
    private void UpdateConversion()
    {
        // TODO: Parse the amount, use CurrencyConverter.ToCzk and fill the result text blocks.
    }
}
