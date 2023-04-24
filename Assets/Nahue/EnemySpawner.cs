using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] GameObject enemyMeleePrefab;
    [SerializeField] GameObject enemyShootPrefab;
    [SerializeField] int maxCuantityAtSameTime = 30;
    private List<GameObject> meleeEnemys;
    private List<GameObject> shotEnemys;
    [SerializeField] int[] timeToSpawn;
    [SerializeField] int[] spawnCuantity;
    private int currentWave;
    private float currentTime;
    [SerializeField] float lastWaveDuration = 10;
    private int currentWaveSpawnedCuantity = 0;
    // Start is called before the first frame update
    void Start()
    {
        currentWaveSpawnedCuantity = 0;
        meleeEnemys = new List<GameObject>();
        shotEnemys = new List<GameObject>();
        currentWave = 0;
        for (int i = 0; i < maxCuantityAtSameTime; i++)
        {
            meleeEnemys.Add(Instantiate(enemyMeleePrefab));
            shotEnemys.Add(Instantiate(enemyShootPrefab));
        }
        for (int i = 0; i < maxCuantityAtSameTime; i++)
        {
            meleeEnemys[i].SetActive(false);
            shotEnemys[i].SetActive(false);
        }
    }

    void SpawnEnemy() 
    {
        int ranEnemy = Random.Range(0, 2);
        GameObject enemy = null;
        if (ranEnemy == 0)
        {
            foreach (GameObject meleeEnemy in meleeEnemys)
            {
                if (!meleeEnemy.activeInHierarchy)
                {
                    enemy = meleeEnemy;
                    break;
                }
            }
        }
        else
        {
            foreach (GameObject shootEnemy in shotEnemys)
            {
                if (!shootEnemy.activeInHierarchy)
                {
                    enemy = shootEnemy;
                    break;
                }
            }
        }

        if (enemy != null)
        { 
            enemy.SetActive(true);
            int ranSpawnPoint = Random.Range(0, spawnPoints.Length);
            enemy.transform.parent = spawnPoints[ranSpawnPoint];
            enemy.transform.localPosition = Vector3.zero;
            currentWaveSpawnedCuantity++;
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentWave < timeToSpawn.Length && currentTime > timeToSpawn[currentWave])
        {
            float waveDuration;
            if (currentWave == timeToSpawn.Length - 1)
                waveDuration = lastWaveDuration;
            else
                waveDuration = (timeToSpawn[currentWave + 1] - timeToSpawn[currentWave]);
            if (currentTime > (timeToSpawn[currentWave] + waveDuration * Mathf.InverseLerp(0, spawnCuantity[currentWave], currentWaveSpawnedCuantity)))
                SpawnEnemy();

            if (currentWave < timeToSpawn.Length - 1)
                if (currentTime > timeToSpawn[currentWave + 1])
                { 
                   currentWave++;
                   currentWaveSpawnedCuantity = 0;
                }
        }
    }
}
