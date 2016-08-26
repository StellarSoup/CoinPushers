using UnityEngine;
using System.Collections;

public class CreateStarRating : MonoBehaviour {

    //Holds the star rating
    private int starRating;
    private GameObject[] stars;


    void Start () {
        //Checks the score and gets the correct star rating
        starRating = KeyDirectory.StarRanking.getStarRating();
        stars = new GameObject[starRating];

        //For each star
        for(int i = 0; i < starRating; i++)
        {
            //Gets the star prefab
            stars[i] = Instantiate(Resources.Load("Prefabs/Star",typeof(GameObject)) as GameObject);
            stars[i].transform.parent = transform;

            RectTransform starRect = stars[i].GetComponent<RectTransform>();
            int starLength = 2 - starRating / 2;

            //Sets the position and size of the star
            if (starRating%2 == 1)
            {
                starRect.anchorMin = new Vector2(0.2f * (i) + (starLength * 0.2f), 0);
                starRect.anchorMax = new Vector2(0.2f * (i + 1) + (starLength * 0.2f), 1);
                starRect.offsetMin = starRect.offsetMax = Vector2.zero;
                starRect.localPosition = new Vector3(starRect.localPosition.x, starRect.localPosition.y, 0);
                starRect.localScale = Vector3.one;
            }else
            {
                starRect.anchorMin = new Vector2(0.2f * (i) + (starLength * 0.2f) + 0.1f, 0);
                starRect.anchorMax = new Vector2(0.2f * (i + 1) + (starLength * 0.2f) + 0.1f, 1);
                starRect.offsetMin = starRect.offsetMax = Vector2.zero;
                starRect.localPosition = new Vector3(starRect.localPosition.x, starRect.localPosition.y, 0);
                starRect.localScale = Vector3.one;
            }
            starRect.offsetMin = starRect.offsetMax = Vector2.zero;
        }
        //Starts the sliding animation for the stars
        StartCoroutine(SlideAllStars());

	}

    //Controls the sliding animation for the stars
    //Creates a wavy pattern
    IEnumerator SlideAllStars()
    {
        //Waits till the stars have been displayed properly
        yield return new WaitForSeconds(7f);

        //Keeps moving the stars up and down every 5 seconds
        while (true)
        {
            
            for (int i = 0; i < starRating; i++)
            {
                stars[i].GetComponent<StarRatingObject>().StartStarSlide();
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(5f);

        }
    }
}
