using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;
using MyBox;

public class BossDragon : Enemy
{
    [Separator("BossDragon Settings")] 
    [SerializeField] private Transform bossSpawnLocation;
    [SerializeField] private ObjectPoolingItem fireball;
    [SerializeField] private Transform fireballSpawnLocation;

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
        GameObject temp = ObjectPooling.Instance.GetObject(fireball.name);
        temp.transform.position = fireballSpawnLocation.transform.position;
        temp.GetComponent<Enemy>().Init();
    }

    private void FixedUpdate()
    {
        if (transform.position.x <= bossSpawnLocation.position.x)
        {
            // var position = transform.position;
            // position = new Vector3(6, position.y, position.z);
            // transform.position = position;
            return;
        }
        transform.Translate(new Vector3(-1, 0, 0) * (2 * Time.deltaTime));
    }
}
