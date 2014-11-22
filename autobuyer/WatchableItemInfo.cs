using System;
using UltimateTeam.Toolkit.Parameters;

namespace Autobuyer {
    internal class WatchableItemInfo {
        public WatchableItemInfo(Func<uint, PlayerSearchParameters> searchParameters,
            uint maxBuyPrice,
            uint minBenefits,
            uint maxBenefits,
            uint playerRating = 0,
            uint id = 0) {
            Id = id;
            PlayerRating = playerRating;
            MaxBenefits = maxBenefits;
            MinBenefits = minBenefits;
            MaxBuyPrice = maxBuyPrice;
            SearchParameters = searchParameters;
        }

        public uint Id { get; set; }

        public uint PlayerRating { get; set; }

        public uint MaxBenefits { get; set; }

        public uint MinBenefits { get; set; }

        public uint MaxBuyPrice { get; set; }

        public Func<uint, PlayerSearchParameters> SearchParameters { get; set; }
    }
}