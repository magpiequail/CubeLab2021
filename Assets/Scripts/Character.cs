using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum CharKeyState
{
    Empty,
    RoundKey,
    TriangleKey,
    SquareKey,
    DiamondKey
}

public class Character : MonoBehaviour
{
    public CharKeyState characterKey;
    public Animator characterAnim;
    public bool isUnitMoveAllowed = true;
    //bool isInputAllowed = true;
    public bool isDeathByLaser = false;
    /*public bool isHavingRoundKey = false;
    public bool isHavingTriangleKey = false;
    public bool isHavingDiamondKey = false;
    public bool isHavingSquareKey = false;*/
    // public GameObject floor;
    protected float keyPressedTime = 0f;

    public int rows = 5;

    public float speed;
    public float inputTriggerTime;
    public float inputWaitTime;

    //public Floor fl;

    //public int charPosX = 2;
    //public int charPosY = 2;
    public LayerMask accessible;

    //[HideInInspector]
    public Vector2 nextPos;
    public Vector2 tempNextCharPos;
    public Vector2 nextCharPos;
    protected Grid grid;
    protected float gridX;
    protected float gridY;
    public Vector2 currPos;
    public Vector3Int currentCharPos;
    protected Vector3Int clickedTilePos;
    //layermask is accessible floor
    protected int rayLayerMask = 1 << 10;

    protected CharactersMovement cm;

    protected PathFinding pf;


    private void Awake()
    {
        grid = FindObjectOfType<Grid>();
        gridX = grid.cellSize.x / 2;
        gridY = grid.cellSize.y / 2;

        currPos = transform.position;
        nextPos = transform.position;
        cm = FindObjectOfType<CharactersMovement>();

        pf = FindObjectOfType<PathFinding>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //fl = floor.GetComponent<Floor>();

        characterAnim = GetComponentInChildren<Animator>();
        characterAnim.SetInteger("Idle", 1);
        characterAnim.SetInteger("StageClear", 0);
        characterAnim.SetInteger("Direction", 3);

        SetCurrentBlock();
        //characterAnim.Play("Idle_SW");

        //transform.position = fl.gridArray[fl.charPosX, fl.charPosY].transform.position;

        //fl.gridArray[fl.charPosX, fl.charPosY].GetComponent<BlockStat>().currentBlock = 1;
    }


