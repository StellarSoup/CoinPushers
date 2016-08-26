using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerSettings : GameState {

    //The Current Health of the Player
    public static int CURRENT_HEALTH;
    //The Starting Health of the players
    public static int INIT_HEALTH;

    //When the player loses a game they lose a life
    public static void LoseHealth()
    {
        CURRENT_HEALTH -= 1;
        print("Health is " + CURRENT_HEALTH);        
    }
    //Sets the player details based on the gamemode and the difficulty mode
    public static void InitCharacterStats()
    {
        if (KeyDirectory.Mode.Game.Get().Equals(KeyDirectory.Mode.Game.ARCADE))
        {
            CURRENT_HEALTH = 1;
            INIT_HEALTH = 1;
            print("Set to Arcade Mode");
        }
        else if (KeyDirectory.Mode.Game.Get().Equals(KeyDirectory.Mode.Game.STORY))
        {
            print("Set to Story Mode");
            if (KeyDirectory.Mode.Story.Get().Equals(KeyDirectory.Mode.Story.EASY))
            {
                CURRENT_HEALTH = 5;
                INIT_HEALTH = 5;
            }
            else if (KeyDirectory.Mode.Story.Get().Equals(KeyDirectory.Mode.Story.NORMAL))
            {
                CURRENT_HEALTH = 3;
                INIT_HEALTH = 3;
            }
            else if(KeyDirectory.Mode.Story.Get().Equals(KeyDirectory.Mode.Story.HARD))
            {
                CURRENT_HEALTH = 1;
                INIT_HEALTH = 1;
            }
        }
    }
}
