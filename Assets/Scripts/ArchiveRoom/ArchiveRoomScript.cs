using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArchiveRoomScript : MonoBehaviour
{
    public Button[] memoryButton;
    public GameObject pauseUI;

    int firstTotalStars;
    int secondTotalStars;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseUI.SetActive(true);

        }

    }
    private void OnEnable()
    {
        for(int i = 0; i < 4; i++)
        {
            memoryButton[i].interactable = false;
        }
        for (int i = 1; i <= 20; i++)
        {
            firstTotalStars += PlayerPrefs.GetInt("" + i + "stars");
        }
        for(int i = 21; i <= 40; i++)
        {
            secondTotalStars += PlayerPrefs.GetInt("" + i + "stars");
        }
        if(firstTotalStars >= 30)
        {
            memoryButton[0].interactable = true;
        }
        if(firstTotalStars >= 60)
        {
            memoryButton[1].interactable = true;
        }

        if(secondTotalStars >= 30)
        {
            memoryButton[2].interactable = true;
        }
        if(secondTotalStars >= 60)
        {
            memoryButton[3].interactable = true;
        }
    }
    
}
