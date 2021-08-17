using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPath : MonoBehaviour
{
    public bool run = true;

    //public int rows = 5;
    //public float scaleX = 0.5f;
    //public float scaleY = -0.25f;
    
    //public GameObject gridPrefab;
    //public GameObject[,] gridArray;
    //public Vector2 startingGrid = new Vector2(0, 0);
    public int startX;
    public int startY;
    
    //position of ending block
    public float endX;
    public float endY;
    public List<GameObject> path = new List<GameObject>();
    //public int charPosX = 2;
    //public int charPosY = 2;
    //public GameObject character;

    public float delayTime;

    //public Floor fl;
    Battery b;

    private void Awake()
    {
        b = FindObjectOfType<Battery>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    /*void Update()
    {

        if (Input.GetMouseButton(0))
        {
            Initialize();
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, transform.forward);
            Debug.DrawRay(worldPoint, transform.forward * 10, Color.red, 0.3f);
            if (hit)
            {
                endX = hit.collider.gameObject.GetComponent<BlockStat>().transform.position.x;
                endY = hit.collider.gameObject.GetComponent<BlockStat>().transform.position.y;


                startX = fl.charPosX;
                startY = fl.charPosY;

                Run();

            }
            else
                return;
        }
        //if mouse up, start 
        if (Input.GetMouseButtonUp(0))
        {
            foreach (GameObject obj in fl.gridArray)
            {
                obj.GetComponent<BlockStat>().currentBlock = 0;
            }
            if (path.Count-1 <= Battery.movesTillGameover)
            {
                StartCoroutine("Delay");
            }
            else
            {
                Debug.Log("not enough moves");
            }
            
        }

    }

    void UnitMovement() 
    {
        for (int i = path.Count; i >= 2; i--)
        {
            //character.transform.position = path[i - 2].transform.position;
            //Moves.possibleMoves -= 1;
            fl.charPosX = path[i - 2].GetComponent<BlockStat>().x;
            fl.charPosY = path[i - 2].GetComponent<BlockStat>().y;

        }
    }

    void Run()
    {
        run = true;
        SetSteps();
        SetPath();
        run = false;
        foreach(GameObject obj in fl.gridArray)
        {
            if (path.Contains(obj) && obj != path[path.Count-1])
            {
                obj.GetComponent<BlockStat>().currentBlock = 2;
            }
            else if(obj == fl.gridArray[fl.charPosX, fl.charPosY])
            {
                obj.GetComponent<BlockStat>().currentBlock = 1;
            }
            else
            {
                obj.GetComponent<BlockStat>().currentBlock = 0;
            }
        }

    }

    IEnumerator Delay()
    {
        for (int i = path.Count; i >= 2; i--)
        {
            
            
            fl.charPosX = path[i - 2].GetComponent<BlockStat>().x;
            fl.charPosY = path[i - 2].GetComponent<BlockStat>().y;
            fl.SetAsCurrent(fl.charPosX, fl.charPosY);
            //character.transform.position = path[i - 2].transform.position;
            yield return new WaitForSeconds(delayTime);
        }
        
    }


    void Initialize()
    {
        foreach(GameObject obj in fl.gridArray)
        {
            obj.GetComponent<BlockStat>().visited = -1;
        }
        fl.gridArray[startX, startY].GetComponent<BlockStat>().visited = 0;

        //fl.gridArray[fl.charPosX, fl.charPosY].GetComponent<BlockStat>().currentBlock = 1;
        //character.transform.position = fl.gridArray[charPosX, charPosY].transform.position;
    }
    
    bool DirectionTest(int x, int y , int step, int direction)
    {
        switch (direction)
        {
            case 1:
                if (y + 1 < fl.rows && fl.gridArray[x, y + 1] && fl.gridArray[x, y + 1].GetComponent<BlockStat>().visited == step)
                    return true;
                else
                    return false;
            case 2:
                if (y - 1 >-1 && fl.gridArray[x, y - 1] && fl.gridArray[x, y - 1].GetComponent<BlockStat>().visited == step)
                    return true;
                else
                    return false;
            case 3:
                if (x + 1 < fl.rows && fl.gridArray[x+1, y] && fl.gridArray[x+1, y].GetComponent<BlockStat>().visited == step)
                    return true;
                else
                    return false;
            case 4:
                if (x - 1 >-1 && fl.gridArray[x-1, y] && fl.gridArray[x-1, y].GetComponent<BlockStat>().visited == step)
                    return true;
                else 
                    return false;

        }
        return false;
    }
    void SetVisited (int x, int y, int step)
    {
        if (fl.gridArray[x, y])
        {
            fl.gridArray[x, y].GetComponent<BlockStat>().visited = step;
        }
    }
    void SetSteps()
    {
        Initialize();
        int x = startX;
        int y = startY;
        int[] moveArray = new int[Moves.possibleMoves];
        for(int step = 1; step< fl.rows * fl.rows; step++)
        {
            foreach(GameObject obj in fl.gridArray)
            {
                if (obj.GetComponent<BlockStat>().visited == step - 1)
                    CheckDirections(obj.GetComponent<BlockStat>().x, obj.GetComponent<BlockStat>().y, step);
            }
        }
    }

    void SetPath()
    {
        int step;
        float x = endX;
        float y = endY;
        List<GameObject> temp = new List<GameObject>();
        path.Clear();
        if(fl.gridArray[endX,endY] && fl.gridArray[endX,endY].GetComponent<BlockStat>().visited > 0)
        {
            path.Add(fl.gridArray[x, y]);
            step = fl.gridArray[x, y].GetComponent<BlockStat>().visited - 1;
        }
        else
        {
            print("impossible move");
            return;
        }
        for(int i = step; step > -1; step--)
        {
            if (DirectionTest(x, y, step, 1))
                temp.Add(fl.gridArray[x, y+1]);
            if (DirectionTest(x, y, step, 2))
                temp.Add(fl.gridArray[x, y - 1]);
            if (DirectionTest(x, y, step, 3))
                temp.Add(fl.gridArray[x+1, y]);
            if (DirectionTest(x, y, step, 4))
                temp.Add(fl.gridArray[x-1, y]);

            GameObject tempObj = FindClosest(fl.gridArray[endX, endY].transform, temp);
            path.Add(tempObj);
            x = tempObj.GetComponent<BlockStat>().x;
            y = tempObj.GetComponent<BlockStat>().y;
            temp.Clear();

        }
    }


    void CheckDirections(int x,int y, int step)
    {
        if (DirectionTest(x, y, -1, 1))
            SetVisited(x, y + 1, step);
        if (DirectionTest(x, y, -1, 2))
            SetVisited(x, y - 1, step);
        if (DirectionTest(x, y, -1, 3))
            SetVisited(x+1, y, step);
        if (DirectionTest(x, y, -1, 4))
            SetVisited(x-1, y, step);

    }
    GameObject FindClosest(Transform destination, List<GameObject> list)
    {
        float currentDist = fl.rows * fl.rows;
        int indexNum = 0;
        for(int i = 0; i < list.Count; i++)
        {
            if (Vector2.Distance(destination.position, list[i].transform.position) < currentDist)
            {
                currentDist = Vector2.Distance(destination.position, list[i].transform.position);
                indexNum = i;
            }
        }
        return list[indexNum];
    }

    //public void SEMovement()
    //{
    //    if (charPosY + 1 < fl.rows && fl.gridArray[charPosX, charPosY + 1]&&Moves.possibleMoves>0)
    //    {
    //        charPosY += 1;
    //        character.transform.position = fl.gridArray[charPosX, charPosY].transform.position;
    //        fl.SetAsCurrent(charPosX, charPosY);
    //        Moves.possibleMoves -= 1;
    //    }

    //}
    //public void NWMovement()
    //{
    //    if (charPosY - 1 > -1 && fl.gridArray[charPosX, charPosY - 1] && Moves.possibleMoves > 0)
    //    {
    //        charPosY -= 1;
    //        character.transform.position = fl.gridArray[charPosX, charPosY].transform.position;
    //        fl.SetAsCurrent(charPosX, charPosY);
    //        Moves.possibleMoves -= 1;
    //    }
    //}
    //public void SWMovement()
    //{
    //    if (charPosX + 1 < fl.rows && fl.gridArray[charPosX + 1, charPosY] && Moves.possibleMoves > 0)
    //    {
    //        charPosX += 1;
    //        character.transform.position = fl.gridArray[charPosX, charPosY].transform.position;
    //        fl.SetAsCurrent(charPosX, charPosY);
    //        Moves.possibleMoves -= 1;
    //    }
    //}
    //public void NEMovement()
    //{
    //    if (charPosX - 1 > -1 && fl.gridArray[charPosX - 1, charPosY] && Moves.possibleMoves > 0)
    //    {
    //        charPosX -= 1;
    //        character.transform.position = fl.gridArray[charPosX, charPosY].transform.position;
    //        fl.SetAsCurrent(charPosX, charPosY);
    //        Moves.possibleMoves -= 1;
    //    }
    //}
    */
}
