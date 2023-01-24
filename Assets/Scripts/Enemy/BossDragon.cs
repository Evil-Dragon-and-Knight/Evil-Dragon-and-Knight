using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;
using MyBox;
using Unity.VisualScripting;
using Random = UnityEngine.Random;

public class BossDragon : Enemy
{
    [Separator("BossDragon Settings")] 
    [SerializeField] private Transform bossSpawnLocation;
    [SerializeField] private ObjectPoolingItem fireball;
    [SerializeField] private Transform fireballSpawnLocation;

    [Separator("BossDragon Attack Settings")] 
    [SerializeField] [MinMaxRange(0, 10)]
    private RangedFloat attackFrequency = new RangedFloat(2f, 5f); 
    [SerializeField] private Transform[] attackLocations;
    [SerializeField] private GameObject target;
    
    private static bool canAttack = false;
    private bool attacking = false;

    public override void Init()
    {
        canAttack = false;
        
        transform.position = new Vector3(13, bossSpawnLocation.position.y, bossSpawnLocation.position.z);

        // Off On for Reset Feedbacks
        gameObject.SetActive(false);
        gameObject.SetActive(true);

        initFeedback.PlayFeedbacks();
    }

    public void Reset()
    {
        canAttack = false;
        
        transform.position = bossSpawnLocation.position;

        // Off On for Reset Feedbacks
        gameObject.SetActive(false);
        gameObject.SetActive(true);

        initFeedback.PlayFeedbacks();
    }

    public static bool IsBossAttackable()
    {
        return canAttack;
    }

    public static void BossAttackable(bool value)
    {
        canAttack = value;
    }

    public override void Attack()
    {
        attackFeedback.PlayFeedbacks();
    }
    
    public override void Die()
    {
        dieFeedback.PlayFeedbacks();
    }

    public void SpawnFireBall()
    {
        GameObject temp = ObjectPooling.GetObject(fireball.name);
        temp.transform.position = fireballSpawnLocation.transform.position;
        temp.GetComponent<Enemy>().Init();
    }

    private void FixedUpdate()
    {   
        MoveFront();
        if (!attacking)
        {
            RandomAttack();
        }
    }

    private void MoveFront()
    {
        if (transform.position.x <= bossSpawnLocation.position.x) return;
        transform.Translate(new Vector3(-1, 0, 0) * (2 * Time.deltaTime));
    }
    
    private void RandomAttack()
    {
        if (!canAttack) return;
        target.gameObject.SetActive(true);
        
        attacking = true;
        transform.position = attackLocations[Random.Range(0, attackLocations.Length)].transform.position;
        Attack();
        
        Invoke(nameof(RandomAttack), Random.Range(attackFrequency.Min, attackFrequency.Max + 1));
    }
    
    private void OnDisable()
    {
        canAttack = false;
        attacking = false;
        target.gameObject.SetActive(false);
    }
}
