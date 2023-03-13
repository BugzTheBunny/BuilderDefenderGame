using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{   
    public static BuildingManager Instance {get; private set;}
    private Camera mainCamera;
    private BuildingTypeListSO buildingTypeList; 
    private BuildingTypeSO activeBuildingType;

    private void Awake() {
        Instance = this;
        
        buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
        activeBuildingType = buildingTypeList.list[0];
    }

    private void Start() {
        mainCamera = Camera.main;
    }
    private void Update() {
        // Places Building if there is the user clicks & mouse is not on UI elements.
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) {
            Instantiate(activeBuildingType.prefab,GetMousePosition(),Quaternion.identity);
        }


    }
    
    private Vector3 GetMousePosition() {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        return mousePosition;
    }

    public void SetActiveBuildingType(BuildingTypeSO buildingType){
        activeBuildingType = buildingType;
    }
}
