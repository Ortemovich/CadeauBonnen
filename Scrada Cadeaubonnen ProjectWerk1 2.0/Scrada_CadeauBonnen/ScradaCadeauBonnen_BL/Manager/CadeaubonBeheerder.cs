using ScradaCadeauBonnen_BL.Models;
using System;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace ScradaCadeauBonnen_BL.Manager
{
    public class CadeaubonBeheerder
    {
        string _connectionString = @"Data Source=SOLOS-LAPTOP\SQLEXPRESS;Initial Catalog=CB_0.2;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
        public List<Cadeaubon> GetBonnenByGebruikerId(int gebruikerId)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string sql = @"
            SELECT CadeaubonId, GebruikerId, ThemaId, Bedrag, Saldo,
                   VervalDatum, CadeaubonCode, AanmaakDatum, Status
            FROM Cadeaubon
            WHERE GebruikerId = @gebruikerId";

                return conn.Query<Cadeaubon>(sql, new { gebruikerId }).ToList();
            }
        }
    }
}
