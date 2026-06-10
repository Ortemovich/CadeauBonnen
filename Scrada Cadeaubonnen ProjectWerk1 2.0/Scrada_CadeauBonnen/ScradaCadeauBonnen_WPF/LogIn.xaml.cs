using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
using Microsoft.VisualBasic;
using ScradaCadeauBonnen_BL.Interfaces;
using ScradaCadeauBonnen_BL.Manager;
using ScradaCadeauBonnen_BL.Models;
using ScradaCadeauBonnen_DL.Repo;

namespace ScradaCadeauBonnen_WPF
{
    /// <summary>
    /// Interaction logic for LogIn.xaml
    /// </summary>
    public partial class LogIn : Window
    {
        private GebruikerBeheerder _gebruikerBeheerder;
        string connectionsString = @"Data Source=SOLOS-LAPTOP\SQLEXPRESS;Initial Catalog=CB_0.2;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
        string LogIn_userEmail;
        string LogIn_userPassword;


        public LogIn()
        {
            InitializeComponent();
            _gebruikerBeheerder = new GebruikerBeheerder(new ScradaRepository(connectionsString)); // moet anders. Voor later!!!!!
        }

        private void btnInloggen_Click(object sender, RoutedEventArgs e)
        {
            LogIn_userEmail = txtEmail.Text;
            LogIn_userPassword = HashPassword(pwdWachtwoord.Password);                            // code to login 

            Gebruiker gebruiker = _gebruikerBeheerder.GeefGebruikerByEmailEnWachtwoord(LogIn_userEmail, LogIn_userPassword);

            if (gebruiker != null)
            {
                MessageBox.Show($"Welkom, {gebruiker.Naam}!");
                Home home= new Home(gebruiker);                                                                 //   nog afwerken wnr. todo home.cs
                home.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Ongeldige email of wachtwoord.");
            }
        }

        private void btnRegistreren_Click(object sender, RoutedEventArgs e)
        {
            Registratie registratie = new Registratie(_gebruikerBeheerder);
            registratie.ShowDialog();
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())                         // code to hash ur password
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }

        private void btnLink_Click(object sender, RoutedEventArgs e)
        {
            string input = Interaction.InputBox("Voer een Link in:", "Link", "");

            BonViaLink bl = new BonViaLink(input, _gebruikerBeheerder);
            bl.ShowDialog();
        }
    }
}
