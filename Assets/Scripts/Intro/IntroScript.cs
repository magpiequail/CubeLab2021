using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScript : MonoBehaviour
{
    bool isOptionShowing = false;
    GameObject optionUI;

    private void Awake()
    {
        optionUI = FindObjectOfType<IntroOption>().gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        optionUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isOptionShowing)
            {
                Time.timeScale = 0f;
                optionUI.SetActive(true);
                isOptionShowing = true;
            }
            else if (isOptionShowing)
            {
                Time.timeScale = 1.0f;
                optionUI.SetActive(false);
                isOptionShowing = false;
            }

        }
    }
    public void BackToIntro()
    {
        optionUI.SetActive(false);
        isOptionShowing = false;
        Time.timeScale = 1.0f;
    }
}
