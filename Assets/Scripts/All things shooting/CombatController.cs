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
    public int currentWeapon = 0;
    public int num = 0;
    private Vector2 inputVector;

    public WeaponBase selectedWeapon;// = weapons;
    bool isShooting = false;

    //functionality refs
    Coroutine shootTimer = null;

    // Use this for initialization
    void Start()
    {

        //selectedWeapon = weapons[num];
    }
    public void WeaponSwitch()
    {
        if (Input.GetKey("1"))
        {
            num = 0;
        }
        
        if (Input.GetKey("2"))
        {
            num = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if ((Input.GetMouseButtonDown(0)) && (Time.time >= selectedWeapon.fireRate))
            {
                 if (isShooting == false)
            {
                 isShooting = true;
                shootTimer = StartCoroutine(ShootWeapon());
            }
            else if (!Input.GetMouseButton(0) && isShooting == true)
            {

                StopCoroutine(shootTimer);
                isShooting = false;
            }
        }

    }

    IEnumerator ShootWeapon()
    {
        while(true)
        {
            GameObject obj = Instantiate(selectedWeapon.projectile, transform.position, Quaternion.identity) as GameObject;
            Destroy(obj, selectedWeapon.bulletLifeTime);

            Rigidbody body = obj.GetComponent<Rigidbody>();
            body.AddForce(transform.forward * selectedWeapon.bulletForce, ForceMode.Impulse);

            Physics.IgnoreCollision(obj.GetComponent<Collider>(), GetComponent<Collider>());

           // yield return new WaitForSeconds(selectedWeapon.fireRate);
        }
    }

    public void Shoot()
    {
       
        

  
        //GameObject obj = Instantiate(selectedWeapon.projectile, transform.position, Quaternion.identity) as GameObject;
        //Destroy(obj, selectedWeapon.bulletLifeTime);

        //Rigidbody body = obj.GetComponent<Rigidbody>();
        //body.AddForce(transform.forward * selectedWeapon.bulletForce, ForceMode.Impulse);

        //Physics.IgnoreCollision(obj.GetComponent<Collider>(), GetComponent<Collider>());


    //    inputVector.x = Input.GetAxis("LookHorizontal");
       // inputVector.y = Input.GetAxis("LookVertical");


        if (Time.time > selectedWeapon.nextFireTime)
        {
            selectedWeapon.nextFireTime = Time.time + selectedWeapon.fireRate;

            Debug.Log("Fired bullet in direction: " + inputVector);
        }
    }
}

