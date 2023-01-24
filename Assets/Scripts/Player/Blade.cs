using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using MyBox;
using UnityEngine;

public class Blade : MonoBehaviour
{
    [Separator("Default Settings")] 
    [SerializeField] protected MMF_Player initFeedback;
    [SerializeField] protected MMF_Player attackFeedback;
    [SerializeField] protected MMF_Player dieFeedback;

    [Separator("Movement Settings")] 
    [SerializeField] private bool active;
    [ConditionalField(nameof(active), false)] [PositiveValueOnly] [SerializeField]
    private float speed = 0f;
    private bool _walkTrigger = false;
    private bool _walkTriggerOffAbsolute = false;
    protected Rigidbody2D _rigidbody2D;

    #region Feedbacks

    // ReSharper disable Unity.PerformanceAnalysis
    public virtual void Init()
    {
        // Off On for Reset Feedbacks
        gameObject.SetActive(false);
        gameObject.SetActive(true);

        initFeedback.PlayFeedbacks();
    }
    
    public virtual void Attack()
    {
        _walkTriggerOffAbsolute = true;
        attackFeedback.PlayFeedbacks();
    }
    
    public virtual void Die()
    {
        _walkTriggerOffAbsolute = true;
        dieFeedback.PlayFeedbacks();
    }

    #endregion

    #region Movement

    public void Walk()
    {
        _walkTrigger = true;
    }

    public void WalkEnd()
    {
        _walkTrigger = false;
    }

    #endregion

    #region Event

    private void Awake()
    {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }
    
    private void FixedUpdate()
    {
        if (active == false || _walkTrigger == false || _walkTriggerOffAbsolute) return;
        _rigidbody2D.MovePosition(_rigidbody2D.position + new Vector2(speed, 0f) * Time.fixedDeltaTime);
        // transform.Translate(new Vector3(-1, 0, 0) * (speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag($"DeadZone"))
        {
            dieFeedback.PlayFeedbacks();
            return;
        }
        if (!col.CompareTag($"Enemy")) return;
        col.GetComponent<Enemy>().Die();
    }

    private void OnEnable()
    {
        _walkTrigger = false;
        _walkTriggerOffAbsolute = false;
    }

    private void OnDisable()
    {
        _walkTrigger = false;
        _walkTriggerOffAbsolute = false;
    }

    #endregion
}
