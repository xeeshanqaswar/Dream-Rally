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
    [SerializeField] private HandheldButton reverse;

    public float HorizontalInput{get;set;}
    public float VerticalInput{get;set;}

    private void Update()
    {
        if (mobileControls)
        {
            HorizontalInput = steerLeft.isPressed ? steerLeft.value * -1f : steerRight.isPressed? steerRight.value:0f;
            VerticalInput = acceleration.isPressed ? acceleration.value : reverse.isPressed ? reverse.value * -1: 0f;
        }
        else
        {
            HorizontalInput =  Input.GetAxis("Horizontal"); //turning input
            VerticalInput = Input.GetAxis("Vertical");     //accelaration input
        }
        
        print(VerticalInput);
    }

}
