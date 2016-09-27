using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TopDownController : MonoBehaviour
{
    public Text scoreText;
    public int score = 0;
    public float speed = 10f;
    public float gravity = 20f;

    public Vector3 moveDirection = new Vector3(0,0,0);
    public CharacterController controller;

    // Use this for initialization
    void Start ()
    {
        controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
            moveDirection = Camera.main.transform.TransformDirection(moveDirection);
            moveDirection *= speed * Time.deltaTime;

        }
        moveDirection.y -= (gravity * Time.deltaTime);
        // Move Character Controller
        controller.Move (moveDirection);
    }
    void OnTriggerEnter(Collider Pickup)
    {
        if (Pickup.gameObject.tag == "MedPack")
        {
            Destroy(Pickup.gameObject);
            score++;
            scoreText.text = "Score : " + score;
        }
    }
    }
