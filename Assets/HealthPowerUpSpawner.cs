using UnityEngine;
using System.Collections;

public class HealthPowerUpSpawner : MonoBehaviour
{
    public GameObject Prefab;
    GameObject health;
    public float respwanTimer = 30;
    public int healthToGive = 30;
    private float originalRepsawnTimer;

    // Use this for initialization
    void Start ()
    {
        health = Instantiate(Prefab);
        health.transform.position = transform.position;
        health.GetComponent<HealthPack>().healthToGive = healthToGive;
        originalRepsawnTimer = respwanTimer;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (health.gameObject.activeSelf == false)
        {
            if (respwanTimer <= 0)
            {
                health.gameObject.SetActive(true);
                respwanTimer = originalRepsawnTimer;
            }
            else
                respwanTimer -= Time.deltaTime;
            }
    }

}
