using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TwinkleAnimation : MonoBehaviour
{
    //SpriteRenderer sr;
    Image im;
    float maxAlpha;
    float minAlpha;
    float duration;
    float accuTime;
    float delay;
    float alpha;

    private void Awake()
    {
        //sr = GetComponent<SpriteRenderer>();
        im = GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
        minAlpha = Random.Range(0.0f, 0.2f);
        maxAlpha = Random.Range(0.8f, 1.0f);
        duration = Random.Range(1.0f, 3.0f);
        delay = Random.Range(0.0f, 3.0f);

    }

    // Update is called once per frame
    void Update()
    {
        //brighter
        if(alpha <= minAlpha )
        {
            alpha += Time.deltaTime / duration;
            im.color = new Color(im.color.r, im.color.g, im.color.b, alpha);
            accuTime = 0f;
        }
        //brighter
        else if(alpha < maxAlpha && alpha > minAlpha && accuTime < delay)
        {
            accuTime = 0f;
            alpha += Time.deltaTime / duration;
            im.color = new Color(im.color.r, im.color.g, im.color.b, alpha);
        }

        else if (alpha >= maxAlpha)
        {

            accuTime += Time.deltaTime;
            //Debug.Log("accuTime = " + accuTime);
        }

        //dimmer
        if (accuTime >= delay)
        {
            
            alpha -= Time.deltaTime / duration;
            im.color = new Color(im.color.r, im.color.g, im.color.b, alpha);
        }

        
    }

    void ChangeAlpha(float f1, float f2, float dura)
    {
        alpha += Time.deltaTime / dura;
        im.color = new Color(im.color.r, im.color.g, im.color.b, alpha);
    }
}
