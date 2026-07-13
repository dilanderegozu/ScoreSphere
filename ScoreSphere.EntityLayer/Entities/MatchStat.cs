using System;
using System.Collections.Generic;
using System.Text;

namespace ScoreSphere.EntityLayer.Entities
{
    public class MatchStat
    {
        public int MatchStatId { get; set; }

        public int MatchId { get; set; }
        public Match? Match { get; set; }

        // Topa Sahip Olma (%)
        public int HomePossession { get; set; }
        public int AwayPossession { get; set; }

        // Şut
        public int HomeShots { get; set; }
        public int AwayShots { get; set; }

        // İsabetli Şut
        public int HomeShotsOnTarget { get; set; }
        public int AwayShotsOnTarget { get; set; }

        // Pas
        public int HomePasses { get; set; }
        public int AwayPasses { get; set; }

        // Pas İsabeti (%)
        public int HomePassAccuracy { get; set; }
        public int AwayPassAccuracy { get; set; }

        // Korner
        public int HomeCorners { get; set; }
        public int AwayCorners { get; set; }

        // Faul
        public int HomeFouls { get; set; }
        public int AwayFouls { get; set; }

        // Ofsayt
        public int HomeOffsides { get; set; }
        public int AwayOffsides { get; set; }

        // Sarı Kart
        public int HomeYellowCards { get; set; }
        public int AwayYellowCards { get; set; }

        // Kırmızı Kart
        public int HomeRedCards { get; set; }
        public int AwayRedCards { get; set; }
    }
}
