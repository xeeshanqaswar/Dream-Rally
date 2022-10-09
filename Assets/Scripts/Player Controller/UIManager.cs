using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("HUD")]
    [SerializeField] private TextMeshProUGUI speedometer;

    private ArcadeVehicleController m_Player;

    public void Init(ArcadeVehicleController AVC)
    {
        m_Player = AVC;
    }

    private void Update() 
    {
        speedometer.text = ((int)m_Player.carVelocity.magnitude).ToString();
    }

}
