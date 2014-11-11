using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Puzzel_game
{
    class player:objects
    {
        public int score { get; set; }

        public int money { get; set; }

        public sbyte bombCount { get; set; }
        public sbyte drillCount { get; set; }
        public sbyte bucketCount { get; set; }
        public sbyte nukeCount { get; set; }

        public void reset()
        {
            money += score / 10;
            score = 0;
        }

        public player()
        {
            bombCount = 5;
            drillCount = 5;
            bucketCount = 5;
            nukeCount = 5;
            money = 50000;
            score = 1000;
        }
    }
}
