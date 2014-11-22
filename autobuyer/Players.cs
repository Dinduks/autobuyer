using UltimateTeam.Toolkit.Parameters;

namespace autobuyer {
    internal class Players {
        private const byte PageSize = 49;

        public static PlayerSearchParameters Aubameyang(uint i = 1) {
            return new PlayerSearchParameters {
                Page = i,
                PageSize = PageSize,
                Level = Level.Gold,
                League = League.Bundesliga,
                Nation = Nation.Gabon,
                Team = Team.BorussiaDortmund,
                MinBuy = 1
            };
        }

        public static PlayerSearchParameters Sterling(uint i = 1) {
            return new PlayerSearchParameters {
                Page = i,
                PageSize = PageSize,
                Level = Level.Gold,
                League = League.BarclaysPremierLeague,
                Nation = Nation.England,
                Position = Position.RightWinger,
                Team = Team.Liverpool,
                MinBuy = 1
            };
        }

        public static PlayerSearchParameters Shaqiri(uint i = 1) {
            return new PlayerSearchParameters {
                Page = i,
                PageSize = PageSize,
                Level = Level.Gold,
                League = League.Bundesliga,
                Nation = Nation.Switzerland,
                Position = Position.RightMidfielder,
                Team = Team.FCBayern,
                MinBuy = 1
            };
        }

        public static PlayerSearchParameters Muller(uint i = 1) {
            return new PlayerSearchParameters {
                Page = i,
                PageSize = PageSize,
                Level = Level.Gold,
                League = League.Bundesliga,
                Nation = Nation.Germany,
                Position = Position.RightMidfielder,
                Team = Team.FCBayern,
                MinBuy = 1
            };
        }

        public static PlayerSearchParameters Doumbia(uint i = 1) {
            return new PlayerSearchParameters {
                Page = i,
                PageSize = PageSize,
                Level = Level.Gold,
                League = League.RussianLeague,
                Nation = Nation.IvoryCoast,
                Position = Position.Striker,
                Team = Team.CSKAMoskva,
                MinBuy = 1
            };
        }

        public static PlayerSearchParameters Mata(uint i = 1) {
            return new PlayerSearchParameters {
                Page = i,
                PageSize = PageSize,
                Level = Level.Gold,
                League = League.BarclaysPremierLeague,
                Nation = Nation.Spain,
                Position = Position.CentralAttackingMidfielder,
                Team = Team.ManchesterUnited,
                MinBuy = 1
            };
        }

        public static PlayerSearchParameters HenryIF(uint i = 1) {
            return new PlayerSearchParameters {
                Page = i,
                PageSize = PageSize,
                Level = Level.Gold,
                League = League.Mls,
                Nation = Nation.France,
                Position = Position.LeftMidfielder,
                Team = Team.NewYorkRedBulls,
                MinBuy = 1
            };
        }

        public static PlayerSearchParameters DonovanIF(uint i = 1) {
            return new PlayerSearchParameters {
                Page = i,
                PageSize = PageSize,
                Level = Level.Gold,
                League = League.Mls,
                Nation = Nation.UnitedStates,
                Position = Position.LeftMidfielder,
                Team = Team.LosAngelesGalaxy,
                MinBuy = 1
            };
        }
    }
}