using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour 
{
	public int health = 100;
	public int currentHealth;
	public Image damageImage;  
	public float flashSpeed = 5f;
	bool isDead;
	bool damaged;
	// Use this for initialization
	void Start () 
	{
		currentHealth = health;
	}
	
	// Update is called once per frame
	void Update ()
	{
		// If the player has just been damaged...
		if(damaged)
		{
			// ... set the colour of the damageImage to the flash colour.
			damageImage.color = flashColour;
		}
		// Otherwise...
		else
		{
			// ... transition the colour back to clear.
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}

		// Reset the damaged flag.
		damaged = false;
	}
}
