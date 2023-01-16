using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleSetting : MonoBehaviour
{
    [SerializeField] private Toggle _toggle;

    public void UpdateToggle()
    {
        if (_toggle == null) return;

        SettingManager.Instance.Toggle_SKIP = _toggle.isOn;
    }
    
    private void Awake()
    {
        if (_toggle == null) return;

        #region Init
        
        _toggle.isOn = SettingManager.Instance.Toggle_SKIP;

        #endregion
    }
}
