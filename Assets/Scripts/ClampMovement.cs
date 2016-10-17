using UnityEngine;
using System.Collections;


public class ClampMovement : MonoBehaviour {

    public Transform target;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        Mathf.Clamp(transform.position.x, 0, 0);
        Mathf.Clamp(transform.position.y, 0, 0);
        Mathf.Clamp(transform.position.z, 0, 0);

    }
}
