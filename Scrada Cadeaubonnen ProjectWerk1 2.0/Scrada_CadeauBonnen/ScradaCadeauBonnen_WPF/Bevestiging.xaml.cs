using Scrada_Stripe_Api.Model;
using ScradaCadeauBonnen_BL.Models;
using ScradaCadeauBonnen_WPF.Bonnen;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ScradaCadeauBonnen_WPF
{
    /// <summary>
    /// Interaction logic for Bevestiging.xaml
    /// </summary>
    public partial class Bevestiging : Window
    {
        public ObservableCollection<BonViewModel> BestellingBonnen { get; set; }
        private readonly Gebruiker _gebruiker;

        public Bevestiging(ObservableCollection<BonViewModel> bonnen, Gebruiker gebruiker)
        {
            InitializeComponent();
            BestellingBonnen = bonnen;
            InfoBestelling.ItemsSource = BestellingBonnen;
            _gebruiker = gebruiker;
        }

        private int BerekenTotaal()
        {
            return (int)(BestellingBonnen.Sum(b => b.Amount) * 100);
        }

        private async Task StartBetalingAsync()
        {
            try
            {
                if (BestellingBonnen == null || BestellingBonnen.Count == 0)
                {
                    MessageBox.Show("Geen bonnen om af te rekenen.");
                    return;
                }

                var eersteBon = BestellingBonnen.First();
                int totaalBedragInCenten = BerekenTotaal();

                var request = new PaymentRequest
                {
                    Amount = totaalBedragInCenten,
                    Currency = "eur",
                    GebruikerId = _gebruiker.GebruikerId,
                    CadeaubonId = 0, 
                    ThemaId = eersteBon.ThemaId 
                };

                using var httpClient = new HttpClient();
                var response = await httpClient.PostAsJsonAsync("https://localhost:7124/api/payment/create-checkout-session", request);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<CheckoutSessionResponse>();

                    if (!string.IsNullOrEmpty(result?.Url))
                    {
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = result.Url,
                            UseShellExecute = true
                        });
                    }
                    else
                    {
                        MessageBox.Show("Geen URL ontvangen van de server.");
                    }
                }
                else
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Fout bij serveraanroep: {errorResponse}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Er is een fout opgetreden: " + ex.Message);
            }
        }

        private async void Bevestig_Click(object sender, RoutedEventArgs e)
        {
            await StartBetalingAsync();
        }

        private async void Betaal_Click(object sender, RoutedEventArgs e)
        {
            await StartBetalingAsync();
        }

        private void RemoveItem_Click(object sender, RoutedEventArgs e)
        {
            if (InfoBestelling.SelectedItem is BonViewModel selectedBon)
            {
                BestellingBonnen.Remove(selectedBon);
            }
            else
            {
                MessageBox.Show("Selecteer een item om te verwijderen.");
            }
        }
    }
}
