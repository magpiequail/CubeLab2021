using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ArchiveRoomEntrance : Interactables
{
    private void Awake()
    {
        interactionMsg = "열기";
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isActivated)
        {
            StartInteraction();
        }
    }
    public override void StartInteraction()
    {
        base.StartInteraction();
        PlayerPrefs.SetInt("CharPosIndex", 3);
        SceneManager.LoadScene(33); // archive room
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Character")
        {
            isActivated = true;
            characterObj = collision.gameObject;
            ShowInteractionUI();

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Character")
        {
            isActivated = false;
            HideInteractionUI();
        }
    }
}
