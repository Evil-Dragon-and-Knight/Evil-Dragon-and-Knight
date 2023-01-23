using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform[] locations;
    [SerializeField] private ObjectPoolingItem blade;
    [SerializeField] private PlayerAttack playerAttack;

    private int currentLocation;
    private bool _canControl;
    private static readonly int Flying = Animator.StringToHash("Flying");
    private static readonly int Attack = Animator.StringToHash("Attack");

    private float moveCoolDown;
    private float currentMoveCoolDown;
    
    private float attackCoolDown;
    private float currentAttackCoolDown;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        moveCoolDown = 0f;
        currentMoveCoolDown = 0f;
        
        attackCoolDown = 0f;
        currentAttackCoolDown = 0f;
        
        _canControl = false;
        currentLocation = locations.Length - 1;
        transform.position = locations[currentLocation].position;
        
        animator.ResetTrigger(Attack);
        animator.SetBool(Flying, false);
    }

    public void Enable()
    {
        _canControl = true;
    }

    public void SpawnBlade()
    {
        GameObject temp = ObjectPooling.GetObject(blade.name);
        temp.transform.position = transform.position;
        temp.GetComponent<Blade>().Init();
    }

    public void NormalAttack()
    {
        playerAttack.Attack();
    }
    
    private void Update()
    {
        if (!_canControl) return;
        currentMoveCoolDown += Time.deltaTime;
        currentAttackCoolDown += Time.deltaTime;
        // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
        MoveListener();
        AttackListener();
    }

    private void MoveListener()
    {
        if (currentMoveCoolDown < moveCoolDown) return;

        int newLocation = currentLocation;

        // UP
        if (Input.GetKeyDown(SettingManager.Instance.Key_UP[0]) || Input.GetKeyDown(SettingManager.Instance.Key_UP[1]))
        {
            moveCoolDown = 0.25f;
            currentMoveCoolDown = 0f;
            
            newLocation -= 1;
            newLocation = newLocation < 0 ? 0 : newLocation;
            newLocation = newLocation > locations.Length - 1 ? locations.Length - 1 : newLocation;
            
            animator.ResetTrigger(Attack);
            animator.SetBool(Flying, newLocation != locations.Length - 1);
            animator.SetTrigger(Attack);

            currentLocation = newLocation;
            transform.position = locations[currentLocation].position;
            
            Invoke(nameof(NormalAttack), 0.25f);
            return;
        }
        
        // DOWN
        if (Input.GetKeyDown(SettingManager.Instance.Key_DOWN[0]) || Input.GetKeyDown(SettingManager.Instance.Key_DOWN[1]))
        {
            moveCoolDown = 0.25f;
            currentMoveCoolDown = 0f;
            
            newLocation += 1;
            newLocation = newLocation < 0 ? 0 : newLocation;
            newLocation = newLocation > locations.Length - 1 ? locations.Length - 1 : newLocation;
            
            animator.ResetTrigger(Attack);
            animator.SetBool(Flying, newLocation != locations.Length - 1);
            animator.SetTrigger(Attack);

            currentLocation = newLocation;
            transform.position = locations[currentLocation].position;
            
            Invoke(nameof(NormalAttack), 0.25f);
            return;
        }
    }
    
    private void AttackListener()
    {
        if (currentMoveCoolDown < moveCoolDown) return;
        if (currentAttackCoolDown < attackCoolDown) return;
        
        // ATTACK
        if (Input.GetKeyDown(SettingManager.Instance.Key_ATTACK[0]) || Input.GetKeyDown(SettingManager.Instance.Key_ATTACK[1]))
        {
            moveCoolDown = 0.25f;
            currentMoveCoolDown = 0f;
            attackCoolDown = 5f;
            currentAttackCoolDown = 0f;
            
            animator.ResetTrigger(Attack);
            animator.SetTrigger(Attack);
            
            Invoke(nameof(SpawnBlade), 0.25f);
            return;
        }
    }
}
