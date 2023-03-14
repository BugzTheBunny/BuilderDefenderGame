using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingSelector : MonoBehaviour
{   

    private void Awake() {
        // Getting template & disabling initial
        Transform btnTemplate = transform.Find("btnTemplate");
        btnTemplate.gameObject.SetActive(false);

        BuildingTypeListSO buildingTypeListSO = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);

        int index = 0;
        foreach(BuildingTypeSO bType in buildingTypeListSO.list) {
            Transform btnTransform = Instantiate(btnTemplate,transform);
            btnTransform.name = bType.nameString;
            btnTransform.gameObject.SetActive(true);

            float offset = 80f;
            btnTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offset * index,0);

            btnTransform.Find("image").GetComponent<Image>().sprite = bType.sprite;
            
            btnTransform.GetComponent<Button>().onClick.AddListener(() => {
                BuildingManager.Instance.SetActiveBuildingType(bType);
                Debug.Log(bType.nameString);
            });

            index ++;
        } 
    }

}
