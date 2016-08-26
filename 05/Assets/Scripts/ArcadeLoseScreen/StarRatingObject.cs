using UnityEngine;
using System.Collections;

public class StarRatingObject : MonoBehaviour {

    public void StartStarSlide()
    {
        //Slides the object up then down.
        StartCoroutine(AnimationCurve());
        
    }
    //Starts a thread to take care of an wavy 
    IEnumerator AnimationCurve()
    {
        float speed = Time.deltaTime*6;
        yield return new WaitForSeconds(0);
        float time = 0;
        Vector3 startPos = transform.localPosition;
        Vector3 newPos = transform.localPosition + new Vector3(0, 10, 0);
        while (time <= 1)
        {
            
            time += speed;
            transform.localPosition = Vector3.Lerp(startPos, newPos, time);
            yield return new WaitForEndOfFrame();
        }
        time = 0;
        while (time <= 1)
        {

            time += speed;
            transform.localPosition = Vector3.Lerp(newPos, startPos, time);
            yield return new WaitForEndOfFrame();
        }
    }
}
