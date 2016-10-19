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

    public Vector3 Target;
    [Space()]
    public float deadZone = 0.1f;
    public int currentWeapon = 0;
    public int num = 0;
    private Vector2 inputVector;
  


    public WeaponBase selectedWeapon;// = weapons;
    bool isShooting = false;
    private bool coroutinerun;

    //functionality refs
    // Coroutine shootTimer = null;

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
            currentWeapon = 0;
            num = currentWeapon;
            Debug.Log("first gun equipped");
        }
        
        if (Input.GetKey("2"))
        {
            num = 1;
            currentWeapon = 1;
            num = currentWeapon;
            Debug.Log("second gun equipped");
        }
    }

    // Update is called once per frame
    void Update()
    {
        WeaponSwitch();
        if ((Input.GetMouseButton(0)))
        {
            if (isShooting == false)
            {
                isShooting = true;
                if (!coroutinerun)
                {
                    StartCoroutine(ShootWeapon());
                }

                // StopCoroutine(shootTimer);
            }
        }

            if (Input.GetMouseButtonUp(0) && isShooting == true)
            {
                Debug.Log("working");
                StopCoroutine(ShootWeapon());
                isShooting = false;
            }   
        
    }
    IEnumerator ShootWeapon()
    {
        coroutinerun = true;
        while (isShooting)
        {
            Debug.Log("hi");


            //GameObject obj = Instantiate()
            GameObject obj = Instantiate(selectedWeapon.projectile, transform.position, Quaternion.identity) as GameObject;
            Destroy(obj, selectedWeapon.bulletLifeTime);

            Debug.Log(transform.position);

            Rigidbody body = obj.GetComponent<Rigidbody>();
            body.AddForce(transform.forward * selectedWeapon.bulletForce, ForceMode.Impulse);

            Physics.IgnoreCollision(obj.GetComponent<Collider>(), GetComponent<Collider>());

            yield return new WaitForSeconds(selectedWeapon.fireRate);
        }
        coroutinerun = false;
        
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


        //if (Time.time > selectedWeapon.nextFireTime)
        //{
        //    selectedWeapon.nextFireTime = Time.time + selectedWeapon.fireRate;

        //    Debug.Log("Fired bullet in direction: " + inputVector);
      //  }
    }
}

