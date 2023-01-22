using System;
using System.Collections;
using System.Collections.Generic;
using MyBox;
using UnityEngine;
using UnityEngine.Events;

public class DialogTrigger : MonoBehaviour
{
    [Separator("Default Settings")] 
    [SerializeField] private DialogManager targetManager;
    
    [Separator("Delay Settings")] 
    [PositiveValueOnly] [SerializeField] private float before = 0f;
    [PositiveValueOnly] [SerializeField] private float after = 0f;

    [Separator("Dialog Settings")] 
    [DefinedValues("Custom", "Player", "Enemy")] [SerializeField]
    private string dialogType = "Custom";

    [ConditionalField(nameof(dialogType), false, "Custom")] [SerializeField]
    private Dialog customDialog;
    
    [SerializeField] private new string name;
    
    [TextArea(4, 4)] [SerializeField] 
    private string text;
    
    [Separator("Etc Settings")]
    [SerializeField] protected UnityEvent initEvents;
    [SerializeField] protected UnityEvent exitEvents;

    private bool actionDone = true;

    public void Play()
    {
        initEvents.Invoke();
        actionDone = true;
        Invoke(nameof(Before), before);
    }

    private void Exit()
    {
        actionDone = true;
        customDialog.Hide();
        exitEvents.Invoke();
        Invoke(nameof(After), after);
    }

    private void Before()
    {
        actionDone = false;
        customDialog.Show(name, text);
    }

    private void After()
    {
        targetManager.PlayNextScene();
    }

    public void CheckInput()
    {
        if (actionDone) return;
        if (!Input.GetKeyDown(KeyCode.Space)) return;
        switch (customDialog.typeDone)
        {
            case false:
                customDialog.TypeInstant();
                break;
            case true:
                Exit();
                break;
        }
    }

    private void Start()
    {
        if (dialogType == "Custom") return;
        switch (dialogType)
        {
            case "Player":
                customDialog = targetManager.playerDialog;
                break;
            case "Enemy":
                customDialog = targetManager.enemyDialog;
                break;
        }
    }
}
