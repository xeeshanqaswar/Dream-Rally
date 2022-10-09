using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("UI REFERENCES")]
    [SerializeField] private GameObject[] panels;

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

    public void OpenPanels(int index)
    {
        CloseAllPanels();
        panels[index].SetActive(true);
        
        Time.timeScale = 0f;
    }

    public void CloseAllPanels()
    {
        foreach (var panel in panels)
        {
            panel.SetActive(false);
        }

        Time.timeScale = 1f;
    }

}
