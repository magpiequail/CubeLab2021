using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MemoryPopup : MonoBehaviour
{
    AudioManager audioManager;
    public float visableForHowLong;
    public GameObject[] memoryPopupPrefab = new GameObject[2];
    int totalLevelNum =40;
    int firstTotalStars;
    int secondTotalStars;
    public int[] requiredStars;
    GameObject MemoryPopupUI;
    bool isUIDestroyed = false;
    float accuTime = 0f;
    bool canDestroyPopup = true;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (GetAllFirstStars() >= 30)
        {
            if(GetAllFirstStars() < 60 && PlayerPrefs.GetInt("isMemoryPopupShowed" + 0) != 1)
            {
                audioManager.PlayAudio("UI_change");
                MemoryPopupUI = Instantiate(memoryPopupPrefab[0]);
                PlayerPrefs.SetInt("isMemoryPopupShowed" + 0, 1);
                return;
            }
            else if(GetAllFirstStars() >= 60 && PlayerPrefs.GetInt("isMemoryPopupShowed" + 1) != 1)
            {
                audioManager.PlayAudio("UI_change");
                MemoryPopupUI = Instantiate(memoryPopupPrefab[1]);
                PlayerPrefs.SetInt("isMemoryPopupShowed" + 1, 1);
                return;
            }
        }
        else if(GetAllSecondStars() >= 30)
        {
            if (GetAllFirstStars() < 60 && PlayerPrefs.GetInt("isMemoryPopupShowed" + 2) != 1)
            {
                audioManager.PlayAudio("UI_change");
                MemoryPopupUI = Instantiate(memoryPopupPrefab[2]);
                PlayerPrefs.SetInt("isMemoryPopupShowed" + 2, 1);
                return;
            }
            else if (GetAllFirstStars() >= 60 && PlayerPrefs.GetInt("isMemoryPopupShowed" + 3) != 1)
            {
                audioManager.PlayAudio("UI_change");
                MemoryPopupUI = Instantiate(memoryPopupPrefab[3]);
                PlayerPrefs.SetInt("isMemoryPopupShowed" + 3, 1);
                return;
            }
        }
        //for(int i = 0; i < memoryPopupPrefab.Length; i++)
        //{
        //    if (i <2 && GetAllFirstStars() >= requiredStars[i] && PlayerPrefs.GetInt("isMemoryPopupShowed" + i) != 1)
        //    {
        //        audioManager.PlayAudio("UI_change");
        //        MemoryPopupUI = Instantiate(memoryPopupPrefab[i]);
        //        PlayerPrefs.SetInt("isMemoryPopupShowed" + i, 1);
        //        return;
        //    }
        //    else if(i >= 2 && GetAllSecondStars() >= requiredStars[i] && PlayerPrefs.GetInt("isMemoryPopupShowed" + i) != 1)
        //    {
        //        audioManager.PlayAudio("UI_change");
        //        MemoryPopupUI = Instantiate(memoryPopupPrefab[i]);
        //        PlayerPrefs.SetInt("isMemoryPopupShowed" + i, 1);
        //        return;
        //    }
        //    else
        //    {
        //        return;
        //    }
        //}
        //MemoryPopupUI = Instantiate(memoryPopupPrefab[0]);
    }

    // Update is called once per frame
    void Update()
    {
        if(isUIDestroyed == false)
        {
            accuTime += Time.deltaTime;
            if (accuTime > visableForHowLong && SceneController.gameState == GameState.Running)
            {
                Destroy(MemoryPopupUI);
                isUIDestroyed = true;
            }
        }

    }

    int GetAllFirstStars()
    {
        if(SceneManager.GetActiveScene().buildIndex <= 20)
        {
            for (int i = 1; i <= 20; i++)
            {
                firstTotalStars += PlayerPrefs.GetInt("" + i + "stars");
            }
            
        }
        return firstTotalStars;

    }  
    
    int GetAllSecondStars()
    {
        if(SceneManager.GetActiveScene().buildIndex > 20)
        {
            for (int i = 21; i <= 40; i++)
            {
                secondTotalStars += PlayerPrefs.GetInt("" + i + "stars");
            }
            
        }
        return secondTotalStars;

    }


    public void ActivateMemoryNarration(GameObject meomoryNarration)
    {
        //canDestroyPopup = false;
        meomoryNarration.SetActive(true);
        SetStateToMem();
        
        Debug.Log("button clicked");
    }
    public void DestroyUI(GameObject popupUI)
    {

        Destroy(popupUI);

    }
    public void SetStateToMem()
    {
        SceneController.gameState = GameState.MemoryPlaying;
    }
    public void SetStateToRun()
    {
        SceneController.gameState = GameState.Running;
    }
}
