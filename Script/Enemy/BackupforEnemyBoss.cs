using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackupforEnemyBoss : MonoBehaviour
{
    public PlayerController playerController; //To reset player
    public SpawnManager spawnManager;
    //Player player // Script for PlayerDamage to Shield ref
    [SerializeField] Transform PlayerResetPos;
    //public float BossShield = 3;
    bool inRange; //Note; use collider or distance
    /*public bool isDamaged;*/ //testing if damage

    public bool notInvulnerable; //indicate if player can now damage boss
    public bool canDamage;

    public int bossPhase; //To determine which phases for boss
    //Animation
    Animator anim;

    bool keyPressed;

    //ONLY FOR TESTING
    [Header("Only for Testing!")]
    public GameObject Shield;
    public int currentShield;
    void Start()
    {
        anim = GetComponent<Animator>();
        inRange = false;

        canDamage = false;
        //isDamaged = false;
        //shieldBreak = false;

        bossPhase = 1;
        SwitchCaseBossPhase(); //has to be in Start instead of Update to prevent switching case
    }
    void Update()
    {

        keyPressed = Input.GetKeyUp(KeyCode.V); //Only for testing to check damage
        Debug.Log(keyPressed);
    }
    void FixedUpdate()
    {

        if (keyPressed) //Change to player damage input
        {
            //bossShield.WhenDestroy(1);
            //AttackOnShield += 1;
            //Debug.Log("AttackOnShield is " + AttackOnShield);
            //ShieldDestroy(1);
            ShieldDamage();
        }
    }
    //public void TakeDamage(float damage)
    //{
    //    if (!notInvulnerable && canDamage && !shieldBreak)
    //    {
    //        isDamaged = true;
    //        ShieldBreak();
    //        float damageTaken = Mathf.Clamp(damage, 0, 1);
    //        BossShield -= damageTaken;
    //        Debug.Log("Damaging");
    //    }
    //}

    void ShieldDamage()//testing if can decrement to 0 then switch state
    {
        if (!notInvulnerable && canDamage)
        {
            currentShield -= 1;
            ShieldState(); //to detect if current shield is now is 0 per shield damage
            Debug.Log("Subtracting Shield");
        }

    }
    void SwitchCaseBossPhase()
    {
        switch (bossPhase)
        {
            case 1:
                currentShield = 1;
                bossPhase = 1;
                anim.SetBool("isAgonize", false);
                Shield.SetActive(true);
                spawnManager.SpawnEnemy(5);
                Debug.Log(bossPhase + " Case");
                break;
            case 2:
                currentShield = 2;
                anim.SetBool("isAgonize", false);
                Shield.SetActive(true);
                Debug.Log(bossPhase + " Case");
                spawnManager.SpawnEnemy(10);
                break;
            case 3:
                currentShield = 3;
                anim.SetBool("isAgonize", false);
                Shield.SetActive(true);
                spawnManager.SpawnEnemy(12);
                Debug.Log(bossPhase + " Case");
                break;
            case 4:
                Debug.Log("End");
                break;
        }
    }
    void ShieldState()
    {
        if (currentShield == 0)
        {
            Debug.Log("Damaging");
            Invulnerable();
        }
    }
    void ShieldBreak()
    {
        //if (isDamaged && !invulnerableTime)
        //{
        //    if (bossPhase == 0) //will trigger if boss is at shield 3f
        //    {
        //        Debug.Log("Bosshield is now on 2");
        //        spawnManager.SpawnEnemy(5);
        //        anim.SetBool("isAgonize", true);
        //        spawnManager.hasSpawned = true;
        //        Invulnerable();
        //        shieldBreak = true;

        //    }
        //    else if (bossPhase == 1)
        //    {
        //        Debug.Log("Bosshield now on 1");
        //        spawnManager.SpawnEnemy(10);
        //        spawnManager.hasSpawned = true;
        //        anim.SetBool("isAgonize", true);
        //        Invulnerable();
        //        shieldBreak = true;
        //    }
        //    else if (bossPhase == 2)
        //    {
        //        Debug.Log("Bosshield is now on last shield");
        //        spawnManager.SpawnEnemy(12);
        //        spawnManager.hasSpawned = true;
        //        anim.SetBool("isAgonize", true);
        //        Invulnerable();
        //        shieldBreak = true;
        //    }
        //    else if (bossPhase == 3)
        //    {
        //        Debug.Log("Destroyed Boss");
        //        Destroy(this);
        //    }
        //}
        //if (isDamaged && !invulnerableTime)
        //{
        //    if (BossShield == 0) //will trigger if boss is at shield 3f
        //    {
        //        Debug.Log("Bosshield is now on 2");
        //        spawnManager.SpawnEnemy(5);
        //        anim.SetBool("isAgonize", true);
        //        spawnManager.hasSpawned = true;
        //        Invulnerable();
        //        shieldBreak = true;

        //    }
        //    else if (BossShield == 5.0f)
        //    {
        //        Debug.Log("Bosshield now on 1");
        //        spawnManager.SpawnEnemy(10);
        //        spawnManager.hasSpawned = true;
        //        anim.SetBool("isAgonize", true);
        //        Invulnerable();
        //        shieldBreak = true;
        //    }
        //    else if(BossShield == 3.0f)
        //    {
        //        Debug.Log("Bosshield is now on last shield");
        //        spawnManager.SpawnEnemy(12);
        //        spawnManager.hasSpawned = true;
        //        anim.SetBool("isAgonize", true);
        //        Invulnerable();
        //        shieldBreak = true;
        //    }
        //    else if (BossShield == 3)
        //    {
        //        Debug.Log("Destroyed Boss");
        //        Destroy(this);

        //    }
        //}
    }

    //void ShieldDestroy(int damage)
    //{
    //    //bossShield.shieldReady -= damage;
    //    //if (bossShield.shieldReady != 0)
    //    //{
    //    //    bossShield.PhaseShield[1].SetActive(false);
    //    //}
    //    if (bossPhase != 0) //FIX FOR FUTURE REFERENCE. working but not setting to false before decrementing
    //    {
    //        for (int i = 0; i < damage; i++) //Note: wont count shieldReady array 0
    //        {
    //            Debug.Log("i: " + i + " testing shield");
    //            bossShield.PhaseShield[i].SetActive(false);
    //            //if (i == 0)
    //            //{
    //            //    bossPhase--; //decrement to return to bosshield Switch Case
    //            //    Debug.Log(bossShield.shieldReady + " Remaining Shield");
    //            //}
    //        }
    //        bossPhase -= 1;
    //        bossShield.shieldAvailable -= 1; //decrement to return to bosshield Switch Case
    //        Debug.Log(bossShield.shieldAvailable + " Remaining Shield");

    //    }
    //if (bossPhase != 0) //FIX FOR FUTURE REFERENCE. working but not setting to false before decrementing
    //{
    //    for (int i = bossShield.shieldReady; i >= 0; i--) //Note: wont count shieldReady array 0
    //    {
    //        Debug.Log("i: " + i + " testing shield");
    //        bossShield.PhaseShield[i].SetActive(false);
    //        //if (i == 0)
    //        //{
    //        //    bossPhase--; //decrement to return to bosshield Switch Case
    //        //    Debug.Log(bossShield.shieldReady + " Remaining Shield");
    //        //}
    //    }
    //    bossPhase -=1;
    //    bossShield.shieldReady -= 1; //decrement to return to bosshield Switch Case
    //    Debug.Log(bossShield.shieldReady + " Remaining Shield");

    //}
    //else
    //{
    //    bossShield.phaseLevel++;
    //}


    //if (bossPhase != 0)
    //{
    //    int shieldDestroy = damage - 1;
    //    for (int i = shieldDestroy; i > bossShield.shieldReady; i--)
    //    {
    //        Debug.Log("i: " + i + " testing shield");
    //        if (i < bossShield.PhaseShield.Length)
    //        {
    //            Debug.Log("Deactivating shield at index " + i);
    //            bossShield.PhaseShield[i].SetActive(false);
    //        }
    //        else
    //        {
    //            Debug.Log("Index " + i + " out of range for PhaseShield.");
    //        }
    //    }
    //}
    //if (bossPhase != 0)
    //{
    //    for (int i = damage - 1; i == bossShield.shieldReady; i--)
    //    {
    //        Debug.Log("i: " + i + " testing shield");
    //        bossShield.PhaseShield[i].SetActive(false);
    //        bossShield.shieldReady--;
    //        Debug.Log(bossShield.shieldReady + " Remaining Shield");
    //    }
    //}
    //}
    void Invulnerable() //Invulnerable time to avoid cheese
    {
        notInvulnerable = true;
        Shield.SetActive(false);
        //PlayerResetPos.position = playerController.transform.position;
        playerController.transform.position = PlayerResetPos.transform.position;
        anim.SetBool("isAgonize", true);
        canDamage = false;

        Debug.Log("Invulnerable");
        StartCoroutine(Immortal());
        //if (!isDamaged)
        //{
        //    notInvulnerable = true;
        //    //PlayerResetPos.position = playerController.transform.position;
        //    playerController.transform.position = PlayerResetPos.transform.position;

        //    canDamage = false;

        //    Debug.Log("Invulnerable");
        //    StartCoroutine(Immortal());
        //}
    }

    void Reset()
    {
        anim.SetBool("isAgonize", false);
        //isDamaged = false;
        spawnManager.hasSpawned = false;
        notInvulnerable = false;
    }
    IEnumerator Immortal() // for Shield to build up 
    {
        yield return new WaitForSeconds(5f);
        Reset();
        bossPhase += 1;
        SwitchCaseBossPhase();
    }
}
