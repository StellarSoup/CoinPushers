using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonImageMovement : MonoBehaviour {

    RawImage rawImg;

    //Image moves between these two points
    Vector2 minPoint = Vector2.zero;
    Vector2 maxPoint = new Vector2(0.3f, 0.5f);

	// Use this for initialization
	void Start () {
        //Sets initil conditions for the button
        rawImg = transform.GetChild(0).GetChild(0).GetComponent<RawImage>();
        rawImg.uvRect = new Rect(Vector2.zero,Vector2.one * 0.5f);
        StartCoroutine(moveImageBetweenTwoPoints(minPoint,maxPoint));
    }

    //Slides the button image between these two points
    IEnumerator moveImageBetweenTwoPoints(Vector2 startPoint,Vector2 endPoint)
    {
        bool slideingTowards = true;
        float speed = Time.deltaTime * 0.01f;
        //Continuosly move the object
        while (true)
        {
            float time = 0;

            while (time < 1)
            {
                time += speed;
                Vector2 curPoint;
                if (slideingTowards)
                {
                    curPoint = Vector2.Lerp(startPoint, endPoint, time);
                }else
                {
                    curPoint = Vector2.Lerp(endPoint, startPoint, time);

                }
                rawImg.uvRect = new Rect(curPoint, rawImg.uvRect.size);
                yield return new WaitForEndOfFrame();
            }
            slideingTowards = !slideingTowards;
        }
    }

}
