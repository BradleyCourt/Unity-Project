using UnityEngine;
using System.Collections;

public class WaveController : MonoBehaviour
{
    public Transform[] spawnPoints;
    [HideInInspector] public int remainingSpawns;
    private int remainingEnemies;
    [HideInInspector] public int waveNumber;
    public float spawnPeriod;
    public float wavePausePeriod;
    float enemiesPerSecond;
    public int baseEnemiesPerWave;
    public int enemyIncreasePerWave;
    public int waveTimeIncrease;
    public int waveTimeAddition;
    public GameObject enemy; //Change to array when more enemies
    static WaveController instance;

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

        baseEnemiesPerWave -= enemyIncreasePerWave;
        StartCoroutine(SpawnWave());
	}

    IEnumerator SpawnWave()
    {
        float extraTime = waveTimeAddition * Mathf.Floor(waveNumber / waveTimeIncrease);
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
        waveNumber += 1; //####################################BADBADBAD########################
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
