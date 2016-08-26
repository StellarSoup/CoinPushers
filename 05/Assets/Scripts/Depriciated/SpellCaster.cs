using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpellCaster : MonoBehaviour {
    private GameObject spellHolder;
    public GameObject effectspanel;
    public GameObject hero;

    private bool spellBeingCasted;

	// Use this for initialization
	void Start () {
        spellBeingCasted = false;
        spellHolder = GameObject.Find("Spells");

        bool keptHeart = KeyDirectory.Lives.getState();

        if (keptHeart)
        {
            StartCoroutine(castSpell());
        }else
        {
            StartCoroutine(receiveDamage());
        }
    }
	
	// Update is called once per frame
	void Update () {
    }
    IEnumerator receiveDamage()
    {
        yield return new WaitForSeconds(0);
    }

    IEnumerator castSpell()
    {
        yield return new WaitForSeconds(0.3f);

        hero.GetComponent<Animator>().Play("DarkMage-Attack");
            StartCoroutine(castIceSpell());
        yield return new WaitUntil(() => spellBeingCasted);
        spellBeingCasted = false;
        StartCoroutine(displayDamageCount());
            StartCoroutine(destroyEnemy());
        hero.GetComponent<Animator>().Play("DarkMage-ResetAttack");

    }

    IEnumerator destroyEnemy()
    {
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        float disposition = 10f;
        Vector2 leftPos = new Vector2(enemy.transform.localPosition.x- disposition, enemy.transform.localPosition.y);
        Vector2 rightPos = new Vector2(enemy.transform.localPosition.x + disposition, enemy.transform.localPosition.y);

        float time = 0;
        int iterations = 0;
        float timeSpeed = Time.deltaTime*6;

        Image enemyImage = enemy.GetComponent<Image>();
        float enemyAlphaTime = 0;
        
        while (enemyImage.color.a > 0)
        {
            if (iterations % 2 == 0)
            {
                enemy.transform.localPosition = Vector2.Lerp(leftPos, rightPos, time);
            }else
            {
                enemy.transform.localPosition = Vector2.Lerp(rightPos, leftPos, time);
            }
            time += timeSpeed;
            if (time > 1)
            {
                iterations++;
                time = 0;
            }
            yield return new WaitForEndOfFrame();
            enemyImage.color = new Color(enemyImage.color.r, enemyImage.color.g, enemyImage.color.b, Mathf.Lerp(1, 0, enemyAlphaTime));
            enemyAlphaTime += Time.deltaTime;
        }
        Destroy(enemy);
    }

    IEnumerator displayDamageCount()
    {
        GameObject damTextObj = GameObject.Find("EnemyDamage");
        Text damageText = damTextObj.GetComponent<Text>();
        Vector3 startPos = damTextObj.transform.localPosition;
        Vector3 endPos = startPos + new Vector3(0, 60f, 0);

        int randomDamage = Random.Range(2000, 9999);
        damageText.text = randomDamage.ToString();
        float time = 0;
        float startSpeed = Time.deltaTime*2;
        while(time < 1)
        {         
            time += startSpeed;
            Color newTextColor = new Color(damageText.color.r, damageText.color.g, damageText.color.b, 1 - time);
            damageText.color = newTextColor;
            damTextObj.transform.localPosition = Vector3.Lerp(startPos, endPos, time);
            yield return new WaitForEndOfFrame();
        }
        
        yield return new WaitForSeconds(0.5f);
        Destroy(damageText);
    }

    IEnumerator changeSpellEffects(Color newColor)
    {
        Image panelImage = effectspanel.GetComponent<Image>();
        Color oldColor = panelImage.color;
        float time = 0;
        while(time <= 1)
        {
            panelImage.color = Color.Lerp(oldColor, newColor, time);
            time += Time.deltaTime*2;
            yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator castIceSpell()
    {
        int numberOfIce = 0;
        int numberToCast = 20;  
        StartCoroutine(changeSpellEffects(new Color(0, 1, 1, 0.2f)));
        while (numberOfIce < numberToCast)
        {
            numberOfIce++;
            Vector2 enemyPos = GameObject.FindGameObjectWithTag("Enemy").transform.localPosition;
            CreateIceSpell(new Vector2(enemyPos.x, enemyPos.y + Screen.height));
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(changeSpellEffects(new Color(0, 0, 0,0)));
        spellBeingCasted = true;
    }
    void CreateIceSpell(Vector2 startPos)
    {
        GameObject iceSpell = Instantiate(Resources.Load("Prefabs/IceSpell",typeof(GameObject))) as GameObject;
        iceSpell.transform.SetParent(spellHolder.transform);
        iceSpell.transform.localPosition = startPos;
        iceSpell.GetComponent<Animator>().enabled = false;

    }
}
