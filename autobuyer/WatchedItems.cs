using System.Collections.Generic;
using autobuyer;

namespace Autobuyer {
    internal class WatchedItems {
        public static List<WatchableItemInfo> items =
            new List<WatchableItemInfo> {
                new WatchableItemInfo(Players.Doumbia, 4500, 300, 500),
                new WatchableItemInfo(Players.Aubameyang, 7000, 300, 500)
            };
    }
}