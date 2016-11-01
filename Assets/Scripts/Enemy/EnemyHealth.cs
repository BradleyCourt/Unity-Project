using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class EnemyHealth : MonoBehaviour
{
    FollowPlayer followPlayer;
    WeaponBase weaponStat;
    GameObject Player;

    Collider[] attackColliders;

    public float attackRange; 

    public int maxHealth = 100;
    public int damageToDeal;

    bool isDead;
    public bool damaged;


    // Use this for initialization
    void Start()
    {
        followPlayer = GameObject.FindObjectOfType<FollowPlayer>();
    }

    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void death()
    {
        if (gameObject.tag == "Enemy" && maxHealth <= 0)
        {
            isDead = true;
            Destroy(gameObject);
        }
    }


    void attackPlayer(Vector3 attackBox)
    {
        attackColliders = Physics.OverlapSphere(attackBox, attackRange);

        while()
        {
            
        }
    }



    //Update is called once per frame
    void Update()
    {

        if (damaged == true)
        {
            //attackPlayer();

        }
        else if (isDead = true && maxHealth <= 0)
        {
            death();
        }
        damaged = false;
    }

    void OnCollisionEnter(Collision col)
    {
        attackPlayer(col.contacts[0].point);
    }
}