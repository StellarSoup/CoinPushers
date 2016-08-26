using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHeartAnimation : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}
    public void dpleteHeart()
    {
        StartCoroutine(depleteHeart());
    }
    IEnumerator depleteHeart()
    {
        yield return new WaitForSeconds(1f);
        Image heartImage = GetComponent<Image>();
        float time = 1;
        float speed = 0;
        while(time > 0)
        {
            
            time -= speed;
            speed += Time.deltaTime/2;
            heartImage.fillAmount = time;

            yield return new WaitForEndOfFrame();
        }

    }
}
