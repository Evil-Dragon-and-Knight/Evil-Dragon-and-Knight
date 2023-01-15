using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingManager : Singleton<SettingManager>
{
    #region KeyCode

    public KeyCode[] Key_UP
    {
        get => new KeyCode[]
        {
            (KeyCode)PlayerPrefs.GetInt("Key@UP1", (int)KeyCode.W),
            (KeyCode)PlayerPrefs.GetInt("Key@UP2", (int)KeyCode.UpArrow)
        };
        set
        {
            PlayerPrefs.SetInt("Key@UP1", (int)value[0]);
            PlayerPrefs.SetInt("Key@UP2", (int)value[1]);
        }
    }
    
    public KeyCode[] Key_DOWN
    {
        get => new KeyCode[]
        {
            (KeyCode)PlayerPrefs.GetInt("Key@DOWN1", (int)KeyCode.S),
            (KeyCode)PlayerPrefs.GetInt("Key@DOWN2", (int)KeyCode.DownArrow)
        };
        set
        {
            PlayerPrefs.SetInt("Key@DOWN1", (int)value[0]);
            PlayerPrefs.SetInt("Key@DOWN2", (int)value[1]);
        }
    }
    
    public KeyCode[] Key_ATTACK
    {
        get => new KeyCode[]
        {
            (KeyCode)PlayerPrefs.GetInt("Key@ATTACK1", (int)KeyCode.LeftShift),
            (KeyCode)PlayerPrefs.GetInt("Key@ATTACK2", (int)KeyCode.Space)
        };
        set
        {
            PlayerPrefs.SetInt("Key@ATTACK1", (int)value[0]);
            PlayerPrefs.SetInt("Key@ATTACK2", (int)value[1]);
        }
    }

    #endregion

    #region MyRegion

    public bool Toggle_SKIP
    {
        get => PlayerPrefs.GetInt("Toggle@SKIP", 0) != 0;
        set => PlayerPrefs.SetInt("Toggle@SKIP", value ? 1 : 0);
    }

    #endregion
}
