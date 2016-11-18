using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    GameObject owner;
    public int bulletDamage;

    void Awake()
    {
        owner = GameObject.FindGameObjectWithTag("Player");
        bulletDamage = owner.GetComponent<CombatController>().selectedWeapon.damage;
    }


    void OnCollisionEnter(Collision other)
    {
		if (other.gameObject != owner && other.gameObject.GetComponent<Health>())
		{
			other.gameObject.GetComponent<Health>().AffectHealth(-bulletDamage);
		}
        Destroy(gameObject);
    }
}
