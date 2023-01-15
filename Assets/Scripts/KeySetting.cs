using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MoreMountains.Feedbacks;
using MyBox;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class KeySetting : MonoBehaviour
{
    #region Variables

    private enum KeyType
    {
        UP1, UP2,
        DOWN1, DOWN2,
        ATTACK1, ATTACK2
    }

    private bool _listeningEvent = false;
    private KeyType _currentListeningButton;

    [SerializeField] private UnityEvent eUnityEvent;

    [Separator("UP Buttons")]
    [SerializeField] private GameObject upBtn1;
    private TextMeshProUGUI _upBtn1;
    [SerializeField] private GameObject upBtn2;
    private TextMeshProUGUI _upBtn2;
    
    [Separator("DOWN Buttons")]
    [SerializeField] private GameObject downBtn1;
    private TextMeshProUGUI _downBtn1;
    [SerializeField] private GameObject downBtn2;
    private TextMeshProUGUI _downBtn2;
    
    [Separator("ATTACK Buttons")]
    [SerializeField] private GameObject attackBtn1;
    private TextMeshProUGUI _attackBtn1;
    [SerializeField] private GameObject attackBtn2;
    private TextMeshProUGUI _attackBtn2;
    
    #endregion

    #region KeyListening

    private readonly KeyCode[] _keyList = new KeyCode[]
    {
        #region Alphabet

        KeyCode.A,
        KeyCode.B,
        KeyCode.C,
        KeyCode.D,
        KeyCode.E,
        KeyCode.F,
        KeyCode.G,
        KeyCode.H,
        KeyCode.I,
        KeyCode.J,
        KeyCode.K,
        KeyCode.L,
        KeyCode.M,
        KeyCode.N,
        KeyCode.O,
        KeyCode.P,
        KeyCode.Q,
        KeyCode.R,
        KeyCode.S,
        KeyCode.T,
        KeyCode.U,
        KeyCode.V,
        KeyCode.W,
        KeyCode.X,
        KeyCode.Y,
        KeyCode.Z,

        #endregion

        #region Numpad
        
        KeyCode.Alpha0,
        KeyCode.Alpha1,
        KeyCode.Alpha2,
        KeyCode.Alpha3,
        KeyCode.Alpha4,
        KeyCode.Alpha5,
        KeyCode.Alpha6,
        KeyCode.Alpha7,
        KeyCode.Alpha8,
        KeyCode.Alpha9,

        #endregion

        #region Arrow

        KeyCode.UpArrow,
        KeyCode.DownArrow,
        KeyCode.LeftArrow,
        KeyCode.RightArrow,

        #endregion

        #region Etc

        KeyCode.BackQuote,
        KeyCode.Minus,
        KeyCode.Equals,
        KeyCode.LeftShift,
        KeyCode.RightShift,
        KeyCode.Space,
        KeyCode.CapsLock,
        KeyCode.Tab,
        KeyCode.LeftControl,
        KeyCode.RightControl,
        KeyCode.LeftAlt,
        KeyCode.RightAlt

        #endregion
    };

    #endregion
    
    #region KeySettings

    private static KeyCode _up1;
    private static KeyCode _up2;
    
    private static KeyCode _down1;
    private static KeyCode _down2;
    
    private static KeyCode _attack1;
    private static KeyCode _attack2;

    #endregion

    #region KeyAccessor

    public KeyCode[] UP
    {
        get => new KeyCode[] { _up1, _up2 };
        set { _up1 = value[0]; _up2 = value[1]; }
    }
    
    public KeyCode[] DOWN
    {
        get => new KeyCode[] { _down1, _down2 };
        set { _down1 = value[0]; _down2 = value[1]; }
    }
    
    public KeyCode[] ATTACK
    {
        get => new KeyCode[] { _attack1, _attack2 };
        set { _attack1 = value[0]; _attack2 = value[1]; }
    }

    #endregion

    public void StartListen()
    {
        _listeningEvent = true;
#if UNITY_EDITOR
        Debug.Log($"[KeySetting] <color=#3ab549>Start</color> Listening from \"<color=cyan>{FileManager.GetFullPath(gameObject.transform)}</color>\"");
#endif
    }
    
    public void StopListen()
    {
        _listeningEvent = false;
#if UNITY_EDITOR
        Debug.Log($"[KeySetting] <color=#ee1b24>Stop</color> Listening from \"<color=cyan>{FileManager.GetFullPath(gameObject.transform)}</color>\"");
#endif
    }

    private string ConvertKeyCode(KeyCode keyCode)
    {
        string cKeyCode;
        switch (keyCode)
        {
            #region Numpad

            case KeyCode.Alpha0:
                cKeyCode = "0";
                break;
            case KeyCode.Alpha1:
                cKeyCode = "1";
                break;
            case KeyCode.Alpha2:
                cKeyCode = "2";
                break;
            case KeyCode.Alpha3:
                cKeyCode = "3";
                break;
            case KeyCode.Alpha4:
                cKeyCode = "4";
                break;
            case KeyCode.Alpha5:
                cKeyCode = "5";
                break;
            case KeyCode.Alpha6:
                cKeyCode = "6";
                break;
            case KeyCode.Alpha7:
                cKeyCode = "7";
                break;
            case KeyCode.Alpha8:
                cKeyCode = "8";
                break;
            case KeyCode.Alpha9:
                cKeyCode = "9";
                break;

            #endregion

            #region Arrow

            case KeyCode.UpArrow:
                cKeyCode = "↑";
                break;
            case KeyCode.DownArrow:
                cKeyCode = "↓";
                break;
            case KeyCode.LeftArrow:
                cKeyCode = "←";
                break;
            case KeyCode.RightArrow:
                cKeyCode = "→";
                break;

            #endregion

            #region Etc

            case KeyCode.BackQuote:
                cKeyCode = "~";
                break;
            case KeyCode.Minus:
                cKeyCode = "-";
                break;
            case KeyCode.Equals:
                cKeyCode = "=";
                break;
            case KeyCode.LeftShift:
                cKeyCode = "LShift";
                break;
            case KeyCode.RightShift:
                cKeyCode = "RShift";
                break;
            case KeyCode.CapsLock:
                cKeyCode = "Caps";
                break;
            case KeyCode.LeftControl:
                cKeyCode = "LCtrl";
                break;
            case KeyCode.RightControl:
                cKeyCode = "RCtrl";
                break;
            case KeyCode.LeftAlt:
                cKeyCode = "LAlt";
                break;
            case KeyCode.RightAlt:
                cKeyCode = "RAlt";
                break;

            #endregion
            
            default:
                cKeyCode = $"{keyCode}";
                break;
        }
        return cKeyCode;
    }
    
    public void UpdateSettings()
    {
        _upBtn1.text = ConvertKeyCode(UP[0]);
        _upBtn2.text = ConvertKeyCode(UP[1]);
        _downBtn1.text = ConvertKeyCode(DOWN[0]);
        _downBtn2.text = ConvertKeyCode(DOWN[1]);
        _attackBtn1.text = ConvertKeyCode(ATTACK[0]);
        _attackBtn2.text = ConvertKeyCode(ATTACK[1]);
    }
    
    private void Awake()
    {
        _listeningEvent = false;

        #region Init

        _up1 = SettingManager.Key_UP[0];
        _up2 = SettingManager.Key_UP[1];
    
        _down1 = SettingManager.Key_DOWN[0];
        _down2 = SettingManager.Key_DOWN[1];
    
        _attack1 = SettingManager.Key_ATTACK[0];
        _attack2 = SettingManager.Key_ATTACK[1];

        #endregion

        #region GetComponent
        
        _upBtn1 = upBtn1.GetComponentInChildren<TextMeshProUGUI>();
        _upBtn2 = upBtn2.GetComponentInChildren<TextMeshProUGUI>();
        _downBtn1 = downBtn1.GetComponentInChildren<TextMeshProUGUI>();
        _downBtn2 = downBtn2.GetComponentInChildren<TextMeshProUGUI>();
        _attackBtn1 = attackBtn1.GetComponentInChildren<TextMeshProUGUI>();
        _attackBtn2 = attackBtn2.GetComponentInChildren<TextMeshProUGUI>();

        #endregion
        
        #region AddOnclickListener

        upBtn1.GetComponent<Button>()?.onClick.AddListener(() =>
        {
            _upBtn1.text = " ";
            _currentListeningButton = KeyType.UP1;
        });
        upBtn2.GetComponent<Button>()?.onClick.AddListener(() =>
        {
            _upBtn2.text = " ";
            _currentListeningButton = KeyType.UP2;
        });
        downBtn1.GetComponent<Button>()?.onClick.AddListener(() =>
        {
            _downBtn1.text = " ";
            _currentListeningButton = KeyType.DOWN1;
        });
        downBtn2.GetComponent<Button>()?.onClick.AddListener(() =>
        {
            _downBtn2.text = " ";
            _currentListeningButton = KeyType.DOWN2;
        });
        attackBtn1.GetComponent<Button>()?.onClick.AddListener(() =>
        {
            _attackBtn1.text = " ";
            _currentListeningButton = KeyType.ATTACK1;
        });
        attackBtn2.GetComponent<Button>()?.onClick.AddListener(() =>
        {
            _attackBtn2.text = " ";
            _currentListeningButton = KeyType.ATTACK2;
        });

        #endregion
        
        UpdateSettings();
    }

    private void Update()
    {
        if (_listeningEvent == false) return;
        foreach (KeyCode t in _keyList)
        {
            if (Input.GetKey(t) == false) continue;
            _listeningEvent = false;
            switch (_currentListeningButton)
            {
                case KeyType.UP1:
                    UP = new KeyCode[] { t, UP[1] };
                    SettingManager.Key_UP = new KeyCode[] { t, UP[1] };
                    break;
                case KeyType.UP2:
                    UP = new KeyCode[] { UP[0], t };
                    SettingManager.Key_UP = new KeyCode[] { UP[0], t };
                    break;
                case KeyType.DOWN1:
                    DOWN = new KeyCode[] { t, DOWN[1] };
                    SettingManager.Key_DOWN = new KeyCode[] { t, DOWN[1] };
                    break;
                case KeyType.DOWN2:
                    DOWN = new KeyCode[] { DOWN[0], t };
                    SettingManager.Key_DOWN = new KeyCode[] { DOWN[0], t };
                    break;
                case KeyType.ATTACK1:
                    ATTACK = new KeyCode[] { t, ATTACK[1] };
                    SettingManager.Key_ATTACK = new KeyCode[] { t, ATTACK[1] };
                    break;
                case KeyType.ATTACK2:
                    ATTACK = new KeyCode[] { ATTACK[0], t };
                    SettingManager.Key_ATTACK = new KeyCode[] { ATTACK[0], t };
                    break;
            }
            UpdateSettings();
            eUnityEvent?.Invoke();
            break;
        }
    }
}
