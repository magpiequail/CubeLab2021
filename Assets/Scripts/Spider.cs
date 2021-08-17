using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Character
{
    private float scale;
    private void Awake()
    {
        Initialize();
        scale = transform.localScale.x;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.forward, 100, rayLayerMask);
        if (hit && hit.collider.transform.parent.GetComponent<Floor>())
        {
            hit.collider.transform.parent.GetComponent<Floor>().charOnFloor = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateFunc();
    }

    public void Initialize()
    {
        grid = FindObjectOfType<Grid>();
        gridX = grid.cellSize.x / 2;
        gridY = grid.cellSize.y / 2;

        currPos = transform.position;
        nextPos = transform.position;
        tempNextCharPos = transform.position;
        nextCharPos = transform.position;
        cm = FindObjectOfType<CharactersMovement>();

        pf = FindObjectOfType<PathFinding>();

        characterAnim = GetComponentInChildren<Animator>();
        characterAnim.SetInteger("Idle", 1);
        characterAnim.SetInteger("StageClear", 0);
        characterAnim.SetInteger("Direction", 3);

        SetCurrentBlock();
    }

    public override bool NWMovement()
    {
        ResetBlockColor();

        if (!isUnitMoveAllowed)
        {
            return false;
        }
        characterAnim.SetInteger("Direction", 1);
        nextPos = new Vector2(currPos.x + gridX * scale, currPos.y - gridY * scale);
        RaycastHit2D hit = Physics2D.Raycast(nextPos, transform.forward, 100, rayLayerMask);
        if (hit)
        {
            tempNextCharPos = hit.collider.gameObject.GetComponent<BlockStat>().blockCharPos;

        }

        if (!Physics2D.OverlapCircle(nextPos, 0.1f, accessible))
        {
            nextPos = currPos;
            return false;
        }


        SetCurrentBlock();
        characterAnim.Play("Walk_NW");
        characterAnim.SetInteger("Idle", 0);
        return true;
    }

    public override bool NEMovement()
    {
        ResetBlockColor();

        if (!isUnitMoveAllowed)
        {
            return false;
        }
        characterAnim.SetInteger("Direction", 2);

        nextPos = new Vector2(currPos.x - gridX * scale, currPos.y - gridY * scale);
        RaycastHit2D hit = Physics2D.Raycast(nextPos, transform.forward, 100, rayLayerMask);
        if (hit)
        {
            tempNextCharPos = hit.collider.gameObject.GetComponent<BlockStat>().blockCharPos;

        }

        if (!Physics2D.OverlapCircle(nextPos, 0.1f, accessible))
        {
            nextPos = currPos;
            return false;
        }

        SetCurrentBlock();
        characterAnim.Play("Walk_NE");
        characterAnim.SetInteger("Idle", 0);
        return true;
    }

    public override bool SWMovement()
    {
        ResetBlockColor();

        if (!isUnitMoveAllowed)
        {
            return false;
        }
        characterAnim.SetInteger("Direction", 3);
        nextPos = new Vector2(currPos.x + gridX * scale, currPos.y + gridY * scale);
        RaycastHit2D hit = Physics2D.Raycast(nextPos, transform.forward, 100, rayLayerMask);
        if (hit)
        {
            tempNextCharPos = hit.collider.gameObject.GetComponent<BlockStat>().blockCharPos;

        }

        if (!Physics2D.OverlapCircle(nextPos, 0.1f, accessible))
        {
            nextPos = currPos;
            return false;
        }
        SetCurrentBlock();
        characterAnim.SetInteger("Idle", 0);
        characterAnim.Play("Walk_SW");

        return true;
    }

    public override bool SEMovement()
    {
        ResetBlockColor();

        if (!isUnitMoveAllowed)
        {
            return false;
        }
        characterAnim.SetInteger("Direction", 4);
        nextPos = new Vector2(currPos.x - gridX * scale, currPos.y + gridY * scale);
        RaycastHit2D hit = Physics2D.Raycast(nextPos, transform.forward, 100, rayLayerMask);
        if (hit)
        {
            tempNextCharPos = hit.collider.gameObject.GetComponent<BlockStat>().blockCharPos;

        }

        if (!Physics2D.OverlapCircle(nextPos, 0.1f, accessible))
        {
            nextPos = currPos;
            return false;
        }




        SetCurrentBlock();
        characterAnim.SetInteger("Idle", 0);
        characterAnim.Play("Walk_SE");

        return true;
    }

    public void Flip()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y * -1, transform.localScale.z);
        scale = scale * -1;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Character" && collision.gameObject.GetComponent<NormalCharacter>())
        {
            Debug.Log("collider name = " + collision.collider.name);
            SceneController.gameState = GameState.GameOver;
        }
    }
}
