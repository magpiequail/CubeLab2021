using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class CharactersMovement : MonoBehaviour
{
    Character[] charactersArray;
    Grid grid;

    float keyPressedTime;
    public float inputTriggerTime;
    public float inputWaitTime;

    [Space(10)]

    public Vector3 tileWorldPos;
    public Vector3Int clickedTilePos;
    //TilemapColor tmc;

    static public bool isInputAllowed = true;
    Battery b;

    AudioManager audioManager;

    //variables for pathfinding
    public LayerMask floorLayerMask = 1 << 10;
    Floor pathFindingFloor;
    public List<GameObject> path = new List<GameObject>();
    public float delayTime;
    float accumulatedTime;

    public int startX;
    public int startY;

    //position of ending block
    public int endX;
    public int endY;

    bool isPfMoveDone = true;

    private void Awake()
    {
        charactersArray = FindObjectsOfType<Character>();
        b = FindObjectOfType<Battery>();
        grid = FindObjectOfType<Grid>();
        //tmc = FindObjectOfType<TilemapColor>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Door.isAllOpen)
        {
            isInputAllowed = false;
        }
        //else if(!Door.isAllOpen && /*SceneController.gameState != GameState.GameOver && */SceneController.gameState == GameState.Running)
        //{
        //    isInputAllowed = true;
        //}
        if (SceneController.gameState == GameState.Died || SceneController.gameState == GameState.GameOver)
        {
            isInputAllowed = false;
        }

        if (isInputAllowed && Battery.movesTillGameover > 0 && Options.input == 0 && !Input.GetKey(KeyCode.Space))
        {
            //W = NE, A = NW, S = SW, D = SE
            if (Input.GetKeyDown(KeyCode.A))
            {
                keyPressedTime = 0f;
                //tilecolor is currently change by material swap
                //tmc.ColorTiles();
                if (isAllCharMovedNW())
                {
                    b.MinusOneMove();
                    audioManager.PlayCharacterFootstep();
                }
            }

            else if (Input.GetKeyDown(KeyCode.S))
            {
                keyPressedTime = 0f;
                //tmc.ColorTiles();
                if (isAllCharMovedSW())
                {
                    b.MinusOneMove();
                    audioManager.PlayCharacterFootstep();
                }
            }

            else if (Input.GetKeyDown(KeyCode.W))
            {
                keyPressedTime = 0f;
                //tmc.ColorTiles();
                if (isAllCharMovedNE())
                {
                    b.MinusOneMove();
                    audioManager.PlayCharacterFootstep();
                }
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                keyPressedTime = 0f;
                //tmc.ColorTiles();
                if (isAllCharMovedSE())
                {
                    b.MinusOneMove();
                    audioManager.PlayCharacterFootstep();
                }
            }
            //while getkey
            if (Input.GetKey(KeyCode.A))
            {
                keyPressedTime += Time.deltaTime;
                if (keyPressedTime > inputTriggerTime)
                {
                    keyPressedTime = inputTriggerTime - inputWaitTime;
                    //tmc.ColorTiles();
                    if (isAllCharMovedNW())
                    {
                        b.MinusOneMove();
                        audioManager.PlayCharacterFootstep();
                    }
                }
            }
            else if (Input.GetKey(KeyCode.S))
            {
                keyPressedTime += Time.deltaTime;
                if (keyPressedTime > inputTriggerTime)
                {
                    keyPressedTime = inputTriggerTime - inputWaitTime;
                    //tmc.ColorTiles();
                    if (isAllCharMovedSW())
                    {
                        b.MinusOneMove();
                        audioManager.PlayCharacterFootstep();
                    }
                }
            }
            else if (Input.GetKey(KeyCode.W))
            {
                keyPressedTime += Time.deltaTime;
                if (keyPressedTime > inputTriggerTime)
                {
                    keyPressedTime = inputTriggerTime - inputWaitTime;
                    //tmc.ColorTiles();
                    if (isAllCharMovedNE())
                    {
                        b.MinusOneMove();
                        audioManager.PlayCharacterFootstep();
                    }
                }
            }
            else if (Input.GetKey(KeyCode.D))
            {
                keyPressedTime += Time.deltaTime;
                if (keyPressedTime > inputTriggerTime)
                {
                    keyPressedTime = inputTriggerTime - inputWaitTime;
                    //tmc.ColorTiles();
                    if (isAllCharMovedSE())
                    {
                        b.MinusOneMove();
                        audioManager.PlayCharacterFootstep();
                    }
                }
            }
        }
        //mouse
        if (isInputAllowed && Battery.movesTillGameover > 0 && Options.input == 1 && isPfMoveDone)
        {
            if (Input.GetMouseButton(0))
            {

                Vector3 worldPoint = CameraManager.currentCam.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(worldPoint, transform.forward, 100, floorLayerMask);
                Debug.DrawLine(worldPoint, transform.forward * 100, Color.red);
                if (hit && hit.collider.GetComponent<BlockStat>())
                {

                    pathFindingFloor = hit.collider.gameObject.transform.parent.GetComponent<Floor>();


                    endX = hit.collider.gameObject.GetComponent<BlockStat>().x;
                    endY = hit.collider.gameObject.GetComponent<BlockStat>().y;

                    if (pathFindingFloor.charOnFloor && pathFindingFloor.charOnFloor.gameObject.GetComponent<NormalCharacter>())
                    {
                        RaycastHit2D charHit = Physics2D.Raycast(pathFindingFloor.charOnFloor.transform.position, transform.forward, 100, floorLayerMask);
                        if (charHit)
                        {
                            startX = charHit.collider.gameObject.GetComponent<BlockStat>().x;
                            startY = charHit.collider.gameObject.GetComponent<BlockStat>().y;
                        }
                        InitializeBlockStat();
                        Run();
                    }


                }
                else
                {

                    if (pathFindingFloor == null)
                    {
                        return;
                    }
                    else
                    {
                        pathFindingFloor = null;
                    }
                    //endX = startX;
                    //endY = startY;
                    //Run();
                    //pathFindingFloor.blockArray[startX, startY].GetComponent<BlockStat>().currentBlock = 1;
                }



            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (pathFindingFloor)
                {
                    foreach (GameObject obj in pathFindingFloor.blockArray)
                    {
                        obj.GetComponent<BlockStat>().currentBlock = 0;
                    }

                    if (startX == endX && startY == endY)
                    {
                        return;
                    }
                    if (pathFindingFloor.charOnFloor.gameObject.GetComponent<Spider>())
                    {
                        return;
                    }
                    if (path.Count - 1 <= Battery.movesTillGameover)
                    {

                        StartCoroutine("Delay");
                        if (SceneController.gameState == GameState.Died || SceneController.gameState == GameState.GameOver)
                        {
                            return;
                        }
                    }

                    else
                    {
                        isPfMoveDone = true;
                    }

                }
                else
                {

                }

            }


            #region this is legacy mouse movement
            /*if (Input.GetMouseButtonDown(0))
            {
                
                for( int i= 0;i < charactersArray.Length; i++)
                {
                    charactersArray[i].currentCharPos = grid.WorldToCell(charactersArray[i].gameObject.transform.position);
                    Debug.Log("character" + i + " = " + charactersArray[i].currentCharPos);
                }

                Debug.Log("current Cam = " + CameraManager.currentCam);
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
                    audioManager.PlayCharacterFootstep();
                }
                if (isCheckingNE())
                {
                    for (int i = 0; i < charactersArray.Length; i++)
                    {
                        charactersArray[i].GetComponent<Character>().NEMovement();
                    }
                    b.MinusOneMove();
                    audioManager.PlayCharacterFootstep();
                }
                if (isCheckingSW())
                {
                    for (int i = 0; i < charactersArray.Length; i++)
                    {
                        charactersArray[i].GetComponent<Character>().SWMovement();
                    }
                    b.MinusOneMove();
                    audioManager.PlayCharacterFootstep();
                }
                if (isCheckingSE())
                {
                    for (int i = 0; i < charactersArray.Length; i++)
                    {
                        charactersArray[i].GetComponent<Character>().SEMovement();
                    }
                    b.MinusOneMove();
                    audioManager.PlayCharacterFootstep();
                }

            }
            else if (Input.GetMouseButtonUp(0))
            {

            }*/
            #endregion
        }
    }

    #region isAllCharMoved
    bool isAllCharMovedSW()
    {
        foreach (Character c in charactersArray)
        {
            c.characterAnim.SetInteger("Direction", 3);
        }
        if (isCheckingSW())
        {
            foreach (Character c in charactersArray)
            {
                c.nextCharPos = c.tempNextCharPos;
            }
            return true;
        }
        else
        {
            foreach (Character c in charactersArray)
            {
                c.nextPos = c.currPos;
                c.tempNextCharPos = c.currPos;
            }
            return false;
        }

    }
    bool isAllCharMovedSE()
    {
        foreach (Character c in charactersArray)
        {
            c.characterAnim.SetInteger("Direction", 4);

        }
        if (isCheckingSE())
        {
            foreach (Character c in charactersArray)
            {
                c.nextCharPos = c.tempNextCharPos;
            }
            return true;
        }
        else
        {
            foreach (Character c in charactersArray)
            {
                c.nextPos = c.currPos;
                c.tempNextCharPos = c.currPos;
            }
            return false;
        }
    }
    bool isAllCharMovedNW()
    {
        foreach (Character c in charactersArray)
        {
            c.characterAnim.SetInteger("Direction", 1);
        }
        if (isCheckingNW())
        {
            foreach (Character c in charactersArray)
            {
                c.nextCharPos = c.tempNextCharPos;
            }
            return true;
        }
        else
        {
            foreach (Character c in charactersArray)
            {
                c.nextPos = c.currPos;
                c.tempNextCharPos = c.currPos;
            }
            return false;
        }
    }
    bool isAllCharMovedNE()
    {
        foreach (Character c in charactersArray)
        {
            c.characterAnim.SetInteger("Direction", 2);
        }
        if (isCheckingNE())
        {
            foreach (Character c in charactersArray)
            {
                c.nextCharPos = c.tempNextCharPos;
            }
            return true;
        }
        else
        {
            foreach (Character c in charactersArray)
            {
                c.nextPos = c.currPos;
                c.tempNextCharPos = c.currPos;
            }
            return false;
        }
    }

    #endregion

    //for mouse click movement



    #region functions used for pathfinding

    IEnumerator Delay()
    {
        isPfMoveDone = false;

        for (int i = path.Count; i >= 2; i--)
        {
            accumulatedTime += Time.deltaTime;
            if (SceneController.gameState == GameState.Died || SceneController.gameState == GameState.GameOver)
            {
                yield break;
            }
            if (pathFindingFloor.charOnFloor.currPos.x < path[i - 2].GetComponent<BlockStat>().blockCharPos.x)
            {
                //NE
                if (pathFindingFloor.charOnFloor.currPos.y < path[i - 2].GetComponent<BlockStat>().blockCharPos.y)
                {
                    /*foreach (Character c in charactersArray)
                    {
                        c.GetComponent<Character>().NEMovement();

                        /*if (c.isCharCanMoveNE())
                        {
                            c.GetComponent<Character>().NEMovement();
                            b.MinusOneMove();
                        }
                        

                    }
                    b.MinusOneMove();*/
                    if (isAllCharMovedNE())
                    {
                        b.MinusOneMove();
                        audioManager.PlayCharacterFootstep();
                    }
                    if (SceneController.gameState == GameState.Died || SceneController.gameState == GameState.GameOver)
                    {
                        yield break;
                    }
                }
                //SE
                else if (pathFindingFloor.charOnFloor.currPos.y > path[i - 2].GetComponent<BlockStat>().blockCharPos.y)
                {
                    /*foreach (Character c in charactersArray)
                    {
                        c.GetComponent<Character>().SEMovement();

                        /*if (c.isCharCanMoveSE())
                        {
                            c.GetComponent<Character>().SEMovement();
                            b.MinusOneMove();
                        }
                        

                    }
                    b.MinusOneMove();*/
                    if (isAllCharMovedSE())
                    {
                        b.MinusOneMove();
                        audioManager.PlayCharacterFootstep();
                    }
                    if (SceneController.gameState == GameState.Died || SceneController.gameState == GameState.GameOver)
                    {
                        yield break;
                    }
                }
            }
            else if (pathFindingFloor.charOnFloor.currPos.x > path[i - 2].GetComponent<BlockStat>().blockCharPos.x)
            {
                //NW
                if (pathFindingFloor.charOnFloor.currPos.y < path[i - 2].GetComponent<BlockStat>().blockCharPos.y)
                {
                    /*foreach (Character c in charactersArray)
                    {
                        c.GetComponent<Character>().NWMovement();

                        /*if (c.isCharCanMoveNW())
                        {
                            c.GetComponent<Character>().NWMovement();
                            b.MinusOneMove();
                        }
                        

                    }
                    b.MinusOneMove();*/
                    if (isAllCharMovedNW())
                    {
                        b.MinusOneMove();
                        audioManager.PlayCharacterFootstep();
                    }
                    if (SceneController.gameState == GameState.Died || SceneController.gameState == GameState.GameOver)
                    {
                        yield break;
                    }
                }
                //SW
                else if (pathFindingFloor.charOnFloor.currPos.y > path[i - 2].GetComponent<BlockStat>().blockCharPos.y)
                {
                    /*foreach (Character c in charactersArray)
                    {
                        c.GetComponent<Character>().SWMovement();

                        /*if (c.isCharCanMoveSW())
                        {
                            c.GetComponent<Character>().SWMovement();
                            b.MinusOneMove();
                        }
                        

                    }
                    b.MinusOneMove();*/
                    if (isAllCharMovedSW())
                    {
                        b.MinusOneMove();
                        audioManager.PlayCharacterFootstep();
                    }
                    if (SceneController.gameState == GameState.Died || SceneController.gameState == GameState.GameOver)
                    {
                        yield break;
                    }
                }
            }
            /*if (path[i - 2].GetComponent<BlockStat>().blockCharPos.x == endX && path[i - 2].GetComponent<BlockStat>().blockCharPos.y == endY)
            {
                isPfMoveDone = true;
            }*/

            if (i == 2)
            {
                isPfMoveDone = true;
            }

            yield return new WaitForSeconds(delayTime);
        }

    }
    void InitializeBlockStat()
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
    }
    #endregion


    #region /*these functions are legacy mouse movement*/
    bool isCheckingNW()
    {
        foreach (Character c in charactersArray)
        {
            //if one of the characters can't move
            if (!c.NWMovement())
            {
                return false;
            }

        }
        return true;
    }
    bool isCheckingNE()
    {
        foreach (Character c in charactersArray)
        {
            if (!c.NEMovement())
            {
                return false;
            }

        }
        return true;
    }
    bool isCheckingSW()
    {
        foreach (Character c in charactersArray)
        {
            if (!c.SWMovement())
            {
                return false;
            }

        }
        return true;
    }
    bool isCheckingSE()
    {
        foreach (Character c in charactersArray)
        {
            if (!c.SEMovement())
            {
                return false;
            }

        }
        return true;
    }
    #endregion
}
