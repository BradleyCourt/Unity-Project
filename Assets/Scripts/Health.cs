using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Health : MonoBehaviour 
{
    TopDownController topDownController;
    Shooting shooting;
    LookScript lookScript;
    public int health = 0;
    //public int lastHealth;
    public int hit; // (being damaged)
	//public int currentHealth;
	public Image damageImage;  
	public float flashSpeed = 5f;
	bool isDead;
	public bool damaged;
	// Use this for initialization
	void Start () 
	{
        
	}

    void death()
    {

    }

    void applyDamage()
    {
        if (damaged)
        {
            health--;
        }

        if (health <= 0)
        {
            isDead = true;
            topDownController.enabled = false;
            shooting.enabled = false;
            lookScript.enabled = false;
            print("DED");

        }
    }


    // Update is called once per frame
    void Update()
    {
        //if ()//Health starts at 5, After damage is 4
        //set lastHealth to health, stop screenflash
        topDownController = GetComponent<TopDownController>();
        shooting = GetComponent<Shooting>();
        lookScript = GetComponent<LookScript>();
            if (damaged == true)
            {
            applyDamage();
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
                damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
            }

            damaged = false;   
        }
    }