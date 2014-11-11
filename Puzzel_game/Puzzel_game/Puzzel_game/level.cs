using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Puzzel_game
{
    class level
    {
        public short currentLevel { get; set; }

        short nextLevelCount;
        short maxNextLevelCount;

        public level()
        {
            nextLevelCount = 0;
            maxNextLevelCount = 0;
            currentLevel = 1;
        }

        public void update()
        {
            if(spawner.lost)
            {
                nextLevelCount = 0;
                maxNextLevelCount = 0;
                currentLevel = 1;
            }
            maxNextLevelCount = (short)(currentLevel * 1000);
            nextLevelCount += 1;
            if(nextLevelCount >= maxNextLevelCount)
            {
                currentLevel += 1;
                nextLevelCount = 0;
            }
        }
    }
}
