using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class WaveData
{
    public string waveName;
    public List<ObjectPool> pools;
    public int spawnCuantity;
    public Transform spawnPoints;
    public float waveDuration;
}


public class EnemySpawner : MonoBehaviour
{
    [SerializeField] EnemyManager enemyManager;
    private int currentWave = 0;
    private float currentTime = 0;
    private int currentWaveSpawnedCuantity = 0;
    private int currentWaveRecycledCuantity = 0;
    [SerializeField] LevelManager level_manager;
    private bool canSpawn = false;
    [SerializeField] List<WaveData> waveData = new List<WaveData>();
    // Start is called before the first frame update
    public void recycleEnemy(GameObject enemy) 
    {
        foreach (ObjectPool pool in waveData[currentWave].pools)
            pool.Release(enemy);
        currentWaveRecycledCuantity++;
    }

    void SpawnEnemy() 
    {
        int ranEnemy = Random.Range(0, waveData[currentWave].pools.Count);
        GameObject enemy = waveData[currentWave].pools[ranEnemy].Get();
        int ranSpawnPoint = Random.Range(0, waveData[currentWave].spawnPoints.childCount);
        enemy.transform.parent = waveData[currentWave].spawnPoints.GetChild(ranSpawnPoint);
        enemy.transform.localPosition = Vector3.zero;
        enemy.GetComponent<Health>().spawner = this;
        enemy.GetComponent<Health>().enemy_manager = enemyManager;
        enemy.GetComponent<EnemyAI>().manager = enemyManager;
        enemy.GetComponent<EnemyAI>().attackTrigger.hitObjects.Clear();
        enemyManager.RemoveEnemy(enemy,true);
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
        if (currentWave < waveData.Count)
        {
            if (currentWaveSpawnedCuantity < waveData[currentWave].spawnCuantity && currentTime > (waveData[currentWave].waveDuration * Mathf.InverseLerp(0, waveData[currentWave].spawnCuantity, currentWaveSpawnedCuantity)))
                SpawnEnemy();

            if (currentWaveRecycledCuantity >= waveData[currentWave].spawnCuantity && currentTime > waveData[currentWave].waveDuration)
                {
                   level_manager.SetMoving();
                   currentWave++;
                }
        }
    }
}
