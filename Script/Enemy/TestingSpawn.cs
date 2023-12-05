using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TestingSpawn : MonoBehaviour
{
    public int minionCreated; //use to decrement minionSPawn to enable canDamage from EnemyBoss
    [SerializeField] Transform[] EnemySpawn; //SpawnPos
    [SerializeField] GameObject EnemyPrefab;
    public bool hasSpawned;
    void Start()
    {
        hasSpawned = false;
    }
    public void SpawnEnemy(int Spawn)
    {
        int minionCreated = Spawn;
        for (int i = 0; i <= Spawn; i++)
        {
            int randomSpawn = Random.Range(0, EnemySpawn.Length);
            //Debug.Log(i);
            Instantiate(EnemyPrefab, EnemySpawn[randomSpawn].position, Quaternion.identity);
        }
    }
}
