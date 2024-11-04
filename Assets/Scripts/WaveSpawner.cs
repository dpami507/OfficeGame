using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public Enemy[] enemyAssets;

    public List<GameObject> spawnedEnemies;

    public float waveStartTime;

    public Camera playerCamera;

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

        for (int i = 0; i < currentEnemyAmount; i++)
        {
            int j = UnityEngine.Random.Range(0, enemyAssets.Length);
            GameObject _enemy = Instantiate(enemyAssets[j].asset);
            _enemy.transform.position = GetSpawnPos();
            spawnedEnemies.Add(_enemy);
        }

        enemiesSpawning = false;
    }

    Vector2 GetSpawnPos()
    {
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float camHeight = playerCamera.orthographicSize * 1.2f;
        float camWidth = camHeight * screenAspect;

        int topOrBottom = (UnityEngine.Random.Range(0, 2));
        int leftOrRight = (UnityEngine.Random.Range(0, 2));
        int TBorLR = (UnityEngine.Random.Range(0, 2));

        if(TBorLR == 0)
        {
            if (topOrBottom == 0)
            {
                return new Vector2(playerCamera.transform.position.x + UnityEngine.Random.Range(-camWidth, camWidth), 
                    camHeight + playerCamera.transform.position.y);
            }
            else
            {
                return new Vector2(playerCamera.transform.position.x + UnityEngine.Random.Range(-camWidth, camWidth), 
                    -camHeight + playerCamera.transform.position.y);
            }
        }
        else
        {
            if (leftOrRight == 0)
            {
                return new Vector2(camWidth + playerCamera.transform.position.x,
                    UnityEngine.Random.Range(-camHeight, camHeight) + playerCamera.transform.position.y);
            }
            else
            {
                return new Vector2(-camWidth + playerCamera.transform.position.x,
                    UnityEngine.Random.Range(-camHeight, camHeight) + playerCamera.transform.position.y);
            }
        }
    }
}

[Serializable]
public class Enemy
{
    public string name;
    public GameObject asset;
    public int chance;
}