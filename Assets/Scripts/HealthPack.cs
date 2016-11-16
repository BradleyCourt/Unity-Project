using UnityEngine;
using System.Collections;

public class HealthPack : MonoBehaviour {
    public int healthToGive;

	// Use this for initialization
	void Start ()
    {
	
	}

    void OnTriggerEnter(Collider other)
    {
        PlayerHealth ph = other.gameObject.GetComponent<PlayerHealth>();
        if (other.gameObject.tag == "Player" && ph.health != ph.maxHealth)
        {
            other.GetComponent<Health>().AffectHealth(healthToGive);
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update ()
    {
	
	}
}
