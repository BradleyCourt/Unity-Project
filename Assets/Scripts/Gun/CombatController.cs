using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using InControl;
using UnityEngine.UI;

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
            }
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

        Text[] array = FindObjectsOfType<Text>();

        foreach (Text t in array)
        {
            if (t.name == "Magazine")
            {

                t.text = selectedWeapon.name + " : " + selectedWeapon.currentAmmo.ToString();
            }
        }
    }
    IEnumerator ShootWeapon()
    {
        coroutinerun = true;
        while (isShooting)
        {
            selectedWeapon.currentAmmo--;

            GameObject obj = Instantiate(selectedWeapon.projectile, Target.transform.position, Quaternion.identity) as GameObject;
            Destroy(obj, selectedWeapon.bulletLifeTime);

            Rigidbody body = obj.GetComponent<Rigidbody>();
            body.AddForce(Target.transform.forward * selectedWeapon.bulletForce, ForceMode.Impulse);
            Debug.Log(transform.forward);

            Physics.IgnoreCollision(obj.GetComponent<Collider>(), GetComponent<Collider>());

            yield return new WaitForSeconds(selectedWeapon.fireRate);

            

            if (selectedWeapon.currentAmmo <= 0)
            {
                yield return new WaitForSeconds(selectedWeapon.reloadSpeed);
                selectedWeapon.currentAmmo = selectedWeapon.ammoCapacity;
                //StartCoroutine(reloadGun());
                // Debug.Log(currentAmmo);
            }


        }
        coroutinerun = false;
    }
}
