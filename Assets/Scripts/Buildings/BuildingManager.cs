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
            if (activeBuildingType != null && CanSpawnBuilding(activeBuildingType,Utilities.GetMousePosition())){
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

    public bool CanSpawnBuilding(BuildingTypeSO buildingTypeSO,Vector3 position) {
        BoxCollider2D boxCollider2D = buildingTypeSO.prefab.GetComponent<BoxCollider2D>();

        Collider2D[] collider2DArray = Physics2D.OverlapBoxAll(position + (Vector3)boxCollider2D.offset, boxCollider2D.size,0);
        bool isAreaClear = collider2DArray.Length == 0;

        if (!isAreaClear) return false;

        collider2DArray = Physics2D.OverlapCircleAll(position, buildingTypeSO.minCostractionRadius);

        foreach (Collider2D collider2D in collider2DArray) {
            // Colliders inside the constraction radius
            BuildingTypeHolder buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
            if (buildingTypeHolder != null) {
                // Has a BuildingTypeHolder
                if (buildingTypeHolder.buildingType == buildingTypeSO) {
                    // There are already building withing the allowed radius.
                    return false;
                }
            }
        }
        
        float maxConstrctionRadius = 25;
        collider2DArray = Physics2D.OverlapCircleAll(position, maxConstrctionRadius);

        foreach (Collider2D collider2D in collider2DArray) {
            // Colliders inside the constraction radius
            BuildingTypeHolder buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
            if (buildingTypeHolder != null) {
                    return true;
            }
        }

        return false;
    }
}
