using UnityEngine;
using System.Collections;

public class WaveController : MonoBehaviour
{
    public Transform[] spawnPoints;
    public int remainingSpawns;
    public int remainingEnemies;
    public int waveNumber;
    public float spawnPeriod;
    float enemiesPerSecond;
    public int baseEnemiesPerWave;
    public int enemyIncreasePerWave;
    static WaveController instance;


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
        remainingEnemies = remainingSpawns = CalcEnemiesPerWave();
        enemiesPerSecond = CalcEnemiesPerSecond(remainingSpawns);
        print("EPS: " + enemiesPerSecond);
        int enemiesPerPulse;
        while (remainingSpawns > 0)
        {
            enemiesPerPulse = (remainingSpawns < spawnPoints.Length) ? remainingSpawns : spawnPoints.Length;
            remainingSpawns -= enemiesPerPulse;
            for (int i = enemiesPerPulse; i > 0; i -= 1)
            {
                print(spawnPoints[i - 1].position.ToString());
            }
            yield return new WaitForSeconds(enemiesPerSecond);
        }
    }

    float CalcEnemiesPerSecond(int totalEnemies)
    {
        if (spawnPoints.Length == 0) { return 0; }

        float enemiesPerSecond = ((totalEnemies / spawnPeriod) / spawnPoints.Length);
        return enemiesPerSecond;
    }

    int CalcEnemiesPerWave()
    {
        int enemiesPerWave = baseEnemiesPerWave + (enemyIncreasePerWave * waveNumber);
        return enemiesPerWave;
    }
}
