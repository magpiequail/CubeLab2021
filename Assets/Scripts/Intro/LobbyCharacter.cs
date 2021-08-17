using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyCharacter : MonoBehaviour
{
    public Animator characterAnim;
    //bool isUnitMoveAllowed = true;
    public static bool isInputAllowed = true;

    AudioManager audioManager;

    // public GameObject floor;


    public int rows = 5;

    public float speed;

    float inputWaitTime = 0.3f;
    float keyPressedTime;

    //public Floor fl;

    //public int charPosX = 2;
    //public int charPosY = 2;

    public Vector2 nextPos;
    Grid grid;
    float gridX;
    float gridY;
    public Vector2 currPos;
    public Vector3Int currentCharPos;
    Vector3Int clickedTilePos;
    Camera mainCam;

    public LayerMask accessible;
    public Vector3 tileWorldPos;

    private void Awake()
    {
        grid = FindObjectOfType<Grid>();
        gridX = grid.cellSize.x / 2;
        gridY = grid.cellSize.y / 2;

        currPos = transform.position;
        nextPos = transform.position;

        mainCam = Camera.main;
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        characterAnim = GetComponentInChildren<Animator>();
        characterAnim.SetInteger("Idle", 1);
        isInputAllowed = true;

        if (PlayerPrefs.GetInt("OptionValue") == 0)
        {
            Options.input = 0;
        }
        else if (PlayerPrefs.GetInt("OptionValue") == 1)
        {
            Options.input = 1;
        }
        //currPos = transform.position;
        //nextPos = transform.position;
    }


    // Update is called once per frame
    void Update()
    {

        if (isInputAllowed)
        {
            if (Physics2D.OverlapCircle(nextPos, 0.1f, accessible))
            {
                transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
            }
            if (Vector3.Distance(transform.position, nextPos) < 0.01f)
            {
                currPos = nextPos;
                characterAnim.SetInteger("Idle", 1);
            }


            if(Options.input == 0)
            {
                if (Input.GetKeyDown(KeyCode.S))
                {
                    keyPressedTime = 0f;
                    SWMovement();
                    audioManager.PlayCharacterFootstep();
                }

                else if (Input.GetKeyDown(KeyCode.D))
                {
                    keyPressedTime = 0f;
                    SEMovement();
                    audioManager.PlayCharacterFootstep();
                }

                else if (Input.GetKeyDown(KeyCode.A))
                {
                    keyPressedTime = 0f;
                    NWMovement();
                    audioManager.PlayCharacterFootstep();

                }
                else if (Input.GetKeyDown(KeyCode.W))
                {
                    keyPressedTime = 0f;
                    NEMovement();
                    audioManager.PlayCharacterFootstep();
                }

                if (Input.GetKey(KeyCode.S))
                {
                    SWMovement();
                    keyPressedTime += Time.deltaTime;
                    if(keyPressedTime > inputWaitTime)
                    {
                        keyPressedTime = 0f;
                        audioManager.PlayCharacterFootstep();
                    }
                }

                else if (Input.GetKey(KeyCode.D))
                {
                    SEMovement();
                    keyPressedTime += Time.deltaTime;
                    if (keyPressedTime > inputWaitTime)
                    {
                        keyPressedTime = 0f;
                        audioManager.PlayCharacterFootstep();
                    }
                }

                else if (Input.GetKey(KeyCode.A))
                {
                    NWMovement();
                    keyPressedTime += Time.deltaTime;
                    if (keyPressedTime > inputWaitTime)
                    {
                        keyPressedTime = 0f;
                        audioManager.PlayCharacterFootstep();
                    }
                }
                else if (Input.GetKey(KeyCode.W))
                {
                    NEMovement();
                    keyPressedTime += Time.deltaTime;
                    if (keyPressedTime > inputWaitTime)
                    {
                        keyPressedTime = 0f;
                        audioManager.PlayCharacterFootstep();
                    }
                }

            }
            else if(Options.input == 1)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    currentCharPos = grid.WorldToCell(gameObject.transform.position);

                    clickedTilePos = grid.WorldToCell(mainCam.ScreenToWorldPoint(Input.mousePosition));
                    tileWorldPos = grid.GetCellCenterWorld(clickedTilePos);


                    if (clickedTilePos.x == currentCharPos.x && clickedTilePos.y == currentCharPos.y + 1 && Physics2D.OverlapCircle(tileWorldPos, 0.01f, accessible))
                    {
                        NWMovement();
                        audioManager.PlayCharacterFootstep();
                    }
                    if (clickedTilePos.x == currentCharPos.x + 1 && clickedTilePos.y == currentCharPos.y && Physics2D.OverlapCircle(tileWorldPos, 0.01f, accessible))
                    {
                        NEMovement();
                        audioManager.PlayCharacterFootstep();
                    }
                    if (clickedTilePos.x == currentCharPos.x - 1 && clickedTilePos.y == currentCharPos.y && Physics2D.OverlapCircle(tileWorldPos, 0.01f, accessible))
                    {
                        SWMovement();
                        audioManager.PlayCharacterFootstep();
                    }
                    if (clickedTilePos.x == currentCharPos.x && clickedTilePos.y == currentCharPos.y - 1 && Physics2D.OverlapCircle(tileWorldPos, 0.01f, accessible))
                    {
                        SEMovement();
                        audioManager.PlayCharacterFootstep();
                    }
                }
            }
            

        }



    }

    public void SWMovement()
    {
        nextPos = new Vector2(currPos.x - gridX, currPos.y - gridY);
        characterAnim.SetInteger("Direction", 3);
        if (!Physics2D.OverlapCircle(nextPos, 0.1f, accessible))
        {
            nextPos = currPos;
        }


        characterAnim.SetInteger("Idle", 0);
        characterAnim.Play("Walk_SW");
        //audioManager.PlayCharacterFootstep();
    }
    public void SEMovement()
    {

        nextPos = new Vector2(currPos.x + gridX, currPos.y - gridY);
        characterAnim.SetInteger("Direction", 4);

        if (!Physics2D.OverlapCircle(nextPos, 0.1f, accessible))
        {
            nextPos = currPos;
        }


        characterAnim.SetInteger("Idle", 0);
        characterAnim.Play("Walk_SE");
        //audioManager.PlayCharacterFootstep();
    }
    public void NWMovement()
    {
        nextPos = new Vector2(currPos.x - gridX, currPos.y + gridY);
        characterAnim.SetInteger("Direction", 1);
        if (!Physics2D.OverlapCircle(nextPos, 0.1f, accessible))
        {
            nextPos = currPos;
        }


        characterAnim.Play("Walk_NW");
        characterAnim.SetInteger("Idle", 0);
        //audioManager.PlayCharacterFootstep();
    }

    public void NEMovement()
    {
        nextPos = new Vector2(currPos.x + gridX, currPos.y + gridY);
        characterAnim.SetInteger("Direction", 2);
        if (!Physics2D.OverlapCircle(nextPos, 0.1f, accessible))
        {
            nextPos = currPos;
        }


        characterAnim.Play("Walk_NE");
        characterAnim.SetInteger("Idle", 0);
        //audioManager.PlayCharacterFootstep();
    }

    /*void InitializeBlockStat()
    {
        if (pathFindingFloor == null)
        {
            return;
        }
        foreach (GameObject obj in pathFindingFloor.blockArray)
        {
            obj.GetComponent<BlockStat>().visited = -1;
        }
        pathFindingFloor.blockArray[startX, startY].GetComponent<BlockStat>().visited = 0;

        //fl.gridArray[fl.charPosX, fl.charPosY].GetComponent<BlockStat>().currentBlock = 1;
        //character.transform.position = fl.gridArray[charPosX, charPosY].transform.position;
    }

    void Run()
    {
        //run = true;
        SetSteps();
        SetPath();
        //run = false;
        foreach (GameObject obj in pathFindingFloor.blockArray)
        {
            if (path.Contains(obj) && obj != path[path.Count - 1] && path.Count - 1 <= Battery.movesTillGameover)
            {
                obj.GetComponent<BlockStat>().currentBlock = 2;
            }
            else if (path.Contains(obj) && path.Count - 1 > Battery.movesTillGameover)
            {
                obj.GetComponent<BlockStat>().currentBlock = 3;
            }
            else if (obj == pathFindingFloor.blockArray[startX, startY])
            {
                obj.GetComponent<BlockStat>().currentBlock = 1;
            }
            else
            {
                obj.GetComponent<BlockStat>().currentBlock = 0;
            }
        }

    }

    void SetSteps()
    {
        //Initialize();
        //int x = startX;
        //int y = startY;
        //int[] moveArray = new int[Moves.possibleMoves];
        for (int step = 1; step < pathFindingFloor.rows * pathFindingFloor.rows; step++)
        {
            foreach (GameObject obj in pathFindingFloor.blockArray)
            {
                if (obj.GetComponent<BlockStat>().visited == step - 1)
                    CheckDirections(obj.GetComponent<BlockStat>().x, obj.GetComponent<BlockStat>().y, step);
            }
        }
    }

    void SetPath()
    {
        int step;
        int x = endX;
        int y = endY;
        List<GameObject> temp = new List<GameObject>();
        path.Clear();
        if (pathFindingFloor.blockArray[endX, endY] && pathFindingFloor.blockArray[endX, endY].GetComponent<BlockStat>().visited > 0)
        {
            path.Add(pathFindingFloor.blockArray[x, y]);
            step = pathFindingFloor.blockArray[x, y].GetComponent<BlockStat>().visited - 1;
        }
        else
        {
            print("impossible move");
            return;
        }
        for (int i = step; step > -1; step--)
        {
            if (DirectionTest(x, y, step, 1))
                temp.Add(pathFindingFloor.blockArray[x, y + 1]);
            if (DirectionTest(x, y, step, 2))
                temp.Add(pathFindingFloor.blockArray[x, y - 1]);
            if (DirectionTest(x, y, step, 3))
                temp.Add(pathFindingFloor.blockArray[x + 1, y]);
            if (DirectionTest(x, y, step, 4))
                temp.Add(pathFindingFloor.blockArray[x - 1, y]);

            GameObject tempObj = FindClosest(pathFindingFloor.blockArray[endX, endY].transform, temp);
            path.Add(tempObj);
            x = tempObj.GetComponent<BlockStat>().x;
            y = tempObj.GetComponent<BlockStat>().y;
            temp.Clear();

        }
    }

    GameObject FindClosest(Transform destination, List<GameObject> list)
    {
        float currentDist = pathFindingFloor.rows * pathFindingFloor.rows;
        int indexNum = 0;
        for (int i = 0; i < list.Count; i++)
        {
            if (Vector2.Distance(destination.position, list[i].transform.position) < currentDist)
            {
                currentDist = Vector2.Distance(destination.position, list[i].transform.position);
                indexNum = i;
            }
        }
        return list[indexNum];
    }

    void CheckDirections(float xf, float yf, int step)
    {
        int x = (int)xf;
        int y = (int)yf;


        if (DirectionTest(x, y, -1, 1))
            SetVisited(x, y + 1, step);
        if (DirectionTest(x, y, -1, 2))
            SetVisited(x, y - 1, step);
        if (DirectionTest(x, y, -1, 3))
            SetVisited(x + 1, y, step);
        if (DirectionTest(x, y, -1, 4))
            SetVisited(x - 1, y, step);

    }

    bool DirectionTest(int x, int y, int step, int direction)
    {
        switch (direction)
        {
            case 1:
                if (y + 1 < pathFindingFloor.rows && pathFindingFloor.blockArray[x, y + 1] && pathFindingFloor.blockArray[x, y + 1].GetComponent<BlockStat>().visited == step)
                    return true;
                else
                    return false;
            case 2:
                if (y - 1 > -1 && pathFindingFloor.blockArray[x, y - 1] && pathFindingFloor.blockArray[x, y - 1].GetComponent<BlockStat>().visited == step)
                    return true;
                else
                    return false;
            case 3:
                if (x + 1 < pathFindingFloor.rows && pathFindingFloor.blockArray[x + 1, y] && pathFindingFloor.blockArray[x + 1, y].GetComponent<BlockStat>().visited == step)
                    return true;
                else
                    return false;
            case 4:
                if (x - 1 > -1 && pathFindingFloor.blockArray[x - 1, y] && pathFindingFloor.blockArray[x - 1, y].GetComponent<BlockStat>().visited == step)
                    return true;
                else
                    return false;

        }
        return false;
    }



    void SetVisited(int x, int y, int step)
    {
        if (pathFindingFloor.blockArray[x, y])
        {
            pathFindingFloor.blockArray[x, y].GetComponent<BlockStat>().visited = step;
        }
    }*/
}
