using UnityEngine;
using InControl;
using System.Collections;

public class LookScript : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    public bool canSee = false;
    public float deadzone = 0.2f;
    public LayerMask mask;

    void Start ()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);


    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        Vector3 angles = new Vector3();

        angles.x = transform.rotation.x;
        angles.y = transform.rotation.y;
        angles.z = transform.rotation.z;



        InputDevice device = InputManager.ActiveDevice;


        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit, 1000f, mask);

        if (hit.collider)
        {
           
            Vector3 aimPoint = hit.point;
            Vector3 playerToAim = aimPoint - transform.position;       
            Quaternion charRotation = Quaternion.LookRotation(playerToAim, Vector3.up);
            charRotation.x = 0;
            charRotation.z = 0;
            transform.rotation = charRotation;
        }

        // CONTROLLER LOOK

        Vector3 lookVec = new Vector3(-device.RightStick.X, 0, -device.RightStick.Y);
        if (lookVec.magnitude > 0.5)
        {
            Quaternion conRotate = Quaternion.FromToRotation(new Vector3(-1, 0, -15f), lookVec);

            transform.rotation = conRotate;
        }

    }
}
