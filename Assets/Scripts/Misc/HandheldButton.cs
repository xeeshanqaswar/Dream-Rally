using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HandheldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isPressed;
    public float value;

    private float smoothTime = 3f;

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
    }
    
    private void Update() 
    {
        if (isPressed)
        {
            value = Mathf.MoveTowards(value,1, smoothTime * Time.deltaTime);
        }
        else
        {
            value = Mathf.MoveTowards(value,0, smoothTime *  Time.deltaTime);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
    }

    private void OnEnable() 
    {
        isPressed = false;
    }


}
