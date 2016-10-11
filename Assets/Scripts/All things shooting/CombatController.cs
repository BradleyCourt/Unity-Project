using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CombatController : MonoBehaviour
{
    public List<WeaponBase> weapons;

    //public GameObject projectile;
    //[Space()]
  
    //public float bulletForce = 100f;
    //public float bulletLifeTime = 5f;

    //public float fireInterval = 0.25f;
    //private float nextFireTime;

    public float deadZone = 0.1f;

    private Vector2 inputVector;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Shoot();
        }
    }
    public void Shoot()
    {
        WeaponBase selectedWeapon = weapons[1];

        GameObject obj = Instantiate(selectedWeapon.projectile, transform.position, Quaternion.identity) as GameObject;
        Destroy(obj, selectedWeapon.bulletLifeTime);

        Rigidbody body = obj.GetComponent<Rigidbody>();
        body.AddForce(transform.forward * selectedWeapon.bulletForce, ForceMode.Impulse);

        Physics.IgnoreCollision(obj.GetComponent<Collider>(), GetComponent<Collider>());


        inputVector.x = Input.GetAxis("LookHorizontal");
        inputVector.y = Input.GetAxis("LookVertical");


        if (Time.time > selectedWeapon.nextFireTime)
        {
            selectedWeapon.nextFireTime = Time.time + selectedWeapon.fireRate;

            Debug.Log("Fired bullet in direction: " + inputVector);
        }
    }
}

