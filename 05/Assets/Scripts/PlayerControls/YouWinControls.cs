using UnityEngine;
using System.Collections;

public class YouWinControls : MonoBehaviour {

    public static bool breakTrans;

    private RectTransform rectTrans;

	// Use this for initialization
	void Start () {
        Init();
    }
    void Init()
    {
        rectTrans = GetComponent<RectTransform>();
        rectTrans.sizeDelta = new Vector2(Screen.width/2, rectTrans.sizeDelta.y);
        rectTrans.localPosition = new Vector3(-Screen.width, rectTrans.transform.localPosition.y,rectTrans.transform.localPosition.z);
    }
    public void SlideInBanner()
    {
        StartCoroutine(moveToPosition(1.5f));
    }
    IEnumerator moveToPosition(float speed)
    {
        breakTrans = false;
        Vector3 startPos = new Vector3(-Screen.width, rectTrans.transform.localPosition.y, rectTrans.transform.localPosition.z);
        Vector3 endPos = new Vector3(-Screen.width / 4, rectTrans.transform.localPosition.y, rectTrans.transform.localPosition.z);

            float time = 0;
            while (time <= 1 && !breakTrans)
            {
                time += Time.deltaTime * speed;
                rectTrans.localPosition = Vector3.Lerp(startPos, endPos, time);
                yield return new WaitForEndOfFrame();
            }
            time = 0;
        if (breakTrans)
        {
            rectTrans.localPosition = startPos;
        }
        yield return new WaitForSeconds(1f);
        while (time <= 1 && !breakTrans)
            {
            
                time += Time.deltaTime * speed;
                rectTrans.localPosition = Vector3.Lerp(endPos, startPos, time);
                yield return new WaitForEndOfFrame();
            }
        if (breakTrans)
        {
            rectTrans.localPosition = startPos;
        }

        breakTrans = false;
        }

    
}
