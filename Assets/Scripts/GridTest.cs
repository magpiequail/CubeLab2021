using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTest : MonoBehaviour
{
    public Grid grid;
    float gridX;
    float gridY;
    Vector3Int clickedTilePos;
    Vector3Int currentCharPos;
    Vector3 tileWorldPos;
    public GameObject character1;
    public GameObject character2;
    GameObject currentChar;
    Character[] characters;
    public LayerMask accessible;
    public float speed = 3f;
    Battery b;

    GameObject char1;
    GameObject char2;

    float halfScreen;
    public Camera leftCam;
    public Camera rightCam;
    public static Camera currentCam;

    

    private void Awake()
    {
        grid = FindObjectOfType<Grid>();
        characters = FindObjectsOfType<Character>();
        currentCharPos = grid.WorldToCell(Camera.main.ScreenToWorldPoint(character1.transform.position));
        gridX = grid.cellSize.x / 2;
        gridY = grid.cellSize.y / 2;
        b = FindObjectOfType<Battery>();

        halfScreen = Screen.width * 0.5f;

        char1 = character1;
        char2 = character2;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentCharPos = grid.WorldToCell(character1.transform.position);
        tileWorldPos = currentCharPos;
        clickedTilePos = currentCharPos;

    }

    // Update is called once per frame
    void Update()
    {


        if (CharactersMovement.isInputAllowed && Options.input == 1)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Input.mousePosition.x < halfScreen)
                {
                    currentChar = character1;
                    currentCam = leftCam;
                }
                else
                {
                    currentChar = character2;
                    currentCam = rightCam;
                }

                currentCharPos = grid.WorldToCell(currentChar.transform.position);
                clickedTilePos = grid.WorldToCell(currentCam.ScreenToWorldPoint(Input.mousePosition));
                tileWorldPos = grid.GetCellCenterWorld(clickedTilePos);

                Debug.Log("clickedTilePos = " + clickedTilePos + "currentCharPos = " + currentCharPos);
            }
            if (Physics2D.OverlapCircle(tileWorldPos, 0.01f, accessible))
            {

                if (clickedTilePos.x == currentCharPos.x && clickedTilePos.y == currentCharPos.y + 1)
                {
                    //currentChar.GetComponent<Character>().nextPos = tileWorldPos;
                    currentCharPos = clickedTilePos;
                    Debug.Log("NW");
                    for (int i = 0; i < characters.Length; i++)
                    {
                        characters[i].GetComponent<Character>().NWMovement();
                    }
                    b.MinusOneMove();
                }
                //NE
                else if (clickedTilePos.x == currentCharPos.x + 1 && clickedTilePos.y == currentCharPos.y)
                {
                    //currentChar.GetComponent<Character>().nextPos = tileWorldPos;
                    currentCharPos = clickedTilePos;
                    for (int i = 0; i < characters.Length; i++)
                    {
                        characters[i].GetComponent<Character>().NEMovement();
                    }
                    b.MinusOneMove();
                }
                //SW
                else if (clickedTilePos.x == currentCharPos.x - 1 && clickedTilePos.y == currentCharPos.y)
                {
                    //currentChar.GetComponent<Character>().nextPos = tileWorldPos;
                    currentCharPos = clickedTilePos;
                    for (int i = 0; i < characters.Length; i++)
                    {
                        characters[i].GetComponent<Character>().SWMovement();
                    }
                    b.MinusOneMove();
                }
                //SE
                else if (clickedTilePos.x == currentCharPos.x && clickedTilePos.y == currentCharPos.y - 1)
                {
                    //currentChar.GetComponent<Character>().nextPos = tileWorldPos;
                    currentCharPos = clickedTilePos;
                    for (int i = 0; i < characters.Length; i++)
                    {
                        characters[i].GetComponent<Character>().SEMovement();
                    }
                    b.MinusOneMove();
                }
                else
                {
                    
                }
                
            }
        }


    }
}
