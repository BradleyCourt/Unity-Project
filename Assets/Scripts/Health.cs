using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Health : MonoBehaviour
{
    TopDownController topDownController;
    CombatController combatController;
    LookScript lookScript;

    public int originalHealth = 5;
    public int health;

    bool isDead;
    public bool damaged;

    public Image damageImage;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.5f);


    // Use this for initialization
    void Start()
    {

    }

    void Awake()
    {
        health = originalHealth;
        topDownController = GetComponent<TopDownController>();
        combatController = GetComponent<CombatController>();
        lookScript = GetComponent<LookScript>();
    }

    void death()
    {
        if (gameObject.tag == "Player" && health <= 0)
        {
            isDead = true;
            topDownController.enabled = false;
            combatController.enabled = false;
            lookScript.enabled = false;
            print("Game Over");
        }
        else if (gameObject.tag == "Enemy" && health <= 0)
        {
            isDead = true;
        }
    }

    public void applyDamage()
    {
        if (damaged == true)
        {
            health--;
            damageImage.color = flashColour;
        }
    }



    // Update is called once per frame
    void Update()
    {
        //if ()//Health starts at 5, After damage is 4
        //set lastHealth to health, stop screenflash

        if (damaged == true)
        {
            applyDamage();

        }
        // Otherwise...
        else if (isDead = true && health <= 0)
        {
            death();
        }
        damaged = false;
        damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        
    }
    
}