using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class WaveController : MonoBehaviour
{
	[Header("Waves")]
	[Tooltip("Base number of enemies, before increase per wave")]
	public int baseEnemiesPerWave;
	[Tooltip("Number of additional enemies per wave (Excludes first wave)")]
	public int enemyIncreasePerWave;
	[Tooltip("Wait period between clearing current wave and spawning the next")]
	public float wavePausePeriod; 

	[Header("Spawn Rate")]
	[Tooltip("Period of time in which to spawn all enemies for the current wave")]
    public float spawnPeriod;
	[Tooltip("How many waves per increase to the spawn period")]
	public int spawnPeriodIncreaseFrequency;
	[Tooltip("How long to increase the spawn period by")]
	public float spawnPeriodTimeAddition;
	float enemiesPerSecond; //Spawn rate based on total enemies, spawn points, and spawn period

	[Header("Spawning")]
	[Tooltip("Possible spawn locations for enemies")]
	public Transform[] spawnPoints;
	[Tooltip("Spawnable enemies, spawn weighting, and minimum wave requirement")]
	public GameObject enemy; //Change to array when more enemies

    [Header("Sound Effects")]
    [Tooltip("Sound effect played when the next wave starts")]
    public AudioClip snd_WaveStart;

	//Hidden wave data
	private int remainingSpawns; //Enemies that have yet to be spawned for the current wave
	[HideInInspector]
	public int waveNumber; //Current wave number
	[HideInInspector]
	private int remainingEnemies; //Enemies left to be killed before the next wave spawns

	//Functionality
    static WaveController instance;
    AudioSource audioSource;

    #region Properties
    public int RemainingEnemies
    {
        get
        {
            return remainingEnemies;
        }
        set
        {
            remainingEnemies = value;
            if (remainingEnemies <= 0)
            {
                waveNumber += 1;
                StartCoroutine(WavePause());
            }
        }
    }
    #endregion Properties

    void Start ()
    {
        if (instance == null || instance == this)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogWarning("No AudioSource Found!", gameObject);
        }


        baseEnemiesPerWave -= enemyIncreasePerWave; //Accounts for spawnWave adding the increase during the initial wave
        StartCoroutine(SpawnWave());
	}

    IEnumerator SpawnWave()
    {
        float extraTime = spawnPeriodTimeAddition * Mathf.Floor(waveNumber / spawnPeriodIncreaseFrequency);
        remainingEnemies = remainingSpawns = CalcEnemiesPerWave();
        enemiesPerSecond = CalcEnemiesPerSecond(remainingSpawns, spawnPeriod + extraTime);
        print("EPS: " + enemiesPerSecond);
        print("Enemies this wave: " + remainingSpawns);

        int enemiesPerPulse;
        while (remainingSpawns > 0)
        {
            enemiesPerPulse = (remainingSpawns < spawnPoints.Length) ? remainingSpawns : spawnPoints.Length;
            remainingSpawns -= enemiesPerPulse;
            for (int i = enemiesPerPulse; i > 0; i -= 1)
            {
                GameObject bob = Instantiate(enemy);
                bob.GetComponent<AudioSource>().pitch = Random.Range(0.8f, 1.2f);
                //bob.GetComponent<AudioSource>(). = Random.Range(0.0f, 0.5f);
            }
            yield return new WaitForSeconds(1 / enemiesPerSecond);
        }
        // ###############Added for testing purposes! Remove ######################
        StartCoroutine(WavePause());
        // ###############Added for testing purposes! Remove ######################
    }

    IEnumerator WavePause()
    {
        print(wavePausePeriod + " seconds until new wave");
        yield return new WaitForSeconds(wavePausePeriod);
        print("Starting next wave");
        waveNumber += 1;
        if (audioSource != null)
        {
            audioSource.PlayOneShot(snd_WaveStart);
        }
        StartCoroutine(SpawnWave());
    }

    float CalcEnemiesPerSecond(int totalEnemies, float totalTime)
    {
        if (spawnPoints.Length == 0) { return 0; }

        float enemiesPerSecond = ((totalEnemies / totalTime) / spawnPoints.Length);
        return enemiesPerSecond;
    }

    int CalcEnemiesPerWave()
    {
        int enemiesPerWave = baseEnemiesPerWave + (enemyIncreasePerWave * waveNumber);
        return enemiesPerWave;
    }
}
