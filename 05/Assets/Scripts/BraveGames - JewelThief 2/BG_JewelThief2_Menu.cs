using UnityEngine;
using System.Collections;

public class BG_JewelThief2_Menu : GameState {


    void Start() {
        initGemBannerPos();
        //If the timer reaches 0 they will lose the game
        TimerLose();
    }

    //Gets the initial starting gem banner pos
    private void initGemBannerPos()
    {
        for (int i = 0; i < gemBanner.transform.childCount; i++)
        {
            GameObject bannerSection = gemBanner.transform.GetChild(i).gameObject;
            float startpos;
            if (i != gemBanner.transform.childCount - 1)
            {
                startpos = -10;
            }
            else
            {
                startpos = 10;
            }
            bannerSection.transform.localPosition = new Vector3(startpos, bannerSection.transform.localPosition.y, bannerSection.transform.localPosition.z);
        }
    }

    public GameObject gemBanner;
    //Slides in the gem banner
    public void slideInGemBanner()
    {
        StartCoroutine(YouGotTheGemWinBannerSlide());
    }
    IEnumerator YouGotTheGemWinBannerSlide()
    {
        float slideTime = 0;

        while (slideTime < 1)
        {
            slideTime += Time.deltaTime;
            for (int i = 0; i < gemBanner.transform.childCount; i++)
            {
                GameObject bannerSection = gemBanner.transform.GetChild(i).gameObject;
                float startpos;
                if(i != gemBanner.transform.childCount -1)
                {
                    startpos = -10;
                }else
                {
                    startpos = 10;
                }
                bannerSection.transform.localPosition = new Vector3(Mathf.Lerp(startpos, 0, slideTime), bannerSection.transform.localPosition.y, bannerSection.transform.localPosition.z);
            }
            yield return new WaitForEndOfFrame();
        }

    }

    
}
