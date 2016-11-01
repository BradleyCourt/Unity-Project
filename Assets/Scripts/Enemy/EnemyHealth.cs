using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class EnemyHealth : MonoBehaviour
{
    FollowPlayer followPlayer;
    WeaponBase weaponStat;
    GameObject Player;

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
        if (gameObject == Player && maxHealth <= 0)
        {
            //isDead = true;
            //topDownController.enabled = false;
            //combatController.enabled = false;
            //lookScript.enabled = false;
            //print("Game Over");
        }
        else if (gameObject.tag == "Enemy" && maxHealth <= 0)
        {
        //    isDead = true;
        //    Destroy(gameObject);
        }
    }


    public void attackPlayer()
    {
        if (gameObject.tag == "Player" && damaged == true)
        {
            maxHealth -= weaponStat.damage;
        }
        damaged = false;
    }



    // Update is called once per frame
    void Update()
    {

        //if (damaged == true)
        //{
        //    applyDamage();

        //}
        //else if (isDead = true && health <= 0)
        //{
        //    death();
        //}
        //damaged = false;
        //if (damageImage != null)
        //{
        //    damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        //}
    }

    //void OnCollisionEnter(Collision projectile)
    //{
    //    }
    //    if (gameObject.tag == "Player")
    //    {
    //        attackPlayer;
    //    }
    //}

