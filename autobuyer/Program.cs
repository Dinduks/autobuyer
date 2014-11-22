using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Autobuyer;
using Nito.AsyncEx;
using UltimateTeam.Toolkit;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Parameters;

namespace autobuyer {
    internal class Program {
        private static readonly FutClient client = new FutClient();
        private static readonly int CreditsThreshold = int.Parse(ConfigurationManager.AppSettings["creditsThreshold"]);
        private static uint _balance;

        private static void Main(string[] args) {
            Console.WriteLine("Start...");
            var loginDetails = new LoginDetails(ConfigurationManager.AppSettings["username"],
                ConfigurationManager.AppSettings["password"],
                ConfigurationManager.AppSettings["secretAnswer"],
                Platform.Pc);
            AsyncContext.Run(() => MainAsync(loginDetails));
        }

        private static async void MainAsync(LoginDetails loginDetails) {
            await client.LoginAsync(loginDetails);
            Console.WriteLine("Logged in.");

//            int max = 10;
//            int bought = 0;

            while (true) {
                foreach (WatchableItemInfo watchedItem in WatchedItems.items) {
                    Console.WriteLine("\nLooking for " + watchedItem.SearchParameters.Method.Name + "...");

                    List<AuctionInfo> results = await SearchPlayer(watchedItem.SearchParameters, 1, 5);
                    results.Sort((x, y) => x.BuyNowPrice.CompareTo(y.BuyNowPrice));
                    results = results.Where(result =>
                        result.BuyNowPrice <= watchedItem.MaxBuyPrice && result.ItemData.Contract > 0).ToList();

                    //PrintCheapestPlayers(results, 5);

                    int loops = 5;
                    foreach (AuctionInfo result in results) {
                        Thread.Sleep(1000);

                        loops--;
                        if (loops < 0) break;

                        Console.Write("Trying to buy for " + result.BuyNowPrice + "...\t");
                        try {
                            await client.PlaceBidAsync(result, result.BuyNowPrice);
                            _balance -= result.BuyNowPrice;
                            Console.WriteLine("Success!");
//                            bought++;

//                            if (bought == max) {
//                                Console.WriteLine("\nBought " + bought + " items.\nBye.");
//                                return;
//                            }
                        } catch (Exception exception) {
                            Console.WriteLine("Error: " + exception.Message);
                        }
                    }

                    Thread.Sleep(60*1000);
                }

                _balance = (await client.GetCreditsAsync()).Credits;
            }
        }

        private static async Task<List<AuctionInfo>> SearchPlayer(Func<uint, PlayerSearchParameters> player,
            uint startPage,
            uint endPage,
            int sleepDuration = 1000) {
            var results = new List<AuctionInfo>();
            for (uint i = startPage; i <= endPage; i++) {
                Console.WriteLine("Searching page " + i + "...");
                AuctionResponse searchResponse = await client.SearchAsync(player(i));
                results.AddRange(searchResponse.AuctionInfo);
                Thread.Sleep(sleepDuration);
            }

            return results;
        }

        private static void PrintCheapestPlayers(List<AuctionInfo> results, int limit = 10) {
            foreach (AuctionInfo result in results.Take(limit)) {
                Console.WriteLine(result.ItemData.Rating + " - " + result.BuyNowPrice + " - " + result.CurrentBid);
                //Console.WriteLine("  " + " - " + Util.ComputeMinSellPrice(result.BuyNowPrice, 300) + " - " +
                //                  Util.ComputeMinSellPrice(result.CurrentBid, 500));
            }
        }
    }
}