using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class EnemyMovement : MonoBehaviour
{
    [PositiveValueOnly] [SerializeField]
    private float animationDuration = 0f;
    
    [PositiveValueOnly] [SerializeField]
    private float speed = 0f;

    private bool _walkTrigger = false;

    public void Walk()
    {
        _walkTrigger = true;
        Invoke(nameof(WalkEnd), animationDuration);
    }

    private void WalkEnd()
    {
        _walkTrigger = false;
    }

    private void Update()
    {
        if (_walkTrigger == false) return;
        transform.Translate(new Vector3(-1, 0, 0) * (speed * Time.deltaTime));
    }
}
