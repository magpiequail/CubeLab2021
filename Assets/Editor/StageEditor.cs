using UnityEngine;
using UnityEditor;

public class StageEditor : EditorWindow
{
    Interactables[] interactableObjArray;
    Door[] doorArray;
    LayerMask floorLayerMask = 1 << 10;
    Vector3 doorColliderOffset = new Vector3(-0.25f, -0.65f, 0f);
    GameObject grid;
    Laser[] laserArray;


    [MenuItem("Custom Editor/Stage Editor")]
    public static void ShowWindow()
    {
        GetWindow<StageEditor>();
    }

    /*private void Awake()
    {
        interactableObjArray = FindObjectsOfType<Interactables>();
        grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<Grid>();
        //
    }*/
    private void OnEnable()
    {
        interactableObjArray = FindObjectsOfType<Interactables>();
        grid = GameObject.FindGameObjectWithTag("Grid");
        doorArray = FindObjectsOfType<Door>();
        laserArray = FindObjectsOfType<Laser>();
            
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Initialize")) 
        {
            interactableObjArray = FindObjectsOfType<Interactables>();
            grid = GameObject.FindGameObjectWithTag("Grid");
            doorArray = FindObjectsOfType<Door>();
            laserArray = FindObjectsOfType<Laser>();
            PlayerPrefs.DeleteAll();
        }


        if(GUILayout.Button("Set Parent As Build Mode"))
        {
            foreach (Interactables inter in interactableObjArray)
            {
                RaycastHit2D hit = Physics2D.Raycast(inter.transform.position, inter.transform.forward, 100, floorLayerMask);
                if (hit && inter.GetComponent<Collider2D>() && inter)
                {

                    inter.gameObject.transform.SetParent(hit.collider.gameObject.transform.parent);
                }

            }
            //this is needed because the door objects have collider and sprite renderer in the same object
            foreach(Door door in doorArray)
            {
                RaycastHit2D hit = Physics2D.Raycast(door.transform.position+doorColliderOffset, door.transform.position + doorColliderOffset+new Vector3(0,0,10), 100, floorLayerMask);
                if (hit && door.GetComponent<Collider2D>())
                {
                    door.gameObject.transform.SetParent(hit.collider.gameObject.transform.parent);
                }
            }
            foreach (Laser laser in laserArray)
            {
                RaycastHit2D hit = Physics2D.Raycast(laser.transform.position, laser.transform.forward,100, floorLayerMask);
                if (hit && laser.GetComponent<Collider2D>())
                {
                    laser.gameObject.transform.SetParent(hit.collider.gameObject.transform.parent);
                }
            }
        }
        if (GUILayout.Button("Set Parent As Edit Mode"))
        {
            foreach (Interactables inter in interactableObjArray)
            {
                if (inter.GetComponent<Collider2D>())
                {
                    inter.gameObject.transform.SetParent(grid.transform);
                }
                
            }
            foreach (Door door in doorArray)
            {
                if (door.GetComponent<Collider2D>())
                {
                    door.gameObject.transform.SetParent(grid.transform);
                }
                
            }
            foreach (Laser laser in laserArray)
            {
                if (laser.GetComponent<Collider2D>())
                {
                    laser.gameObject.transform.SetParent(grid.transform);
                }
            }
        }


    }

}
