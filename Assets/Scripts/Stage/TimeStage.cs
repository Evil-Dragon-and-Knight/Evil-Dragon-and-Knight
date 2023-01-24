using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class TimeStage : MonoBehaviour
{
    [SerializeField] private float timeRemaining = 60.0f;
    [SerializeField] private TextMeshProUGUI _text;
    
    private bool timerIsRunning = false;
    
    [SerializeField] private UnityEvent uEvent;
    
    private void Update()
    {
        if (!timerIsRunning) return;
        
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            TimeSpan time = TimeSpan.FromSeconds(timeRemaining);
            _text.text = $"{time:mm\\:ss}";
        }
        else
        {
            uEvent.Invoke();
            timerIsRunning = false;
        }
    }

    private void OnEnable()
    {
        timerIsRunning = true;
    }
    
    private void OnDisable()
    {
        timerIsRunning = false;
    }
}
