using UnityEngine;
using System.Collections;

public class moveTopDownCharacter : MonoBehaviour {

    private Vector3 startPos = Vector3.zero;

    void Update()
    {
        transform.GetChild(0).position = startPos;
    }
    void OnMouseDrag()
    {
        Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        point.z = gameObject.transform.position.z;
        point = new Vector3((int)point.x, (int)point.y, (int)point.z);   
        gameObject.transform.position = point;
    }
    void OnMouseUp()
    {
        Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        point.z = gameObject.transform.position.z;
        point = new Vector3((int)point.x, (int)point.y, (int)point.z);
        startPos = point;
    }
}
