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

	//Health Events. Not shown in inspector, used for animation system and UI
	public delegate void HealthEvent();
	public event HealthEvent OnDeath;
	public event HealthEvent OnHealthChange;

	public abstract void Death();
    
    public void AffectHealth(int healthDelta)
    { 
        health += healthDelta;

		if (OnHealthChange != null) { OnHealthChange(); } //Broadcast event

		if (health > maxHealth)
        {
            health = maxHealth;
        }
        if (health <= 0 && isDead == false)
        {
            isDead = true;
			if (OnDeath != null) { OnDeath(); } // Broadcast event
			Death();
        }
    }
}   