using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VehicleSelection : MonoBehaviour
{
    public Transform vehicleHolder;
    public  Gamestate gamestate;
    public VehicleInventorySO vehicleCollection;


    [Header("UI REFERENCES")]
    public Button[] shuffleButtons;
    public TextMeshProUGUI displayName, displayPrice;
    public Image speed, acceleration, handling;

    [SerializeField] private int currentVehicleIndex = 0;
    private const int pDifference = 17;

    private void OnEnable()
    {
        StartCoroutine(UpdateStats());
    }

    private void Start()
    {
        // Spawn vehicles
        
        for (int i = 0; i < vehicleCollection.vehiclesContainer.Length; i++)
        {
            Instantiate(vehicleCollection.vehiclesContainer[i].driveableObject, 
                        Vector3.forward * pDifference * i ,
                        Quaternion.identity , vehicleHolder);
        }
    }

    IEnumerator UpdateStats()
    {

        // Setting Data
        displayName.text = vehicleCollection.vehiclesContainer[currentVehicleIndex].name;
        if (!vehicleCollection.vehiclesContainer[currentVehicleIndex].unlocked)
        {
            displayPrice.text = $"RS /- {vehicleCollection.vehiclesContainer[currentVehicleIndex].price.ToString()}";
        }
        else
        {
            displayPrice.text = "";
        }

        // Reset
        displayName.rectTransform.DOAnchorPos(new Vector2(-700f, -70f), 0f);
        displayPrice.rectTransform.DOAnchorPos(new Vector2(-700f, -175f), 0f);

        speed.fillAmount = 0f;
        acceleration.fillAmount = 0f;
        handling.fillAmount = 0f;

        yield return new WaitForSeconds(0.1f);
        
        // Animation
        displayName.rectTransform.DOAnchorPos(new Vector2(80f, -70f), 0.5f);
        displayPrice.rectTransform.DOAnchorPos(new Vector2(80f, -175f), 0.5f).SetDelay(0.1f);

        speed.DOFillAmount(0.5f, 0.5f); 
        acceleration.DOFillAmount(0.5f, 0.5f);
        handling.DOFillAmount(0.5f, 0.5f);

        foreach (var item in shuffleButtons)
        {
            item.interactable = true;
        }
    }

    public void ExploreVehicle(int count)
    {
        currentVehicleIndex += count;

        if (currentVehicleIndex < 0 || currentVehicleIndex > vehicleCollection.vehiclesContainer.Length - 1)
        {
            currentVehicleIndex = Mathf.Clamp(currentVehicleIndex, 0, vehicleCollection.vehiclesContainer.Length - 1);
            return;
        }

        foreach (var item in shuffleButtons)
        {
            item.interactable = false;
        }

        vehicleHolder.transform.DOMoveZ(-currentVehicleIndex * pDifference, 0.5f);
        StartCoroutine(UpdateStats());
    }

}
