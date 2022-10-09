using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class CamerShake : MonoBehaviour
{
    
    #region FIELDS DECELERATION

    [SerializeField] private CinemachineVirtualCamera camera;
    
    private ArcadeVehicleController m_VehicleController;

    CinemachineBasicMultiChannelPerlin cinemachineNoise;
    Rigidbody m_Rigibody;

    float shakeTimer;

    #endregion

    private void Awake() 
    {
        m_Rigibody = GetComponent<Rigidbody>();
        m_VehicleController = GetComponent<ArcadeVehicleController>();

        cinemachineNoise = camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void ShakeCamera(float intensity, float time)
    {
        cinemachineNoise.m_AmplitudeGain = intensity;
        shakeTimer = time;
    }

    private void Update() 
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0)
            {
                cinemachineNoise.m_AmplitudeGain = 0f;
            }
        }    
    }

    private void OnCollisionEnter(Collision other)
    {
        float calculatedIntensity  = Mathf.Lerp(0.5f, 2f, Mathf.InverseLerp(10f, 45f, m_VehicleController.carVelocity.z));
        ShakeCamera(calculatedIntensity,0.35f);
    }

}
