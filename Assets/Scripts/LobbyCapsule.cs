using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyCapsule : Interactables
{

    GameObject LobbyChar;
    Animator capsuleAnim;
    public int sceneIndex;
    
    

    private void Awake()
    {
        isActivated = false;
        interactionMsg = "접속";
        capsuleAnim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isActivated && Input.GetKeyDown(KeyCode.Space) && LobbyCharacter.isInputAllowed)
        {
            PlayerPrefs.SetInt("LobbyOnce", 1);
            LobbyCharacter.isInputAllowed = false;
            LobbyChar.GetComponentInChildren<Animator>().SetInteger("Idle", 1);
            HideInteractionUI();
            capsuleAnim.Play("Open_Lobby");
            FindObjectOfType<AudioManager>().PlayAudio("Lobby_incu_open");
            FindObjectOfType<AudioManager>().PlayAudio("Lobby_incu_steam");
        }
    }
    public override void StartInteraction()
    {
        base.StartInteraction();
        
        HideInteractionUI();
        capsuleAnim.Play("Open_Lobby");
        FindObjectOfType<AudioManager>().PlayAudio("Lobby_incu_open");
        FindObjectOfType<AudioManager>().PlayAudio("Lobby_incu_steam");
        
        //SceneManager.LoadScene(sceneBuildIndex: sceneIndex);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Character")
        {
            isActivated = true;
            characterObj = collision.gameObject;
            ShowInteractionUI();
            LobbyChar =  collision.transform.parent.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        HideInteractionUI();
        isActivated = false;
    }

    public void PlayCharAnim()
    {
        
        LobbyChar.GetComponentInChildren<Animator>().Play("LieDown");
    }
    public void LoadStageSelect()
    {
        SceneManager.LoadScene(sceneBuildIndex:sceneIndex);
    }
}
