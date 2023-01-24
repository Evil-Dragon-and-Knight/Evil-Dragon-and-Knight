using System;
using System.Collections;
using System.Collections.Generic;
using MyBox;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnController : MonoBehaviour
{
    [Separator("Enemy Spawn Point Settings")] 
    [SerializeField] private Transform dragonSpawnPoint1;
    [SerializeField] private Transform dragonSpawnPoint2;
    [SerializeField] private Transform foxSpawnPoint;
    
    [Separator("Enemy Spawn Frequency Setting")]
    [SerializeField] [MinMaxRange(0, 10)]
    private RangedFloat spawnFrequency = new RangedFloat(2f, 5f); 

    [Separator("Boss Spawn Point Settings")] 
    [SerializeField] private Transform bossDragonSpawnPoint;

    private bool _spawnEnable = false;
    private float _nextFrequency;
    private float _currentFrequency;

    public void Level1()
    {
        spawnFrequency = new RangedFloat(2f, 5f);
    }

    public void Level2()
    {
        spawnFrequency = new RangedFloat(1f, 2f);
    }
    
    public void BossAttackable(bool value)
    {
        BossDragon.BossAttackable(value);
    }

    private void GetNewFrequency()
    {
        _currentFrequency = 0f;
        _nextFrequency = Random.Range(spawnFrequency.Min, spawnFrequency.Max);
    }

    public void SpawnEnable()
    {
        GetNewFrequency();
        _spawnEnable = true;
    }
    
    public void SpawnDisable()
    {
        _spawnEnable = false;
    }

    public void KillEverySpawnObject(string tag)
    {
        ObjectPooling.KillEveryActiveObject(tag);
    }

    public void InactiveBoss()
    {
        ObjectPooling.InactiveEveryActiveObject("Boss");
    }
    
    public void SpawnBoss()
    {
        GameObject temp = ObjectPooling.GetObject("Enemy@BossDragon");
        temp.transform.position = bossDragonSpawnPoint.transform.position;
        temp.GetComponent<BossDragon>().Init();
    }
    
    private void SpawnEnemy()
    {
        int randomRange = Random.Range(0, 3);
        GameObject temp;
        switch (randomRange)
        {
            case 0:
                temp = ObjectPooling.GetObject("Enemy@Dragon");
                // temp = Instantiate(dragonPrefab);
                temp.transform.position = dragonSpawnPoint1.transform.position;
                temp.GetComponent<Enemy>().Init();
                break;
            case 1:
                temp = ObjectPooling.GetObject("Enemy@Dragon");
                // temp = Instantiate(dragonPrefab);
                temp.transform.position = dragonSpawnPoint2.transform.position;
                temp.GetComponent<Enemy>().Init();
                break;
            case 2:
                temp = ObjectPooling.GetObject("Enemy@Fox");
                // temp = Instantiate(foxPrefab);
                temp.transform.position = foxSpawnPoint.transform.position;
                temp.GetComponent<Enemy>().Init();
                break;
        }
    }
    
    #region Event

    private void Start()
    {
        GetNewFrequency();
    }

    private void Update()
    {
        if (!_spawnEnable)
        {
            _currentFrequency = 0;
            return;
        }
        _currentFrequency += Time.deltaTime;
        if (_currentFrequency >= _nextFrequency)
        {
            GetNewFrequency();
            SpawnEnemy();
        }
    }

    #endregion
}
