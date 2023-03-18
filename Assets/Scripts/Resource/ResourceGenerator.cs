using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    private BuildingTypeSO buildingType;
    private float timerMax;

    private float timer;

    private void Awake() {
        buildingType = GetComponent<BuildingTypeHolder>().buildingType;
        timerMax = buildingType.resourceGeneratedData.timerMax;
    }

    private void Update() {
        timer -= Time.deltaTime;
        if (timer < 0f) {
            timer += timerMax;
            ResourceManager.Instance.AddResource(buildingType.resourceGeneratedData.resourceTypeSO, 1);
        }
    }

    
}
