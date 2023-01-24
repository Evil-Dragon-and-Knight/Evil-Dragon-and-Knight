using System.Collections;
using System.Collections.Generic;
using MyBox;
using UnityEngine;

public class LoadingTrigger : MonoBehaviour
{
    [Scene] [SerializeField] 
    private string loadScene;

    public void Load()
    {
        LoadingController.LoadScene(loadScene);
    }
}
