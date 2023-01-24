using System.Collections;
using System.Collections.Generic;
using MyBox;
using UnityEngine;
using UnityEngine.Events;

public class DialogManager : MonoBehaviour
{
    [Separator("Default Settings")] 
    public Dialog playerDialog;
    public Dialog enemyDialog;
    
    [Separator("Scene Settings")] 
    [SerializeField] private DialogTrigger[] sceneList;
#if UNITY_EDITOR
    [ButtonMethod]
    private void AutoSceneFinder()
    {
        List<DialogTrigger> dump = new List<DialogTrigger>();
        foreach (Transform child in transform)
        {
            DialogTrigger temp = child.GetComponent<DialogTrigger>();
            if (temp == null) continue;
            dump.Add(temp);
        }
        sceneList = dump.ToArray();
    }
#endif
    
    [Separator("Etc Settings")] 
    [SerializeField] protected UnityEvent initEvents;
    [SerializeField] protected UnityEvent exitEvents;

    private bool _checkInput = false;
    private int _currentScene = -1;

    public void Play()
    {
        initEvents.Invoke();
        _checkInput = true;
        PlayNextScene();
    }

    public void PlayNextScene()
    {
        if (_currentScene + 1 > sceneList.Length - 1)
        {
            _checkInput = false;
            exitEvents.Invoke();
            return;
        }
        sceneList[++_currentScene].Play();
    }
    
    private void Update()
    {
        if (!_checkInput) return;
        sceneList[_currentScene].CheckInput();
    }
}
