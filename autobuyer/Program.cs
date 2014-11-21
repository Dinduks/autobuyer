using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using Nito.AsyncEx;
using UltimateTeam.Toolkit;
using UltimateTeam.Toolkit.Models;

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

            for (uint i = 1; i <= 5; i++)
            {
                Console.WriteLine("Searching page " + i + "...");
                AuctionResponse searchResponse = await client.SearchAsync(Players.Aubameyang(i));
                results.AddRange(searchResponse.AuctionInfo);
            }
            results.Sort((x, y) => x.BuyNowPrice.CompareTo(y.BuyNowPrice));
            results = results.Take(5).ToList();

            foreach (AuctionInfo result in results)
            {
                Console.WriteLine(result.ItemData.Rating + " - " + result.BuyNowPrice + " - " + result.CurrentBid);
                Console.WriteLine("  " + " - " + Util.ComputeMinSellPrice(result.BuyNowPrice, 1000) + " - " +
                                  Util.ComputeMinSellPrice(result.CurrentBid, 1000));
            }

            foreach (AuctionInfo result in results.Where(result => result.BuyNowPrice <= 7000 && result.ItemData.Contract > 0))
            {
                try
                {
                    await client.PlaceBidAsync(result, result.BuyNowPrice);
                    Console.WriteLine("Bought one for " + result.BuyNowPrice);
                    Thread.Sleep(6000);
                }
                catch (Exception exception)
                {
                    Console.WriteLine("Error: " + exception.Message);
                }
            }

            Console.WriteLine("Bye.");
        }
    }
}