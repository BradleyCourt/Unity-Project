using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public abstract class Health : MonoBehaviour
{
    [Header ("Health")]
    [Tooltip("Maximum Health")]
    public int maxHealth;
    public int health;

    public bool isDead;

    //public Image damageImage;
    //public float flashSpeed = 5f;
    //public Color flashColour = new Color(1f, 0f, 0f, 0.5f);


    public abstract void Death();
    

    public void AffectHealth(int healthDelta)
    {
        health += healthDelta;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}   