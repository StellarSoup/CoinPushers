using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EasyGameMidGameDetails : MonoBehaviour {

	// Use this for initialization
	void Start () {
        print("TotalWins = " + KeyDirectory.Games.Score.CountWins());
    }
	
	// Update is called once per frame
	void Update () {

    }
}
