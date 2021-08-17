using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditBlock : MonoBehaviour
{
    public GameObject roundKey;

    // Start is called before the first frame update
    void Start()
    {
        roundKey = GameObject.Find("key_round");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
