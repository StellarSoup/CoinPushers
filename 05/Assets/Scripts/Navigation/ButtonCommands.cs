using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonCommands : MonoBehaviour {

    //Handles commands to move to other sections
	public void LoadLevelOnButtonPress(string level)
    {
        SceneManager.LoadScene(level);
    }
}
