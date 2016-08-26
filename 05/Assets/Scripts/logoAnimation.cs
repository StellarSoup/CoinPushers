using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class logoAnimation : MonoBehaviour {

    //Holds company name and logo
    private Text compName;
    private Image compLogo;

	// Use this for initialization
	void Start () {
        compName = GameObject.Find("CompanyName").GetComponent<Text>();
        compLogo = GameObject.Find("CompanyLogo").GetComponent<Image>();
        compName.color = compLogo.color = new Color(1, 1, 1, 0);
        compLogo.transform.localPosition = Vector3.zero;

        //Moves to the title card
        StartCoroutine(startAnimation());
    }
    //Fades the company logo in and out and loads the title card
    IEnumerator startAnimation()
    {
        Vector2 startBor = compLogo.rectTransform.offsetMin;
        Vector2 endBor = compLogo.rectTransform.offsetMax;
        yield return new WaitForSeconds(1);
        float transTime = 0;
        while (transTime < 1)
        {
            transTime += Time.deltaTime;
            compLogo.color = new Color(1, 1, 1, transTime);
            yield return new WaitForEndOfFrame();
        }
        transTime = 0;
        while (transTime < 1)
        {
            transTime += Time.deltaTime;
            compLogo.rectTransform.offsetMin = Vector2.Lerp(startBor, Vector2.zero, transTime);
            compLogo.rectTransform.offsetMax = Vector2.Lerp(endBor, Vector2.zero, transTime);
            yield return new WaitForEndOfFrame();
        }
        transTime = 0;
        while(transTime < 1)
        {
            transTime += Time.deltaTime;
            compName.color = new Color(1, 1, 1, transTime);
            yield return new WaitForEndOfFrame();
        }
        transTime = 0;
        yield return new WaitForSeconds(0.5f);
        while (transTime < 1)
        {
            transTime += Time.deltaTime;
            compName.color = compLogo.color = new Color(1, 1, 1, 1-transTime);
            
            yield return new WaitForEndOfFrame();
        }

        if (SceneManager.GetActiveScene().name.Equals("Logo"))
        {
            SceneManager.LoadScene("Title Card");
        }
    }
    // Update is called once per frame
    void Update () {
	
	}
}
