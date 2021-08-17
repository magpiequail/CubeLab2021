using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public GameObject[,] blockArray = new GameObject[5, 5];
    public Character charOnFloor;
    public int rows = 5;
    Transform[] childObj;
    bool brightSprite;
    bool isOriginalColor;

    private void Awake()
    {
        //put blocks in block array
        foreach (Transform child in transform)
        {
            if (child.GetComponent<BlockStat>())
            {
                blockArray[child.gameObject.GetComponent<BlockStat>().x, child.gameObject.GetComponent<BlockStat>().y] = child.gameObject;
            }

        }

        childObj = GetComponentsInChildren<Transform>();

    }

    // Start is called before the first frame update
    void Start()
    {
        isOriginalColor = true;
        if (charOnFloor)
        {
            brightSprite = false;

        }
        else if (charOnFloor == null)
        {
            brightSprite = true;
        }
        /*if (charOnFloor == null && brightSprite == true)
        {

            foreach (Transform child in childObj)
            {
                if (child.GetComponent<SpriteRenderer>())
                {
                    DarkenSprites(child.GetComponent<SpriteRenderer>());
                }
                
            }
            brightSprite = false;

        }
        else if (charOnFloor && brightSprite == false)
        {
            foreach (Transform child in childObj)
            {
                if (child.GetComponent<SpriteRenderer>())
                {
                    LightenSprites(child.GetComponent<SpriteRenderer>());
                }

            }
            brightSprite = true;
        }*/
    }

    // Update is called once per frame
    void Update()
    {


        if (charOnFloor == null && brightSprite == true)
        {
            for (int i = 0; i < childObj.Length; i++)
            {
                if (childObj[i] == true && childObj[i].GetComponent<SpriteRenderer>())
                {
                    DarkenSprites(childObj[i].GetComponent<SpriteRenderer>());
                }
            }

            brightSprite = false;
            isOriginalColor = false;

        }
        else if (charOnFloor && brightSprite == false)
        {
            foreach (Transform child in childObj)
            {
                if (child.GetComponent<SpriteRenderer>())
                {
                    LightenSprites(child.GetComponent<SpriteRenderer>());
                }
                /*if (child.GetComponentInChildren<SpriteRenderer>())
                {
                    LightenSprites(child.GetComponentInChildren<SpriteRenderer>());
                }*/
            }
            brightSprite = true;
        }
    }




    void DarkenSprites(SpriteRenderer sr)
    {
        sr.color = new Color(sr.color.r * 0.5f, sr.color.g * 0.5f, sr.color.b * 0.5f, sr.color.a);
    }
    void LightenSprites(SpriteRenderer sr)
    {
        if (!isOriginalColor)
        {
            sr.color = new Color(sr.color.r * 2f, sr.color.g * 2f, sr.color.b * 2f, sr.color.a);
        }

    }


}
