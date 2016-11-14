using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    GameObject player;
    public int bulletDamage;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        bulletDamage = player.GetComponent<CombatController>().selectedWeapon.damage;
    }


    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Health>())
        {
            other.gameObject.GetComponent<Health>().AffectHealth(-bulletDamage);
        }
        Debug.Log("collided");
        Destroy(gameObject);
    }
}
