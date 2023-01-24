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

    [SerializeField] private GameObject skillInactive;
    [SerializeField] private GameObject skillActive;

    private int _currentLocation;
    private bool _canControl;
    private static readonly int Flying = Animator.StringToHash("Flying");
    private static readonly int Attack = Animator.StringToHash("Attack");

    private const float _defaultMoveCoolDown = 0.35f;
    private float _moveCoolDown;
    private float _currentMoveCoolDown;
    
    private const float _defaultAttackCoolDown = 10f;
    private float _attackCoolDown;
    private float _currentAttackCoolDown;

    private void Start()
    {
        Init();
        
        _moveCoolDown = 0f;
        _currentMoveCoolDown = 0f;
        
        _attackCoolDown = 0f;
        _currentAttackCoolDown = 0f;
        
        SkillActive();
    }

    public void Init()
    {
        _canControl = false;
        _currentLocation = locations.Length - 1;
        transform.position = locations[_currentLocation].position;
        
        animator.ResetTrigger(Attack);
        animator.SetBool(Flying, false);
    }

    private void SkillActive()
    {
        skillInactive.SetActive(false);
        skillActive.SetActive(true);
    }

    private void SkillInactive()
    {
        skillInactive.SetActive(true);
        skillActive.SetActive(false);
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
        _currentMoveCoolDown += Time.deltaTime;
        _currentAttackCoolDown += Time.deltaTime;
        // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
        MoveListener();
        AttackListener();
    }

    private void MoveListener()
    {
        if (_currentMoveCoolDown < _moveCoolDown) return;

        int newLocation = _currentLocation;

        // UP
        if (Input.GetKeyDown(SettingManager.Instance.Key_UP[0]) || Input.GetKeyDown(SettingManager.Instance.Key_UP[1]))
        {
            _moveCoolDown = _defaultMoveCoolDown;
            _currentMoveCoolDown = 0f;

            newLocation -= 1;
            newLocation = newLocation < 0 ? 0 : newLocation;
            newLocation = newLocation > locations.Length - 1 ? locations.Length - 1 : newLocation;
            
            if (newLocation != _currentLocation)
            {
                playerAttack._enemy = null;
            }
            
            animator.ResetTrigger(Attack);
            animator.SetBool(Flying, newLocation != locations.Length - 1);
            animator.SetTrigger(Attack);

            _currentLocation = newLocation;
            transform.position = locations[_currentLocation].position;
            
            Invoke(nameof(NormalAttack), 0.35f);
            return;
        }
        
        // DOWN
        if (Input.GetKeyDown(SettingManager.Instance.Key_DOWN[0]) || Input.GetKeyDown(SettingManager.Instance.Key_DOWN[1]))
        {
            _moveCoolDown = _defaultMoveCoolDown;
            _currentMoveCoolDown = 0f;

            newLocation += 1;
            newLocation = newLocation < 0 ? 0 : newLocation;
            newLocation = newLocation > locations.Length - 1 ? locations.Length - 1 : newLocation;

            if (newLocation != _currentLocation)
            {
                playerAttack._enemy = null;
            }
            
            animator.ResetTrigger(Attack);
            animator.SetBool(Flying, newLocation != locations.Length - 1);
            animator.SetTrigger(Attack);

            _currentLocation = newLocation;
            transform.position = locations[_currentLocation].position;
            
            Invoke(nameof(NormalAttack), 0.35f);
            return;
        }
    }
    
    private void AttackListener()
    {
        if (_currentMoveCoolDown < _moveCoolDown) return;
        if (_currentAttackCoolDown < _attackCoolDown)
        {
            SkillInactive();
            return;
        };

        SkillActive();
        
        // ATTACK
        if (Input.GetKeyDown(SettingManager.Instance.Key_ATTACK[0]) || Input.GetKeyDown(SettingManager.Instance.Key_ATTACK[1]))
        {
            _moveCoolDown = _defaultMoveCoolDown;
            _currentMoveCoolDown = 0f;
            _attackCoolDown = _defaultAttackCoolDown;
            _currentAttackCoolDown = 0f;

            animator.ResetTrigger(Attack);
            animator.SetTrigger(Attack);
            
            Invoke(nameof(SpawnBlade), 0.25f);
            return;
        }
    }
}
