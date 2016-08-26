using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameDetails : GameState {
    public static bool isPaused;
    public static int numberOfGames = 17; //Total number of games currently implemented
    public static string timeLeft;
    //Holds the details for each game
    struct Details
    {
        //Each game needs a game name and company name
        private string Name;
        private string Company;
        private string Description;
        private string Instructions;

        //return game name
        public string getGameName()
        {
            return Name;
        }
        //return company name
        public string getCompanyName()
        {
            return Company;
        }
        //return game description
        public string getDescription()
        {
            return Description;
        }
        //return game instructions
        public string getInstructions()
        {
            return Instructions;
        }

        //Set details of the game
        public void setDetails(string company,string game, string description,string instructions)
        {
            Company = company;
            Name = game;
            Description = description;
            Instructions = instructions;
        }

    }
    private Details[] listOfGames = new Details[numberOfGames];
    private GameStopWatchGUI gsGUI;
    void Start () {
        KeyDirectory.Games.setNextLevel();//Sets the next level

        //If the game is in arcade mode automatically pause the game
        if (KeyDirectory.Mode.Game.Get().Equals(KeyDirectory.Mode.Game.ARCADE))
        {
            isPaused = true;
        }
        else
        {
            isPaused = false;
        }
        startOrStopTime(false);
        GSResetTimer();//Resets timer back to full
        /*Below is every game currently implemented*/
        listOfGames[0].setDetails("BraveGames", "Putt Putt Golf","Score the ball into the hole","Shoot the white ball into the hole");
        listOfGames[1].setDetails("BraveGames", "Q And A","Give the correct answer","Answer the Question");
        listOfGames[2].setDetails("BraveGames", "Button Presser","Press the button, or not?","Follow the Instructions");
        listOfGames[3].setDetails("BraveGames", "Space Thruster","Ignite the engine","Tap the screen when the green bar is in the red");
        listOfGames[4].setDetails("BraveGames", "Jewel Thief 2","Get the Jewel","Tap the screen to change direction");
        listOfGames[5].setDetails("BraveGames", "Match Shapes","Match the Shapes","Drag the Shapes into their holes");
        listOfGames[6].setDetails("BraveGames", "Waiting For The Bus", "Catch the next bus","Tap the screen when the bus is at the bus stop");
        listOfGames[7].setDetails("BraveGames", "Animal Stacker","Stack the Animals On Top Of Eachother", "Stack all 3 blocks");
        listOfGames[8].setDetails("BraveGames & Kered", "Hot Stuff", "Give the sun some love", "Show some affection");
        listOfGames[9].setDetails("BraveGames", "Finger Fighter", "Beat through the enemies", "Tap the left and right side of the screen");
        listOfGames[10].setDetails("BraveGames", "Brick Breaker", "Keep the ball in the air", "Keep the ball in the air");
        listOfGames[11].setDetails("BraveGames", "Western Shootout", "Shoot all of the enemies before they shoot you", "Tap the enemies to defeat them");
        listOfGames[12].setDetails("BraveGames & Kered", "Lift Off", "Get the spaceship into space", "Tap the screen");
        listOfGames[13].setDetails("BraveGames", "To Gouda To Be True", "Slice Through the Gouda", "Keep swiping to cut through the cheese");
        listOfGames[14].setDetails("BraveGames", "Tunnel Runner", "Run Throught the tunnels while dodging the barriers", "Tap the sides of the screens to rotate the cube, and dodge the barriers");
        listOfGames[15].setDetails("BraveGames & Kered", "Ad Attacker", "Eliminate All The Ads on Screen", "Close the Ads");
        listOfGames[16].setDetails("BraveGames & Kered", "RecepieMaker", "Match the recepies", "Match the correct recepies");


        gsGUI = GameObject.Find("GameCanvas").GetComponent<GameStopWatchGUI>();
    }
    //Non-static way to pause the game
    public void nonStaticSetPaused()
    {
        setPaused();
    }
    //Way to pause the game
    public static void setPaused()
    {
        //If in arcade mode change bool
        if (KeyDirectory.Mode.Game.Get().Equals(KeyDirectory.Mode.Game.ARCADE))
        {
            /*Note: This approach is so that minigames can be played in the mid game section*/
            isPaused = !isPaused;
        }else
        {
            //Set timescale to 0
            if(Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }else
            {
                Time.timeScale = 0;
            }
        }
    }
    public float time;
    private float breakTime = 4;

    //Handles loading of next game
    private void loadNextGame()
    {
        int gameIndex = KeyDirectory.Games.getNextLevel();
        if (!isPaused)
        {
            time += Time.deltaTime;
        }
        if(time >= breakTime)
        {
            startOrStopTime(true);
            gsGUI.SlideTimer(true, 0.5f);
            KeyDirectory.Games.setLevelInstuructions(listOfGames[gameIndex].getInstructions());
            SceneManager.LoadScene(gameIndex + 1);
        }
    }
    //Moves to the main menu and destroys the game canvas and music player
    public void goToMainMenu()
    {
        SceneManager.LoadScene("Title Card");
        Time.timeScale = 1;
        Destroy(GameObject.Find("GameCanvas"));
        Destroy(GameObject.Find("Timer&MusicPlayer"));
    }

    //Changes the instructions for the next game
    public void ChangeGame(int index){     
        ChangeCanvasText(GameObject.Find("GameName/Text"), listOfGames[index].getGameName());
        ChangeCanvasText(GameObject.Find("GameInstructions/Text"), listOfGames[index].getDescription());

    }
    private void ChangeCanvasText(GameObject textObject, string newText)
    {
        textObject.GetComponent<Text>().text = newText;
    }
	
	void Update () {

        loadNextGame();
        //Displays how much time the player has left for the minigame
        string newTime = System.Math.Round(breakTime-time, 1).ToString();
        if(newTime.Length == 1)
        {
            newTime = newTime + ".0";
        }
        timeLeft = newTime;
	}
}
