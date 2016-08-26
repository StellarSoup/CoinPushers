using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetPlayerLives : MonoBehaviour {

    public GameObject[] hearts;
    private int currentHearts = PlayerSettings.CURRENT_HEALTH;
    private int initHearts = PlayerSettings.INIT_HEALTH;
	// Use this for initialization

    public void NewGUI()
    {
        setUpHearts();
        depleteHearts();
    }
    void setUpHearts()
    {
        hearts = new GameObject[initHearts];
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i] = Instantiate(Resources.Load("Prefabs/Heart", typeof(GameObject)) as GameObject);
            hearts[i].transform.parent = transform;
            RectTransform heartImage = hearts[i].GetComponent<RectTransform>();
            if (initHearts % 2 == 1)
            {
                int index = -initHearts/2 * 90;
                heartImage.offsetMin = new Vector2(0 + index + 90*i, 0);
                heartImage.offsetMax = new Vector2(0 + index + 90*i, 0);
                heartImage.localScale = Vector3.one;
            }
            else
            {
                int index = -initHearts / 2 * 90;
                heartImage.offsetMin = new Vector2(0 + index + 180 * i, 0);
                heartImage.offsetMax = new Vector2(0 + index, 0);
                heartImage.localScale = Vector3.one;
            }
            if (i < currentHearts)
            {
                hearts[i].GetComponent<Image>().fillAmount = 1;
                hearts[i].transform.GetChild(0).GetComponent<Image>().fillAmount = 1;
            }
            else
            {
                hearts[i].GetComponent<Image>().fillAmount = 1;
                hearts[i].transform.GetChild(0).GetComponent<Image>().fillAmount = 0;
            }
        }
    }
    void depleteHearts()
    {
        bool loseState = KeyDirectory.Lives.getState();
        if (!loseState && currentHearts < initHearts)
        {
            hearts[currentHearts].GetComponent<Image>().fillAmount = 1;
            hearts[currentHearts].transform.GetChild(0).GetComponent<Image>().fillAmount = 1;
            print("Curr Hearts : " + currentHearts);
            hearts[currentHearts].transform.GetChild(0).GetComponent<PlayerHeartAnimation>().dpleteHeart();
        }
    }
	
}
