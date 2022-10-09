using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileControls : MonoBehaviour
{
    [SerializeField] private bool mobileControls;

    [Header("UI REFERENCES")]
    [SerializeField] private HandheldButton steerLeft;
    [SerializeField] private HandheldButton steerRight;
    [SerializeField] private HandheldButton acceleration;

    public float HorizontalInput{get;set;}
    public float VerticalInput{get;set;}

    private void Awake() {
        
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            mobileControls = true;
        }
        else
        {
            mobileControls = false;
        }
    }

    private void Update()
    {
        if (mobileControls)
        {
            HorizontalInput = steerLeft.isPressed ? steerLeft.value * -1f : steerRight.isPressed? steerRight.value:0f;
            VerticalInput = acceleration.value;
        }
        else
        {
            HorizontalInput =  Input.GetAxis("Horizontal"); //turning input
            VerticalInput = Input.GetAxis("Vertical");     //accelaration input
        }
    }

}
