using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonMouseHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    Image buttonImage;
    AudioManager am;

    public float size;
    public Vector3 originalSize = new Vector3(1.0f,1.0f,1.0f);
    public bool isSoundOn;
    public bool isInitialSelection;

    private void Awake()
    {
        buttonImage = GetComponent<Image>();
        am = FindObjectOfType<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        if (isInitialSelection)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isSoundOn)
        {
            am.PlayAudio("UI_select");
        }
        EventSystem.current.SetSelectedGameObject(gameObject);
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        buttonImage.rectTransform.localScale = new Vector3(1.0f, 1.0f, 1.0f) * size;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        buttonImage.rectTransform.localScale = originalSize;
    }
}
