using System;

namespace autobuyer
{
    public class Util
    {
        public static uint ComputeMinSellPrice(double price, uint benefits = 0)
        {
            return RoundPrice(Math.Ceiling(price/0.95)) + benefits;
        }

        public static uint RoundPrice(double price)
        {
            return RoundPrice((uint) price);
        }

        public static uint RoundPrice(uint price)
        {
            if (price == 0)
            {
                return 150;
            }
            if (price < 1000)
            {
                return (uint) Math.Floor((double) (price + 50)/50)*50;
            }
            if (price < 10000)
            {
                return (uint) Math.Round((double) (price + 100)/100)*100;
            }
            if (price < 50000)
            {
                return (uint) Math.Round((double) (price + 250)/250)*250;
            }
            if (price < 100000)
            {
                return (uint) Math.Round((double) (price + 500)/500)*500;
            }

            return (uint) Math.Round((double) (price + 1000)/1000)*1000;
        }
    }
}