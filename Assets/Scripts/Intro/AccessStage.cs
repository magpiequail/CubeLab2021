using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AccessStage : Interactables
{
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
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Character")
        {
            isActivated = true;
            ShowInteractionUI();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isActivated = false;
        HideInteractionUI();
    }
    public override void StartInteraction()
    {
        base.StartInteraction();

        SceneManager.LoadScene("Stage Select");
    }

}
