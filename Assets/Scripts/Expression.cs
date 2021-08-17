using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expression : MonoBehaviour
{
    public static Animator faceAnim;

    private void Awake()
    {
        faceAnim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneController.gameState == GameState.Died)
        {
            faceAnim.Play("Shocked");
        }
        else if(SceneController.gameState == GameState.GameOver)
        {
            faceAnim.Play("Stiff");
        }
    }
}
