using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectScene : MonoBehaviour
{
    public GameObject[] page;
    int currentPageIndex;
    public Button leftButton;
    public Button rightButton;

    bool isPause = false;
    public GameObject option;

    // Start is called before the first frame update
    void Start()
    {
        currentPageIndex = PlayerPrefs.GetInt("StageSelectPage");
        InitializePage();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateButton();
        if (Input.GetKeyDown(KeyCode.Escape) && option)
        {
            if (!isPause && Time.timeScale !=0)
            {
                
                option.SetActive(true);
                isPause = true;
            }
            else if (isPause && Time.timeScale != 0)
            {
                option.SetActive(false);
                isPause = false;
                option.GetComponentInChildren<Options>().SaveCurrentOption();
            }
        }
    }

    public void InitializePage()
    {
        
        for (int i = 0; i < page.Length; i++)
        {
            if(i != currentPageIndex)
            {
                page[i].SetActive(false);
            }
            else
            {
                page[i].SetActive(true);
            }
            
        }
        
    }

    public void UpdateButton()
    {
        if (currentPageIndex == 0)
        {
            leftButton.interactable = false;
        }
        else
        {
            leftButton.interactable = true;
        }
        if (currentPageIndex == page.Length - 1)
        {
            rightButton.interactable = false;
        }
        else
        {
            rightButton.interactable = true;
        }
    }
    public void UpdatePage()
    {
        
    }
    public void IndexPlus()
    {
        if(currentPageIndex != page.Length - 1)
        {
            currentPageIndex++;
            PlayerPrefs.SetInt("StageSelectPage", currentPageIndex);
        }
        InitializePage();
    }
    public void IndexMinus()
    {
        if (currentPageIndex != 0)
        {
            currentPageIndex--;
            PlayerPrefs.SetInt("StageSelectPage", currentPageIndex);
        }
        InitializePage();
    }
}
