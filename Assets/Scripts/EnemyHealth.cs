﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class EnemyHealth : MonoBehaviour
{
    FollowPlayer followPlayer;
    WeaponBase weaponStat;
    GameObject Player;

    public int maxHealth = 100;
    public int health;
    public int damageToDeal;

    bool isDead;
    public bool damaged;

    public Image damageImage;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.5f);


    // Use this for initialization
    void Start()
    {
        //combatController = GameObject.FindObjectOfType<CombatController>();
        //lookScript = GameObject.FindObjectOfType<LookScript>();
        //followPlayer = GameObject.FindObjectOfType<FollowPlayer>();
    }

    void Awake()
    {
        //health = maxHealth;
        //Player = GameObject.FindGameObjectWithTag("Player");
        //topDownController = Player.GetComponent<TopDownController>();
    }

    void death()
    {
        if (gameObject == Player && health <= 0)
        {
            //isDead = true;
            //topDownController.enabled = false;
            //combatController.enabled = false;
            //lookScript.enabled = false;
            //print("Game Over");
        }
        else if (gameObject.tag == "Enemy" && health <= 0)
        {
        //    isDead = true;
        //    Destroy(gameObject);
        }
    }


    public void applyDamage()
    {
        //print(damageDone);
        //if (gameObject.tag == "Player" && damaged == true)
        //{
        //    health--;
        //    damageImage.color = flashColour;
        //    healthText.text = "Health = " + health;
        //}
        //if (gameObject.tag == "Enemy" && damaged == true)
        //{
        //    followPlayer.attack();
        //    health -= damageDone;
        //}
        //damaged = false;
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

    void OnCollisionEnter(Collision projectile)
    {
        //if (gameObject.tag == "Enemy")
        //{
        //    damageDone = combatController.weapons[combatController.num].damage;
        //    damaged = true;

        //}
        //else if (gameObject.tag == "Player")
        //{
        //    followPlayer.attack();
        //}
    }
    void OnTriggerEnter(Collider Pickup)
    {
    //    if (Pickup.gameObject.tag == "MedPack")
    //    {
    //        if (health >= maxHealth)
    //        {
    //            //Do Nothing
    //        }
    //        else
    //        {
    //            Destroy(Pickup.gameObject);

    //            health++;
    //            healthText.text = "Health : " + health;
    //        }
    //    }
    }

}