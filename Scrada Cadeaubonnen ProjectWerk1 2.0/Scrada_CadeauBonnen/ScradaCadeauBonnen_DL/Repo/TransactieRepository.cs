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
    public class TransactieRepository : ITransactieRepository
    {
        private string connectionString;

        public TransactieRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public void VerwerkVolledigeBetaling(Cadeaubon cadeaubon, Betaling betaling, Transactie transactie)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // Check of de gebruiker bestaat
                        var gebruikerBestaat = conn.ExecuteScalar<int>(
                            "SELECT COUNT(1) FROM Gebruiker WHERE GebruikerId = @GebruikerId",
                            new { GebruikerId = cadeaubon.GebruikerId },
                            transaction
                        ) > 0;

                        if (!gebruikerBestaat)
                        {
                            throw new Exception($"Gebruiker met ID {cadeaubon.GebruikerId} bestaat niet.");
                        }

                        // Check of het thema bestaat
                        var themaBestaat = conn.ExecuteScalar<int>(
                            "SELECT COUNT(1) FROM Thema WHERE ThemaId = @ThemaId",
                            new { ThemaId = cadeaubon.ThemaId },
                            transaction
                        ) > 0;

                        if (!themaBestaat)
                        {
                            throw new Exception($"Thema met ID {cadeaubon.ThemaId} bestaat niet.");
                        }

                        // 1. Cadeaubon opslaan
                        string cadeaubonQuery = @"
                    INSERT INTO Cadeaubon (GebruikerId, ThemaId, Bedrag, Saldo, VervalDatum, CadeaubonCode, AanmaakDatum, Status)
                    VALUES (@GebruikerId, @ThemaId, @Bedrag, @Saldo, @VervalDatum, @CadeaubonCode, @AanmaakDatum, @Status);
                    SELECT CAST(SCOPE_IDENTITY() as int);";
                        int cadeaubonId = conn.QuerySingle<int>(cadeaubonQuery, cadeaubon, transaction);

                        // 2. Betaling opslaan (met CadeaubonId)
                        betaling.CadeaubonId = cadeaubonId;
                        string betalingQuery = @"
                    INSERT INTO Betaling (GebruikerId, CadeaubonId, StripeBetalingId, BetaalMethode, BetalingDatum, Bedrag)
                    VALUES (@GebruikerId, @CadeaubonId, @StripeBetalingId, @BetaalMethode, @BetalingDatum, @Bedrag);";
                        conn.Execute(betalingQuery, betaling, transaction);

                        // 3. Transactie opslaan (met CadeaubonId)
                        transactie.CadeaubonId = cadeaubonId;
                        transactie.Status = "bevestigd";
                        string transactieQuery = @"
                    INSERT INTO Transactie (CadeaubonId, Bedrag, Datum, Reden, Status, StripeBetalingId)
                    VALUES (@CadeaubonId, @Bedrag, @Datum, @Reden, @Status, @StripeBetalingId);";
                        conn.Execute(transactieQuery, transactie, transaction);

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}
