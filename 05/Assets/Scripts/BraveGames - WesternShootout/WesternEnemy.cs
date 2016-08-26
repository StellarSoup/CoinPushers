using UnityEngine;
using System.Collections;

public class WesternEnemy : MonoBehaviour {
    private float time = 0;
    private bool isSliding = false;
    void Start()
    {
        StartCoroutine(slideEnemy());
    }
    IEnumerator slideEnemy()
    {
        transform.position = new Vector3(-10, transform.position.y, transform.position.z);
        yield return new WaitForSeconds(Random.value * 2);
        isSliding = true;
        
    }
    void Update()
    {
        if (isSliding)
        {
            transform.position = new Vector3(Mathf.Lerp(-10, 10, time), transform.position.y, transform.position.z);
            time += Time.deltaTime / 5;
        }
    }

    void OnMouseDown()
    {
        Destroy(gameObject);
    }
}
