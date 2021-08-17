using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class InteractionButton : MonoBehaviour, IPointerClickHandler
{
    Button interaction;
    public Text buttonText;
    public string keyInputString = "SPACE";
    public string mouseInputString;
    Interactables[] interactablesArray;

    private void Awake()
    {
        interaction = GetComponentInChildren<Button>();
        buttonText = GetComponentInChildren<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeButtonState();
    }

    // Update is called once per frame
    void Update()
    {
        //ChangeButtonState();
        GetComponent<Canvas>().worldCamera = CameraManager.currentCam;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(Options.input == 1)
        {
            InteractionClick();
        }
        
    }

    public void ChangeButtonState()
    {
        if (Options.input == 0)
        {

            buttonText.text = keyInputString;
            /*interactablesArray = FindObjectsOfType<Interactables>();
            foreach (Interactables inter in interactablesArray)
            {
                inter.HideInteractionUI();
                inter.ShowInteractionUI();
            }*/
        }
        if (Options.input == 1)
        {

                buttonText.text = mouseInputString;
            /*interactablesArray = FindObjectsOfType<Interactables>();
            foreach (Interactables inter in interactablesArray)
            {
                inter.HideInteractionUI();
                inter.ShowInteractionUI();
            }*/
        }
    }
    public void InteractionClick()
    {
        interactablesArray = FindObjectsOfType<Interactables>();
        for(int i = 0; i < interactablesArray.Length; i++)
        {
            if (interactablesArray[i].isActivated)
            {
                interactablesArray[i].StartInteraction();
            }
        }
        //GetComponentInParent<Interactables>().StartInteraction();
        Debug.Log("Button Clicked");
    }

    
}

