using UnityEngine;
using System.Collections;

public class DragObject : MonoBehaviour {

    
    //Object can be dragged on mouse down
    void OnMouseDrag()
    {
            Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            point.z = gameObject.transform.position.z;
            gameObject.transform.position = point;
    }
}
