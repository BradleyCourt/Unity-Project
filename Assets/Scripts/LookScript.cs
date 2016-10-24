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
        InputDevice device = InputManager.ActiveDevice;

        float horizontal = Input.GetAxis("Horizontal") + device.RightStick.X;
        float vertical = Input.GetAxis("Vertical") + device.RightStick.Y;

        //Vector3 conVec = new Vector3(-Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //if (conVec.magnitude > 0.5)
        //{
        //    Quaternion charRotation = Quaternion.FromToRotation(new Vector3(-1, 0, 0), conVec);
        //    transform.rotation = charRotation;
        //}

        // if (Mathf.Abs(horizontal) <= deadzone)
        //{
        //Debug.Log(device.RightStick.X);
        //    transform.Rotate(0, device.RightStick.Y, 0);
        //transform.LookAt(gunbarrel);
        // }
        /** old code
        Vector2 playerPos = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        Vector2 mousePos = Input.mousePosition;

        Vector2 posDiff = mousePos - playerPos;
        Vector3 playerRot = gameObject.transform.rotation.eulerAngles;

        playerRot.y = Mathf.Atan2(posDiff.x, posDiff.y) * Mathf.Rad2Deg;
    **/


            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit, 1000f, mask);

        if (hit.collider)
        {
            // Rotate player to look at where the raycast hit
            // use hit.point
            Vector3 aimPoint = hit.point;
            Vector3 playerToAim = aimPoint - transform.position;
            //Quaternion charRotation = Quaternion.FromToRotation(transform.position, aimPoint);
            Quaternion charRotation = Quaternion.LookRotation(playerToAim, Vector3.up);
            charRotation.x = 0;
            charRotation.z = 0;
            transform.rotation = charRotation;
            //Camera.main.transform.rotation;
            // cancel out main camera rotation
        }

        Vector3 lookVec = new Vector3(-Input.GetAxis("LookVertical"), 0, Input.GetAxis("LookHorizontal"));
        //  Debug.Log("Magnitude " + lookVec);
        if (lookVec.magnitude > 0.5)
        {
            Quaternion charRotation = Quaternion.FromToRotation(new Vector3(-1, 0, 0), lookVec);
            transform.rotation = charRotation;
        }

        //Debug.Log(lookVec);

    }
}
