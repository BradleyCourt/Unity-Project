using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using InControl;

public class TopDownController : MonoBehaviour
{
    public Text scoreText;
    public int score = 0;
    public float speed = 10f;
    public float gravity = 20f;
    public float deadzone = 0.2f;

    public Vector3 moveDirection = new Vector3(0,0,0);
    public CharacterController controller;

    // Use this for initialization
    void Start ()
    { 
        controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        InputDevice device = InputManager.ActiveDevice;

            float horizontal = Input.GetAxis("Horizontal") + device.LeftStick.X;
            float vertical = Input.GetAxis("Vertical") + device.LeftStick.Y;

            moveDirection *= speed * Time.deltaTime;

            if (Mathf.Abs(horizontal) <= deadzone)
            {
                horizontal = 0;
            }
        if (Mathf.Abs(vertical) <= deadzone)
        {
            vertical = 0;
        }

        moveDirection = new Vector3(horizontal, 0, vertical);
        moveDirection = Camera.main.transform.TransformDirection(moveDirection);

        moveDirection *= speed * Time.deltaTime;
        moveDirection.y -= (gravity * Time.deltaTime);
        
        controller.Move (moveDirection);
    }
    void OnTriggerEnter(Collider Pickup)
    {
        if (Pickup.gameObject.tag == "MedPack")
        {
            //Destroy(Pickup.gameObject);
            
            //score++;
            //scoreText.text = "Score : " + score;
        }
    }
    }
