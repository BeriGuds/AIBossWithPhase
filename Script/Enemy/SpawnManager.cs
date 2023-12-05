using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //Attach Gameobject to access Script
    public GameObject Pfab_Boss; //testing if can use in prefab with Script

    public EnemyBoss enemyBoss;

    [SerializeField] Transform[] EnemySpawn; //SpawnPos for Enemy
    [SerializeField] GameObject EnemyPrefab; //Minion Prefab

    public bool hasSpawned;

    /*public int minionCreated; *///use to decrement minionSPawn to enable canDamage from EnemyBoss
    public int currentSpawned = 0;
    void Awake()
    {
        enemyBoss = Pfab_Boss.GetComponent<EnemyBoss>();
    }
    void Start()
    {
        //currentSpawned = 0;
        hasSpawned = false;
    }
    public void SpawnEnemy(int Spawn)
    {
        for (int i = 1; i < Spawn + 1; i++)
        {
            
            int randomSpawn = Random.Range(0, EnemySpawn.Length);
            GameObject EnemyMinion = Instantiate(EnemyPrefab, EnemySpawn[randomSpawn].position, Quaternion.identity); //Real One

            TestingMinion E_Minion = EnemyMinion.gameObject.AddComponent<TestingMinion>();
            currentSpawned += 1;
            //BEFORE CHANGE
            //int randomSpawn = Random.Range(0, EnemySpawn.Length);
            //Instantiate(EnemyPrefab, EnemySpawn[randomSpawn].position, Quaternion.identity);
        }
        
    }

    public void NoMoreEnemy()
    {
        currentSpawned--;
         
        Debug.Log(currentSpawned);
        if (currentSpawned == 0f)
        {
            enemyBoss.canDamage = true;
            Debug.Log("canDamage now");
        }
    }
}
