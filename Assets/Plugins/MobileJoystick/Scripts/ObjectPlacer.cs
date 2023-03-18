using System;
using System.Collections;
using System.Collections.Generic;
using DitzeGames.MobileJoystick;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectPlacer : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Joystick objecttoPlace;

    private Vector3 originalPosition;
    
    private void Start()
    {
        originalPosition = objecttoPlace.transform.position;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        objecttoPlace.transform.position = eventData.position;
        objecttoPlace.OnPointerDown(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        objecttoPlace.transform.position = originalPosition;
    }
}
