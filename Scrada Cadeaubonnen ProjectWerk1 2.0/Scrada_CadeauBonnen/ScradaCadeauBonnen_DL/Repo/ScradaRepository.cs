using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using ScradaCadeauBonnen_BL.Interfaces;
using ScradaCadeauBonnen_BL.Models;

namespace ScradaCadeauBonnen_DL.Repo
{
    public class ScradaRepository : IScradaRepository
    {
        private string connectionString;

        public ScradaRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }


        public void VoegGebruikerToe(Gebruiker gebruiker)
        {
            string query = @" INSERT INTO Gebruiker (Naam, Achternaam, Email, Wachtwoord)
                            OUTPUT INSERTED.GebruikerId
                            VALUES (@Naam, @Achternaam, @Email, @Wachtwoord);";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        gebruiker.GebruikerId = connection.ExecuteScalar<int>(query, new { gebruiker.Naam, gebruiker.Achternaam, gebruiker.Email, gebruiker.Wachtwoord }, transaction: transaction);

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
        //public void VoegGebruikerToe(Gebruiker gebuiker)
        //{
        //    string query = "INSERT INTO Gebruiker (Naam, Achternaam, Email, Wachtwoord) OUTPUT INSERTED.GebruikerId VALUES (@Naam, @Achternaam, @Email, @Wachtwoord)";
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    using (SqlCommand command = connection.CreateCommand())
        //    {
        //        connection.Open();
        //        SqlTransaction transaction = connection.BeginTransaction();
        //        command.Transaction = transaction;
        //        try
        //        {
        //            command.CommandText = query;
        //            command.Parameters.AddWithValue("@Naam", gebuiker.Naam);
        //            command.Parameters.AddWithValue("@Achternaam", gebuiker.Achternaam);
        //            command.Parameters.AddWithValue("@Email", gebuiker.Email);
        //            command.Parameters.AddWithValue("@Wachtwoord", gebuiker.Wachtwoord);
        //            gebuiker.GebruikerId = (int)command.ExecuteScalar();

        //            transaction.Commit();
        //        }
        //        catch (Exception ex)
        //        {
        //            transaction.Rollback();
        //            Console.WriteLine(ex.Message);
        //        }
        //    }
        //}

        public Gebruiker GeefGebruikerByEmailEnWachtwoord(string email, string wachtwoord)
        {
            string query = "SELECT * FROM Gebruiker WHERE Email = @Email AND Wachtwoord = @Wachtwoord";

            using (var connection = new SqlConnection(connectionString))
            {
                return connection.QueryFirstOrDefault<Gebruiker>(query, new { Email = email, Wachtwoord = wachtwoord });

            }
        }

        //public Gebruiker GeefGebruikerByEmailEnWachtwoord(string email, string wachtwoord)
        //{
        //    Gebruiker gebruiker = null;
        //    string query = "SELECT * FROM Gebruiker WHERE Email = @Email AND Wachtwoord = @Wachtwoord";

        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    using (SqlCommand command = new SqlCommand(query, connection))
        //    {
        //        command.Parameters.AddWithValue("@Email", email);
        //        command.Parameters.AddWithValue("@Wachtwoord", wachtwoord);
        //        connection.Open();


        public Cadeaubon ZoekCadeaubonOpCode(string code)
        {
            using var conn = new SqlConnection(connectionString);
            conn.Open();

            var query = @"SELECT CadeaubonId, GebruikerId, ThemaId, Bedrag, Saldo, VervalDatum, CadeaubonCode, AanmaakDatum, Status
                      FROM Cadeaubon
                      WHERE CadeaubonCode = @code";

            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@code", code);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Cadeaubon
                {
                    CadeaubonId = (int)reader["CadeaubonId"],
                    GebruikerId = (int)reader["GebruikerId"],
                    ThemaId = (int)reader["ThemaId"],
                    Bedrag = (decimal)reader["Bedrag"],
                    Saldo = (decimal)reader["Saldo"],
                    VervalDatum = (DateTime)reader["VervalDatum"],
                    CadeaubonCode = (string)reader["CadeaubonCode"],
                    AanmaakDatum = (DateTime)reader["AanmaakDatum"],
                    Status = (string)reader["Status"]
                };
            }

            return null;
        }




    }




}
