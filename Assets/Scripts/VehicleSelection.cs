using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VehicleSelection : MonoBehaviour
{
    [Header("UI REFERENCES")]
    public TextMeshProUGUI displayName, displayPrice;
    public Image speed, acceleration, handling;

    private int currentVehicleIndex = 0;

    private void OnEnable()
    {
        StartCoroutine(UpdateStats());
    }

    IEnumerator UpdateStats()
    {
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
    }

}
