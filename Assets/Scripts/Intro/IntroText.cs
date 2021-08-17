using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class IntroText : MonoBehaviour
{
    public string[] sentences;
    Text currentText;
    public static int state = 0;
    public GameObject timeline;
    Image subtitleImg;
    public GameObject optionButton;
    

    bool showedInputOption = false;

    public AudioMixerGroup ttsAudioMixerGroup;
    public Sound[] ttsAudio;


    IntroCharacter introChar;

    public int index = 0;
    public float delayBetweenLine = 2.0f;

    private void Awake()
    {
        currentText = GetComponent<Text>();
        timeline.SetActive(false);
        subtitleImg = GetComponentInParent<Image>();
        optionButton.SetActive(false);

        foreach (Sound s in ttsAudio)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            //s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = ttsAudioMixerGroup;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        IntroCharacter.isInputAllowed = false;
        introChar = FindObjectOfType<IntroCharacter>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(index < sentences.Length && Time.timeScale !=0f)
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if(state == 0 && index < 7)
                {

                    index++;
                    
                }
                else if(state == 2 && index < sentences.Length - 1 )
                {
                    ttsAudio[index].volume = 1f;
                    Debug.Log("index is " + index);
                    if(ttsAudio[index].source.isPlaying == true && ttsAudio[index].source != null)
                    {
                        ttsAudio[index].source.Stop();
                        index++;
                        if(index > 8)
                        {
                            ttsAudio[index].source.Play();
                            
                        }
                        
                    }
                    else if(!ttsAudio[index].source.isPlaying && ttsAudio[index].source != null)
                    {
                        index++;
                        ttsAudio[index].source.Play();
                        
                    }
                    
                    //if (!ttsAudio[index-1].source.isPlaying && ttsAudio[index].source != null)
                    //{
                    //    ttsAudio[index].source.Play();
                    //}
                    if (index == sentences.Length - 1)
                    {
                        optionButton.SetActive(true);
                        showedInputOption = true;
                    }
                }

                
            }
            /*else if (index == 7)
            {

                subtitleImg.enabled = false;
                timeline.SetActive(true);
                StartCoroutine(StateTwo(0.5f));
                //subtitleImg.enabled = true;
                
            }*/

            else if (Input.GetKeyDown(KeyCode.Tab))
            {
                if(index < 7)
                {
                    index = 7;
                }
                else if(index >= 8 && !showedInputOption && subtitleImg.enabled == true)
                {
                    index = sentences.Length - 1;
                    introChar.TriggerGetUp();
                    ttsAudio[index].volume = 1f;
                    ttsAudio[sentences.Length-1].source.Play();
                    Debug.Log(index + " tts is played");
                    //IntroCharacter.isInputAllowed = true;
                    optionButton.SetActive(true);
                    showedInputOption = true;
                }

            }
            
        }
        if (index > sentences.Length)
        {
            
        }
        if (index == 7)
        {

            subtitleImg.enabled = false;
            timeline.SetActive(true);
            StartCoroutine(StateTwo(0.5f));
            //subtitleImg.enabled = true;

        }
        /*if (index == sentences.Length - 1)
        {
            IntroCharacter.isInputAllowed = true;
        }
        else
        {
            IntroCharacter.isInputAllowed = false;
        }*/
        /*else if (state == 2)     
        {
            subtitleImg.enabled = true;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                index++;
            }
        }*/


        currentText.text = sentences[index];
        if (optionButton.activeSelf)
        {
            index = sentences.Length - 1;
            currentText.text = sentences[index];
        }
        
    }

    public void SkipIntro()
    {

    }

    
    IEnumerator StateTwo(float sec)
    {
        yield return new WaitForSeconds(sec);
        state = 2;
        index = 8;
        subtitleImg.enabled = true;
        ttsAudio[index].source.Play();
        ttsAudio[index].source.volume = 0f;
    }
}
