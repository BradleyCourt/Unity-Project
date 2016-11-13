using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerHUD : MonoBehaviour
{
	[Header("Enemy Counter")]
	[Tooltip("Text display showing the remaining number of enemies")]
	public Text enemiesRemainingText;
	[Header("Weapon Display")]
	public Text ammoRemainingText;
	public Text ammoTotalText;
	public Transform bulletIconDisplay;
	public GameObject bulletIconPrefab;
	List<GameObject> bulletIcons = new List<GameObject>(0);
	[Header("Health Display")]
	public ImageFillBar healthBar;
	public PlayerHealth playerHealth;

	public int testAmmo = 5;
	public static PlayerHUD instance;

	void Awake ()
	{
		if (instance == null || instance == this)
		{
			instance = this;
		}
		else
		{
			Destroy(this);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		UpdateEnemyCounter();
		UpdatePlayerHealth();
		UpdateWeaponDisplay();
	}

	public void UpdateEnemyCounter()
	{
		if (WaveController.instance != null)
		{
			enemiesRemainingText.text = WaveController.instance.RemainingEnemies.ToString();
		}
	}

	public void UpdatePlayerHealth()
	{
		if (playerHealth != null && healthBar != null)
		{
			healthBar.UpdateBar(playerHealth.health, playerHealth.maxHealth);
		}
	}

	public void UpdateWeaponDisplay()
	{
		int currentAmmo = testAmmo;
		if (bulletIcons.Count < currentAmmo)
		{
			int increaseBy = currentAmmo - bulletIcons.Count;
			float iconWidth = bulletIconPrefab.GetComponent<RectTransform>().rect.width;
			GameObject newIcon;
			for (int i = 0; i < increaseBy; i += 1)
			{
				newIcon = (GameObject) Instantiate(bulletIconPrefab, bulletIconDisplay);
				bulletIcons.Add(newIcon);

				float xPos = -(iconWidth * (bulletIcons.Count - 1));
				float yPos = 0;

				newIcon.GetComponent<RectTransform>().localPosition = new Vector3(xPos, yPos, 0);
			}
		}
		else if (bulletIcons.Count > currentAmmo)
		{
			int decreaseBy = bulletIcons.Count - currentAmmo;
			GameObject oldIcon;
			for (int i = 0; i < decreaseBy; i += 1)
			{
				oldIcon = bulletIcons[(bulletIcons.Count - 1) - i];
				bulletIcons.Remove(oldIcon);
				Destroy(oldIcon);
			}
		}
	}
}
