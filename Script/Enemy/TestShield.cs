using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestShield : MonoBehaviour
{
    public GameObject[] P_Shield;
    public int availShield;
    public int shieldReady;
    // Start is called before the first frame update
    void Start()
    {
        shieldReady = 1;
        availShield = shieldReady;
    }

    // Update is called once per frame
    void Update()
    {
        ShieldCount();
    }

    void ShieldCount()
    {
        int spawnShield = shieldReady;
        for (int i = 0; i < shieldReady; i++)
        {
            P_Shield[i].SetActive(true);
        }

    }

    void DestroyingShield(int damage)
    {

    }
}
