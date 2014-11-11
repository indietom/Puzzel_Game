using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Puzzel_game
{
    class fileManager
    {
        static public bool newHighscore;

        const sbyte LINES_OF_INFO = 5;
        const sbyte HIGHSCORE_LIST_SIZE = 10;

        string gameSavePath = @"data\gameSave.sav";
        string highScorePath = @"data\highscore.sav";

        string[] setInfo = new string[LINES_OF_INFO];
        string[] getInfo = new string[LINES_OF_INFO];

        public int[] highScores = new int[HIGHSCORE_LIST_SIZE];
        public string[] highScoreNames = new string[HIGHSCORE_LIST_SIZE];

        public fileManager()
        {
            loadHighScore();
        }

        public void saveMiscInfo()
        {

        }
        public void loadMiscInfo()
        {

        }

        public void saveHighScore(player player)
        {
            for(int i = 0; i < HIGHSCORE_LIST_SIZE; i++)
            {
                if(player.score > highScores[i])
                {
                    highScores[i] = player.score;
                    if (player.score >= highScores[HIGHSCORE_LIST_SIZE - 1])
                    {
                        newHighscore = true;
                    }
                    break;
                }
            }
            StreamWriter sw = new StreamWriter(highScorePath);
            for(int i = 0; i < HIGHSCORE_LIST_SIZE; i++)
            {
                sw.WriteLine(highScores[i]);
            }
            sw.Dispose();
        }
        public void loadHighScore()
        {
            StreamReader sr = new StreamReader(highScorePath);
            for (int i = 0; i < HIGHSCORE_LIST_SIZE; i++)
            {
                highScores[i] = int.Parse(sr.ReadLine());
            }
            sr.Dispose();
        }

        public void saveGame(player player)
        {
            setInfo[0] = player.money.ToString();
            setInfo[1] = player.bombCount.ToString();
            setInfo[2] = player.drillCount.ToString();
            setInfo[3] = player.bucketCount.ToString();
            setInfo[4] = player.nukeCount.ToString();

            StreamWriter sw = new StreamWriter(gameSavePath);
            for (int i = 0; i < LINES_OF_INFO; i++)
            {
                sw.WriteLine(setInfo[i]);
            }
            sw.Dispose();
        }
        public void loadGame(ref player player)
        {
            if(!File.Exists(gameSavePath))
            {
                saveGame(player);
            }
            StreamReader sr = new StreamReader(gameSavePath);
            for (int i = 0; i < LINES_OF_INFO; i++)
            {
                getInfo[i] = sr.ReadLine();
            }
            player.money = int.Parse(getInfo[0]);
            player.bombCount = sbyte.Parse(getInfo[1]);
            player.drillCount = sbyte.Parse(getInfo[2]);
            player.bucketCount = sbyte.Parse(getInfo[3]);
            player.nukeCount = sbyte.Parse(getInfo[4]);
            sr.Dispose();
        }
    }
}
