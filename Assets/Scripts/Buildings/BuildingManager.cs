using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{   
    private Camera mainCamera;
    private BuildingTypeListSO buildingTypeList; 
    private BuildingTypeSO buildingType;

    private void Awake() {
        buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
        buildingType = buildingTypeList.list[0];
    }

    private void Start() {
        mainCamera = Camera.main;
    }
    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Instantiate(buildingType.prefab,GetMousePosition(),Quaternion.identity);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            buildingType = buildingTypeList.list[0];
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            buildingType = buildingTypeList.list[1];
        }

    }
    
    private Vector3 GetMousePosition() {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        return mousePosition;
    }
}
