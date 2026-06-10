using ScradaCadeauBonnen_BL.Manager;
using ScradaCadeauBonnen_DL.Repo;
using System;
using System.Collections.Generic;
using System.Configuration;
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
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private AdminBeheerder _adminBeheerder;

        public AdminWindow()
        {
            InitializeComponent();
            string connStr = @"Data Source=SOLOS-LAPTOP\SQLEXPRESS;Initial Catalog=CB_0.2;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"; 
            var repo = new AdminRepository(connStr); 
            _adminBeheerder = new AdminBeheerder(repo); 
            Loaded += AdminWindow_Loaded;
        }

        private void AdminWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                dgUsers.ItemsSource = _adminBeheerder.GetGebruikers();
                dgBonnen.ItemsSource = _adminBeheerder.GetBonnen();
                dgTransacties.ItemsSource = _adminBeheerder.GetTransacties();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fout bij het laden van gegevens: " + ex.Message);
            }
        }
    }
}
