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
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<Health>().AffectHealth(healthToGive);
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update ()
    {
	
	}
}
