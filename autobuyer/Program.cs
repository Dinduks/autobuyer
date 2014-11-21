using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using Nito.AsyncEx;
using UltimateTeam.Toolkit;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Parameters;

namespace autobuyer
{
    internal class Program
    {
        private static readonly FutClient client = new FutClient();

        private static void Main(string[] args)
        {
            Console.WriteLine("Start...");
            var loginDetails = new LoginDetails(ConfigurationManager.AppSettings["username"],
                ConfigurationManager.AppSettings["password"],
                ConfigurationManager.AppSettings["secretAnswer"],
                Platform.Pc);
            AsyncContext.Run(() => MainAsync(loginDetails));
        }

        private static async void MainAsync(LoginDetails loginDetails)
        {
            await client.LoginAsync(loginDetails);
            Console.WriteLine("Logged in.");

            var results = new List<AuctionInfo>();

            var max = 10;
            var bought = 0;

            Func<uint, PlayerSearchParameters> player = (uint i) => Players.Doumbia(i);
            var maxBIN = 4500;

            while (true)
            {
                Console.WriteLine();

                for (uint i = 1; i <= 5; i++)
                {
                    Console.WriteLine("Searching page " + i + "...");
                    AuctionResponse searchResponse = await client.SearchAsync(player(i));
                    results.AddRange(searchResponse.AuctionInfo);
                    Thread.Sleep(1000);
                }
                results.Sort((x, y) => x.BuyNowPrice.CompareTo(y.BuyNowPrice));

                //printCheapestPlayers(results, 5);

                var _loops = 5;
                foreach (AuctionInfo result in results.Where(result => result.BuyNowPrice <= maxBIN && result.ItemData.Contract > 0))
                {
                    Thread.Sleep(1000);

                    _loops--;
                    if (_loops < 0) break;

                    Console.Write("Trying to buy for " + result.BuyNowPrice + "...\t");
                    try
                    {
                        await client.PlaceBidAsync(result, result.BuyNowPrice);
                        Console.WriteLine("Success!");
                        bought++;

                        if (bought == max)
                        {
                            Console.WriteLine("\nBought " + bought + " items.\nBye.");
                            return;
                        }
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine("Error: " + exception.Message);
                    }
                }

                Thread.Sleep(60 * 1000);
            }

            Console.WriteLine("Bye.");
        }

        private static void printCheapestPlayers(List<AuctionInfo> results, int limit = 10)
        {
            foreach (AuctionInfo result in results.Take(limit))
            {
                Console.WriteLine(result.ItemData.Rating + " - " + result.BuyNowPrice + " - " + result.CurrentBid);
                //Console.WriteLine("  " + " - " + Util.ComputeMinSellPrice(result.BuyNowPrice, 300) + " - " +
                //                  Util.ComputeMinSellPrice(result.CurrentBid, 500));
            }
        }
    }
}