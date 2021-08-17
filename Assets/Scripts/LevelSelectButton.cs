using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LevelSelectButton : MonoBehaviour/*, IPointerEnterHandler, IPointerExitHandler*/
{
    public int buildIndex;
    Image image;
    public Sprite oneStar;
    public Sprite twoStars;
    public Sprite threeStars;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.GetInt("" + buildIndex + "stars");
        if (PlayerPrefs.GetInt("" + buildIndex + "stars" )== 1)
        {
            image.sprite = oneStar;
        }
        else if (PlayerPrefs.GetInt("" + buildIndex + "stars") == 2)
        {
            image.sprite = twoStars;
        }
        else if (PlayerPrefs.GetInt("" + buildIndex + "stars") == 3)
        {
            image.sprite = threeStars;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void ButtonClicked()
    {
        SceneManager.LoadScene(buildIndex);
    }
    public int GetStar()
    {
        return PlayerPrefs.GetInt("" + buildIndex + "stars");
    }

    /*public void OnPointerEnter(PointerEventData eventData)
    {
        image.rectTransform.localScale = new Vector3(0.9f, 0.9f, 1.0f);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        image.rectTransform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }*/
    private void OnEnable()
    {
        //PlayerPrefs.GetInt("" + buildIndex + "stars");
        if (PlayerPrefs.GetInt("" + buildIndex + "stars") == 1)
        {
            image.sprite = oneStar;
        }
        else if (PlayerPrefs.GetInt("" + buildIndex + "stars") == 2)
        {
            image.sprite = twoStars;
        }
        else if (PlayerPrefs.GetInt("" + buildIndex + "stars") == 3)
        {
            image.sprite = threeStars;
        }
    }
}
