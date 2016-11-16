using UnityEngine;
using System.Collections;

public class AmmoPowerUpSpawner : MonoBehaviour
{
    public GameObject Prefab;
    GameObject ammo;
    public float respwanTimer = 30;
    public int ammoToGive = 30;
    private float originalRepsawnTimer;

    // Use this for initialization
    void Start ()
    {
        ammo = Instantiate(Prefab);
        ammo.transform.position = transform.position;
        ammo.GetComponent<AmmoPickup>().ammoToGive = ammoToGive;
        originalRepsawnTimer = respwanTimer;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (ammo.gameObject.activeSelf == false)
        {
            if (respwanTimer <= 0)
            {
                ammo.gameObject.SetActive(true);
                respwanTimer = originalRepsawnTimer;
            }
            else
                respwanTimer -= Time.deltaTime;
        }
    }

}
