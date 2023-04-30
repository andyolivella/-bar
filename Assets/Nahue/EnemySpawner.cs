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
    [SerializeField] int[] spawnCuantity;
    private int currentWave;
    private float currentTime;
    [SerializeField] float waveDuration = 10;
    private int currentWaveSpawnedCuantity = 0;
    private int currentWaveRecycledCuantity = 0;
    [SerializeField] LevelManager level_manager;
    private bool canSpawn = false;
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

    public void recycleEnemy(GameObject enemy) 
    {
        if(meleeEnemys.Contains(enemy))
        {
            for (int i = 0; i < maxCuantityAtSameTime; i++)
            {
                if (meleeEnemys[i] == enemy)
                { 
                    meleeEnemys[i].SetActive(false);
                    currentWaveRecycledCuantity++;
                }
            }
        }
        if(shotEnemys.Contains(enemy))
        {
            for (int i = 0; i < maxCuantityAtSameTime; i++)
            {
                if (shotEnemys[i] == enemy)
                { 
                    shotEnemys[i].SetActive(false);
                    currentWaveRecycledCuantity++;
                }
            }
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
            enemy.transform.parent = spawnPoints[currentWave].GetChild(ranSpawnPoint);
            enemy.transform.localPosition = Vector3.zero;
            enemy.GetComponent<Health>().spawner = this;
            currentWaveSpawnedCuantity++;
            return;
        }
    }

    public void Activate() {
        canSpawn = true;
        currentTime = 0;

    }

    public void Deactivate()
    {
        canSpawn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canSpawn) return;

        currentTime += Time.deltaTime;
        if (currentWave < spawnCuantity.Length)
        {
            if (currentWaveSpawnedCuantity < spawnCuantity[currentWave] && currentTime > (waveDuration * Mathf.InverseLerp(0, spawnCuantity[currentWave], currentWaveSpawnedCuantity)))
                SpawnEnemy();

            if (currentWaveRecycledCuantity >= spawnCuantity[currentWave])
                {
                   level_manager.SetMoving();
                   currentTime = 0;
                   currentWave++;
                   currentWaveSpawnedCuantity = 0;
                   currentWaveRecycledCuantity = 0;
                }
        }
    }
}
