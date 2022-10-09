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

    #endregion

    private void Start() 
    {
        UIManager.Init(playerVehicle);    
    }


}
