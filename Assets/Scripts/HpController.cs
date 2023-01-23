using System;
using System.Collections;
using System.Collections.Generic;
using MyBox;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class HpController : MonoBehaviour
{
    
    private const int _maxHp = 6;
    [Range(0, _maxHp)] [SerializeField] 
    private int currentHp = _maxHp;

    [SerializeField] private GameObject hpGameObject;
    [SerializeField] private FillAmount[] images;

    [SerializeField] private UnityEvent dieEvents;
#if UNITY_EDITOR
    [ButtonMethod]
    private void AutoFillAmountFinder()
    {
        if (hpGameObject == null) return;
        List<FillAmount> dump = new List<FillAmount>();
        foreach (Transform child in hpGameObject.transform)
        {
            FillAmount temp = child.GetComponent<FillAmount>();
            if (temp == null) continue;
            dump.Add(temp);
        }
        images = dump.ToArray();
    }
#endif

    public void Init()
    {
        currentHp = _maxHp;
        UpdateImage();
    }

    public void Increse(int value)
    {
        currentHp += Mathf.Abs(value);
        UpdateImage();
    }

    public void Decrease(int value)
    {
        currentHp -= Mathf.Abs(value);
        currentHp = currentHp < 0 ? 0 : currentHp;
        UpdateImage();
    }

    private void UpdateImage()
    {
        int dump = Mathf.FloorToInt(currentHp / 2f);
        
        
        for (int i = 0; i < dump; i++)
        {
            images[i].UpdateValue(2);
        }

        for (int i = dump; i < _maxHp / 2; i++)
        {
            int v = currentHp - i * 2;
            images[i].UpdateValue(v < 0 ? 0 : v);
        }
    }
    
#if UNITY_EDITOR
    private void FixedUpdate()
    {
        UpdateImage();
    }
#endif
}
