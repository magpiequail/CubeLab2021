using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Dialog : MonoBehaviour
{

    public TextMeshPro TextDisplay;
    public string[] sentences;
    private int index;
    public float typingSpeed;

    void Start()
    {

        StartCoroutine(Type());
    }

    IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            TextDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);

        }
    }
}