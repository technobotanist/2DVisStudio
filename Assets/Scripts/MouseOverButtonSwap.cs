using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseOverButtonSwap : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Sprite sprite;
    public Sprite highlightSprite;

    private bool mouse_over = false;
    void Update()
    {
        if (mouse_over)
        {
            GetComponent<Image>().sprite = highlightSprite;
        }
        else
        {
            GetComponent<Image>().sprite = sprite;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouse_over = true;
        Debug.Log("Mouse enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouse_over = false;
        Debug.Log("Mouse exit");
    }
}
