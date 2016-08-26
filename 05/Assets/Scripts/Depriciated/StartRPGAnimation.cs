using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityStandardAssets.ImageEffects;

public class StartRPGAnimation : MonoBehaviour {

    private GameObject door;
    private GameObject character;

	// Use this for initialization
	void Start () {
        door = GameObject.Find("Canvas/ForeGround/Door");
        character = GameObject.Find("Canvas/ForeGround/Character");
        StartCoroutine(playAnimation());
	}

    IEnumerator playAnimation()
    {
        //Open door
        door.transform.GetChild(1).gameObject.SetActive(false);
        yield return new WaitForSeconds(0f);
        float yTime = 0;
        while (yTime < 1)
        {
            character.transform.localPosition = character.transform.localPosition - new Vector3(0,3,0);
            yTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        float xTime = 0;
        while(xTime < 1)
        {
            character.transform.localPosition = character.transform.localPosition + new Vector3(3, 0, 0);
            xTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        Twirl twirl = Camera.main.gameObject.GetComponent<Twirl>();
        float twirlTime = 0;
        while(twirlTime < 1)
        {

            twirl.angle = Mathf.Lerp(0, 180, twirlTime/2);
            twirlTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();

        }
        SceneManager.LoadScene("GameDetails");


    }

    // Update is called once per frame
    void Update () {
	
	}
}
