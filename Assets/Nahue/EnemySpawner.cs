using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] EnemyManager enemyManager;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] int[] spawnCuantity;
    private int currentWave = 0;
    private float currentTime = 0;
    [SerializeField] List<float> waveDuration = new List<float>();
    private int currentWaveSpawnedCuantity = 0;
    private int currentWaveRecycledCuantity = 0;
    [SerializeField] LevelManager level_manager;
    private bool canSpawn = false;
    [SerializeField] ObjectPool[] enemysPools;
    // Start is called before the first frame update
    public void recycleEnemy(GameObject enemy) 
    {
        foreach (ObjectPool pool in enemysPools)
            pool.Release(enemy);
        currentWaveRecycledCuantity++;
    }

    void SpawnEnemy() 
    {
        int ranEnemy = Random.Range(0, enemysPools.Length);
        GameObject enemy = enemysPools[ranEnemy].Get();
        int ranSpawnPoint = Random.Range(0, spawnPoints[currentWave].childCount);
        enemy.transform.parent = spawnPoints[currentWave].GetChild(ranSpawnPoint);
        enemy.transform.localPosition = Vector3.zero;
        enemy.GetComponent<Health>().spawner = this;
        enemy.GetComponent<Health>().enemy_manager = enemyManager;
        enemy.GetComponent<EnemyAI>().manager = enemyManager;
        currentWaveSpawnedCuantity++;
    }

    public void Activate() {
        canSpawn = true;
        currentTime = 0;
        currentWaveSpawnedCuantity = 0;
        currentWaveRecycledCuantity = 0;
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
            if (currentWaveSpawnedCuantity < spawnCuantity[currentWave] && currentTime > (waveDuration[currentWave] * Mathf.InverseLerp(0, spawnCuantity[currentWave], currentWaveSpawnedCuantity)))
                SpawnEnemy();

            if (currentWaveRecycledCuantity >= spawnCuantity[currentWave] && currentTime > waveDuration[currentWave])
                {
                   level_manager.SetMoving();
                   currentWave++;
                }
        }
    }
}
