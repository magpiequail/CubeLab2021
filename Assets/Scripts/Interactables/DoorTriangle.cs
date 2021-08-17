using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriangle : Door
{
     Animator triDoorAnim;

    /*public GameObject interactionPrefab;
    GameObject interactionObj;
    public string interactionMsg = "사용";*/

    private void Awake()
    {
        isOpened = false;
        triDoorAnim = GetComponentInChildren<Animator>();
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
            triDoorAnim.SetInteger("Open", 1);
        }
        else
        {
            triDoorAnim.SetInteger("Open", 0);
        }
        if (isAllOpen)
        {
            triDoorAnim.SetInteger("Open", 2);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Character" /*&& collision.GetComponent<Character>().isHavingTriangleKey*/)
        {
            if (collision.GetComponent<Character>().characterKey == CharKeyState.TriangleKey&& collision.gameObject.GetComponent<NormalCharacter>())
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
        triDoorAnim.SetInteger("Open", 2);
    }
}
