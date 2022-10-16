using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VehicleData", menuName = "CustomObjects/VehicleData")]
public class VehicleSO : ScriptableObject
{
    public VehilceStruct[] vehiclesContainer;


    [Serializable]
    public struct VehilceStruct
    {
        public string name;
        public GameObject driveableObject;
        public float speed;
        public float acceleration;
        public float handling;
    }

}
