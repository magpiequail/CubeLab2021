using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchiveMonitor : Interactables
{
    public GameObject monitorWindow;

    private void Awake()
    {
        interactionMsg = "접속";
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
        monitorWindow.SetActive(true);

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
