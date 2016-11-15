using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public static GameObject enemyStorage;

    public void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, transform.position, Quaternion.identity, enemyStorage.transform);
    }
}
