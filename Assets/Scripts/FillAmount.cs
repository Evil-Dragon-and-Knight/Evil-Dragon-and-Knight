using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillAmount : MonoBehaviour
{
    [SerializeField] private Image hpImage;
    [Range(0, 2)] [SerializeField] 
    private int range = 2;

    private bool onUpdate = false;

    public void UpdateValue(int value)
    {
        if (value < 0) value = 0;
        range = value;
    }

    private void Start()
    {
        onUpdate = true;
        if (hpImage == null)
            onUpdate = false;
    }

    private void Update()
    {
        if (!onUpdate) return;
        hpImage.fillAmount = (float)range / 2;
    }
}
