using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Health : MonoBehaviour 
{
    TopDownController topDownController;
    CombatController combatController;
    LookScript lookScript;
    public int originalHealth = 5;
    public int health;
    //public int lastHealth;
    public int hit; // (being damaged)
	//public int currentHealth;
	public Image damageImage;  
	public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.5f);
	bool isDead;
	public bool damaged;
	// Use this for initialization
	void Start () 
	{
        
	}

    public void death()
    {
        if (health <= 0)
        {
            isDead = true;
            topDownController.enabled = false;
            combatController.enabled = false;
            lookScript.enabled = false;
            print("DED");
        }
    }

    public void applyDamage(int damage)
    {
        if (damaged == true)
        {
            health -= damage;
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

    }
    


    // Update is called once per frame
    void Update()
    {
        //if ()//Health starts at 5, After damage is 4
        //set lastHealth to health, stop screenflash
        topDownController = GetComponent<TopDownController>();
        combatController = GetComponent<CombatController>();
        lookScript = GetComponent<LookScript>();
            if (damaged == true)
            {
            applyDamage();
                // ... set the colour of the damageImage to the flash colour.                  
            }
            // Otherwise...
            else if (damaged = true && health <= 0)
            {

            death();
                //Works, proven by the Debug code.
                //Debug.Break();
                // ... transition the colour back to clear.

            }
        damaged = false;
    }
    }