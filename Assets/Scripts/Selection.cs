using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Selection : MonoBehaviour
{

    #region FIELDS DECLERATION

    public Gamestate gamestate;

    #endregion

    public void SelectRegion(int state)
    {
        gamestate.selectedRegion = (Region)state;
    }

    public void SelectGamemode(int index)
    {
        gamestate.selectedGameMode = (GameMode)index;
    }

}
