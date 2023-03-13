using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{   
    public static ResourceManager Instance { get ; private set; }
    
    private Dictionary<ResourceTypeSO, int> resourceAmountDict;

    public event EventHandler OnResourceAmountChanged;
    private void Awake() {
        Instance = this;
        
        resourceAmountDict = new Dictionary<ResourceTypeSO, int>();

        ResourceTypeListSO resourceTypeListSO = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name); 

        foreach (ResourceTypeSO rType in resourceTypeListSO.list) {
            resourceAmountDict[rType] = 0;
        }

        TestLogResourceDict();
    }

    private void Update() {

    }

    private void TestLogResourceDict() {
        foreach (ResourceTypeSO rType in resourceAmountDict.Keys) {
            Debug.Log(rType.name + ": " + resourceAmountDict[rType]);
        }
    }

    public void AddResource(ResourceTypeSO rType, int amount) {
        resourceAmountDict[rType] += amount;

        OnResourceAmountChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetResourceAmount(ResourceTypeSO resourceType) {
        return resourceAmountDict[resourceType];
    }
}
