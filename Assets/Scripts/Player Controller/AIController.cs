using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using DitzeGames.MobileJoystick;
using PathCreation.Examples;
using UnityEngine;
using DG.Tweening;
using Vector3 = UnityEngine.Vector3;

public class AIController : MonoBehaviour
{
    public Transform followObject;
    public float rayDistance;
    public LayerMask playerLayer;
    public float turnSmoothing = 0.5f;
    public bool m_ObstacleDetected;
    
    private PathFollower m_PathFollower;
    
    private Rigidbody m_Rigidbody;
    private ArcadeVehicleController m_ArcadeVC;

    private float m_distanceFromTarget;
    private float m_OriginalSpeed;
    private float m_ObstacleTurning = 0;
    private float m_StoppingDistance = 3f;
    
    public float HorizontalInput{get;set;} // Left and right turns
    public float VerticalInput{get;set;} // Forward and Backward
    
    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_PathFollower = followObject.GetComponent<PathFollower>();
        
        m_ArcadeVC = GetComponent<ArcadeVehicleController>();

        m_OriginalSpeed = m_ArcadeVC.MaxSpeed;
        m_PathFollower.speed = m_OriginalSpeed;
    }
    
    private void Update()
    {
        m_distanceFromTarget = Vector3.Distance(transform.position, followObject.position);
        Vector3 target = followObject.position - transform.position;
        float dotProduct = Vector3.Dot(target.normalized, transform.forward.normalized);
        float angleValue = Vector3.SignedAngle(target, transform.forward.normalized, transform.TransformDirection(Vector3.up));

        // print($"Angle {angleValue} Distance {distance}");

        #region ACCELERATION & STEERING

        if (m_distanceFromTarget > m_StoppingDistance)
        {
            if (!m_ObstacleDetected)
            {
                if (angleValue > 10 && angleValue < 170)
                {
                    DOTween.To(()=> HorizontalInput, x=> HorizontalInput = x, -1, turnSmoothing);
                }
                else if(angleValue < -10f && angleValue > -170f)
                {
                    DOTween.To(()=> HorizontalInput, x=> HorizontalInput = x, 1, turnSmoothing);
                }
                else
                {
                    DOTween.To(()=> HorizontalInput, x=> HorizontalInput = x, 0, turnSmoothing);
                }
            }
            
            VerticalInput = dotProduct > 0 ? 1 : -1;
        }
        else
        {
            m_Rigidbody.velocity = Vector3.zero; // Apply brakes
            VerticalInput = 0f;
        }

        #endregion

        LimitAI();
        ObstacleDetectionAndAvoidance();
        
    }

    private void LimitAI()
    {
        float minDistanceBtwPlayerAndTarget = 15f;
        if (m_distanceFromTarget > minDistanceBtwPlayerAndTarget)
        {
            m_PathFollower.canMove = false;
        }
        else
        {
            m_PathFollower.canMove = true;
        }
    }

    private void ObstacleDetectionAndAvoidance()
    {
        // Straight Detection
        Ray ray1 = new Ray(transform.TransformPoint(new Vector3(0f,-0.5f ,2f)), transform.TransformDirection(Vector3.forward));
        float turnAmount = 0.5f;
        
        bool observeRaycast1 = Physics.Raycast(ray1, rayDistance, playerLayer);
        if (observeRaycast1)
        {
            // Straight
            // m_ObstacleTurning = 1f;
        }
        
        // Right Detection
        Ray ray2 = new Ray(transform.TransformPoint(new Vector3(0.65f,-0.5f ,1.9f)), transform.TransformDirection(Vector3.forward)); // right straight
        bool observeRaycast2 = Physics.Raycast(ray2, rayDistance, playerLayer);
        if (observeRaycast2)
        {
            // Right 
            m_ObstacleTurning += -turnAmount;
        }
        
        // Right Right Detection
        Ray ray3 = new Ray(transform.TransformPoint(new Vector3(0.65f,-0.5f ,1.9f)), transform.TransformDirection(new Vector3(0.3f, 0f, 1f))); // right right
        bool observeRaycast3 = Physics.Raycast(ray3, rayDistance, playerLayer);
        if (observeRaycast3)
        {
            // Right Right
            m_ObstacleTurning = -turnAmount * 2f;
        }
        
        Ray ray4 = new Ray(transform.TransformPoint(new Vector3(-0.65f,-0.5f ,1.9f)), transform.TransformDirection(Vector3.forward));
        bool observeRaycast4 = Physics.Raycast(ray4, rayDistance, playerLayer);
        if (observeRaycast4)
        {
            // Left 
            m_ObstacleTurning = turnAmount;
        }
        
        Ray ray5 = new Ray(transform.TransformPoint(new Vector3(-0.65f,-0.5f ,1.9f)), transform.TransformDirection(new Vector3(-0.3f, 0f, 1f)));
        bool observeRaycast5 = Physics.Raycast(ray5, rayDistance, playerLayer);
        if (observeRaycast5)
        {
            // Left Left
            m_ObstacleTurning = turnAmount * 2f;
        }

        m_ObstacleDetected = observeRaycast1 || observeRaycast2 || observeRaycast3 || observeRaycast4 || observeRaycast5;

        if (m_ObstacleDetected)
        {
            SetSpeed(m_OriginalSpeed / 2);
            HorizontalInput = m_ObstacleTurning;
        }
        else
        {
            m_ObstacleTurning = 0f;
            SetSpeed(m_OriginalSpeed);
        }
    }

    public void SetSpeed(float speed)
    {
        float smoothing = 0.5f;
        DOTween.To(()=> m_PathFollower.speed, x=> m_PathFollower.speed = x, speed, smoothing);
        DOTween.To(()=> m_ArcadeVC.MaxSpeed, x=> m_ArcadeVC.MaxSpeed = x, speed, smoothing);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color =Color.red;
        Gizmos.DrawRay(transform.TransformPoint(new Vector3(0f,-0.5f ,2f)) , transform.TransformDirection(Vector3.forward) * rayDistance);
        
        Gizmos.DrawRay(transform.TransformPoint(new Vector3(0.65f,-0.5f ,1.9f)) , transform.TransformDirection(Vector3.forward) * rayDistance);
        Gizmos.DrawRay(transform.TransformPoint(new Vector3(0.65f,-0.5f ,1.9f)) , transform.TransformDirection(new Vector3(0.3f, 0f, 1f)) * rayDistance);
        
        Gizmos.DrawRay(transform.TransformPoint(new Vector3(-0.65f,-0.5f ,1.9f)) , transform.TransformDirection(Vector3.forward) * rayDistance);
        Gizmos.DrawRay(transform.TransformPoint(new Vector3(-0.65f,-0.5f ,1.9f)) , transform.TransformDirection(new Vector3(-0.3f, 0f, 1f)) * rayDistance);
    }
}
