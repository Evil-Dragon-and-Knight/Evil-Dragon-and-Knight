using System;
using System.Collections;
using System.Collections.Generic;
using MyBox;
using UnityEngine;
using UnityEngine.Events;

public class DialogTrigger : MonoBehaviour
{
    [Separator("Default Settings")] 
    [SerializeField] protected UnityEvent initEvents;
    [SerializeField] private DialogManager targetManager;

    [Separator("Dialog Settings")] 
    [DefinedValues("Custom", "Player", "Enemy")] [SerializeField]
    private string dialogType = "Custom";

    [ConditionalField(nameof(dialogType), false, "Custom")] [SerializeField]
    private Dialog customDialog;
    
    [SerializeField] private new string name;
    [TextArea(4, 4)] [SerializeField] 
    private string text;
    
    [Separator("Etc Settings")] 
    [SerializeField] protected UnityEvent exitEvents;

    public void Play()
    {
        initEvents.Invoke();
        customDialog.Show(name, text);
    }

    private void Exit()
    {
        customDialog.Hide();
        exitEvents.Invoke();
        targetManager.PlayNextScene();
        gameObject.SetActive(false);
    }

    private void CheckInput()
    {
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

    private void Update()
    {
        CheckInput();
    }
}
