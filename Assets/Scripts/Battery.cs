using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class Battery : MonoBehaviour
{
    //public static int firstBatt;
    //public static int secondBatt;
    //public static int thirdBatt;

    //public int totalMoves;
    public static int movesforEmotion;
    public static int movesforStageClear;
    public static int movesTillGameover;

    public GameObject firstBatt;
    public GameObject secondBatt;
    public GameObject thirdBatt;

    public Text first;
    public Text second;
    public Text third;

    Animator firstAnim;
    Animator secondAnim;
    Animator thirdAnim;

    float firstPercent;
    float secondPercent;
    float thirdPercent;

    SceneInfo s;
    public static int stars = 0;

    private void Awake()
    {

        s = FindObjectOfType<SceneInfo>();

        firstAnim = firstBatt.GetComponent<Animator>();
        secondAnim = secondBatt.GetComponent<Animator>();
        thirdAnim = thirdBatt.GetComponent<Animator>();

        firstPercent = 1f;
        secondPercent = 1f;
        thirdPercent = 1f;

        firstAnim.SetFloat("Percent", firstPercent);
        secondAnim.SetFloat("Percent", secondPercent);
        thirdAnim.SetFloat("Percent", thirdPercent);

        
    }

    // Start is called before the first frame update
    void Start()
    {
        movesforEmotion = s.firstBattery;
        movesforStageClear = s.secondBattery;
        movesTillGameover = s.thirdBattery;

        first.text = "" + movesforEmotion;
        second.text = "" + movesforStageClear;
        third.text = "" + movesTillGameover;

    }

    // Update is called once per frame
    void Update()
    {

        if(movesTillGameover == 0)
        {

            SceneController.gameState = GameState.GameOver;
            stars = 0;
        }
        if(Door.isAllOpen)
        {
            if (movesforStageClear == s.secondBattery)
            {
                stars = 3;
            }
            else if (movesforStageClear >= 0 && movesTillGameover == s.thirdBattery)
            {
                stars = 2;

            }
            else if (movesforStageClear == 0 && movesTillGameover < s.thirdBattery)
            {
                stars = 1;
            }
        }
    }
    public void MinusOneMove()
    {
        //totalMoves -= 1;

        if(movesforEmotion != 0)
        {
            movesforEmotion -= 1;
            firstPercent =  (float)movesforEmotion/ s.firstBattery;
        }
        else if(movesforEmotion == 0 && movesforStageClear != 0)
        {
            movesforStageClear -= 1;
            secondPercent = (float)movesforStageClear / s.secondBattery;
        }
        else if(movesforEmotion ==0 && movesforStageClear == 0)
        {
            movesTillGameover -= 1;
            thirdPercent = (float)movesTillGameover / s.thirdBattery;
        }
        if(movesforEmotion == 0)
        {
            firstAnim.Play("Grey");
        }
        if(movesforStageClear == 0)
        {
            secondAnim.Play("Grey");
        }
        if(movesTillGameover == 0)
        {
            thirdAnim.Play("Grey");
            CollectGameOverData();
        }

        first.text = "" + movesforEmotion;
        second.text = "" + movesforStageClear;
        third.text = "" + movesTillGameover;

        firstAnim.SetFloat("Percent", firstPercent);
        secondAnim.SetFloat("Percent", secondPercent);
        thirdAnim.SetFloat("Percent", thirdPercent);

    }
    public void CollectGameOverData()
    {
        Analytics.CustomEvent("GameOver", new Dictionary<string, object>
    {
        { "Stage Index", SceneManager.GetActiveScene().buildIndex },

    });

    }
}
