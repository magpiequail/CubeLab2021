using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Bgm : MonoBehaviour
{

    public static AudioSource a;

    private void Awake()
    {
        a = gameObject.GetComponent<AudioSource>();
        /*if (a == null)
        {
            a = gameObject.GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);*/

    }

    // Start is called before the first frame update
    void Start()
    {
        a.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
