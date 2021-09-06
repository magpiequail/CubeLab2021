using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : Interactables
{
    //otherTele is better with Teleporter, not GameObject
    public GameObject otherTele;
    Teleporter otherT;
    public int floorOrCeiling; //0 = floor 1 = ceiling

    public int posX;
    public int posY;
    public Animator teleAnim;
    Teleporter[] teleArray;

    //public GameObject interactionPrefab;
    //GameObject interactionObj;
    //public string interactionMsg = "사용";

    public Floor attachedFloor;
    AudioManager audioManager;
    public bool isCharOn = false;


    private void Awake()
    {
        teleAnim = GetComponentInChildren<Animator>();
        teleArray = FindObjectsOfType<Teleporter>();
        attachedFloor = GetComponentInParent<Floor>();
        audioManager = FindObjectOfType<AudioManager>();
        otherT = otherTele.GetComponent<Teleporter>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (characterObj == null)
        {

        }

        if (!Input.GetKey(KeyCode.A) &&
            !Input.GetKey(KeyCode.S) &&
            !Input.GetKey(KeyCode.D) &&
            !Input.GetKey(KeyCode.W))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //detect characterObj by raycast
                /*RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.forward, 100, ~(1 << 10));
                if (hit && hit.collider.tag == "Character")
                {
                    characterObj = hit.collider.gameObject;
                    Debug.Log("characterObj Detected");
                }*/
                if (/*Input.GetKeyDown(KeyCode.Space) && */isCharOn && characterObj.GetComponent<Character>().isUnitMoveAllowed && CharactersMovement.isInputAllowed)
                {

                    StartInteraction();


                }
            }

        }

    }

    public override void StartInteraction()
    {
        base.StartInteraction();
        if (characterObj.GetComponent<Character>().isUnitMoveAllowed && CharactersMovement.isInputAllowed)
        {

            RaycastHit2D hit = Physics2D.Raycast(this.transform.position, transform.forward);
            if(hit.collider.tag == "Character")
            {
                characterObj = hit.collider.gameObject;
            }
            else
            {

            }

            {
                for (int i = 0; i < teleArray.Length; i++)
                {
                    characterObj.GetComponent<Character>().ResetBlockColor();
                    if (teleArray[i].isActivated)
                    {


                        teleAnim.Play("TeleportSend");
                        FindObjectOfType<AudioManager>().PlayAudio("Lobby_incu_open");
                    }
                    else if (teleArray[i].isActivated == false && teleArray[i].isCharOn == true)
                    {
                        Debug.Log("play battery audio");
                        audioManager.PlayAudio("Ingame_battery");
                        return;
                    }
                }
            }

        }


        //otherTele.GetComponent<Teleporter>().teleAnim.Play("TeleportSend");
    }

    //}
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Character")
        {
            characterObj = collision.gameObject;

            if (characterObj.GetComponent<NormalCharacter>() && otherT.floorOrCeiling == 1)
            {
                isActivated = false;
                HideInteractionUI();
            }
            else if (characterObj.GetComponent<Spider>() && floorOrCeiling == 1 && otherT.isCharOn == true && otherT.characterObj.GetComponent<NormalCharacter>())
            {
                isActivated = false;
                HideInteractionUI();
            }
            else
            {
                isActivated = true;
            }

            //if (isActivated)
            //{
            //    ShowInteractionUI();
            //}

        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Character")
        {
            isCharOn = false;
            isActivated = false;
            HideInteractionUI();

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Character")
        {
            characterObj = collision.gameObject;
            isCharOn = true;

            //if normal character moves from floor to ceiling
            if (characterObj.GetComponent<NormalCharacter>() && otherT.floorOrCeiling == 1)
            {
                isActivated = false;
            }
            else if (characterObj.GetComponent<Spider>() && floorOrCeiling == 1 && otherT.isCharOn == true && otherT.characterObj.GetComponent<NormalCharacter>())
            {
                isActivated = false;
            }
            else
            {
                isActivated = true;
            }

            if (isActivated)
            {
                ShowInteractionUI();
            }

        }
    }



    //AnimEvent
    public void CharacterSpriteOn()
    {
        characterObj.GetComponentInChildren<SpriteRenderer>().enabled = true;
        ShowInteractionUI();
    }
    public void CharacterSpriteOff()
    {
        characterObj.GetComponentInChildren<SpriteRenderer>().enabled = false;
        HideInteractionUI();
    }
    public void SendCharacterToOther()
    {
        HideInteractionUI();
        //while the sprite is disabled, flip the spider character 
        if (characterObj.GetComponent<Spider>())
        {
            if (floorOrCeiling - otherT.floorOrCeiling != 0)
            {
                characterObj.GetComponent<Spider>().Flip();
            }
        }
        //if a normal character is trying to teleport to ceiling
        if (characterObj.GetComponent<NormalCharacter>())
        {
            if (floorOrCeiling - otherT.floorOrCeiling != 0)
            {
                Debug.Log("play battery audio");
                audioManager.PlayAudio("Ingame_battery");
                return;
            }
            //return;
        }
        //
        characterObj.transform.position = otherTele.transform.position;
        characterObj.GetComponent<Character>().currPos = otherTele.transform.position;
        characterObj.GetComponent<Character>().nextPos = otherTele.transform.position;
        characterObj.GetComponent<Character>().nextCharPos = otherTele.transform.position;

        if (otherT.attachedFloor.charOnFloor)
        {
            otherT.attachedFloor.charOnFloor = characterObj.GetComponent<Character>();

        }
        else if (!otherT.attachedFloor.charOnFloor)
        {
            attachedFloor.charOnFloor = null;
            otherT.attachedFloor.charOnFloor = characterObj.GetComponent<Character>();
        }



    }
    public void PlayReceive()
    {
        otherTele.GetComponentInChildren<Animator>().Play("TeleportReceive");
    }



}

