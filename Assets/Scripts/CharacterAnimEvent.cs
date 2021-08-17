using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimEvent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallGameOver()
    {
        SceneController.gameState = GameState.GameOver;
    }
    public void AllowMovement()
    {
        CharactersMovement.isInputAllowed = true;
    }
    public void DisableMovement()
    {
        CharactersMovement.isInputAllowed = false;
    }
    public void PlayAudio()
    {
        FindObjectOfType<AudioManager>().PlayAudio("Lobby_incu_ambient");
    }
}
