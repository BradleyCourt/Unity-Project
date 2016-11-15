using UnityEngine;
using System.Collections;

public class PauseScript : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
	
	}
	
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
           // PauseGame();
        }
    }

	// Update is called once per frame
	void PauseGame()
    {
       // Time.timeScale = 0;
	}

    public void UnpauseGame()
    {
        //Time.timeScale = 1;
    }
}
