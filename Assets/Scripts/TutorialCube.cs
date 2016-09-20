using UnityEngine;
using System.Collections;

public class TutorialCube : MonoBehaviour
{
    public float maxTorque = 100;
    private Rigidbody m_rigidbody;
    // Use this for initialization
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        float movementX = Input.GetAxis("Horizontal");
        float movementZ = Input.GetAxis("Vertical");
        //    transform.position += Vector3.right * movementX * Time.deltaTime;
        m_rigidbody.AddTorque(movementZ * maxTorque, 0, movementX * maxTorque);
    }
}
