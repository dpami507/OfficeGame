using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject[] enemyAssets;

    public List<GameObject> spawnedEnemies;

    public float waveDelay;
    public float waveDelayDecrease = 0.09f;
    public float waveDelayBase = 5;
    public float minDelay = 1f;

    public float waveEnemies;
    public float waveEnemiesIncrease = .1f;
    public float waveEnemiesBase = 5;
    public float maxEnemiesSpawn = 50;

    public float maxEnemies = 1000;

    public Camera playerCamera;

    public int wave = 0;
    public bool spawning;

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
        if (spawning)
        {
            yield return new WaitForSeconds(waveDelay);

            if (spawnedEnemies.Count < maxEnemies)
            {
                wave++;

                waveDelay = (waveDelayDecrease * wave) + waveDelayBase;
                waveDelay = Mathf.Clamp(waveDelay, minDelay, waveDelayBase);

                waveEnemies = Mathf.RoundToInt((waveEnemiesIncrease * wave) + waveEnemiesBase);
                waveEnemies = Mathf.Clamp(waveEnemies, waveEnemiesBase, maxEnemiesSpawn);

                for (int i = 0; i < waveEnemies; i++)
                {
                    int j = UnityEngine.Random.Range(0, enemyAssets.Length);
                    GameObject _enemy = Instantiate(enemyAssets[j].gameObject);
                    _enemy.transform.position = GetSpawnPos();
                    spawnedEnemies.Add(_enemy);
                }
            }

            StartCoroutine(WaveStart());
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