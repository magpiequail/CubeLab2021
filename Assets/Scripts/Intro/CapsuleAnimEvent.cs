using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleAnimEvent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void PlaySteamSound()
    {
        FindObjectOfType<AudioManager>().PlayAudio("Lobby_incu_steam");
    }
    public void PlayOpenSound()
    {
        FindObjectOfType<AudioManager>().PlayAudio("Lobby_incu_open");
    }
}
