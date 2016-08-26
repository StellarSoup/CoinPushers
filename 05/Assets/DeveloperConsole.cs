using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DeveloperConsole : MonoBehaviour {

    /*Allows the developer to select which game to play*/

    public static bool developerOptionsEnabled;

    //Which level is being tested
    public static int currentLevel;

    //Changes the level selected
    private void ChangeValue()
    {
        Slider slider = GameObject.Find("Slider").GetComponent<Slider>();
        currentLevel = (int)slider.value;
        displayCurrentValue();
    }
    //Displays level selected
    private void displayCurrentValue()
    {
        Text level = GameObject.Find("Level").GetComponent<Text>();
        level.text = currentLevel.ToString();
    }
    //Sets weather developer options are enabled or not
    public void ChangeDeveloperState()
    {
        developerOptionsEnabled = !developerOptionsEnabled;
    }
	
    //Handles the displaying of the developer options
    private void DisplayDeveloper()
    {
        GameObject buttonText = GameObject.Find("Enable/Text");

        if (developerOptionsEnabled)
        {
            buttonText.GetComponent<Text>().text = "True";
        }
        else
        {
            buttonText.GetComponent<Text>().text = "False";
        }

    }

    void Update () {
        //Displays developer options
        DisplayDeveloper();
        ChangeValue();
	}
}
