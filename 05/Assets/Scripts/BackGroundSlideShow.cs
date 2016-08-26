using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BackGroundSlideShow : MonoBehaviour {
    /*Controls the slideshow that appears behind the buttons in the title card
      and the main menu*/
    
    //List of all the images in the slideshow
    public Image[] collectionOfImages;

    //Used to scale the images to the correct size
    float scale = 1.7f;

    // Use this for initialization
    void Start() {
        //Doesnt destory the slideshow when loading new levels
        DontDestroyOnLoad(gameObject);
        //Imports a random set of images into the slideshow
        setUpImages();
	}
    //Loads in the images and sets them in the correct position
    public void setUpImages()
    {
        collectionOfImages = new Image[10];
        for (int i = 0; i < collectionOfImages.Length; i++)
        {
            GameObject imageHolder = Instantiate(new GameObject(i.ToString(),typeof(Image)));

            Image newImage = imageHolder.GetComponent<Image>();
            //Each image is randomly selected from a folder of game images
            int imageIndex = Random.Range(0, 7);
            newImage.sprite = Resources.Load<Sprite>("CaptureShotsForSlideShow/" + imageIndex);
            collectionOfImages[i] = newImage;

            RectTransform newImageRect = newImage.GetComponent<RectTransform>();
            newImageRect.sizeDelta = new Vector2(1336, 768);
            //Sets the images to be displayed correctly
            imageHolder.transform.gameObject.transform.SetParent(GameObject.Find(transform.name + "/ImageHolder").transform);
            newImage.transform.localScale = Vector3.one * scale;
            newImage.transform.localRotation = new Quaternion(0, 0, 0, 0);
            newImage.transform.localPosition = new Vector2(0, newImageRect.sizeDelta.y * scale * (i - 1));
        }
    }

    //Handles the movement of the slideshow
    private void MoveSlideShow()
    {
        //Moves all the images down
        for (int i = 0; i < collectionOfImages.Length; i++)
        {
            Image currImage = collectionOfImages[i];
            RectTransform currImageRect = currImage.GetComponent<RectTransform>();
            currImage.transform.localPosition = currImage.transform.localPosition - new Vector3(0, 0.1f, 0);

            //If the image moves past the camera completely
            if (currImage.transform.localPosition.y < -currImageRect.sizeDelta.y * 2 * scale)
            {
                //Move it to the top of the slideshow
                currImage.transform.localPosition = new Vector2(0, currImageRect.sizeDelta.y * scale * (collectionOfImages.Length)) - new Vector2(0, currImageRect.sizeDelta.y * scale * 2);
            }
        }
    }
    
	
	// Update is called once per frame
	void Update () {
        MoveSlideShow();
	}
}
