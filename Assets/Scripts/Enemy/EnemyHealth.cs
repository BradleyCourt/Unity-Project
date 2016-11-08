using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class EnemyHealth : Health
{
    public override void Death()
    {
        if (WaveController.instance != null)
        {
            WaveController.instance.RemainingEnemies -= 1;
        }
        Destroy(gameObject);
    }
}