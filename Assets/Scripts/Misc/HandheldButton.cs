﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HandheldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    
    public bool isPressed;

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
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
