using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    [SerializeField] private KeyCode key = KeyCode.Escape;
    [SerializeField] private UnityEvent eUnityEvent;
    private void Update()
    {
        if (Input.GetKeyDown(key))
        {
            eUnityEvent?.Invoke();
        }
    }
}
