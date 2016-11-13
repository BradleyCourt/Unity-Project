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
        //GameObject obj = GameObject.Find("Magazine");
        //magazineText = obj.GetComponent<Text>();
    }

    public void WeaponSwitch()
    {
        
        // Keyboard Weapon Switching (Press 0-9)
        for (int i = 0; i < weapons.Count; i++)
        {
            
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                if (weaponID == i)
                {
                    // your already holding the gun your trying to switch to
                    // do nothing
                }
                else
                {
                    // the gun your holding is not the same as the gun you are trying to switch to
                    // switch weapon
                    weaponID = i;
                    selectedWeapon = weapons[weaponID];
                    StopAllCoroutines();
                    isReloading = false;
                    coroutinerun = false;
                    break;
                }
            }
        }
        // Gamepad Weapon Switching
        if (device.Action4.WasPressed)    // Y BUTTON  
        {
            Debug.Log(weaponID);
            weaponID = (weaponID + 1) % weapons.Count;
            selectedWeapon = weapons[weaponID];
            StopAllCoroutines();
            isReloading = false;
            coroutinerun = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        device = InputManager.ActiveDevice;

        WeaponSwitch();
        if ((Input.GetMouseButton(0)) || (InputManager.ActiveDevice.GetControl(InputControlType.RightTrigger)))
        {

            Debug.Log(isReloading);
            if (isShooting == false && !isReloading)
            {
                isShooting = true;
                if (!coroutinerun)
                {
                    if (selectedWeapon.currentAmmo <= 0)
                    {
                        StartCoroutine(wepReload());
                    }
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
                isReloading = false;
            }
        }
        coroutinerun = false;
    }
    IEnumerator wepReload()
    {
        if (selectedWeapon.currentAmmo == selectedWeapon.ammo)
        {
            // full on ammo, dont reload
        }
        else
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
            


            isReloading = false;
        }
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Bullet")
        {
            Destroy(col.gameObject);
        }
    }
}
