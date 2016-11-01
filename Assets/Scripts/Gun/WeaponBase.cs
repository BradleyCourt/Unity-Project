using UnityEngine;
using System.Collections;

public class WeaponBase : MonoBehaviour
{
    public GameObject projectile;
    [Space()]
   
    public int damage = 10;
    public float fireRate = 3f;
    public float nextFireTime = 25.5f;
    public int ammoCapacity = 8;
    public float reloadSpeed = 2.5f;
    public float bulletForce = 100f;
    public float bulletLifeTime = 5f;
    public float currentAmmo;
    

    // Use this for initialization
    void Start ()
    {
        currentAmmo = ammoCapacity;
	}
	
	// Update is called once per frame
	void Update ()
    {
     
    }
}
