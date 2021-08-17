using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Elevator : Interactables
{
    public GameObject otherElevator;
    //public GameObject attachedFloor;
    //bool isCharOn;
    //public bool isActivated = false;
    
    SpriteRenderer sprite;

    Animator elevatorAnim; // state 1 is when it is opening, state 0 is when it is closing, open receive = 2, close receive = 3

    public Floor attachedFloor;
    Elevator[] elevatorArray;

    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        elevatorAnim = GetComponentInChildren<Animator>();
        isActivated = false;
        attachedFloor = GetComponentInParent<Floor>();

        elevatorArray = FindObjectsOfType<Elevator>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (CharactersMovement.isInputAllowed)
        {
            if (!Input.GetKey(KeyCode.A) &&
                        !Input.GetKey(KeyCode.S) &&
                        !Input.GetKey(KeyCode.D) &&
                        !Input.GetKey(KeyCode.W))
            {
                if (Input.GetKeyDown(KeyCode.Space) && isActivated && characterObj.GetComponent<Character>().isUnitMoveAllowed /*&& CharactersMovement.isInputAllowed*/)
                {
                    ////캐릭터가 속한 플로어 바꾸기
                    //characterColl.GetComponentInParent<CharacterMovement>().floor = otherTele.GetComponent<Teleporter>().attachedFloor;
                    ////캐릭터의 플로어 스크립트 변경
                    //characterColl.GetComponentInParent<CharacterMovement>().fl = characterColl.GetComponentInParent<CharacterMovement>().floor.GetComponent<Floor>();
                    //캐릭터의 위치 변경

                    //이펙트 재생
Debug.Log(gameObject.name + CharactersMovement.isInputAllowed);


                    StartInteraction();
                    Debug.Log("elevator interaction started, " + gameObject.name);
                    


                    //characterColl.GetComponentInParent<Character>().fl.charPosX = otherTele.GetComponent<Teleporter>().posX;
                    // characterColl.GetComponentInParent<Character>().fl.charPosY = otherTele.GetComponent<Teleporter>().posY;
                }


            }
        }

        //if (Input.GetKeyDown(KeyCode.Space) && isActivated)
        //{
        //    Debug.Log("elevator interaction started, " + gameObject.name);
        //    Debug.Log(gameObject.name + CharactersMovement.isInputAllowed);
        //}

    }

    //private void OnTriggerStay2D(Collider2D other)
    //{
    //    if (other.tag == "Character")
    //    {
    //        if (isSpacePressed /*&& otherTele.GetComponent<Teleporter>().isCharOn*/)
    //        {
    //            Debug.Log("space pressed");
    //            other.gameObject.GetComponentInParent<CharacterMovement>().floor = otherTele.GetComponent<Teleporter>().attachedFloor;
    //            other.gameObject.transform.parent.gameObject.transform.position = otherTele.transform.position;
    //            other.gameObject.GetComponentInParent<CharacterMovement>().fl = other.gameObject.GetComponentInParent<CharacterMovement>().floor.GetComponent<Floor>();
    //            //Debug.Log("character position = " +other.gameObject.transform.parent.gameObject.transform.position + other.gameObject.transform.parent.gameObject.name);
    //            // Debug.Log("other teleporter = " +otherTele.transform.position + otherTele.gameObject.name);
    //            isSpacePressed = false;
    //        }
    //    }

    //}

    public override void StartInteraction()
    {
        base.StartInteraction();
        
        if (isActivated && characterObj.GetComponent<Character>().isUnitMoveAllowed && CharactersMovement.isInputAllowed)
        {
            CharactersMovement.isInputAllowed = false;
            for (int i = 0; i < elevatorArray.Length; i++)
            {
                if (elevatorArray[i].isActivated)
                {
                    elevatorArray[i].characterObj.GetComponent<Character>().ResetBlockColor();

                    if (elevatorArray[i].sprite.transform.position.x < elevatorArray[i].characterObj.transform.position.x)
                    {
                        elevatorArray[i].characterObj.GetComponent<Character>().characterAnim.SetInteger("Direction", 1);
                    }
                    else
                    {
                        elevatorArray[i].characterObj.GetComponent<Character>().characterAnim.SetInteger("Direction", 2);
                    }
                    elevatorArray[i].elevatorAnim.SetInteger("State", 1);
                }

            }
            FindObjectOfType<AudioManager>().PlayAudio("Ingame_elevator");
            //characterColl.transform.position = otherElevator.transform.position;
            //characterColl.GetComponent<Character>().currPos = otherElevator.transform.position;
            //characterColl.GetComponent<Character>().nextPos = otherElevator.transform.position;
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Character")
        {
            characterObj = collision.gameObject;
            isActivated = true;
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Character" /*&& collision.GetComponentInChildren<SpriteRenderer>().enabled*/)
        {
            characterObj = collision.gameObject;
            ShowInteractionUI();
        }
    }



    //AnimEvent
    public void CharacterSpriteOn()
    {
        characterObj.GetComponentInChildren<SpriteRenderer>().enabled = true;
        if (characterObj.GetComponentInChildren<Key>())
        {
            characterObj.GetComponentInChildren<Key>().keyMesh.SetActive(true);
        }
        /*if (characterObj.GetComponentInChildren<InteractionButton>())
        {
            characterObj.GetComponentInChildren<InteractionButton>().enabled = true;
        }*/
        ShowInteractionUI();

    }
    public void CharacterSpriteOff()
    {
        characterObj.GetComponentInChildren<SpriteRenderer>().enabled = false;
        if (characterObj.GetComponentInChildren<Key>())
        {
            characterObj.GetComponentInChildren<Key>().keyMesh.SetActive(false);
        }

    }
    public void SendCharacterToOther()
    {

        characterObj.transform.position = otherElevator.transform.position;
        characterObj.GetComponent<Character>().currPos = otherElevator.transform.position;
        characterObj.GetComponent<Character>().nextPos = otherElevator.transform.position;
        characterObj.GetComponent<Character>().nextCharPos = otherElevator.transform.position;
        if (sprite.transform.position.x < characterObj.transform.position.x)
        {
            characterObj.GetComponent<Character>().characterAnim.SetInteger("Direction", 4);
        }
        else
        {
            characterObj.GetComponent<Character>().characterAnim.SetInteger("Direction", 3);
        }
        
    }
    public void PlayOpenReceive()
    {
        attachedFloor.charOnFloor = null;
        otherElevator.GetComponent<Elevator>().attachedFloor.charOnFloor = characterObj.GetComponent<Character>();

        otherElevator.GetComponentInChildren<Animator>().SetInteger("State",2);
    }
    public void PlayCloseReceive()
    {
        elevatorAnim.SetInteger("State", 3);
    }
    public void CloseElevator()
    {
        elevatorAnim.SetInteger("State", 0);
        
    }

}
