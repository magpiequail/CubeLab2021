using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputSettings : MonoBehaviour
{
    Button interaction;
    Text buttonText;

    private void Awake()
    {
        interaction = GetComponentInChildren<Button>();
        buttonText = GetComponentInChildren<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Options.input == 0)
        {
            interaction.interactable = false;
            buttonText.text = "SPACE";
        }
    }
}
