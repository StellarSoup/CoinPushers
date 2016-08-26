using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MidGameControls : MonoBehaviour {

	// Use this for initialization
	void Start () {
        ChangeText(GameObject.Find(gameObject.name + "/Score/Text"), KeyDirectory.Games.Score.CountWins().ToString());

        if (KeyDirectory.Timer.isTimerSpeedingUp())
        {
            ChangeText(GameObject.Find(gameObject.name + "/Timer/Text"), "Faster");
        }
        else
        {
            ChangeText(GameObject.Find(gameObject.name + "/Timer/Text"), "");
        }
	}
	private void ChangeText(GameObject textObject, string newText)
    {
        textObject.GetComponent<Text>().text = newText;
    }
	// Update is called once per frame
	void Update () {
	
	}
}
