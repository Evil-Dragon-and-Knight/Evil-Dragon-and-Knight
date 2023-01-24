using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenResolution : MonoBehaviour
{
    [SerializeField] private bool active = true;

    [Header("Resolution")]
    [SerializeField] private int width = 1920;
    [SerializeField] private int height = 1080;
    
    private Camera _camera;
    
    private void Awake()
    {
        if (active == false) return;
        
        Screen.SetResolution(width, height, true);
        
        // _camera = GetComponent<Camera>();
        // if (_camera == null) return;
        // Rect rect = _camera.rect;
        // float scaleHeight = ((float)Screen.width / Screen.height) / ((float)width / height);
        // float scaleWidth = 1f / scaleHeight;
        // if (scaleHeight < 1)
        // {
        //     rect.height = scaleHeight;
        //     rect.y = (1f - scaleHeight) / 2f;
        // }
        // else
        // {
        //     rect.width = scaleWidth;
        //     rect.x = (1f - scaleWidth) / 2f;
        // }
        // _camera.rect = rect;
    }
}