using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    private ResourceGeneratedData resourceGeneratedData;
    private float timerMax;

    private float timer;

    private void Awake() {
        resourceGeneratedData = GetComponent<BuildingTypeHolder>().buildingType.resourceGeneratedData;
        timerMax = resourceGeneratedData.timerMax;
    }

    private void Start() {
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, resourceGeneratedData.resourceDetectionRadius);
        int nearbyResourceAmount = 0;
        foreach (Collider2D collider2D in collider2DArray) {
            ResourceNode resourceNode = collider2D.GetComponent<ResourceNode>();
            if (resourceNode != null) {
                if (resourceNode.resourceTypeSO == resourceGeneratedData.resourceTypeSO) {
                    nearbyResourceAmount++;
                }
            }
        }
        
        nearbyResourceAmount = Mathf.Clamp(nearbyResourceAmount,0,resourceGeneratedData.maxResourceAmount);

        if (nearbyResourceAmount == 0 ) {
            // disabled script if there is no resource nodes near.
            enabled = false;
        } else {
            timerMax = (resourceGeneratedData.timerMax /2f) + resourceGeneratedData.timerMax * ( 1 - (float)nearbyResourceAmount/ resourceGeneratedData.maxResourceAmount);
            enabled = true;
        }

        Debug.Log("resource amount " + nearbyResourceAmount.ToString());

    }

    private void Update() {
        timer -= Time.deltaTime;
        if (timer < 0f) {
            timer += timerMax;
            ResourceManager.Instance.AddResource(resourceGeneratedData.resourceTypeSO, 1);
        }
    }

    
}
