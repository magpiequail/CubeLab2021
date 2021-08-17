using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScript : MonoBehaviour
{
    public GameObject continueButton;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("LobbyOnce"))
        {
            continueButton.SetActive(false);
        }
        Debug.Log("LobbyOnce = " + PlayerPrefs.HasKey("LobbyOnce"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
