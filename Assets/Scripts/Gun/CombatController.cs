using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using InControl;

public class CombatController : MonoBehaviour
{
    public List<WeaponBase> weapons;
    public GameObject Target;
    [Space()]
    public float deadZone = 0.1f;
    public int currentWeapon = 0;
    public int num = 0;
    private Vector2 inputVector;


    public WeaponBase selectedWeapon;
    bool isShooting = false;
    private bool coroutinerun;
    InputDevice device;

    // Use this for initialization
    void Start()
    {

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
            }k
        }
        // Gamepad Weapon Switching
        if (device.Action3.WasPressed)    // X BUTTON  
        {
            num = (num + 1) % weapons.Count;
            selectedWeapon = weapons[num];
        }
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
}

