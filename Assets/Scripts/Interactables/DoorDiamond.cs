using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDiamond : Door
{
    Animator diaDoorAnim;

    /*public GameObject interactionPrefab;
    GameObject interactionObj;
    public string interactionMsg = "사용";*/

    private void Awake()
    {
        isOpened = false;
        diaDoorAnim = GetComponentInChildren<Animator>();
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
            diaDoorAnim.SetInteger("Open", 1);
        }
        else
        {
            diaDoorAnim.SetInteger("Open", 0);
        }
        if (isAllOpen)
        {
            diaDoorAnim.SetInteger("Open", 2);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Character" /*&& collision.GetComponent<Character>().isHavingDiamondKey*/)
        {
            if (collision.GetComponent<Character>().characterKey == CharKeyState.DiamondKey && collision.gameObject.GetComponent<NormalCharacter>())
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
        diaDoorAnim.SetInteger("Open", 2);
    }
}
