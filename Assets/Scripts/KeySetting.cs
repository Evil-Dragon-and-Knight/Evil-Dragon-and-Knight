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
        KeyCode.Z
    };

    #endregion
    
    #region KeySettings

    private static KeyCode _up1 = KeyCode.W;
    private static KeyCode _up2 = KeyCode.UpArrow;
    
    private static KeyCode _down1 = KeyCode.S;
    private static KeyCode _down2 = KeyCode.DownArrow;
    
    private static KeyCode _attack1 = KeyCode.LeftShift;
    private static KeyCode _attack2 = KeyCode.Space;

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

    public void UpdateSettings()
    {
        _upBtn1.text = $"{UP[0]}";
        _upBtn2.text = $"{UP[1]}";
        _downBtn1.text = $"{DOWN[0]}";
        _downBtn2.text = $"{DOWN[1]}";
        _attackBtn1.text = $"{ATTACK[0]}";
        _attackBtn2.text = $"{ATTACK[1]}";
    }
    
    private void Awake()
    {
        _listeningEvent = false;

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
                    _upBtn1.text = $"{t}";
                    break;
                case KeyType.UP2:
                    UP = new KeyCode[] { UP[0], t };
                    _upBtn2.text = $"{t}";
                    break;
                case KeyType.DOWN1:
                    DOWN = new KeyCode[] { t, DOWN[1] };
                    _downBtn1.text = $"{t}";
                    break;
                case KeyType.DOWN2:
                    DOWN = new KeyCode[] { DOWN[0], t };
                    _downBtn2.text = $"{t}";
                    break;
                case KeyType.ATTACK1:
                    ATTACK = new KeyCode[] { t, ATTACK[1] };
                    _attackBtn1.text = $"{t}";
                    break;
                case KeyType.ATTACK2:
                    ATTACK = new KeyCode[] { ATTACK[0], t };
                    _attackBtn2.text = $"{t}";
                    break;
            }
            eUnityEvent?.Invoke();
            break;
        }
    }
}
