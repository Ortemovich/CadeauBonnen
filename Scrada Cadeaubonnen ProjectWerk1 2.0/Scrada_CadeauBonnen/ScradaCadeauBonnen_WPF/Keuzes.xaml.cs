using ScradaCadeauBonnen_BL.Models;
using ScradaCadeauBonnen_WPF.Bonnen;
using ScradaCadeauBonnen_WPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ScradaCadeauBonnen_WPF
{
    /// <summary>
    /// Interaction logic for Keuzes.xaml
    /// </summary>
    public partial class Keuzes : Window
    {
        private int geselecteerdThemaId = 1;
        private ObservableCollection<BonViewModel> bonnen = new();
        private ObservableCollection<CadeaubonUI> cadeaubons;
        private Gebruiker _gebruiker;

        public Keuzes(Gebruiker gebruiker)
        {
            InitializeComponent();
            _gebruiker = gebruiker;
            var fancyTemplate = (DataTemplate)Resources["FancyTemplate"];
            var moderneTemplate = (DataTemplate)Resources["ModerneTemplate"];
            var feestelijkeTemplate = (DataTemplate)Resources["FeestelijkeTemplate"];


            lstThemas.ItemsSource = new List<string> { "Fancy", "Moderne", "Feestelijk" };
            lstThemas.SelectedIndex = 0;


            var selector = new BonThemaSelector
            {
                FancyTemplate = fancyTemplate,
                ModerneTemplate = moderneTemplate,
                FeestelijkeTemplate = feestelijkeTemplate
            };


            ItemListBox.ItemTemplateSelector = selector;
            ItemListBox.ItemsSource = bonnen;
        }


        private void Toevoegen_Click(object sender, RoutedEventArgs e)
        {
            decimal bedrag = 0;


            foreach (var child in BedragRadioPanel.Children)
            {
                if (child is RadioButton rb && rb.IsChecked == true)
                {
                    if (decimal.TryParse(rb.Tag?.ToString(), out decimal selectedBedrag))
                    {
                        bedrag = selectedBedrag;
                    }
                    break;
                }
            }


            if (bedrag == 0 && decimal.TryParse(CustomTextBox.Text, out decimal customBedrag))
            {
                bedrag = customBedrag;
            }

            if (bedrag == 0)
            {
                MessageBox.Show("Selecteer een bedrag.");
                return;
            }

            var nieuweBon = new BonViewModel
            {
                Amount = bedrag,
                BonCode = "****",
                ValidUntil = DateTime.Now.AddMonths(12),
                ThemaId = geselecteerdThemaId
            };

            bonnen.Add(nieuweBon);
        }

        private void DeleteBon_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is BonViewModel bon)
            {
                bonnen.Remove(bon);
            }
        }


        private void Betaal_Click(object sender, RoutedEventArgs e)
        {
            var gekozenBonnen = new ObservableCollection<BonViewModel>(bonnen.ToList());
            Bevestiging b = new Bevestiging(gekozenBonnen, _gebruiker);
            b.ShowDialog();
        }



        private void RemoveItem_Click(object sender, RoutedEventArgs e)
        {
            if (ItemListBox.SelectedItem is BonViewModel selectedBon)
            {
                bonnen.Remove(selectedBon);
            }

        }

        private void ItemListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ItemListBox.SelectedItem is BonViewModel bon)
            {
                UserControl preview = bon.ThemaId switch
                {
                    1 => new FancyBon { Bon = bon },
                    2 => new ModerneBon { Bon = bon },
                    3 => new FeestelijkeBon { Bon = bon },
                    _ => null
                };

                ThemaPreview.Content = preview;
            }
            else
            {
                ThemaPreview.Content = null;
            }
        }

        private void VerwijderBonViaMenu_Click(object sender, RoutedEventArgs e)
        {
            if (ItemListBox.SelectedItem is BonViewModel geselecteerdeBon)
            {
                bonnen.Remove(geselecteerdeBon);
            }
        }



        private void lstThemas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstThemas.SelectedItem is string themaNaam)
            {
                switch (themaNaam)
                {
                    case "Fancy":
                        geselecteerdThemaId = 1;
                        ThemaPreview.Content = new FancyBon();
                        break;
                    case "Feestelijk":
                        geselecteerdThemaId = 2;
                        ThemaPreview.Content = new ModerneBon();
                        break;
                    case "Moderne":
                        geselecteerdThemaId = 3;
                        ThemaPreview.Content = new FeestelijkeBon();
                        break;
                }
            }
        }
    } }