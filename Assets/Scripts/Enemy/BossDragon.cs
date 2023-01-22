using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;
using MyBox;

public class BossDragon : Enemy
{
    [Separator("Object Pooing Settings")] [SerializeField]
    private ObjectPooling objectPollingController;
    
    [Separator("BossDragon Settings")] 
    [SerializeField] private GameObject fireBall;
    [SerializeField] private Transform spawnLocation;

    public override void Init()
    {
        var position = transform.position;
        position = new Vector3(13, position.y, position.z);
        transform.position = position;

        // Off On for Reset Feedbacks
        gameObject.SetActive(false);
        gameObject.SetActive(true);

        initFeedback.PlayFeedbacks();
    }
    
    public override void Attack()
    {
        attackFeedback.PlayFeedbacks();
    }

    public void SpawnFireBall()
    {
        GameObject temp = objectPollingController.GetObject("Enemy@Fireball");
        temp.transform.position = spawnLocation.transform.position;
        temp.GetComponent<Enemy>().Init();
    }

    private void FixedUpdate()
    {
        if (transform.position.x <= 6)
        {
            // var position = transform.position;
            // position = new Vector3(6, position.y, position.z);
            // transform.position = position;
            return;
        }
        transform.Translate(new Vector3(-1, 0, 0) * (2 * Time.deltaTime));
    }
}
