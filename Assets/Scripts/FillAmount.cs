using System;
using System.Collections;
using System.Collections.Generic;
using MyBox;
using UnityEngine;
using UnityEngine.UI;

public class FillAmount : MonoBehaviour
{
    [SerializeField] private Image hpImage;
    [PositiveValueOnly] [SerializeField] private int maxRange = 2;
    [PositiveValueOnly] [SerializeField] private int range = 2;

    private bool _onUpdate = false;

    public void UpdateValue(int value)
    {
        if (value < 0) value = 0;
        if (value > maxRange) value = maxRange;
        range = value;
    }
    
    public void DecreaseValue(int value)
    {
        int dump = range - Mathf.Abs(value);
        if (dump < 0) dump = 0;
        range = dump;
    }

    private void Start()
    {
        _onUpdate = true;
        if (hpImage == null)
            _onUpdate = false;
    }

    public bool Check()
    {
        return range == 0;
    }

    private void Update()
    {
        if (!_onUpdate) return;
        int value = range;
        if (value < 0) value = 0;
        if (value > maxRange) value = maxRange;
        hpImage.fillAmount = (float)value / maxRange;
    }
}
