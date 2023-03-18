using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
public class BuildingManager : MonoBehaviour
{   
    public static BuildingManager Instance {get; private set;}

    private Camera mainCamera;
    private BuildingTypeListSO buildingTypeList; 
    private BuildingTypeSO activeBuildingType;

    public event EventHandler<OnActiveBuildingTypeChangedEventArgs> OnActiveBuildingTypeChanged;

    public class OnActiveBuildingTypeChangedEventArgs :  EventArgs {
        public BuildingTypeSO activeBuildingType;
    }

    private void Awake() {
        Instance = this;
        buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
        activeBuildingType = null;
    }

    private void Start() {
        mainCamera = Camera.main;
    }
    private void Update() {
        // Places Building if there is the user clicks & mouse is not on UI elements.
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) {
            if (activeBuildingType != null){
                Instantiate(activeBuildingType.prefab,Utilities.GetMousePosition(),Quaternion.identity);
                SetActiveBuildingType(activeBuildingType);
            }
        }
        if (Input.GetMouseButtonDown(1) && !EventSystem.current.IsPointerOverGameObject()) {
            activeBuildingType = null;
            OnActiveBuildingTypeChanged?.Invoke(this,new OnActiveBuildingTypeChangedEventArgs {activeBuildingType = null});
        }
    }
    
    public void SetActiveBuildingType(BuildingTypeSO buildingType){
        OnActiveBuildingTypeChanged?.Invoke(this,new OnActiveBuildingTypeChangedEventArgs {activeBuildingType = buildingType});
        activeBuildingType = buildingType;
    }

    public BuildingTypeSO GetActiveBuildingType() {
        return activeBuildingType;
    }
}
