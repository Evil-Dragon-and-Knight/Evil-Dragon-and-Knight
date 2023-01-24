using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class BossHP : MonoBehaviour
{
    [SerializeField] private FillAmount fillAmount;
    [SerializeField] private UnityEvent uEvent;

    private static FillAmount _fillAmount;
    private static UnityEvent _uEvent;
    
    private static bool _done = true;

    private void Awake()
    {
        _fillAmount = fillAmount;
        _uEvent = uEvent;
    }

    public static void Init()
    {
        _done = false;
        _fillAmount.UpdateValue(15);
    }
    
    public static void DecreaseValue(int value)
    {
        _fillAmount.DecreaseValue(value);
    }
    
    private void Update()
    {
        if (_done) return;
        if (_fillAmount.Check())
        {
            _done = true;
            _uEvent.Invoke();
        }
    }

    private void OnEnable()
    {
        Init();
    }
    
    private void OnDisable()
    {
        _done = true;
        _fillAmount.UpdateValue(15);
    }
}
