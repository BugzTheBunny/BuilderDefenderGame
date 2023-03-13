using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ResourceType")]
public class ResourceTypeSO : ScriptableObject
{
    [SerializeField] public string nameString;
    [SerializeField] public Sprite sprite;
}
