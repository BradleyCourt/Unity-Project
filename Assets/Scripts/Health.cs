using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Health : MonoBehaviour 
{
	public int health = 5;
    public int lastHealth;
    public int hit; // (being damaged)
	//public int currentHealth;
	public Image damageImage;  
	public float flashSpeed = 5f;
	bool isDead;
	bool damaged;
	// Use this for initialization
	void Start () 
	{

	}

    // Update is called once per frame
    void Update()
    {
       

        if (health > 5)
        {
            health = 5;
        }
        if (health <= 0)
        {
            health = 0;
        }

        if ()//Health starts at 5, After damage is 4
        //set lastHealth to health, stop screenflash
        )

        {


            if (damaged == true)
            {

                damaged = true;
                Color col = damageImage.color;
                col.a = 1;
                damageImage.color = col;

                health--;
                // ... set the colour of the damageImage to the flash colour.
                //damageImage.color = flashColour;
            }
            // Otherwise...
            else if (damaged = true && health <= 0)
            {
                isDead = true;
                //Works, proven by the Debug code.
                //Debug.Break();
                // ... transition the colour back to clear.
                //damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
            }

            damaged = false;
            
        }
        lastHealth = health;
    }

        
}

