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
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        gameOverPanel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (playerHealth.isDead)
        {
            print("Player Dead - Game Over");
            gameOverPanel.SetActive(true);
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

    public void TryAgain()
    {
        //Application.LoadLevel(Application.loadedLevel);
        SceneManager.LoadScene("Learning");
    }
}
