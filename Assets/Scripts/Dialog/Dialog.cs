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

    public bool typeDone = false;
    
    private new string _name;
    private new string _text;
    
    public void Show(string name, string text)
    {
        model.SetActive(true);
        typeDone = false;
        _name = name;
        _text = text;
        SetName();
        StopAllCoroutines();
        StartCoroutine(TypeSentence());
    }

    public void Hide()
    {
        typeDone = false;
        model.SetActive(false);
        StopAllCoroutines();
    }

    public void SetName()
    {
        dialogName.text = _name;
    }

    public void TypeInstant()
    {
        StopAllCoroutines();
        dialogText.text = _text;
        typeDone = true;
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
