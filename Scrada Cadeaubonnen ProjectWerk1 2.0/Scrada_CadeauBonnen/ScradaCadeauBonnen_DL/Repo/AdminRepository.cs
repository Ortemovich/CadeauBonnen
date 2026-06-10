using Dapper;
using Microsoft.Data.SqlClient;
using ScradaCadeauBonnen_BL.Interfaces;
using ScradaCadeauBonnen_BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScradaCadeauBonnen_DL.Repo
{
    public class AdminRepository : IAdminRepository
    {
        private readonly string _connStr;

        public AdminRepository(string connectionString)
        {
            _connStr = connectionString;
        }

        public List<Gebruiker> GetGebruikers()
        {
            using (var conn = new SqlConnection(_connStr))
            {
                string sql = @"
            SELECT g.Email, g.AanmaakDatum, COUNT(cb.CadeaubonId) AS AantalBonnen
            FROM Gebruiker g
            LEFT JOIN Cadeaubon cb ON g.GebruikerId = cb.GebruikerId
            GROUP BY g.Email, g.AanmaakDatum";

                return conn.Query<Gebruiker>(sql).ToList();
            }
        }

        public List<Cadeaubon> GetBonnen()
        {
            using (var conn = new SqlConnection(_connStr))
            {
                string sql = @"
            SELECT c.CadeaubonCode, c.Bedrag, c.Saldo, c.VervalDatum, t.Naam AS ThemaNaam
            FROM Cadeaubon c
            INNER JOIN Thema t ON c.ThemaId = t.ThemaId";

                return conn.Query<Cadeaubon>(sql).ToList();
            }
        }

        public List<Transactie> GetTransacties()
        {
            using (var conn = new SqlConnection(_connStr))
            {
                string sql = @"
            SELECT t.Datum, g.Email AS GebruikerEmail, cb.CadeaubonCode, t.Status
            FROM Transactie t
            INNER JOIN Cadeaubon cb ON t.CadeaubonId = cb.CadeaubonId
            INNER JOIN Gebruiker g ON cb.GebruikerId = g.GebruikerId";

                return conn.Query<Transactie>(sql).ToList();
            }
        }

        public Gebruiker AuthenticateAdmin(string email, string hashedPassword)
        {
            using (var conn = new SqlConnection(_connStr))
            {
                string sql = @"
                SELECT * FROM Gebruiker
                WHERE Email = @Email AND Wachtwoord = @Wachtwoord";
                return conn.QuerySingleOrDefault<Gebruiker>(sql, new { Email = email, Wachtwoord = hashedPassword });
            }

        }

    }
}
