using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDiamond : Key
{
    public float keyPosition;
    Animator diamondKeyAnim;
    Vector2 originPos;
    //public SpriteRenderer sprite;
    bool isWithChar = false;
    //GameObject character;
    


    private void Awake()
    {
        diamondKeyAnim = GetComponentInChildren<Animator>();
        keyAnim = diamondKeyAnim;
        originPos = transform.position;
        //sprite = GetComponentInChildren<SpriteRenderer>();
        isActivated = false;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetKey(KeyCode.A) &&
            !Input.GetKey(KeyCode.S) &&
            !Input.GetKey(KeyCode.D) &&
            !Input.GetKey(KeyCode.W))
        {
            if (isActivated && CharactersMovement.isInputAllowed)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    GetKey();

                }
            }
        }
        if (Door.isAllOpen)
        {
            keyMesh.SetActive(false);
        }

    }

    private void GetKey()
    {
        if (characterObj.GetComponent<Character>().characterKey == CharKeyState.Empty)
        {
            isWithChar = true;
            if (characterObj.GetComponent<Spider>())
            {
                gameObject.transform.localScale = new Vector3(-1, -1, 1);
            }
                diamondKeyAnim.SetInteger("State", 2);
            effectAnim.SetTrigger("EffectTrigger");

            FindObjectOfType<AudioManager>().PlayAudio("Ingame_elevator");
            if (FindObjectOfType<Expression>())
            {
                Expression.faceAnim.Play("Happy");
            }
            if (characterObj.GetComponent<NormalCharacter>())
            {
                characterObj.GetComponentInChildren<Animator>().SetTrigger("Joy");
                if (characterObj.GetComponentInChildren<Animator>().GetInteger("Direction") < 3)
                {
                    characterObj.GetComponentInChildren<Animator>().SetInteger("Direction", 3);
                }
            }
            


            gameObject.transform.SetParent(characterObj.transform);
            //currently position is controlled by animation
            //gameObject.transform.position = new Vector2(originPos.x, originPos.y + keyPosition); //keyPosition not working properly. shifting position with animation
            characterObj.GetComponent<Character>().characterKey = CharKeyState.DiamondKey;
            isActivated = false;
        }


    }

    public override void StartInteraction()
    {
        base.StartInteraction();
        if (isActivated && CharactersMovement.isInputAllowed && characterObj.GetComponent<Character>().isUnitMoveAllowed)
        {
            GetKey();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Character")
        {
            characterObj = collision.gameObject;
            ShowInteractionUI();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Character")
        {
            isActivated = true;
            diamondKeyAnim.SetInteger("State", 1);
            //sprite.gameObject.transform.position = new Vector2(originPos.x, originPos.y + keyPosition);
            characterObj = other.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Character")
        {
            if (!isWithChar)
            {
                isActivated = false;
                diamondKeyAnim.SetInteger("State", 0);
                //sprite.gameObject.transform.position = originPos;
            }
            HideInteractionUI();

        }
    }
}
