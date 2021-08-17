using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLight : MonoBehaviour
{
    public Door matchingDoor;
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (matchingDoor.isOpened == true)
        {
            anim.Play("Light_Blue");
        }
        if(matchingDoor.isOpened == false)
        {
            anim.Play("Light_Red");
        }
    }
}
