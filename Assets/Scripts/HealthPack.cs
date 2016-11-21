using UnityEngine;
using System.Collections;

public class HealthPack : MonoBehaviour
{
    public int healthToGive;
	public delegate void PickupEvent();
	public static event PickupEvent PickedUp;

    void OnTriggerEnter(Collider other)
    {
        PlayerHealth ph = other.gameObject.GetComponent<PlayerHealth>();
        if (other.gameObject.tag == "Player" && ph.health != ph.maxHealth)
        {
            other.GetComponent<Health>().AffectHealth(healthToGive);
			PickedUp();
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update ()
    {
	
	}
}
