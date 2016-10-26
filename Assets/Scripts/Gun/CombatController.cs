using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using InControl;

public class CombatController : MonoBehaviour
{

    public List<WeaponBase> weapons;


    //public GameObject projectile;
    //[Space()]

    //public float bulletForce = 100f;
    //public float bulletLifeTime = 5f;

    //public float fireInterval = 0.25f;
    //private float nextFireTime;

    public GameObject Target;
    [Space()]
    public float deadZone = 0.1f;
    public int currentWeapon = 0;
    public int num = 0;
    private Vector2 inputVector;


    public WeaponBase selectedWeapon;// = weapons;
    bool isShooting = false;
    private bool coroutinerun;
    InputDevice device;
    //functionality refs
    // Coroutine shootTimer = null;

    // Use this for initialization
    void Start()
    {

        //selectedWeapon = weapons[num];
    }
    public void WeaponSwitch()
    {
        // Keyboard Weapon Switching (Press 0-9)
        for (int i = 0; i < weapons.Count; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                num = i;
                selectedWeapon = weapons[num];
                break;
            }
        }
        // Gamepad Weapon Switching
        if (device.Action3.WasPressed)    // X BUTTON  
        {
            num = (num + 1) % weapons.Count;
            selectedWeapon = weapons[num];
        }

        //if (Input.GetKey("2") || device.Action4) // B BUTTON
        //{
        //    num = 1;
        //    currentWeapon = 1;
        //    num = currentWeapon;
        //    selectedWeapon = weapons[num];
        //    Debug.Log("second gun equipped");
        //}
    }

    // Update is called once per frame
    void Update()
    {
        device = InputManager.ActiveDevice;

        WeaponSwitch();
        if ((Input.GetMouseButton(0)) || (InputManager.ActiveDevice.GetControl(InputControlType.RightTrigger)))
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
        if (InputManager.ActiveDevice.GetControl(InputControlType.RightTrigger) && isShooting == true)
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
            
            GameObject obj = Instantiate(selectedWeapon.projectile, Target.transform.position , Quaternion.identity) as GameObject;
            Destroy(obj, selectedWeapon.bulletLifeTime);

            Rigidbody body = obj.GetComponent<Rigidbody>();
            body.AddForce(Target.transform.forward * selectedWeapon.bulletForce, ForceMode.Impulse);
            Debug.Log(transform.forward);

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

