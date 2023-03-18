using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGhost : MonoBehaviour
{
    private GameObject spriteGameObject;
    private void Awake() {
        spriteGameObject = transform.Find("sprite").gameObject;
        spriteGameObject.SetActive(false);
    }

    private void Start() {
        BuildingManager.Instance.OnActiveBuildingTypeChanged += BuildingManager_OnActiveBuildingTypeChange;
    }

    private void BuildingManager_OnActiveBuildingTypeChange(object sender ,BuildingManager.OnActiveBuildingTypeChangedEventArgs e) {
        if (e.activeBuildingType == null){
            Hide();
        } else {
            Show(e.activeBuildingType.sprite);
        }
    }

    private void Update(){
        transform.position = Utilities.GetMousePosition();
    }

    private void Show(Sprite ghostSprite) {
        spriteGameObject.GetComponent<SpriteRenderer>().sprite = ghostSprite;
        spriteGameObject.SetActive(true);
    }

    
    private void Hide() {
        spriteGameObject.SetActive(false);
    }
}
