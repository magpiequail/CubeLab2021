using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    Character[] charactersArray;
    Grid grid;

    public Vector3 tileWorldPos;
    public Vector3Int clickedTilePos;
    TilemapColor tmc;

    AudioManager audioManager;

    Battery b;

    //from here are new variables
    public LayerMask floorLayerMask = 1 << 10;
    Floor pathFindingFloor;
    public List<GameObject> path = new List<GameObject>();
    public float delayTime;

    public int startX;
    public int startY;

    //position of ending block
    public int endX;
    public int endY;

    private void Awake()
    {
        charactersArray = FindObjectsOfType<Character>();
        b = FindObjectOfType<Battery>();
        grid = FindObjectOfType<Grid>();
        tmc = FindObjectOfType<TilemapColor>();
        //from here
        

    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (CharactersMovement.isInputAllowed && Battery.movesTillGameover > 0 /*&& Options.input == 1*/)
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 worldPoint = CameraManager.currentCam.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(worldPoint, transform.forward, 100, floorLayerMask);
                Debug.DrawLine(worldPoint,transform.forward *100,Color.red);
                if (hit)
                {
                    
                    pathFindingFloor = hit.collider.gameObject.transform.parent.GetComponent<Floor>();
                    InitializeBlockStat();

                    endX = hit.collider.gameObject.GetComponent<BlockStat>().x;
                    endY = hit.collider.gameObject.GetComponent<BlockStat>().y;

                    RaycastHit2D charHit = Physics2D.Raycast(pathFindingFloor.charOnFloor.transform.position, transform.forward, 100, floorLayerMask);
                    if (charHit)
                    {
                        startX = charHit.collider.gameObject.GetComponent<BlockStat>().x;
                        startY = charHit.collider.gameObject.GetComponent<BlockStat>().y;
                    }

                    Run();
                }


                ////////////////////////
                ///////////////////////
                //////////////////////
                
                
                /*for (int i = 0; i < charactersArray.Length; i++)
                {
                    charactersArray[i].currentCharPos = grid.WorldToCell(charactersArray[i].gameObject.transform.position);
                    //Debug.Log("character" + i + " = " + charactersArray[i].currentCharPos);
                }

                //Debug.Log("current Cam = " + CameraManager.currentCam);
                //currentCharPos = grid.WorldToCell(currentChar.transform.position);
                clickedTilePos = grid.WorldToCell(CameraManager.currentCam.ScreenToWorldPoint(Input.mousePosition));
                tileWorldPos = grid.GetCellCenterWorld(clickedTilePos);

                tmc.ColorTiles();

                if (isCheckingNW())
                {
                    for (int i = 0; i < charactersArray.Length; i++)
                    {
                        charactersArray[i].GetComponent<Character>().NWMovement();
                    }
                    b.MinusOneMove();
                    //audioManager.PlayCharacterFootstep();
                }
                if (isCheckingNE())
                {
                    for (int i = 0; i < charactersArray.Length; i++)
                    {
                        charactersArray[i].GetComponent<Character>().NEMovement();
                    }
                    b.MinusOneMove();
                    //audioManager.PlayCharacterFootstep();
                }
                if (isCheckingSW())
                {
                    for (int i = 0; i < charactersArray.Length; i++)
                    {
                        charactersArray[i].GetComponent<Character>().SWMovement();
                    }
                    b.MinusOneMove();
                    //audioManager.PlayCharacterFootstep();
                }
                if (isCheckingSE())
                {
                    for (int i = 0; i < charactersArray.Length; i++)
                    {
                        charactersArray[i].GetComponent<Character>().SEMovement();
                    }
                    b.MinusOneMove();
                    //audioManager.PlayCharacterFootstep();
                }*/

            }
            else if (Input.GetMouseButtonUp(0))
            {
                foreach (GameObject obj in pathFindingFloor.blockArray)
                {
                    obj.GetComponent<BlockStat>().currentBlock = 0;
                }
                if (path.Count - 1 <= Battery.movesTillGameover)
                {
                    StartCoroutine("Delay");
                }
                else
                {
                    Debug.Log("not enough moves");
                }
            }
        }
    }


    IEnumerator Delay()
    {
        for (int i = path.Count; i >= 2; i--)
        {
            if(pathFindingFloor.charOnFloor.currPos.x < path[i - 2].GetComponent<BlockStat>().blockCharPos.x)
            {
                //NE
                if(pathFindingFloor.charOnFloor.currPos.y < path[i - 2].GetComponent<BlockStat>().blockCharPos.y)
                {
                    foreach (Character c in charactersArray)
                    {
                        c.GetComponent<Character>().NEMovement();
                        
                        /*if (c.isCharCanMoveNE())
                        {
                            c.GetComponent<Character>().NEMovement();
                            b.MinusOneMove();
                        }*/

                    }b.MinusOneMove();
                }
                //SE
                else if(pathFindingFloor.charOnFloor.currPos.y > path[i - 2].GetComponent<BlockStat>().blockCharPos.y)
                {
                    foreach (Character c in charactersArray)
                    {
                        c.GetComponent<Character>().SEMovement();
                        
                        /*if (c.isCharCanMoveSE())
                        {
                            c.GetComponent<Character>().SEMovement();
                            b.MinusOneMove();
                        }*/

                    }b.MinusOneMove();
                }
            }
            else if(pathFindingFloor.charOnFloor.currPos.x > path[i - 2].GetComponent<BlockStat>().blockCharPos.x)
            {
                //NW
                if (pathFindingFloor.charOnFloor.currPos.y < path[i - 2].GetComponent<BlockStat>().blockCharPos.y)
                {
                    foreach (Character c in charactersArray)
                    {
                        c.GetComponent<Character>().NWMovement();
                        
                        /*if (c.isCharCanMoveNW())
                        {
                            c.GetComponent<Character>().NWMovement();
                            b.MinusOneMove();
                        }*/

                    }b.MinusOneMove();
                }
                //SW
                else if (pathFindingFloor.charOnFloor.currPos.y > path[i - 2].GetComponent<BlockStat>().blockCharPos.y)
                {
                    foreach (Character c in charactersArray)
                    {
                        c.GetComponent<Character>().SWMovement();
                        
                        /*if (c.isCharCanMoveSW())
                        {
                            c.GetComponent<Character>().SWMovement();
                            b.MinusOneMove();
                        }*/

                    }b.MinusOneMove();
                }
            }



            /*pathFindingFloor.charPosX = path[i - 2].GetComponent<BlockStat>().x;
            pathFindingFloor.charPosY = path[i - 2].GetComponent<BlockStat>().y;

            pathFindingFloor.SetAsCurrent(pathFindingFloor.charPosX, pathFindingFloor.charPosY);*/
            //character.transform.position = path[i - 2].transform.position;
                yield return new WaitForSeconds(delayTime);
        }

    }

    void InitializeBlockStat()
    {
        if(pathFindingFloor == null)
        {
            Debug.Log("floor is null");
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
            if (path.Contains(obj) && obj != path[path.Count - 1])
            {
                obj.GetComponent<BlockStat>().currentBlock = 2;
            }
            else if (obj == pathFindingFloor.blockArray[startX,startY])
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
    }


}
