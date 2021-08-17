using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDefault : Door
{
    Animator defaultDoorAnim;


    private void Awake()
    {
        isOpened = false;
        defaultDoorAnim = GetComponentInChildren<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isAllOpen)
        {
            defaultDoorAnim.SetInteger("Open", 2);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Character" )
        {
            if (collision.gameObject.GetComponent<NormalCharacter>())
            {
                isOpened = true;
                characterObj = collision.gameObject;
                ShowInteractionUI();
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Character")
        {
            isOpened = false;
            HideInteractionUI();
        }
    }

    protected override void PlayOpenAnim()
    {
        base.PlayOpenAnim();
        defaultDoorAnim.SetInteger("Open", 2);
    }
}
