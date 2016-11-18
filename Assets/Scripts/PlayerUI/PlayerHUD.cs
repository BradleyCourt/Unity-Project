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
    public CombatController playerWeapons;
	List<GameObject> bulletIcons = new List<GameObject>(0);
	[Header("Health Display")]
	public ImageFillBar healthBar;
	public PlayerHealth playerHealth;
	[Header("Wave Display")]
	public GameObject waveDisplay;
	public float waveDisplayTime = 3.0f;

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

	void Start()
	{
		//Assign updates to events and run initial update
		if (WaveController.instance != null)
		{
			UpdateEnemyCounter();
			WaveController.instance.OnEnemyDeath += UpdateEnemyCounter;
			WaveController.instance.OnWaveStart += UpdateEnemyCounter;
			WaveController.instance.OnWaveEnd += ShowWaveDisplay;
		}

		if (playerHealth != null)
		{
			UpdatePlayerHealth();
			playerHealth.OnHealthChange += UpdatePlayerHealth;
		}
	}

	void OnDestroy()
	{
		if (WaveController.instance != null)
		{
			WaveController.instance.OnEnemyDeath -= UpdateEnemyCounter;
			WaveController.instance.OnWaveStart -= UpdateEnemyCounter;
			WaveController.instance.OnWaveEnd -= ShowWaveDisplay;
		}

		if (playerHealth != null)
		{
			playerHealth.OnHealthChange -= UpdatePlayerHealth;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		UpdateWeaponDisplay();
	}

	public void UpdateEnemyCounter()
	{
		enemiesRemainingText.text = WaveController.instance.RemainingEnemies.ToString();
	}

	public void UpdatePlayerHealth()
	{
		if (healthBar != null)
		{
			healthBar.UpdateBar(playerHealth.health, playerHealth.maxHealth);
		}
	}

	public void UpdateWeaponDisplay()
	{
		int currentAmmo = playerWeapons.selectedWeapon.currentAmmo;
        int totalAmmo = playerWeapons.selectedWeapon.ammoCapacity;

        ammoRemainingText.text = currentAmmo.ToString();
        ammoTotalText.text = totalAmmo.ToString();

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
                int index = (bulletIcons.Count - 1) - i;
                if (index >= 0 && index < bulletIcons.Count)
                {
                    oldIcon = bulletIcons[index];
                    bulletIcons.Remove(oldIcon);
                    Destroy(oldIcon);
                }
			}
		}

        
	}

	public void ShowWaveDisplay()
	{
		print("terry");
		StartCoroutine(TimedWaveAnnoucement());
	}

	public IEnumerator TimedWaveAnnoucement()
	{
		waveDisplay.SetActive(true);
		print("bob");
		yield return new WaitForSeconds(waveDisplayTime);
		waveDisplay.SetActive(false);
	}
}
