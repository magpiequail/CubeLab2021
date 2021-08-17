using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

public class Door :Interactables
{
    public float delayTillDoorOpen = 0.5f;
    public float delayTillStageClear=2.0f;
    public float delayTillNextStage = 2.5f;

    public bool isOpened = true;
    protected bool isHavingKey;
    private GameObject text;
    Door[] doorsArray;
    public static bool isAllOpen = false;

    bool isAudioPlayed = false;
    bool sceneTransitionAnimOn;

    Animator stars;

    private void Awake()
    {
        text = GameObject.FindGameObjectWithTag("Stage Clear");
        stars = GameObject.FindGameObjectWithTag("Stars").GetComponent<Animator>();
        if(SceneManager.GetActiveScene().buildIndex == 20 && PlayerPrefs.GetInt("" + SceneManager.GetActiveScene().buildIndex + "stars") == 0)
        {
            sceneTransitionAnimOn = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        stars.gameObject.SetActive(false);
        text.SetActive(false);
        doorsArray = FindObjectsOfType<Door>();
        isAllOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsAllDoorsOpen() == true && Input.GetKeyDown(KeyCode.Space) && CharactersMovement.isInputAllowed)
        {
            
            if (FindObjectOfType<Expression>())
            {
                Expression.faceAnim.Play("Happy");
            }
            StartCoroutine(Open());
        }
            
        if (isAllOpen)
        {

        }
    }

    public override void StartInteraction()
    {
        base.StartInteraction();
        if(IsAllDoorsOpen() == true)
        {
            StartCoroutine(Open());
            Debug.Log("start interaction called");
        }
        
    }

    private bool IsAllDoorsOpen()
    {
        for (int i = 0; i < doorsArray.Length; ++i)
        {
            if (doorsArray[i].isOpened == false)
            {
                isActivated = false;
                return false;
            }
        }
        if (doorsArray.Length == 0)
        {
            
            return false;
        }
        isActivated = true;
        Rate();
        return true;
    }

    protected virtual void PlayOpenAnim()
    {

    }

    
    IEnumerator Open()
    {

        isAllOpen = true;
        //Rate();
        if (!isAudioPlayed)
        {
            FindObjectOfType<AudioManager>().PlayAudio("Lobby_incu_steam");
            isAudioPlayed = true;
        }

        //yield return new WaitForSeconds(0);

        //foreach not working all the time
        //for not working all the time
        /*for(int i = 0;i<doorsArray.Length;i ++)
        {
            doorsArray[i].PlayOpenAnim();
        }*/


        yield return new WaitForSeconds(delayTillStageClear);
        
        FindObjectOfType<AudioManager>().PlayAudio("UI_change");
        text.SetActive(true);
        ShowStars();

        //if first time solving 20th stage
        if (sceneTransitionAnimOn == true)
        {
            SceneManager.LoadScene(32); // Lobby With Ani
            PlayerPrefs.SetInt("isSecondCapsuleOpen", 1);
            PlayerPrefs.SetInt("StageSelectPage", 0);
        }

        yield return new WaitForSeconds(delayTillNextStage);

         
        
        //go to next stage

        //GoToNextStage();

    }
    public void Rate()
    {
        if(PlayerPrefs.GetInt(""+ SceneManager.GetActiveScene().buildIndex+"stars") < Battery.stars)
        {
            PlayerPrefs.SetInt("" + SceneManager.GetActiveScene().buildIndex + "stars", Battery.stars);
        }
        
    }
    public void ShowStars()
    {
        stars.gameObject.SetActive(true);
        stars.SetInteger("Stars", Battery.stars);
    }
    public void GoToNextStage()
    {
        isAllOpen = false;
        if (SceneManager.GetActiveScene().buildIndex == 40)
        {
            SceneManager.LoadScene("Lobby");
        }
        CharactersMovement.isInputAllowed = true;
        /*Analytics.CustomEvent("StageClear", new Dictionary<string, object>
    {
        { "stage_index", SceneManager.GetActiveScene().buildIndex },
        { "how_many_stars", Battery.stars },
        { "how_many_moves_left", (Battery.movesforEmotion + Battery.movesforStageClear+Battery.movesTillGameover) }

    });*/

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void CollectStageClearData()
    {
        Analytics.CustomEvent("StageClear", new Dictionary<string, object>
    {
        { "Stage Index", SceneManager.GetActiveScene().buildIndex },
        { "How Many Stars", Battery.stars },
        { "How Many Moves Left", (Battery.movesforEmotion + Battery.movesforStageClear+Battery.movesTillGameover) }

    });
    }
}
