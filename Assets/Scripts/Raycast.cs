using UnityEngine;
using System.Collections;

public class Raycast : MonoBehaviour
{
    // is this script really needed
    // issac
    void RayCast()
    {
        RaycastHit hit;
        float rayDistance;

        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(transform.position, forward, Color.green);

        if (Physics.Raycast(transform.position, (forward), out hit))
        {
            rayDistance = hit.distance;
          //  print(rayDistance + " " + hit.collider.gameObject.name);
        }
    }

    // Use this for initialization
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        RayCast();
    }
}