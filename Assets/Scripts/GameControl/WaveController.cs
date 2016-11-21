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
	public EnemySpawner[] spawnPoints;
	[Tooltip("Spawnable enemies, spawn weighting, and minimum wave requirement")]
	public GameObject enemy; //Change to array when more enemies
    [Tooltip("Object to store all the spawned enemies. Creates empty object if null")]
    public GameObject enemyStorage;

    [Header("Sound Effects")]
    [Tooltip("Sound effect played when the next wave starts")]
    public AudioClip snd_WaveStart;

	//Hidden wave data
	private int remainingSpawns; //Enemies that have yet to be spawned for the current wave
	[HideInInspector]
	public int waveNumber; //Current wave number
	[HideInInspector]
	private int remainingEnemies = 0; //Enemies left to be killed before the next wave spawns

	//Events
	public delegate void WaveEvent();
	public event WaveEvent OnWaveEnd;
	public event WaveEvent OnWaveStart;
	public event WaveEvent OnEnemyDeath;

	//Functionality
    public static WaveController instance;

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
			if (OnEnemyDeath != null) { OnEnemyDeath(); } //Broadcast Event

			if (remainingEnemies <= 0)
            {
                waveNumber += 1;
                StartCoroutine(WavePause());
				if (OnWaveEnd != null) { OnWaveEnd(); } //Broadcast Event
			}
        }
    }
    #endregion Properties

	void Awake()
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

    void Start ()
    {

        if (enemyStorage == null)
        {
            enemyStorage = Instantiate(new GameObject("Enemies"));
        }

        EnemySpawner.enemyStorage = enemyStorage;
        baseEnemiesPerWave -= enemyIncreasePerWave; //Accounts for spawnWave adding the increase during the initial wave
        StartCoroutine(WavePause());
	}

    IEnumerator SpawnWave()
    {
        float extraTime = spawnPeriodTimeAddition * Mathf.Floor(waveNumber / spawnPeriodIncreaseFrequency);
        remainingEnemies = remainingSpawns = CalcEnemiesPerWave();
        enemiesPerSecond = CalcEnemiesPerSecond(remainingSpawns, spawnPeriod + extraTime);
        print("EPS: " + enemiesPerSecond);
        print("Enemies this wave: " + remainingSpawns);

		if (OnWaveStart != null) { OnWaveStart(); } //Broadcast Event

		int enemiesPerPulse;
        while (remainingSpawns > 0)
        {
            enemiesPerPulse = (remainingSpawns < spawnPoints.Length) ? remainingSpawns : spawnPoints.Length;
            remainingSpawns -= enemiesPerPulse;
            for (int i = 0; i < enemiesPerPulse; i += 1)
            {
                spawnPoints[i].SpawnEnemy(enemy);
            }
            yield return new WaitForSeconds(1 / enemiesPerSecond);
        }
    }

    IEnumerator WavePause()
    {
        print(wavePausePeriod + " seconds until new wave");
        yield return new WaitForSeconds(wavePausePeriod);
        print("Starting next wave");

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
