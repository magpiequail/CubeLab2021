using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : Interactables
{
    private void Awake()
    {
        interactionMsg = "나가기";
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
        
        SceneManager.LoadScene("Lobby"); 
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
