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
    public int weaponID = 0;

    Text magazineText;

    public WeaponBase selectedWeapon;
    bool isShooting = false;
    private bool coroutinerun;
    bool isReloading = false;
    InputDevice device;

    // Use this for initialization
    void Start()
    {
        GameObject obj = GameObject.Find("Magazine");
        magazineText = obj.GetComponent<Text>();
    }

    public void WeaponSwitch()
    {
        // Keyboard Weapon Switching (Press 0-9)
        for (int i = 0; i < weapons.Count; i++)
        {
            if (isReloading == true)
            {

            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Alpha1 + i))
                {
                    weaponID = i;
                    selectedWeapon = weapons[weaponID];
                    break;
                }
            }
            // Gamepad Weapon Switching
            if (device.Action4.WasPressed)    // Y BUTTON  
            {
                weaponID = (weaponID + 1) % weapons.Count;
                selectedWeapon = weapons[weaponID];
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        device = InputManager.ActiveDevice;

        WeaponSwitch();
        if ((Input.GetMouseButton(0)) || (InputManager.ActiveDevice.GetControl(InputControlType.RightTrigger)))
        {
            if (isShooting == false && !isReloading)
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

        if (magazineText != null)
        {
            Text t = magazineText;
            if (selectedWeapon.ammoCapacity == 0 && selectedWeapon.currentAmmo == 0)
            {
                t.text = selectedWeapon.name + " : " + selectedWeapon.currentAmmo.ToString() + "  /  " + selectedWeapon.ammoCapacity + "  Out Of Ammo!  ";
            }
            else if (isReloading == true && selectedWeapon.ammoCapacity != 0)
            {
                t.text = selectedWeapon.name + " : " + selectedWeapon.currentAmmo.ToString() + "  /  " + selectedWeapon.ammoCapacity + "  Reloading...  ";
                //disable weapon switch
                
            }
            else if (isReloading == false)
            {
                t.text = selectedWeapon.name + " : " + selectedWeapon.currentAmmo.ToString() + "  /  " + selectedWeapon.ammoCapacity + "  Ready!  ";
            }
        }

        if (Input.GetKeyDown(KeyCode.R) || (InputManager.ActiveDevice.GetControl(InputControlType.Action3)))
        {
            Debug.Log("Reloading");

            StartCoroutine(wepReload());
        }

    }



    IEnumerator ShootWeapon()
    {
        coroutinerun = true;
        while (isShooting && selectedWeapon.currentAmmo != 0)
        {

            selectedWeapon.currentAmmo--;
            selectedWeapon.shotsFired++;

            GameObject obj = Instantiate(selectedWeapon.projectile, Target.transform.position, Quaternion.identity) as GameObject;
            Destroy(obj, selectedWeapon.bulletLifeTime);


            Rigidbody body = obj.GetComponent<Rigidbody>();
            body.AddForce(Target.transform.forward * selectedWeapon.bulletForce, ForceMode.Impulse);
            Debug.Log(transform.forward);

            Physics.IgnoreCollision(obj.GetComponent<Collider>(), GetComponent<Collider>());

            yield return new WaitForSeconds(selectedWeapon.fireRate);

            if (selectedWeapon.currentAmmo <= 0)
            {
                isReloading = true;
                yield return new WaitForSeconds(selectedWeapon.reloadSpeed);

                int ammoNeeded = selectedWeapon.ammo - selectedWeapon.currentAmmo;

                if (ammoNeeded < selectedWeapon.ammoCapacity)
                {
                    selectedWeapon.ammoCapacity -= ammoNeeded;
                    selectedWeapon.currentAmmo = selectedWeapon.ammo;
                }
                else if (ammoNeeded > selectedWeapon.ammoCapacity)
                {
                    selectedWeapon.currentAmmo += selectedWeapon.ammoCapacity;
                    selectedWeapon.ammoCapacity = 0;
                }
                else if (ammoNeeded == selectedWeapon.ammoCapacity)
                {
                    selectedWeapon.currentAmmo += selectedWeapon.ammoCapacity;
                    selectedWeapon.ammoCapacity = 0;
                }
                else
                {
                    Debug.Log("out of ammo");
                }



                //if (selectedWeapon.ammoCapacity == 0)
                //{
                //    Debug.Log("cannot relod");
                //}
                //else if (selectedWeapon.ammoCapacity <= 0)
                //{
                //    selectedWeapon.ammoCapacity = 0;
                //}
                //else
                //{
                //    selectedWeapon.ammoCapacity -= selectedWeapon.shotsFired;
                //    selectedWeapon.currentAmmo = selectedWeapon.ammo;
                //    selectedWeapon.shotsFired = 0;
                //}

                isReloading = false;

            }


        }
        coroutinerun = false;
    }
    IEnumerator wepReload()
    {


        int ammoNeeded = selectedWeapon.ammo - selectedWeapon.currentAmmo;

        if (ammoNeeded < selectedWeapon.ammoCapacity)
        {
            selectedWeapon.ammoCapacity -= ammoNeeded;
            selectedWeapon.currentAmmo = selectedWeapon.ammo;
        }
        else if (ammoNeeded > selectedWeapon.ammoCapacity)
        {
            selectedWeapon.currentAmmo += selectedWeapon.ammoCapacity;
            selectedWeapon.ammoCapacity = 0;
        }
        else if (ammoNeeded == selectedWeapon.ammoCapacity)
        {
            selectedWeapon.currentAmmo += selectedWeapon.ammoCapacity;
            selectedWeapon.ammoCapacity = 0;
        }
        else
        {
            Debug.Log("out of ammo");
        }
        isReloading = true;
        yield return new WaitForSeconds(selectedWeapon.reloadSpeed);

        //if (selectedWeapon.ammoCapacity == 0)
        //{
        //    Debug.Log("cannot relod");
        //}
        //else if (selectedWeapon.ammoCapacity <= 0)
        //{
        //    selectedWeapon.ammoCapacity = 0;
        //}
        //else
        //{
        //    selectedWeapon.ammoCapacity -= selectedWeapon.shotsFired;
        //    selectedWeapon.currentAmmo = selectedWeapon.ammo;
        //    selectedWeapon.shotsFired = 0;
        //}

        isReloading = false;

    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Bullet")
        {
            Destroy(col.gameObject);
        }
    }
}
