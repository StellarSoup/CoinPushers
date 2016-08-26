using UnityEngine;
using System.Collections;

public class BlockBehaviour : MonoBehaviour {

    //If a block collides with the ball destroy it
    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.transform.name.Equals("PaddleBall"))
        {
            Destroy(gameObject);
        }
    }
}
