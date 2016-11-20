using UnityEngine;
using System.Collections;

public class PlayerSoundHandler : MonoBehaviour
{
	public AudioClip healthPickup;
	public AudioClip waveStart;

	public PlayerHealth playerHealth;

	AudioSource audSrc_healthPickup;
	AudioSource audSrc_lowHealth;
	AudioSource audSrc_waveStart;
	AudioSource audSrc_footsteps;

	void Awake()
	{
		audSrc_healthPickup = gameObject.AddComponent<AudioSource>();
		audSrc_waveStart = gameObject.AddComponent<AudioSource>();
		audSrc_waveStart.clip = waveStart;
		audSrc_healthPickup.clip = healthPickup;
	}

	void Start ()
	{
		WaveController.instance.OnWaveStart += WaveStart;
		playerHealth.OnHealthChange += CheckHealth;
	}

	void OnDestroy()
	{

	}

	void CheckHealth()
	{

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
