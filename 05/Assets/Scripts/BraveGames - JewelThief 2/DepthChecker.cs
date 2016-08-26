using UnityEngine;
using System.Collections;

public class DepthChecker : MonoBehaviour {

    private GameObject car;
    private GameObject tree;
    private float startDepth;

	// Use this for initialization
	void Start () {
        car = GameObject.Find("Car");
        tree = transform.parent.gameObject;
        startDepth = tree.transform.localPosition.z;
	}
	
	// Update is called once per frame
	void Update () {
        float ydepth = transform.position.y - (car.transform.position.y - 0f);
        if(ydepth <= 0)
        {
            float min = startDepth - 10;

            tree.transform.localPosition = new Vector3(tree.transform.localPosition.x, tree.transform.localPosition.y, min);
        }else
        {
            float min = startDepth;
            tree.transform.localPosition = new Vector3(tree.transform.localPosition.x, tree.transform.localPosition.y, min);
        }
	}
}
