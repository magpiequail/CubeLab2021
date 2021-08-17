using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lobby : MonoBehaviour
{
    bool isPause = false;
    public GameObject option;
    GameObject lobbyChar;
    public Transform[] charPositions;

    public GameObject secondCapsule;
    public CircleCollider2D capsuleCollider;

    private void Awake()
    {
        //pauseUI = GameObject.FindGameObjectWithTag("Pause");
        lobbyChar = FindObjectOfType<LobbyCharacter>().gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        lobbyChar.transform.position = charPositions[ PlayerPrefs.GetInt("CharPosIndex")].position;
        lobbyChar.GetComponent<LobbyCharacter>().currPos = charPositions[PlayerPrefs.GetInt("CharPosIndex")].position;
        lobbyChar.GetComponent<LobbyCharacter>().nextPos = charPositions[PlayerPrefs.GetInt("CharPosIndex")].position;
    
        if(PlayerPrefs.GetInt("isSecondCapsuleOpen") != 1)
        {
            capsuleCollider.enabled = false;
            secondCapsule.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPause)
            {
                option.SetActive(true);
                isPause = true;
                LobbyCharacter.isInputAllowed = false;
            }
            else if (isPause)
            {
                option.SetActive(false);
                isPause = false;
                option.GetComponentInChildren<Options>().SaveCurrentOption();
                LobbyCharacter.isInputAllowed = true;
            }
        }
    }

    private void OnEnable()
    {
        
    }

    /*public void LoadStage1()
    {
        SceneManager.LoadScene("Stage01");
    }
    public void LoadStage2()
    {
        SceneManager.LoadScene("Stage02");
    }
    public void LoadStage3()
    {
        SceneManager.LoadScene("Stage03");
    }
    public void LoadStage4()
    {
        SceneManager.LoadScene("Stage04");
    }
    public void LoadStage5()
    {
        SceneManager.LoadScene("Stage05");
    }
    public void LoadStage6()
    {
        SceneManager.LoadScene("Stage06");
    }*/
}
