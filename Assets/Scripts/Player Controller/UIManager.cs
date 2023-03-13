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
    [SerializeField] private GameObject[] gearIndicator;

    private ArcadeVehicleController m_Player;

    public void Init(ArcadeVehicleController AVC)
    {
        m_Player = AVC;
    }

    private void Update()
    {
        float speedInKmh = m_Player.carVelocity.magnitude * 2.2369362912f;
        speedometer.text = ((int)speedInKmh).ToString("000");
        float pointsToActivate = gearIndicator.Length * (m_Player.carVelocity.magnitude / (m_Player.MaxSpeed - 10f));
        pointsToActivate = pointsToActivate % gearIndicator.Length;
        for (int i = 0; i < gearIndicator.Length; i++)
        {
            gearIndicator[i].transform.GetChild(0).gameObject.SetActive(i < Mathf.Round(pointsToActivate));
        }
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
