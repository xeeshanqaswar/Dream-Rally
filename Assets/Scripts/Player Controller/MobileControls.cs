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
    [SerializeField] private HandheldButton back;

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
            VerticalInput = acceleration.value != 0 ? acceleration.value : back.value != 0 ? back.value * -1f : 0f;
        }
        else
        {
            HorizontalInput =  Input.GetAxis("Horizontal"); //turning input
            VerticalInput = Input.GetAxis("Vertical");     //accelaration input
        }
    }

}
