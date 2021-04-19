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
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var betPairings = new List<BetPairing>();
            using (var connection = new SqlConnection(Environment.GetEnvironmentVariable("connectionString")))
            {
                var cmdText = "SELECT bp.EventTime, " + 
                    "b1.Bookmaker as b1_Bookmaker, b1.OutcomeDescription as b1_OutcomeDescription, b1.Odds as b1_Odds, b1.Wager as b1_Wager, b1.Offset as b1_Offset, " +
                    "b2.Bookmaker as b2_Bookmaker, b2.OutcomeDescription as b2_OutcomeDescription, b2.Odds as b2_Odds, b2.Wager as b2_Wager, b2.Offset as b2_Offset " +
                    "FROM BetPairing bp, Bet b1, Bet b2 WHERE bp.Bet1 = b1.Id AND bp.Bet2 = b2.Id;";
                var sqlCommand = new SqlCommand(cmdText, connection);
                connection.Open();
                using (var sqlDataReader = sqlCommand.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        var betPairing = new BetPairing()
                        {
                            EventTime = (DateTimeOffset)sqlDataReader["EventTime"],
                            Bet1 = new Bet()
                            {
                                Bookmaker = (string)sqlDataReader["b1_Bookmaker"],
                                OutcomeDescription = (string)sqlDataReader["b1_OutcomeDescription"],
                                Odds = (int)sqlDataReader["b1_Odds"],
                                Wager = (decimal)sqlDataReader["b1_Wager"],
                                Offset = (decimal)sqlDataReader["b1_Offset"],
                            },
                            Bet2 = new Bet()
                            {
                                Bookmaker = (string)sqlDataReader["b2_Bookmaker"],
                                OutcomeDescription = (string)sqlDataReader["b2_OutcomeDescription"],
                                Odds = (int)sqlDataReader["b2_Odds"],
                                Wager = (decimal)sqlDataReader["b2_Wager"],
                                Offset = (decimal)sqlDataReader["b2_Offset"],
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

