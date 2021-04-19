using System;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;
using CloudClassProject.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BlazorApp.Api
{
    public static class AddBetPairing
    {
        [FunctionName("AddBetPairing")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            try
            {
                var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var betPairing = JsonConvert.DeserializeObject<BetPairing>(requestBody);

                using (var sqlConnection = new SqlConnection(Environment.GetEnvironmentVariable("connectionString")))
                {
                    sqlConnection.Open();
                    var bet1Id = InsertBet(betPairing.Bet1, sqlConnection);
                    var bet2Id = InsertBet(betPairing.Bet2, sqlConnection);
                    _ = InsertBetPairing(betPairing, bet1Id, bet2Id, sqlConnection);
                    sqlConnection.Close();
                }
                return new OkObjectResult(betPairing);
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }
        }

        private static int InsertBet(Bet bet, SqlConnection sqlConnection)
        {
            using (var cmd = new SqlCommand("INSERT INTO Bet(Bookmaker, OutcomeDescription, Odds, Wager, Offset) OUTPUT INSERTED.ID VALUES(@Bookmaker, @OutcomeDescription, @Odds, @Wager, @Offset)", sqlConnection))
            {
                cmd.Parameters.AddWithValue("@Bookmaker", bet.Bookmaker);
                cmd.Parameters.AddWithValue("@OutcomeDescription", bet.OutcomeDescription);
                cmd.Parameters.AddWithValue("@Odds", bet.Odds);
                cmd.Parameters.AddWithValue("@Wager", bet.Wager);
                cmd.Parameters.AddWithValue("@Offset", bet.Offset);
                var id = (int)cmd.ExecuteScalar();
                return id;
            }
        }

        private static int InsertBetPairing(BetPairing betPairing, int bet1Id, int bet2Id, SqlConnection sqlConnection)
        {
            using (var cmd = new SqlCommand("INSERT INTO BetPairing(EventTime, Bet1, Bet2) OUTPUT INSERTED.ID VALUES(@EventTime, @Bet1, @Bet2)", sqlConnection))
            {
                cmd.Parameters.AddWithValue("@EventTime", betPairing.EventTime);
                cmd.Parameters.AddWithValue("@Bet1", bet1Id);
                cmd.Parameters.AddWithValue("@Bet2", bet2Id);
                var id = (int)cmd.ExecuteScalar();
                return id;
            }
        }
    }
}
