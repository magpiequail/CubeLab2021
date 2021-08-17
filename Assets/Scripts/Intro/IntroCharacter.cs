using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroCharacter : MonoBehaviour
{
    public Animator characterAnim;
    //bool isUnitMoveAllowed = true;
    public static bool isInputAllowed = true;


    // public GameObject floor;


    public float speed;


    public Vector2 nextPos;
    Grid grid;
    float gridX;
    float gridY;
    public Vector2 currPos;
    public Vector3Int currentCharPos;
    Vector3Int clickedTilePos;
    public Vector3 tileWorldPos;

    public LayerMask accessible;
    IntroText it;
    Camera mainCam;

    AudioManager audioManager;

    private void Awake()
    {
        grid = FindObjectOfType<Grid>();
        gridX = grid.cellSize.x / 2;
        gridY = grid.cellSize.y / 2;

        currPos = transform.position;
        nextPos = transform.position;

        it = FindObjectOfType<IntroText>();

        mainCam = Camera.main;

        audioManager = FindObjectOfType<AudioManager>();

    }

    // Start is called before the first frame update
    void Start()
    {
        characterAnim = GetComponentInChildren<Animator>();
        characterAnim.SetInteger("Idle", 1);
        //isInputAllowed = true;

        Debug.Log(Options.input);
    }


    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) && IntroText.state == 2 && Time.timeScale !=0f )
        {
            characterAnim.SetTrigger("GetUp");

            characterAnim.SetInteger("Idle", 1);
        }
        /*if(it.index > it.sentences.Length - 2)
        {

        }*/
        if (isInputAllowed && Time.timeScale != 0f)
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
                Debug.Log("keyboard");
                if (Input.GetKeyDown(KeyCode.S))
                {
                    SWMovement();
                }

                else if (Input.GetKeyDown(KeyCode.D))
                {
                    SEMovement();
                }

                else if (Input.GetKeyDown(KeyCode.A))
                {
                    NWMovement();
                }
                else if (Input.GetKeyDown(KeyCode.W))
                {
                    NEMovement();
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
                    }
                    if (clickedTilePos.x == currentCharPos.x+1 && clickedTilePos.y == currentCharPos.y && Physics2D.OverlapCircle(tileWorldPos, 0.01f, accessible))
                    {
                        NEMovement();
                    }
                    if (clickedTilePos.x == currentCharPos.x - 1 && clickedTilePos.y == currentCharPos.y && Physics2D.OverlapCircle(tileWorldPos, 0.01f, accessible))
                    {
                        SWMovement();
                    }
                    if (clickedTilePos.x == currentCharPos.x && clickedTilePos.y == currentCharPos.y - 1 && Physics2D.OverlapCircle(tileWorldPos, 0.01f, accessible))
                    {
                        SEMovement();

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
        audioManager.PlayCharacterFootstep();
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
        audioManager.PlayCharacterFootstep();
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
        audioManager.PlayCharacterFootstep();
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
        audioManager.PlayCharacterFootstep();
    }

    public void TriggerGetUp()
    {
        characterAnim.SetTrigger("GetUp");
        characterAnim.SetInteger("Idle", 1);
    }
    public void AllowMovement()
    {
        isInputAllowed = true;
    }
}
