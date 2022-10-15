using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Gamestate", menuName = "CustomObjects/Gamestate")]
public class Gamestate : ScriptableObject
{
    public GameMode selectedGameMode;
    public Region selectedRegion;
}

[Serializable]public enum GameMode { FreeRoaming, StoryMode }
[Serializable] public enum Region { Hunza, Muree, Skardu }
