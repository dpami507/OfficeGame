using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public EnemyPrefab[] enemyAssets;

    public List<GameObject> spawnedEnemies;

    public List<GameObject> pooledObjects;

    public float waveDelay;
    public float waveDelayDecrease = 0.09f;
    public float waveDelayBase = 5;
    public float minDelay = 1f;

    public float waveEnemies;
    public float waveEnemiesIncrease = .1f;
    public float waveEnemiesBase = 5;
    public float maxEnemiesSpawn = 50;

    public float maxEnemies = 1000;
    float accumulatedWeights;
    System.Random rand = new System.Random();

    public Camera playerCamera;

    public int wave = 0;
    public bool spawning;

    private void Awake()
    {
        CalcWeights();
    }

    private void Start()
    {
        spawning = true;
        StartCoroutine(WaveStart());
    }

    private void Update()
    {
        foreach (GameObject item in spawnedEnemies)
        {
            if(item == null)
            {
                spawnedEnemies.Remove(item);
            }
        }
    }

    IEnumerator WaveStart()
    {
        yield return new WaitForSeconds(waveDelay);

        if (spawning)
        {
            if (spawnedEnemies.Count - maxEnemies < maxEnemies)
            {
                wave++;

                waveDelay = (waveDelayDecrease * wave) + waveDelayBase;
                waveDelay = Mathf.Clamp(waveDelay, minDelay, waveDelayBase);

                waveEnemies = Mathf.RoundToInt((waveEnemiesIncrease * wave) + waveEnemiesBase);
                waveEnemies = Mathf.Clamp(waveEnemies, waveEnemiesBase, maxEnemiesSpawn);

                SpawnEnemies();
            }

            StartCoroutine(WaveStart());
        }
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < waveEnemies; i++)
        {
            EnemyPrefab randomEnemy = enemyAssets[GetRandEnemyIndex()];

            GameObject poolObj = IsTypeInPool(randomEnemy.prefab);

            if (poolObj != null)
            {
                Debug.Log("Pulling Enemy from Pool");
                poolObj.GetComponent<EnemyAI>().EnemyStart();
                poolObj.transform.position = GetSpawnPos();
                spawnedEnemies.Add(poolObj);
            }
            else
            {
                GameObject _enemy = Instantiate(randomEnemy.prefab, this.transform);
                _enemy.transform.position = GetSpawnPos();
                spawnedEnemies.Add(_enemy);
                pooledObjects.Add(_enemy);
                Debug.Log("Spawning New");
            }
        }
    }

    GameObject IsTypeInPool(GameObject enemy)
    {
        EnemyAI ai = enemy.GetComponent<EnemyAI>();
        string type = ai.enemyType;

        for (int i = 0; i < pooledObjects.Count; i++)
        {
            EnemyAI poAi = pooledObjects[i].GetComponent<EnemyAI>();

            if (poAi.enemyType == type && poAi.isActiveAndEnabled == false)
            {
                return poAi.gameObject;
            }
        }    

        return null;
    }

    int GetRandEnemyIndex()
    {
        double r = rand.NextDouble() * accumulatedWeights;

        for (int i = 0; i < enemyAssets.Length; i++)
            if (enemyAssets[i].levelStart < wave)
            {
                if (enemyAssets[i]._weight >= r)
                    return i;
            }
        return 0;
    }

    void CalcWeights()
    {
        accumulatedWeights = 0f;
        foreach (var enemy in enemyAssets)
        {
            accumulatedWeights += enemy.percentChance;
            enemy._weight = accumulatedWeights;
        }
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

[System.Serializable]
public class EnemyPrefab
{
    public GameObject prefab;
    [Range(0f, 100f)] public float percentChance;
    public float levelStart;
    [HideInInspector] public float _weight;
}
