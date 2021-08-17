using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorAnimEvent : MonoBehaviour
{
    Elevator e;

    private void Awake()
    {
        e = GetComponentInParent<Elevator>();
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
        e.CharacterSpriteOn();
    }
    public void CharacterSpriteOff()
    {
        e.CharacterSpriteOff();
    }
    public void SendCharacterToOther()
    {
        e.SendCharacterToOther();
    }
    public void PlayOpenReceive()
    {
        e.PlayOpenReceive();
    }
    public void PlayCloseReceive()
    {
        e.PlayCloseReceive();
    }
    public void CloseElevator()
    {
        e.CloseElevator();
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
