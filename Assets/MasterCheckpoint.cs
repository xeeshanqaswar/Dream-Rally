using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterCheckpoint : MonoBehaviour
{
    public bool handleGameStart;
    public bool handleGameEnd;
    public bool handleLapCounter;

    private bool m_GameStarted, m_GameEnded;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (handleGameStart && m_GameStarted)
            {
                m_GameStarted = true;
                GameManager.GameStartedInvoke();
            }

            if (handleGameEnd && m_GameEnded)
            {
                m_GameEnded = true;
                GameManager.GameFinishedInvoke();
            }

            if (handleLapCounter && m_GameStarted)
            {
                GameManager.LapFinishedInvoke();
            }
        }
    }
}
