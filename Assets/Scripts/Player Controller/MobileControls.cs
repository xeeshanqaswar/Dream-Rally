using System;
using System.Collections;
using System.Collections.Generic;
using DitzeGames.MobileJoystick;
using UnityEngine;

public class MobileControls : MonoBehaviour
{
    [SerializeField] private bool mobileControls;

    [SerializeField] private Joystick steerJoystick;
    [SerializeField] private Joystick motionJoystick;

    private Rigidbody m_Rigidbody;
    
    public float HorizontalInput{get;set;}
    public float VerticalInput{get;set;}

    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (mobileControls)
        {
            // HorizontalInput = steerLeft.isPressed ? steerLeft.value * -1f : steerRight.isPressed? steerRight.value:0f;
            // VerticalInput = acceleration.isPressed ? acceleration.value : reverse.isPressed ? reverse.value * -1: 0f;
            
            HorizontalInput = steerJoystick.InputVector.x > 0.25f ? 1 : steerJoystick.InputVector.x < -0.25f ? -1: 0f;
            VerticalInput = motionJoystick.InputVector.y > 0.25f ? 1 : motionJoystick.InputVector.y < -0.25f ? -1: 0f;
        }
        else
        {
            HorizontalInput =  Input.GetAxis("Horizontal"); //turning input
            VerticalInput = Input.GetAxis("Vertical");     //accelaration input
        }
    }

}