    // Update is called once per frame
    void Update()
    {
        if (Door.isAllOpen)
        {
            //characterAnim.Play("Idle_NE");
            characterAnim.SetInteger("Direction", 2);
            //characterAnim.Play("StageClear");
            characterAnim.SetInteger("StageClear", 1);
        }
        else
        {

        }
        if (SceneController.gameState == GameState.GameOver && isDeathByLaser == false)
        {
            characterAnim.Play("GameOver");
        }

        //Debug.DrawLine(currPos, transform.forward,Color.red);


        //transform.position = Vector3.MoveTowards(transform.position, fl.gridArray[fl.charPosX, fl.charPosY].transform.position, speed * Time.deltaTime);

        //Vector3 dist = transform.position - fl.gridArray[fl.charPosX, fl.charPosY].transform.position;
        //if (dist.sqrMagnitude > 0f) //move
        //{
        //    characterAnim.SetInteger("Idle", 0);
        //    //isUnitMoveAllowed = false;

        //}
        //else //idle
        //{
        //    characterAnim.SetInteger("Idle", 1);
        //    //isUnitMoveAllowed = true;
        //}


        ////for keyboard inputs

        //if (isInputAllowed)
        //{
        //    if (Input.GetKey(KeyCode.A))
        //    {
        //        keyPressedTime += Time.deltaTime;
        //        if (keyPressedTime > inputTriggerTime)
        //        {
        //            keyPressedTime = inputTriggerTime - inputWaitTime;
        //            fl.SWMovement();
        //            //characterAnim.SetInteger("Idle", 0);
        //        }
        //    }
        //    if (Input.GetKey(KeyCode.S))
        //    {
        //        keyPressedTime += Time.deltaTime;
        //        if (keyPressedTime > inputTriggerTime)
        //        {
        //            keyPressedTime = inputTriggerTime - inputWaitTime;
        //            fl.SEMovement();
        //            //characterAnim.SetInteger("Idle", 0);
        //        }
        //    }
        //    if (Input.GetKey(KeyCode.Q))
        //    {
        //        keyPressedTime += Time.deltaTime;
        //        if (keyPressedTime > inputTriggerTime)
        //        {
        //            keyPressedTime = inputTriggerTime - inputWaitTime;
        //            fl.NWMovement();
        //            //characterAnim.SetInteger("Idle", 0);
        //        }
        //    }
        //    if (Input.GetKey(KeyCode.W))
        //    {
        //        keyPressedTime += Time.deltaTime;
        //        if (keyPressedTime > inputTriggerTime)
        //        {
        //            keyPressedTime = inputTriggerTime - inputWaitTime;
        //            fl.NEMovement();
        //            //characterAnim.SetInteger("Idle", 0);
        //        }
        //    }
        //    if (isUnitMoveAllowed)
        //    {


        //        if (Input.GetKeyDown(KeyCode.A))
        //        {
        //            fl.SWMovement();
        //            //characterAnim.Play("Idle_SW");
        //            //characterAnim.SetTrigger("Jump");
        //            characterAnim.SetInteger("Direction", 3);
        //            characterAnim.Play("Walk");
        //            keyPressedTime = 0f;
        //        }

        //        if (Input.GetKeyDown(KeyCode.S))
        //        {
        //            fl.SEMovement();
        //            //characterAnim.Play("Idle_SE");
        //            //characterAnim.SetTrigger("Jump");
        //            characterAnim.SetInteger("Direction", 4);
        //            characterAnim.Play("Walk_SE");
        //            keyPressedTime = 0f;
        //        }

        //        if (Input.GetKeyDown(KeyCode.Q))
        //        {
        //            fl.NWMovement();
        //            //characterAnim.Play("Idle_NW");
        //            //characterAnim.SetTrigger("Jump");
        //            characterAnim.SetInteger("Direction", 1);
        //            characterAnim.Play("Walk_NW");
        //            keyPressedTime = 0f;
        //        }
        //        if (Input.GetKeyDown(KeyCode.W))
        //        {
        //            fl.NEMovement();
        //            //characterAnim.Play("Idle_NE");
        //            //characterAnim.SetTrigger("Jump");
        //            characterAnim.SetInteger("Direction", 2);
        //            characterAnim.Play("Walk_NE");
        //            keyPressedTime = 0f;
        //        }
        //        if (Input.GetKeyUp(KeyCode.A)
        //            || Input.GetKeyUp(KeyCode.S)
        //            || Input.GetKeyUp(KeyCode.Q)
        //            || Input.GetKeyUp(KeyCode.W))
        //        {
        //            keyPressedTime = 0f;
        //        }
        //    }
        //}


        if (Physics2D.OverlapCircle(nextPos, 0.1f, accessible))
        {
            transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
        }
        if (Vector3.Distance(transform.position, nextPos) < 0.01f)
        {
            isUnitMoveAllowed = true;
            currPos = nextPos;
            characterAnim.SetInteger("Idle", 1);
        }
        else
        {
            isUnitMoveAllowed = false;
        }

    }

    /*public virtual void Initialize()
    {
        grid = FindObjectOfType<Grid>();
        gridX = grid.cellSize.x / 2;
        gridY = grid.cellSize.y / 2;

        currPos = transform.position;
        nextPos = transform.position;
        cm = FindObjectOfType<CharactersMovement>();

        characterAnim = GetComponentInChildren<Animator>();
        characterAnim.SetInteger("Idle", 1);
        characterAnim.SetInteger("StageClear", 0);
        characterAnim.SetInteger("Direction", 3);

        SetCurrentBlock();
    }*/

    public virtual void UpdateFunc()
    {
        if (Door.isAllOpen)
        {
            //characterAnim.Play("Idle_NE");
            characterAnim.SetInteger("Direction", 2);
            //characterAnim.Play("StageClear");
            characterAnim.SetInteger("StageClear", 1);
        }
        else
        {

        }
        if (SceneController.gameState == GameState.GameOver && isDeathByLaser == false)
        {
            characterAnim.Play("GameOver");
        }


        if (Physics2D.OverlapCircle(nextPos, 0.1f, accessible))
        {
            //move to nextCharPos
            transform.position = Vector3.MoveTowards(transform.position, nextCharPos, speed * Time.deltaTime);
        }
        if (Vector3.Distance(transform.position, nextCharPos) < 0.01f)
        {
            isUnitMoveAllowed = true;
            currPos = nextPos;
            characterAnim.SetInteger("Idle", 1);
        }
        else
        {
            isUnitMoveAllowed = false;
        }
    }

