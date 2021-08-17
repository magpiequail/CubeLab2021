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


        return true;
    }
    public virtual bool SEMovement()
    {

        return true;
    }
    public virtual bool NWMovement()
    {

        return true;
    }

    public virtual bool NEMovement()
    {

        return true;
    }


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
