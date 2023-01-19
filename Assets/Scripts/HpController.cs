using System.Collections;
using System.Collections.Generic;
using MyBox;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class HpController : MonoBehaviour
{
    [PositiveValueOnly] [SerializeField] 
    private int hpPerIcon;

    [SerializeField] private Image[] Images;
#if UNITY_EDITOR
    [ButtonMethod]
    private string AppendAuto()
    {
        Images = FindObjectsOfType<Image>();
        return Images.Length + " Images found on scene, cached";
    }
#endif
}
