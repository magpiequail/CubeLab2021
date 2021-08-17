using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum GameState
{
    Running,
    Paused,
    Died,
    GameOver,
    MemoryPlaying
}

public class SceneController : MonoBehaviour
{
    public static GameState gameState = GameState.Running;
    public GameObject gameOver;
    GameObject gameOverUI;
    public float delayTillGameOver = 0.5f;
    public float delayTillUI = 2.0f;
    bool isGameOver = false;
    public GameObject pauseUI;
    public GameObject optionUI;
    public GameObject stageSelectUI;

    bool isAudioPlayed = false;

    public static AudioManager audioManager;

    private void Awake()
    {
        //stageSelectUI = GameObject.FindGameObjectWithTag("StageSelect");
        gameOver = GameObject.FindGameObjectWithTag("Game Over");
        gameOverUI = gameOver.GetComponentInChildren<Button>().transform.parent.gameObject;
        
        gameState = GameState.Running;
        pauseUI = GameObject.FindGameObjectWithTag("Pause");
        optionUI = pauseUI.GetComponentInChildren<Options>().gameObject;
        stageSelectUI = GameObject.FindGameObjectWithTag("StageSelect");

        audioManager = FindObjectOfType<AudioManager>();

        
    }

    // Start is called before the first frame update
    void Start()
    {
        //stageSelectUI = GameObject.FindGameObjectWithTag("StageSelect");
        gameOverUI.SetActive(false);
        gameOver.SetActive(false);
        optionUI.SetActive(false);
        pauseUI.SetActive(false);
        stageSelectUI.SetActive(false);
        CharactersMovement.isInputAllowed = true;
        FindObjectOfType<AudioManager>().PlayAudio("StageBgm");

        if (SceneManager.GetActiveScene().buildIndex <= 20)
        {
            PlayerPrefs.SetInt("CharPosIndex", 1);
        }
        else if (SceneManager.GetActiveScene().buildIndex > 20)
        {
            PlayerPrefs.SetInt("CharPosIndex", 2);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState == GameState.GameOver && !isGameOver)
        {
            StartCoroutine(GameOver());
            
        }
        else if ( gameState == GameState.Running)
        {
            ShowPauseScreen();
            Time.timeScale = 1f;
            if(AudioListener.pause == true)
            {
                AudioListener.pause = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && stageSelectUI.activeSelf ==true)
        {
            //stageSelectUI.SetActive(false);
            //gameState = GameState.Running;
            BackToGame();
        }
        else if(gameState == GameState.Paused)
        {
            Time.timeScale = 0f;
            CharactersMovement.isInputAllowed = false;
            if(AudioListener.pause == false)
            {
                AudioListener.pause = true;
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                optionUI.SetActive(false);
                BackToGame();
            }
            
        }
        
        else if(gameState == GameState.MemoryPlaying)
        {
            ShowPauseScreen();
            if (AudioListener.pause == false)
            {
                AudioListener.pause = true;
            }
        }
        else if(gameState == GameState.GameOver)
        {
            ShowPauseScreen();
        }

        //else if(Input.GetKeyDown(KeyCode.Escape) && gameState == GameState.Paused)
        //{
        //    gameState = GameState.Running;
        //    pauseUI.SetActive(false);
        //}

        
    }
    public void BackToGame()
    {
        if(stageSelectUI == null)
        {
            stageSelectUI = GameObject.FindGameObjectWithTag("StageSelect");
        }
        if(pauseUI == null)
        {
            pauseUI = GameObject.FindGameObjectWithTag("Pause");
        }
        
        if (pauseUI)
        {
            pauseUI.SetActive(false);
        }
        if (stageSelectUI)
        {
            stageSelectUI.SetActive(false);
        }
       
        CharactersMovement.isInputAllowed = true;
        if(gameState == GameState.Paused)
        {
            gameState = GameState.Running;
        }
        
    }
    public void BackToLobby()
    {
        gameState = GameState.Running;
        SceneManager.LoadScene("Lobby");
        CharactersMovement.isInputAllowed = true;
        if (SceneManager.GetActiveScene().buildIndex <= 20)
        {
            PlayerPrefs.SetInt("CharPosIndex", 1);
        }
        else if(SceneManager.GetActiveScene().buildIndex > 20)
        {
            PlayerPrefs.SetInt("CharPosIndex", 2);
        }
        Debug.Log("playerpref CharPosIndex is " + PlayerPrefs.GetInt("CharPosIndex"));
    }
    public void BackToTitle()
    {
        gameState = GameState.Running;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Title");
        CharactersMovement.isInputAllowed = true;
    }
    public void Restart()
    {
        gameState = GameState.Running;
        Door.isAllOpen = false;
        CharactersMovement.isInputAllowed = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
    public void BackToLevelSelect(GameObject obj)
    {
        //stageSelectUI = GameObject.FindGameObjectWithTag("StageSelect");
        stageSelectUI = obj;
        pauseUI = GameObject.FindGameObjectWithTag("Pause");
        if(gameState == GameState.Running)
        {
            gameState = GameState.Paused;
        }
        
        CharactersMovement.isInputAllowed = false;

        if (pauseUI)
        {
            pauseUI.SetActive(false);
        }
        
        stageSelectUI.SetActive(true);
    }
    public void NextStage()
    {
        Door.isAllOpen = false;
        if (SceneManager.GetActiveScene().buildIndex == 27)
        {
            SceneManager.LoadScene("Stage Select");
        }
        CharactersMovement.isInputAllowed = true;
        

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LevelSelectScene()
    {
        SceneManager.LoadScene("Stage Select");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetFloat("Volume", 1.0f);
        InitializeOptions();
        SceneManager.LoadScene("Intro01");
    }

    IEnumerator GameOver()
    {
        

        yield return new WaitForSeconds(delayTillGameOver);
        if (!isAudioPlayed)
        {
            audioManager.PlayAudio("GameOver");
            isAudioPlayed = true;
        }
        gameOver.SetActive(true);
        yield return new WaitForSeconds(delayTillUI);
        gameOverUI.SetActive(true);
        isGameOver = true;
        
    }
    void ShowPauseScreen()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameState = GameState.Paused;
            if (pauseUI == null)
            {
                pauseUI = GameObject.FindGameObjectWithTag("Pause");
            }
            pauseUI.SetActive(true);
        }
    }
    public void InitializeOptions()
    {
        PlayerPrefs.SetFloat("Volume",1.0f);
    }
    

}
