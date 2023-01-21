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
}
