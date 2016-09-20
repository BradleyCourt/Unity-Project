using UnityEngine;
using System.Collections;

public class Raycast : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        RaycastHit hit;
        Ray landingRay = new Ray(transform.position, Vector3.forward);

        if (Physics.Raycast(landingRay, out hit))
        {
            if (hit.collider.tag == "Enemy")
            {

            }
        }
	}
}
