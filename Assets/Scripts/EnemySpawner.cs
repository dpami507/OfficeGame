using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Enemy[] enemyAssets;

    public List<GameObject> spawnedEnemies;

    public float waveStartTime;

    public int amountPerWave;
    public int currentEnemyAmount;
    public float enemyIncreasePerWave;

    public bool waveOngoing;
    public bool enemiesSpawning;

    public int wave = 0;

    void Update()
    {
        for (int i = 0; i < spawnedEnemies.Count; i++)
        {
            if (spawnedEnemies[i].gameObject == null) { spawnedEnemies.Remove(spawnedEnemies[i].gameObject); }
        }

        if (waveOngoing == false)
        {
            Debug.Log("Starting Wave");
            StartCoroutine(WaveStart());
        }

        if(spawnedEnemies.Count <= 0 && enemiesSpawning == false)
        {
            Debug.Log("Stopping Wave");
            WaveOver();
        }
    }

    void WaveOver()
    {
        waveOngoing = false;
        wave++;
    }

    IEnumerator WaveStart()
    {
        waveOngoing = true;
        enemiesSpawning = true;

        currentEnemyAmount = Mathf.RoundToInt(Mathf.Pow((enemyIncreasePerWave * wave), 2) + amountPerWave); 

        yield return new WaitForSeconds(waveStartTime);

        for (int i = 0; i < amountPerWave; i++)
        {
            int j = UnityEngine.Random.Range(0, enemyAssets.Length);
            spawnedEnemies.Add(Instantiate(enemyAssets[j].asset));
        }

        enemiesSpawning = false;
    }
}

[Serializable]
public class Enemy
{
    public string name;
    public GameObject asset;
    public int chance;
}