using UnityEngine;
using System.Collections;

public class AdsController : ab_AddAttacker_Menu {


    //When the player presses the cross button it closes the add
    public void CloseAd()
    {
        numberOfAds -= 1;
        checkIfthereAreNoMoreAds();
        Destroy(gameObject);
    }

    //When the player presses the subscribe button on Kered's youtube channel 
    //they will be directed to his channel
    public void SubscribeToChannel()
    {
        Application.OpenURL("https://www.youtube.com/channel/UCKx_TElXtAqvB4vDyxfuAyQ");
    }

    public void Start()
    {
        numberOfAds += 1;
    }

    
}
