using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;
using MyBox;

public class BossDragon : Enemy
{
    [Separator("BossDragon Settings")] 
    [SerializeField] private GameObject fireBall;
    [SerializeField] private Transform spawnLocation;

    public override void Attack()
    {
        attackFeedback.PlayFeedbacks();
    }

    public void SpawnFireBall()
    {
        GameObject temp = Instantiate(fireBall);
        temp.transform.position = spawnLocation.transform.position;
        temp.GetComponent<Enemy>().Init();
    }
}
