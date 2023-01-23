using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Enemy _enemy;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
    }

    public void Attack()
    {
        if(_enemy == null) return;
        _enemy.Die();
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag($"Enemy")) return;
        _enemy = col.GetComponent<Enemy>();
    }

    private void OnTriggerExit(Collider other)
    {
        _enemy = null;
    }

    private void OnDisable()
    {
        _enemy = null;
    }
}
