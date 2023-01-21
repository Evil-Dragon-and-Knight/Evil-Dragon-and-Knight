using System.Collections;
using System.Collections.Generic;
using MyBox;
using UnityEngine;

[CreateAssetMenu]
public class ObjectPoolingItem : ScriptableObject
{
    public bool active = true;
    [Separator("Settings")] 
    [MustBeAssigned] public GameObject prefab;
    [RegexString("[a-zA-Z0-9]+", RegexStringMode.WarningIfNotMatch)] [ConditionalField(nameof(active), false)] 
    [MustBeAssigned] public string name;
#if UNITY_EDITOR
    [ButtonMethod]
    private void NamingAuto()
    {
        if (prefab != null) 
            name = prefab.name;
    }
#endif
    [ConditionalField(nameof(active), false)]
    public bool canExtendSelf = true;
    [ConditionalField(nameof(active), false)] [PositiveValueOnly]
    public int initializeCount = 10;
}
