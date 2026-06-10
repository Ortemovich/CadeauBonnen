using ScradaCadeauBonnen_BL.Models;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        private Gebruiker _gebruiker;
        public Home(Gebruiker gebruiker)
        {
            InitializeComponent();
            _gebruiker = gebruiker;
            btnAdminManagement.Visibility = _gebruiker.IsAdmin ? Visibility.Visible : Visibility.Collapsed; 

        }

        private void Kopen_Click(object sender, RoutedEventArgs e)
        {
            Keuzes keuzes = new Keuzes(_gebruiker);
            keuzes.Show();
        }

        private void Bestellingen_Click(object sender, RoutedEventArgs e)
        {
            Cadeaubonnen cadeaubonnen = new Cadeaubonnen();
            cadeaubonnen.Show();

        }

        private void Cadeaubonnen_Click(object sender, RoutedEventArgs e)
        {
            Cadeaubonnen cadeaubonnen = new Cadeaubonnen();
            cadeaubonnen.Show();

        }

        private void AdminManagement_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow adminW = new AdminWindow();
            bool? result = adminW.ShowDialog();
        }
    }
}


// er moet nog boven in xaml staan de info van gebruiker