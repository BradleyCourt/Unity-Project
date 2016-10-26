using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Health : MonoBehaviour
{
    TopDownController topDownController;
    CombatController combatController;
    LookScript lookScript;
    WeaponBase weaponStat;
    GameObject Player;

    public int originalHealth = 5;
    public int health;
    public int damageDone;

    bool isDead;
    bool inEnemyRange;
    public bool damaged;

    public Image damageImage;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.5f);


    // Use this for initialization
    void Start()
    {
        combatController = GameObject.FindObjectOfType<CombatController>();
    }

    void Awake()
    {
        health = originalHealth;
        Player = GameObject.FindGameObjectWithTag("Player");
        topDownController = Player.GetComponent<TopDownController>();    
        lookScript = Player.GetComponent<LookScript>();
    }

    void death()
    {
        if (gameObject == Player && health <= 0)
        {
            isDead = true;
            topDownController.enabled = false;
            combatController.enabled = false;
            lookScript.enabled = false;
            Destroy(Player);
            print("Game Over");
        }
        else if (gameObject.tag == "Enemy" && health <= 0)
        {
            isDead = true;
            Destroy(gameObject);
        }
    }

    public void applyDamage()
    {
        print(damageDone);
        if (gameObject.tag == "Player" && damaged == true || inEnemyRange)
        {
            health --;
            damageImage.color = flashColour;
        }
        if (gameObject.tag == "Enemy" && damaged == true)
        {
            health -= damageDone;
        }
        damaged = false;
    }



    // Update is called once per frame
    void Update()
    {
        
        if (damaged == true)
        {
            applyDamage();

        }
        else if (isDead = true && health <= 0)
        {
            death();
        }
        damaged = false;
        if (damageImage != null)
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
    }

    void OnCollisionEnter(Collision projectile)
    {
        if (gameObject.tag == "Enemy")
        {
            // ... the player is in range.
            damageDone = combatController.weapons[combatController.num].damage;
            damaged = true;

        }
    }

}   