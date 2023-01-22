using System.Collections;
using System.Collections.Generic;
using System.Threading;
using MyBox;
using TMPro;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    [Separator("Default Settings")] [PositiveValueOnly] [Range(0, 1)] [SerializeField]
    private double typingSpeed = 0.015d;
    
    [Separator("Dialog Settings")] 
    [SerializeField] private GameObject model;
    [SerializeField] private TextMeshProUGUI dialogName;
    [SerializeField] private TextMeshProUGUI dialogText;

    [HideInInspector] public bool typeDone = false;
    
    private string _name;
    private string _text;
    
    public void Show(string name, string text)
    {
        _name = name;
        _text = text;
        typeDone = false;
        model.SetActive(true);
        SetName();
        StopAllCoroutines();
        StartCoroutine(TypeSentence());
    }

    public void Hide()
    {
        typeDone = false;
        StopAllCoroutines();
        model.SetActive(false);
    }

    public void SetName()
    {
        dialogName.text = _name;
    }

    public void TypeInstant()
    {
        StopAllCoroutines();
        typeDone = true;
        dialogText.text = _text;
    }
    
    IEnumerator TypeSentence()
    {
        dialogText.text = "";
        foreach (char letter in _text)
        {
            dialogText.text += letter;
            Thread.Sleep((int)(typingSpeed * 1000));
            yield return null;
        }
        typeDone = true;
    }
}