    public virtual bool SWMovement()
    {
        /*if (!isUnitMoveAllowed)
        {
            return false;
        }
        ResetBlockColor();
        nextPos = new Vector2(currPos.x - gridX, currPos.y - gridY);
        characterAnim.SetInteger("Direction", 3);
        if (!Physics2D.OverlapCircle(nextPos, 0.1f, accessible))
        {
            nextPos = currPos;
            return false;
        }

        SetCurrentBlock();
        characterAnim.SetInteger("Idle", 0);
        characterAnim.Play("Walk_SW");*/

        return true;
    }
    public virtual bool SEMovement()
    {
        /*if (!isUnitMoveAllowed)
        {
            return false;
        }
        ResetBlockColor();
        nextPos = new Vector2(currPos.x + gridX, currPos.y - gridY);
        characterAnim.SetInteger("Direction", 4);

        if (!Physics2D.OverlapCircle(nextPos, 0.1f, accessible))
        {
            nextPos = currPos;
            return false;
        }

        SetCurrentBlock();
        characterAnim.SetInteger("Idle", 0);
        characterAnim.Play("Walk_SE");*/

        return true;
    }
    public virtual bool NWMovement()
    {
        /*if (!isUnitMoveAllowed)
        {
            return false;
        }
        ResetBlockColor();
        nextPos = new Vector2(currPos.x - gridX, currPos.y + gridY);
        characterAnim.SetInteger("Direction", 1);
        if (!Physics2D.OverlapCircle(nextPos, 0.1f, accessible) )
        {
            nextPos = currPos;
            return false;
        }

        SetCurrentBlock();
        characterAnim.Play("Walk_NW");
        characterAnim.SetInteger("Idle", 0);*/
        return true;
    }

    public virtual bool NEMovement()
    {
        /*if (!isUnitMoveAllowed)
        {
            return false;
        }
        ResetBlockColor();
        nextPos = new Vector2(currPos.x + gridX, currPos.y + gridY);
        characterAnim.SetInteger("Direction", 2);
        if (!Physics2D.OverlapCircle(nextPos, 0.1f, accessible))
        {
            nextPos = currPos;
            return false;
        }

        SetCurrentBlock();
        characterAnim.Play("Walk_NE");
        characterAnim.SetInteger("Idle", 0);*/
        return true;
    }

    //this function is not being used
    /*void GetTile()
    {
        tmc.tilemap.RefreshAllTiles();
        
        Vector3 worldPoint = CameraManager.currentCam.ScreenToWorldPoint(nextPos);
        RaycastHit2D hit = Physics2D.Raycast(worldPoint, transform.forward);
        Debug.DrawRay(worldPoint, transform.forward * 10, Color.red, 0.3f);
        if (hit)
        {
        tmc.x = tmc.tilemap.WorldToCell(nextPos).x;
        tmc.y = tmc.tilemap.WorldToCell(nextPos).y;

        }
    }*/


    //these functions are used for mouse click movement

    #region functions currently not used
    public virtual bool isCharCanMoveNW()
    {
        if (cm.clickedTilePos.x == currentCharPos.x && cm.clickedTilePos.y == currentCharPos.y + 1 && Physics2D.OverlapCircle(cm.tileWorldPos, 0.01f, accessible))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public virtual bool isCharCanMoveNE()
    {
        if (cm.clickedTilePos.x == currentCharPos.x + 1 && cm.clickedTilePos.y == currentCharPos.y && Physics2D.OverlapCircle(cm.tileWorldPos, 0.01f, accessible))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public virtual bool isCharCanMoveSW()
    {
        if (cm.clickedTilePos.x == currentCharPos.x - 1 && cm.clickedTilePos.y == currentCharPos.y && Physics2D.OverlapCircle(cm.tileWorldPos, 0.01f, accessible))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public virtual bool isCharCanMoveSE()
    {
        if (cm.clickedTilePos.x == currentCharPos.x && cm.clickedTilePos.y == currentCharPos.y - 1 && Physics2D.OverlapCircle(cm.tileWorldPos, 0.01f, accessible))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    #endregion


    public void ResetBlockColor()
    {
        RaycastHit2D hit = Physics2D.Raycast(currPos, transform.forward, 100, rayLayerMask);

        if (hit)
        {
            hit.collider.gameObject.GetComponent<BlockStat>().currentBlock = 0;
        }
    }
    public void SetCurrentBlock()
    {
        RaycastHit2D hit = Physics2D.Raycast(nextPos, transform.forward, 100, rayLayerMask);

        if (hit)
        {
            hit.collider.gameObject.GetComponent<BlockStat>().currentBlock = 1;
        }
    }

    //when teleporting, change the floor which the character is standing on
    public void ResetFloor()
    {
        ResetBlockColor();


    }

}
