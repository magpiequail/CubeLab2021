using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypingTest : MonoBehaviour
{
    //public GameObject subtitle;
    Text subtitleText;
    public float typingSpeed;
    public float delayBetween; //delay time till next sentence

    [TextArea(3, 10)]
    public string[] sentences;
    //public float[] forHowLong;
    int index = 0;
    float accumulatedTime  =0f;
    bool isSentenceDone = true;

    private void Awake()
    {
        subtitleText = GetComponent<Text>();
    }


    // Start is called before the first frame update
    void Start()
    {
        subtitleText.text = null;
        //StartCoroutine("Type");
    }

    // Update is called once per frame
    void Update()
    {
        //accumulatedTime += Time.deltaTime;
        //if (accumulatedTime >= typingSpeed && isSentenceDone)
        //{
        //    accumulatedTime = 0f;

        //}
        //if (accumulatedTime >= typingSpeed)
        accumulatedTime += Time.deltaTime;
        if (accumulatedTime >= delayBetween && isSentenceDone)
        {
            isSentenceDone = false;
            StartCoroutine("Type");
            
        }


        //for (int i = 0; i < sentences[index].ToCharArray().Length; i++)
        //{
        //    accumulatedTime += Time.deltaTime;
        //    subtitleText.text += sentences[index].ToCharArray()[i];
        //}

        //index++;
        
    }
    private void LateUpdate()
    {
        
    }

    IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            
            if(subtitleText.text == sentences[index])
            {
                subtitleText.text = null;
                accumulatedTime = 0f;
                index++;
                isSentenceDone = true;
                yield return new WaitForSeconds(delayBetween);
            }
            else
            {
                subtitleText.text += letter; 
                yield return new WaitForSeconds(typingSpeed);
            }
            
        }
        //accumulatedTime = 0f;
        index++;
        isSentenceDone = true;
        yield return new WaitForSeconds(delayBetween);
    }
    
}
