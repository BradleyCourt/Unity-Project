using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

    public GameObject gameOverPanel;
    public GameObject menuPanel;

    public PlayerHealth playerHealth;
    public GameObject player;

	// Use this for initialization
	void Start ()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerHealth = player.GetComponent<PlayerHealth>();
            gameOverPanel.SetActive(false);
        }
        else
        {
            //Do Nothing
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            if (playerHealth.isDead)
            {
                print("Player Dead - Game Over");
                gameOverPanel.SetActive(true);
            }
        }
        else
        {
            //Do Nothing
        }
	}

    public void QuitGame()
    {
        Application.Quit();
    }

    //void EnterMainMenu()
    //{
    //    if (Input.GetKeyDown("Escape"))
    //    {
    //        menuPanel
    //    }
    //}

    public void LoadLevel()
    {
        //Application.LoadLevel(Application.loadedLevel);
        SceneManager.LoadScene("Learning");
    }
}
