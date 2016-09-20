using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public Transform target;

    private Vector3 m_offset;

	// Use this for initialization
	void Start ()
    {
        m_offset = transform.position - target.position;
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.LookAt(target);
     //   transform.position = target.position + m_offset;


	
	}
}
