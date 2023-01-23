using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    private HpController _player;
    private Enemy _enemy;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
    }

    public void Attack(int damage)
    {
        if(_player == null) return;
        _player.Decrease(damage);
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;
        if (_player != null) return;
        _player = col.GetComponent<HpController>();
        _enemy.Attack();
    }
    
    private void OnDisable()
    {
        _player = null;
    }
}
