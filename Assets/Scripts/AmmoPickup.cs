using UnityEngine;
using System.Collections;

public class AmmoPickup : MonoBehaviour
{
    public int ammoToGive;

    // Use this for initialization
    void Start()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            /* Add ammo for current gun */
            CombatController cc = other.gameObject.GetComponentInChildren<CombatController>();
            if (cc)
            {
                cc.selectedWeapon.ammoCapacity += ammoToGive;
            }
            /* Add ammo for all guns */
            //WeaponBase[] weapons = other.gameObject.GetComponentsInChildren<WeaponBase>();
            //foreach (WeaponBase w in weapons)
            //{
            //    if (w != null)
            //        w.ammoCapacity += ammoToGive;
            //}
        }
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
