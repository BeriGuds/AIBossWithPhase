using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestingMinion : MonoBehaviour
{
    public GameObject E_Boss;
    public GameObject S_Manager;

    private EnemyBoss enemyBoss;
    private SpawnManager spawnManager;
    NavMeshAgent navAgent;
    //EnemyState
    public float MinionHP = 5;
    bool minionStop; //to stop navAgent to Setting Dest
    void Awake()
    {
        E_Boss = GameObject.FindGameObjectWithTag("EnemyBoss");
        S_Manager = GameObject.FindGameObjectWithTag("SpawnManager");

        spawnManager = S_Manager.GetComponent<SpawnManager>();
        enemyBoss = E_Boss.GetComponent<EnemyBoss>();

    }
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        minionStop = false;
    }
    void Update()
    {
        Move();
        Attack();
    }

    void Move()
    {
        if (!minionStop)
        {
            navAgent.SetDestination(PlayerController.playerPos);
        }
        else
        {
            //navAgent.Stop();
            navAgent.isStopped = true;
        }
        //SetDestination to player pos

    }
    void Attack()
    {
        float distance = Vector3.Distance(transform.position, PlayerController.playerPos);
        if (distance <= 2f)
        {
            minionStop = true;
            Destroy(this.gameObject);
            //spawnManager.NoMoreEnemy();


            //Insert PlayerScript Damage
            //insert PlayerAttack Anim
            //[TESTING ONLY]if Destroyed, it can decrement to shield break limit
            Debug.Log("Attacking with anim");

        }
    }
    public void TakeDamage(float damage)
    {
        float damageTaken = Mathf.Clamp(damage, 0, 1);
        MinionHP -= damageTaken;

        if (MinionHP <= 0)
        {
            //spawnManager.NoMoreEnemy(); 
            Debug.Log("Destroyed");
        }
    }

    void OnDestroy()
    {
        Debug.Log("OnDestroy");
        spawnManager.NoMoreEnemy();
    }
}
