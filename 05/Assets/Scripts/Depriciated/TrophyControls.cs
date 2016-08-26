using UnityEngine;
using System.Collections;

public class TrophyControls : MonoBehaviour {

    public float speed = 5;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.up * Time.deltaTime* speed);
    }
}
