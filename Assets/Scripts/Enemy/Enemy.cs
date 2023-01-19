using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;
using MyBox;

public class Enemy : MonoBehaviour
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

    #region Feedbacks

    public virtual void Init()
    {
        gameObject.SetActive(true);
        initFeedback.PlayFeedbacks();
    }
    
    public virtual void Attack()
    {
        attackFeedback.PlayFeedbacks();
    }
    
    public virtual void Die()
    {
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

    private void Update()
    {
        if (active == false || _walkTrigger == false) return;
        transform.Translate(new Vector3(-1, 0, 0) * (speed * Time.deltaTime));
    }
    
    private void OnDisable()
    {
        _walkTrigger = false;
    }

    #endregion
    
}
