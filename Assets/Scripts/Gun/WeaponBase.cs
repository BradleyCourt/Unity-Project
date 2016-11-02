using UnityEngine;
using System.Collections;

public class WeaponBase : MonoBehaviour
{
    public GameObject projectile;
    [Space()]
   
    public int damage = 10;
    public float fireRate = 3f;
    public float nextFireTime = 25.5f;
    public int ammo = 8;
    public int ammoCapacity = 256;
    public float reloadSpeed = 2.5f;
    public float bulletForce = 100f;
    public float bulletLifeTime = 5f;
    public int currentAmmo;
    public int shotsFired;



    // Use this for initialization
    void Start ()
    {
        currentAmmo = ammo;
        shotsFired = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
     
    }
}
