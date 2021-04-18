using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;
using CloudClassProject.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace Api
{
    public static class GetBetPairings
    {
        [FunctionName("GetBetPairings")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var betPairings = new List<BetPairing>();
            using (var connection = new SqlConnection(Environment.GetEnvironmentVariable("connectionString")))
            {
                var cmdText = "SELECT *" +
                    "FROM BetPairing as bp, Bet as b1, Bet as b2" +
                    "WHERE bp.Bet1 = b1.Id AND bp.Bet2 = b2.Id";
                var sqlCommand = new SqlCommand(cmdText, connection);
                connection.Open();
                using (var sqlDataReader = sqlCommand.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        var betPairing = new BetPairing()
                        {
                            EventTime = (DateTimeOffset)sqlDataReader["bp.EventTime"],
                            Bet1 = new Bet()
                            {
                                Bookmaker = (string)sqlDataReader["b1.Bookmaker"],
                                OutcomeDescription = (string)sqlDataReader["b1.OutcomeDescription"],
                                Odds = (int)sqlDataReader["b1.Odds"],
                                Wager = (decimal)sqlDataReader["b1.Wager"],
                                Offset = (decimal)sqlDataReader["b1.Offset"],
                            },
                            Bet2 = new Bet()
                            {
                                Bookmaker = (string)sqlDataReader["b2.Bookmaker"],
                                OutcomeDescription = (string)sqlDataReader["b2.OutcomeDescription"],
                                Odds = (int)sqlDataReader["b2.Odds"],
                                Wager = (decimal)sqlDataReader["b2.Wager"],
                                Offset = (decimal)sqlDataReader["b2.Offset"],
                            }
                        };
                        betPairings.Add(betPairing);
                    }
                    connection.Close();
                }
            }

            return new OkObjectResult(betPairings);
        }
    }
}

