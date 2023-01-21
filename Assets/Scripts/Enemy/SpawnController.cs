using System;
using System.Collections;
using System.Collections.Generic;
using MyBox;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnController : MonoBehaviour
{
    [Separator("Object Pooing Settings")] [SerializeField]
    private ObjectPooling objectPollingController;
    
    [Separator("Enemy Spawn Point Settings")] 
    [SerializeField] private Transform dragonSpawnPoint1;
    [SerializeField] private Transform dragonSpawnPoint2;
    [SerializeField] private Transform foxSpawnPoint;
    
    [Separator("Enemy Spawn Frequency Setting")]
    [SerializeField] [MinMaxRange(0, 10)]
    private RangedFloat spawnFrequency = new RangedFloat(2f, 5f); 

    [Separator("Boss Spawn Point Settings")] 
    [SerializeField] private Transform bossDragonSpawnPoint;

    private float _nextFrequency;
    private float _currentFrequency;

    private void GetNewFrequency()
    {
        _currentFrequency = 0f;
        _nextFrequency = Random.Range(spawnFrequency.Min, spawnFrequency.Max);
    }
    
    private void SpawnEnemy()
    {
        int randomRange = Random.Range(0, 3);
        GameObject temp;
        switch (randomRange)
        {
            case 0:
                temp = objectPollingController.GetObject("Enemy@Dragon");
                // temp = Instantiate(dragonPrefab);
                temp.transform.position = dragonSpawnPoint1.transform.position;
                temp.GetComponent<Enemy>().Init();
                break;
            case 1:
                temp = objectPollingController.GetObject("Enemy@Dragon");
                // temp = Instantiate(dragonPrefab);
                temp.transform.position = dragonSpawnPoint2.transform.position;
                temp.GetComponent<Enemy>().Init();
                break;
            case 2:
                temp = objectPollingController.GetObject("Enemy@Fox");
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
        _currentFrequency += Time.deltaTime;
        if (_currentFrequency >= _nextFrequency)
        {
            GetNewFrequency();
            SpawnEnemy();
        }
    }

    #endregion
}
