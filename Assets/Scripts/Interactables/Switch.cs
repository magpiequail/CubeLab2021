using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : Interactables
{
    public Laser[] controlledLasers;
    //bool isSwitchActive = false;
    SpriteRenderer currentSprite;
    public Sprite sprite1;
    public Sprite sprite2;

    private void Awake()
    {
        currentSprite = GetComponentInChildren<SpriteRenderer>();
        currentSprite.sprite = sprite1;
        isActivated = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) && CharactersMovement.isInputAllowed)
        {
            StartInteraction();
        }
        
    }

    public override void StartInteraction()
    {
        base.StartInteraction();
        HideInteractionUI();
        if (isActivated && CharactersMovement.isInputAllowed)
        {
            if (currentSprite.sprite == sprite1)
            {
                currentSprite.sprite = sprite2;
            }
            else if (currentSprite.sprite == sprite2)
            {
                currentSprite.sprite = sprite1;
            }
            foreach (Laser laser in controlledLasers)
            {
                laser.LaserActivation();
            }
            ShowInteractionUI();
        }

        
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
        isActivated = false;
        if (collision.tag == "Character")
        {
            HideInteractionUI();
            characterObj = null;
        }
    }

}
