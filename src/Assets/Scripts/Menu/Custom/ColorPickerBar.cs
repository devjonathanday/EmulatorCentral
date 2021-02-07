using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ColorPickerBar : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float position = 0;
    public bool mouseDragging;
    public float UIRange;
    public RectTransform widget;
    public Vector2 returnRange;

    public void OnPointerDown(PointerEventData eventData)
    {
        mouseDragging = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        mouseDragging = false;
    }

    private void Update()
    {
        if(mouseDragging)
        {
            position += Input.GetAxis("MouseX");
        }
        position = Mathf.Clamp(position, -UIRange, UIRange);
        widget.anchoredPosition = new Vector2(position, widget.anchoredPosition.y);
    }

    public float GetValue()
    {
        var normalized = ((position / UIRange) + 1) / 2;
        return Mathf.Lerp(returnRange.x, returnRange.y, normalized);
    }
}