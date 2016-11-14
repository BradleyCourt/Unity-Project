using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public abstract class Health : MonoBehaviour
{
    [Header ("Health")]
    [Tooltip("Maximum Health")]
    public int maxHealth;
    [Tooltip("Current Health")]
    public int health;
    [Tooltip("Is agent dead or not")]
    public bool isDead;
    


    public abstract void Death();
    
    
    public void AffectHealth(int healthDelta)
    { 
        health += healthDelta;
  
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        if (health <= 0)
        {
            isDead = true;
            Death();
        }
    }
    // Update is called once per frame
    void Update()
    {
       
    }
}   