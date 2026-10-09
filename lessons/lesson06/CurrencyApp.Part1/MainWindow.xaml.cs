using System.IO;
using System.Windows;
using System.Windows.Controls;
using CurrencyApp.Part1.Models;
using CurrencyApp.Part1.Services;
using Microsoft.Win32;

namespace CurrencyApp.Part1;

public partial class MainWindow : Window
{
    private const int RandomDayMaxDaysBack = 365;

    private readonly RatesStorage _storage = new(Path.Combine(AppContext.BaseDirectory, "rates.json"));
    // private readonly CnbClient _cnbClient = new();

    private DailyRates? _displayedRates;

    private bool _hasUnsavedRates;

    public MainWindow()
    {
        InitializeComponent();
        RefreshSavedDays();
    }

    // ==================================================================
    // Part 1 – local XML, JSON storage, browsing saved days (solved)
    // ==================================================================

    private void LoadXmlButton_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new OpenFileDialog
        {
            Title = "Vyberte kurzovní lístek ČNB",
            Filter = "XML soubory (*.xml)|*.xml",
            InitialDirectory = Path.Combine(AppContext.BaseDirectory, "Data"),
        };

        if (dialog.ShowDialog() == true)
        {
            try
            {
                DailyRates rates = CnbXmlParser.Load(dialog.FileName);

                if (rates.Rates.Count > 0)
                {
                    SavedDaysListBox.SelectedItem = null;
                    ShowRates(rates);

                    _hasUnsavedRates = true;
                    SaveButton.IsEnabled = true;
                    StatusTextBlock.Text = $"Načteno ze souboru {Path.GetFileName(dialog.FileName)} – zatím neuloženo.";
                }
                else
                {
                    MessageBox.Show("Soubor neobsahuje žádné kurzy.", "Chyba",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Soubor se nepodařilo načíst:\n{ex.Message}", "Chyba",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        if (_displayedRates is null || !_hasUnsavedRates)
        {
            return;
        }

        try
        {
            _storage.Save(_displayedRates);
        }
        catch (RatesStorageException ex)
        {
            MessageBox.Show(ex.Message, "Chyba",
                MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        _hasUnsavedRates = false;
        SaveButton.IsEnabled = false;
        RefreshSavedDays(_displayedRates.Date);
        StatusTextBlock.Text = $"Kurzy k {_displayedRates.Date:dd.MM.yyyy} uloženy.";
    }

    private void SavedDaysListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (SavedDaysListBox.SelectedItem is not DailyRates day)
        {
            return;
        }

        ShowRates(day);

        _hasUnsavedRates = false;
        SaveButton.IsEnabled = false;
        StatusTextBlock.Text = $"Zobrazen uložený den {day.Date:dd.MM.yyyy}.";

        // TODO: Part 2 – enable DeleteButton.
    }

    private void RefreshSavedDays(DateTime? dateToSelect = null)
    {
        List<DailyRates> savedDays;
        try
        {
            savedDays = _storage.LoadAll();
        }
        catch (RatesStorageException ex)
        {
            MessageBox.Show(ex.Message, "Chyba",
                MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        SavedDaysListBox.ItemsSource = savedDays;

        if (dateToSelect is not null)
        {
            SavedDaysListBox.SelectedItem = savedDays.FirstOrDefault(day => day.Date.Date == dateToSelect.Value.Date);
        }
    }

    /// <param name="requestedDate">Part 2: date the user asked for; show a warning if the CNB returned another one.</param>
    private void ShowRates(DailyRates rates, DateTime? requestedDate = null)
    {
        _displayedRates = rates;

        DateTextBlock.Text = $"Kurzy ČNB k {rates.Date:dd.MM.yyyy}";
        RatesDataGrid.ItemsSource = rates.Rates;

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
