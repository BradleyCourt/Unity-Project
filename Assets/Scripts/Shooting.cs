using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour
{
    public GameObject projectile;
    [Space()]
    public float bulletForce = 100f;
    public float bulletLifeTime = 5f;

    public float fireInterval = 0.25f;
    private float nextFireTime;

    public float deadZone = 0.1f;

    private Vector2 inputVector;

	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject obj = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
            Destroy(obj, bulletLifeTime);

            Rigidbody body = obj.GetComponent<Rigidbody>();
            body.AddForce(transform.forward * bulletForce, ForceMode.Impulse);

            Physics.IgnoreCollision(obj.GetComponent<Collider>(), GetComponent<Collider>());
        }

        inputVector.x = Input.GetAxis("LookHorizontal");
        inputVector.y = Input.GetAxis("LookVertical");

        if (inputVector.magnitude > deadZone)
        {
            if (Time.time > nextFireTime)
            {
                nextFireTime = Time.time + fireInterval;

                Debug.Log("Fired bullet in direction: " + inputVector);
            }
        }
	}
}
