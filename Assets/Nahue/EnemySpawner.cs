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
    // Start is called before the first frame update
    void Start()
    {
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

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentWave < timeToSpawn.Length && currentTime > timeToSpawn[currentWave])
        {
            for (int j = 0; j < spawnCuantity[currentWave]; j++)
            {
                int ranEnemy = Random.Range(0, 2);
                if (ranEnemy == 0)
                {
                    foreach (GameObject meleeEnemy in meleeEnemys)
                    { 
                        if(!meleeEnemy.activeInHierarchy)
                        {
                            meleeEnemy.SetActive(true);
                            int ranSpawnPoint = Random.Range(0, spawnPoints.Length);
                            meleeEnemy.transform.parent = spawnPoints[ranSpawnPoint];
                            meleeEnemy.transform.localPosition = Vector3.zero;
                        }
                    }
                }
                else
                {
                    foreach (GameObject shootEnemy in shotEnemys)
                    {
                        if (!shootEnemy.activeInHierarchy)
                        {
                            shootEnemy.SetActive(true);
                            int ranSpawnPoint = Random.Range(0, spawnPoints.Length);
                            shootEnemy.transform.parent = spawnPoints[ranSpawnPoint];
                            shootEnemy.transform.localPosition = Vector3.zero;
                        }
                    }
                }
            }
            currentWave++;
        }
    }
}
