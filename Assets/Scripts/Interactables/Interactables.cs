using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactables : MonoBehaviour
{
    public GameObject spaceInteractionPrefab;
    public GameObject mouseInteractionPrefab;
    public GameObject interactionObj;
    public bool isActivated;
    public string interactionMsg = "S\r\nP\r\nA\r\nC\r\nE";

    protected GameObject characterObj;
 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void StartInteraction()
    {
        
        HideInteractionUI();
    }
    public virtual void ShowInteractionUI()
    {
        if (characterObj != null && !characterObj.GetComponent<InteractionButton>() && characterObj.GetComponentInChildren<SpriteRenderer>().enabled)
        {
            if (Options.input == 0)
            {
                interactionObj = Instantiate(spaceInteractionPrefab, /*characterObj.transform.position, Quaternion.identity, */characterObj.transform);
                //interactionObj.GetComponent<InteractionButton>().mouseInputString = interactionMsg;
            }
            else if(Options.input == 1)
            {
                interactionObj = Instantiate(spaceInteractionPrefab, /*characterObj.transform.position, Quaternion.identity, */characterObj.transform);
                interactionObj.GetComponent<InteractionButton>().mouseInputString = interactionMsg;
            }
        }
        
    }
    public virtual void HideInteractionUI()
    {
        if (characterObj != null && characterObj.GetComponentInChildren<InteractionButton>())
        {
            Destroy(characterObj.GetComponentInChildren<InteractionButton>().gameObject);
        }
    }
}
