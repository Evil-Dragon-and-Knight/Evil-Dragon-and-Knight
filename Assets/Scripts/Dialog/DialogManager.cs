using System.Collections;
using System.Collections.Generic;
using MyBox;
using UnityEngine;
using UnityEngine.Events;

public class DialogManager : MonoBehaviour
{
    [Separator("Default Settings")] 
    [SerializeField] protected UnityEvent initEvents;
    
    public Dialog playerDialog;
    public Dialog enemyDialog;
    
    [Separator("Scene Settings")] 
    [SerializeField] private DialogTrigger[] sceneList;
    
    [Separator("Etc Settings")] 
    [SerializeField] protected UnityEvent exitEvents;
    
    private int _currentScene = -1;

    public void Play()
    {
        initEvents.Invoke();
        PlayNextScene();
    }

    public void PlayNextScene()
    {
        if (_currentScene + 1 > sceneList.Length - 1)
        {
            exitEvents.Invoke();
            return;
        }
        sceneList[++_currentScene].Play();
    }
}
