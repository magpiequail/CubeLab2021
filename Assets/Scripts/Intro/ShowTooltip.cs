using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    public GameObject tooltip;
    Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    // Start is called before the first frame update
    void Start()
    {
        tooltip.SetActive(false);
    }

    private void Update()
    {
        /*if(EventSystem.current.currentSelectedGameObject == button)
        {
            tooltip.SetActive(true);
        }
        else
        {
            tooltip.SetActive(false);
        }*/
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        tooltip.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        tooltip.SetActive(false);
    }
    public void OnSelect(BaseEventData eventData)
    {
        tooltip.SetActive(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        tooltip.SetActive(false);
    }

}
