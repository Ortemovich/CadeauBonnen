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
using ScradaCadeauBonnen_BL.Interfaces;
using ScradaCadeauBonnen_BL.Manager;
using ScradaCadeauBonnen_BL.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ScradaCadeauBonnen_WPF
{
    /// <summary>
    /// Interaction logic for Registratie.xaml
    /// </summary>
    public partial class Registratie : Window
    {
        private string Voornaam;
        private string Achternaam;
        private string Email;
        private string wachtwoord;
        private GebruikerBeheerder _gebruikerBeheerder;

        
        public Registratie(GebruikerBeheerder gebruikerBeheerder) 
        {
            InitializeComponent();
            _gebruikerBeheerder = gebruikerBeheerder;

        }
        

        private void btnRegistreren_Click(object sender, RoutedEventArgs e)
        {
            Voornaam = txtVoornaam.Text;
            Achternaam = txtAchternaam.Text;
            Email = txtEmail.Text;
            wachtwoord = pwdWachtwoord.Password;
            

            if (string.IsNullOrWhiteSpace(Voornaam) ||
                string.IsNullOrWhiteSpace(Achternaam) ||
                string.IsNullOrWhiteSpace(Email) ||
                string.IsNullOrWhiteSpace(wachtwoord))
            {
                MessageBox.Show("Gelieve alle velden in te vullen.");
                return;
            }

            wachtwoord = HashPassword(wachtwoord);
            DateTime localDate = DateTime.Now;                              // doesnt work properly

            Gebruiker nieuweGebruiker = new Gebruiker(Voornaam, Achternaam, Email, wachtwoord, localDate);       // later DTO toevoegen!

            _gebruikerBeheerder.VoegGebruikerToe(nieuweGebruiker);          // in DB zetten
            DialogResult = true;
            Close();

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
    }
}
