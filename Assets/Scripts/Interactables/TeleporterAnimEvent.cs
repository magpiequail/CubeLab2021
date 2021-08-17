using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterAnimEvent : MonoBehaviour
{
    Teleporter t;

    private void Awake()
    {
        t = GetComponentInParent<Teleporter>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CharacterSpriteOn()
    {
        t.CharacterSpriteOn();
    }
    public void CharacterSpriteOff()
    {
        t.CharacterSpriteOff();
    }
    public void SendCharacterToOther()
    {
        t.SendCharacterToOther();
    }
    public void PlayReceive()
    {
        t.PlayReceive();
    }

    public void AllowMovement()
    {
        CharactersMovement.isInputAllowed = true;
        //Debug.Log("AllowMovement , isInputAllowed = " + CharactersMovement.isInputAllowed);

    }
    public void DisableMovement()
    {
        CharactersMovement.isInputAllowed = false;
    }

}
