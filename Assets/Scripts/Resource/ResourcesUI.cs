using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ResourcesUI : MonoBehaviour
{   
    private ResourceTypeListSO resourceTypeList;
    private Dictionary<ResourceTypeSO,Transform> resourceTypeTransformDict;

    private void Awake() {
        resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);

        resourceTypeTransformDict = new Dictionary<ResourceTypeSO, Transform>();

        Transform resourceTemplate = transform.Find("resourceTemplate");
        resourceTemplate.gameObject.SetActive(false);

        int index = 0;
        foreach (ResourceTypeSO rType in resourceTypeList.list) {
            Transform resourceTransform = Instantiate(resourceTemplate,transform);

            resourceTransform.gameObject.SetActive(true);
            
            float offsetAmount = -190f;
            resourceTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount*index,-75);

            resourceTransform.Find("image").GetComponent<Image>().sprite = rType.sprite;
            
            resourceTypeTransformDict[rType] = resourceTransform;

            index++;
        }   
    } 

    private void Start() {
        ResourceManager.Instance.OnResourceAmountChanged += ResourceManager_OnResourceAmountChanged;
        UpdateResourceAmount();
    }
 
    private void ResourceManager_OnResourceAmountChanged(object sender, System.EventArgs e) {
        UpdateResourceAmount();
    }

    private void UpdateResourceAmount() {
        foreach (ResourceTypeSO rType in resourceTypeList.list) {
            Transform resourceTransform = resourceTypeTransformDict[rType];

            int resourceAmount = ResourceManager.Instance.GetResourceAmount(rType);
            resourceTransform.Find("text").GetComponent<TextMeshProUGUI>().SetText(resourceAmount.ToString());
        }
    }
}
