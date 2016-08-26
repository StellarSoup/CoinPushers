using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KeyDirectory : MonoBehaviour
{
    public class Timer
    {
        //Holds how many games have to be beaten before time starts to speed up
        static int[] arcadeTimeBarriers = { 5, 10, 15, 20 };
        static int[] easyTimeBarriers = { 5, 12 };
        static int[] normalTimeBarriers = { 5, 10, 15 };
        static int[] hardTimeBarriers = { 5, 10, 15, 20, 25 };

        //Gets the new time, uses the barriers as a reference
        //and depreciates by 0.5f for every barrier reached
        public static float getNewTimer()
        {
            float newTime = 0;

            if (Mode.Game.Get().Equals(Mode.Game.ARCADE))
            {
                //Arcade mode starts at 6 
                newTime = setTimer(6, arcadeTimeBarriers);
            }
            else
            {
                //Handles story mode
                if (Mode.Story.Get().Equals(Mode.Story.EASY))
                {
                    newTime = setTimer(6, easyTimeBarriers);
                    //Easy mode starts at 6
                }
                else if (Mode.Story.Get().Equals(Mode.Story.NORMAL))
                {
                    //Normal mode starts at 5.5f
                    newTime = setTimer(5.5f, normalTimeBarriers);
                }
                else
                {
                    //Hard mode starts at 4.5f
                    newTime = setTimer(4.5f, hardTimeBarriers);
                }
            }
            //returns new time
            return newTime;
        }
        //Sets the timer based on the barriers
        private static float setTimer(float startingTime, int[] barriers)
        {
            for (int i = barriers.Length - 1; i >= 0; i--)
            {
                if (Games.Score.CountWins() >= barriers[i])
                {
                    return startingTime - (0.5f * (i + 1));
                }
            }
            return startingTime;
        }
        //Checks if the timer is speeding up by checking the score against the barries
        public static bool isTimerSpeedingUp()
        {
            bool timerSpeedUp = false;
            if (Mode.Game.Get().Equals(Mode.Game.ARCADE))
            {
                timerSpeedUp = checkIfSpeedingUp(arcadeTimeBarriers);
            }
            else
            {
                if (Mode.Story.Get().Equals(Mode.Story.EASY))
                {
                    timerSpeedUp = checkIfSpeedingUp(easyTimeBarriers);
                }
                else if (Mode.Story.Get().Equals(Mode.Story.NORMAL))
                {
                    timerSpeedUp = checkIfSpeedingUp(normalTimeBarriers);
                }
                else
                {
                    timerSpeedUp = checkIfSpeedingUp(hardTimeBarriers);
                }
            }

            return timerSpeedUp;
        }
        //Checks if the current score matches any of the barrier thresholds
        private static bool checkIfSpeedingUp(int[] timeBarriers)
        {
            bool timerSpeedIncrease = false;
            for (int i = 0; i < timeBarriers.Length; i++)
            {
                if (timeBarriers[i] == Games.Score.CountWins())
                {
                    return true;
                }
            }

            return timerSpeedIncrease;
        }
    }
    //Handles games
    public class Games
    {
        //Handels highscores
        public class HighScore
        {
            private static string highScoreKey = "HighScore";
            private static int numberOfHighScores = 5;

            //Get a list of highscores starting from the highest to the lowest
            public static int[] getListOfHighScores()
            {
                int[] highScores = new int[numberOfHighScores];
                for (int i = 0; i < highScores.Length; i++)
                {
                    highScores[i] = getHighScore(i);
                }

                return highScores;
            }

            //Get highscore at current index
            public static int getHighScore(int index)
            {
                return PlayerPrefs.GetInt(highScoreKey + index);
            }
            //Sets new highscore if it beats any of the previous scores
            public static void setNewHighScore(int newScore)
            {
                //Sets the highscore index to off the list

                int[] highScore = getListOfHighScores();
                int highScoreIndex = -1;
                //Loops through all of the highscore
                for (int i = 0; i < highScore.Length; i++)
                {
                    //If score is greater than highscore than it gets that index
                    if (newScore > highScore[i])
                    {
                        highScoreIndex = i;
                        break;
                    }
                }
                //If an index was given then it updates the score board
                if (highScoreIndex >= 0)
                {
                    int tempNumber = newScore;
                    for (int i = highScoreIndex; i < highScore.Length; i++)
                    {
                        int temp = highScore[i];
                        highScore[i] = tempNumber;
                        tempNumber = temp;

                    }
                    for (int i = 0; i < highScore.Length; i++)
                    {
                        PlayerPrefs.SetInt(highScoreKey + i, highScore[i]);
                    }
                }


            }
        }
        //Handles normal scoreing
        public class Score
        {
            private static string gamesWon = "GamesWon";

            //Counts total number of wins
            public static int CountWins()
            {
                return PlayerPrefs.GetInt(gamesWon);
            }
            //Resets the score
            public static void ResetScore()
            {
                PlayerPrefs.SetInt(gamesWon, 0);
            }
            //Adds a win
            public static void AddWin()
            {
                PlayerPrefs.SetInt(gamesWon, CountWins() + 1);
            }
            //Checks if the player has enough wins to move onto the next stage.
            /*Note: This section of the game was cut*/
            public static bool doesPlayerHaveEnoughWins()
            {
                int EasyWins = 20;
                int NormalWins = 30;
                int HardWins = 40;

                bool playerHasEnoughWins = false;
                if (Mode.Story.Get().Equals(Mode.Story.EASY))
                {
                    if (CountWins() >= EasyWins)
                    {
                        playerHasEnoughWins = true;
                    }
                }
                else if (Mode.Story.Get().Equals(Mode.Story.NORMAL))
                {
                    if (CountWins() >= NormalWins)
                    {
                        playerHasEnoughWins = true;
                    }
                }
                else if (Mode.Story.Get().Equals(Mode.Story.HARD))
                {
                    if (CountWins() >= HardWins)
                    {
                        playerHasEnoughWins = true;
                    }
                }


                return playerHasEnoughWins;
            }
        }





        private static int levelIndex;
        private static string levelInstructions;
        //get current level index
        public static int getNextLevel()
        {
            return levelIndex;
        }
        //Sets the new level index
        public static void setNextLevel()
        {
            //If developer mode is enabled the player can select the level
            if (DeveloperConsole.developerOptionsEnabled)
            {
                levelIndex = DeveloperConsole.currentLevel;
            }
            else
            {
                //Gets the next level in list
                levelIndex = nextLevelInList();
            }
        }
        //Gets the next level in list
        private static int nextLevelInList()
        {
            int newNextLevel = (getNextLevel() + 1) % GameDetails.numberOfGames;
            return newNextLevel;
        }
        //Gets a random level
        private static int newRandomLevel()
        {
            int newRandomLevel = (int)(Random.value * GameDetails.numberOfGames);
            return newRandomLevel;
        }
        //Sets the new level instrcuctions based on level selected
        public static void setLevelInstuructions(string newInstructions)
        {
            levelInstructions = newInstructions;
        }
        //Gets level instructions
        public static string getLevelInstructions()
        {
            return levelInstructions;
        }

    }

    //Handles the star ranking
    public class StarRanking
    {
        //If the player beats these score thresholds they gain more stars
        private static int[] scoreThreshold = { 3, 6, 9, 12, 15 };
        //Returns how many stars the player is eligable to get
        public static int getStarRating()
        {
            //Set to 5 star rating
            int rating = scoreThreshold.Length;
            while (rating > 0)
            {
                if (Games.Score.CountWins() >= scoreThreshold[rating - 1])
                {
                    return rating;
                }
                else
                {
                    rating--;
                }
            }
            return rating;
        }
    }
    /*Handles lives*/
    public class Lives
    {
        private static string loseLife = "LoseHeart";
        //Sets weather the player lost or not
        public static void wasLifeLost(bool answer)
        {
            string newheartstate = "";
            if (answer)
            {
                newheartstate = "Lose";
            }
            else
            {
                newheartstate = "Win";
            }

            PlayerPrefs.SetString(loseLife, newheartstate);
        }
        //Gets weather the player lost a life or not
        public static bool getState()
        {
            string heart = PlayerPrefs.GetString(loseLife);
            if (heart.Equals("Lose"))
            {
                return false;
            }
            return true;
        }
    }
    //Handles mode selection
    public class Mode
    {
        private static string gameModeKey = "GameMode";
        private static string storyDifficultyKey = "StoryDifficulty";

        //Handles game mode selection
        public class Game
        {
            public static readonly string ARCADE = "Arcade";
            public static readonly string STORY = "Story"
            //Sets the new game mode
            public static void Set(string mode)
            {
                PlayerPrefs.SetString(gameModeKey, mode);
            }
            //Gets the new game mode
            public static string Get()
            {
                return PlayerPrefs.GetString(gameModeKey);
            }
        }
        //Handles story mode selection
        public class Story
        {
            public static readonly string EASY = "Easy";
            public static readonly string NORMAL = "Normal";
            public static readonly string HARD = "Hard";
            //Sets the story mode
            public static void Set(string mode)
            {
                PlayerPrefs.SetString(storyDifficultyKey, mode);
            }
            //Gets the story mode
            public static string Get()
            {
                return PlayerPrefs.GetString(storyDifficultyKey);
            }
        }
    }
}
