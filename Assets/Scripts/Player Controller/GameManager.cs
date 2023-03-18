using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    #region FIELDS DECLERATION

    [Header("PANEL REFERENCES")]
    public UIManager UIManager;

    [Header("COMPONENT REFERENCES")]
    public ArcadeVehicleController playerVehicle;

    public static event Action GameStartedAction;
    public static event Action GameFinishedAction;
    public static event Action LapFinishedAction;

    public static void GameStartedInvoke()
    {
        GameStartedAction.Invoke();
    }

    public static void GameFinishedInvoke()
    {
        GameFinishedAction.Invoke();
    }

    public static void LapFinishedInvoke()
    {
        LapFinishedAction.Invoke();
    }
    
    #endregion

    private void Start() 
    {
        UIManager.Init(playerVehicle);    
    }


}
