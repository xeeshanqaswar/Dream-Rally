using System.Collections;
using System.Collections.Generic;
using DitzeGames.MobileJoystick;
using PathCreation.Examples;
using UnityEngine;
using DG.Tweening;

public class AIController : MonoBehaviour
{
    public float turnSmoothing = 0.5f;
    private float stoppingDistance = 3f;
    public Transform followObject;
    private PathFollower m_PathFollower;
    
    private Rigidbody m_Rigidbody;
    private ArcadeVehicleController m_ArcadeVC;
    
    public float HorizontalInput{get;set;} // Left and right turns
    public float VerticalInput{get;set;} // Forward and Backward

    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_PathFollower = followObject.GetComponent<PathFollower>();

        m_ArcadeVC = GetComponent<ArcadeVehicleController>();
        m_PathFollower.speed = m_ArcadeVC.MaxSpeed;
    }
    
    private void Update()
    {
        float distance = Vector3.Distance(transform.position, followObject.position);
        Vector3 target = followObject.position - transform.position;
        float dotProduct = Vector3.Dot(target.normalized, transform.forward.normalized);
        float angleValue = Vector3.SignedAngle(target, transform.forward.normalized, transform.TransformDirection(Vector3.up));

        // print($"Angle {angleValue} Distance {distance}");

        #region ACCELERATION & STEERING

        if (distance > stoppingDistance)
        {
            if (angleValue > 10 && angleValue < 170)
            {
                // HorizontalInput = -1;
                DOTween.To(()=> HorizontalInput, x=> HorizontalInput = x, -1, turnSmoothing);
            }
            else if(angleValue < -10f && angleValue > -170f)
            {
                // HorizontalInput = 1;
                DOTween.To(()=> HorizontalInput, x=> HorizontalInput = x, 1, turnSmoothing);
            }
            else
            {
                // HorizontalInput = 0f;
                DOTween.To(()=> HorizontalInput, x=> HorizontalInput = x, 0, turnSmoothing);
            }
            
            VerticalInput = dotProduct > 0 ? 1 : -1;
        }
        else
        {
            m_Rigidbody.velocity = Vector3.zero; // Apply brakes
            VerticalInput = 0f;
        }

        #endregion

        #region LIMIT AI

        if (distance > 20f)
        {
            m_PathFollower.canMove = false;
        }
        else
        {
            m_PathFollower.canMove = true;
        }

        #endregion

        #region PLAYER DETECTION

        

        #endregion
        
    }

}
