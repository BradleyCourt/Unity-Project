using UnityEngine;
using System.Collections;

public class PlayerSoundHandler : MonoBehaviour
{
	public AudioClip healthPickup;
	public AudioClip waveStart;
	public AudioClip lowHealth;

	public PlayerHealth playerHealth;

	AudioSource audSrc_healthPickup;
	AudioSource audSrc_lowHealth;
	AudioSource audSrc_waveStart;
	AudioSource audSrc_footsteps;

	void Awake()
	{
		audSrc_healthPickup = gameObject.AddComponent<AudioSource>();
		audSrc_waveStart = gameObject.AddComponent<AudioSource>();
		audSrc_lowHealth = gameObject.AddComponent<AudioSource>();

		audSrc_waveStart.clip = waveStart;
		audSrc_healthPickup.clip = healthPickup;
		audSrc_lowHealth.clip = lowHealth;
		audSrc_lowHealth.loop = true;
	}

	void Start ()
	{
		WaveController.instance.OnWaveStart += WaveStart;
		HealthPack.PickedUp += HealthPickup;
		playerHealth.OnHealthChange += CheckHealth;
	}

	void OnDestroy()
	{
		WaveController.instance.OnWaveStart -= WaveStart;
		HealthPack.PickedUp -= HealthPickup;
		playerHealth.OnHealthChange -= CheckHealth;
	}

	void CheckHealth()
	{
		if (playerHealth.health <= 35 && audSrc_lowHealth.isPlaying == false)
		{
			audSrc_lowHealth.Play();
		}
		else if (playerHealth.health > 35)
		{
			audSrc_lowHealth.Stop();
		}
	}

	void HealthPickup()
	{
		audSrc_healthPickup.Play();
	}

	void WaveStart()
	{
		audSrc_waveStart.Play();
	}
}
