using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRound : Door
{
     Animator roundDoorAnim;

    /*public GameObject interactionPrefab;
    GameObject interactionObj;
    public string interactionMsg = "사용";*/

    private void Awake()
    {
        isOpened = false;
        roundDoorAnim =  GetComponentInChildren<Animator>();
    }

// Start is called before the first frame update
void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isOpened == true && !isAllOpen)
        {
            roundDoorAnim.SetInteger("Open", 1);
        }
        else
        {
            roundDoorAnim.SetInteger("Open", 0);
        }
        if (isAllOpen)
        {
            roundDoorAnim.SetInteger("Open", 2);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Character" /*&& collision.GetComponent<Character>().isHavingRoundKey*/)
        {
            if (collision.GetComponent<Character>().characterKey == CharKeyState.RoundKey && collision.gameObject.GetComponent<NormalCharacter>())
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
        roundDoorAnim.SetInteger("Open", 2);
        Debug.Log("PlayOpenAnim Called");
    }

}
