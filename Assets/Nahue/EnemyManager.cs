using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private List<GameObject> attackingQueue = new List<GameObject>();
    [SerializeField]float timeForEveryEnemy = 5f;
    float currentTime = 0;
    int currentAttacking = 0;
    private List<GameObject> waitingToRemove = new List<GameObject>();
    private List<float> removeTimer = new List<float>();
    [SerializeField] float removeAttackTurnDelay = 5;
    [SerializeField] float timeBetweenEnemys = 2;
    private bool betweenEnemys = false;

    private void Start()
    {
        betweenEnemys = false;
        currentTime = 0;
        currentAttacking = 0;
    }

    public void RemoveEnemy(GameObject enemy, bool removeInstantly = false) 
    {
        if(removeInstantly)
            attackingQueue.Remove(enemy);

        if (!waitingToRemove.Contains(enemy))
        { 
            waitingToRemove.Add(enemy);
            removeTimer.Add(removeAttackTurnDelay);
        }
    }
    public bool CanAttack(GameObject enemy)
    {
        if (betweenEnemys) return false;

        if (waitingToRemove.Contains(enemy))
        {
            for (int i = 0; i < waitingToRemove.Count; i++)
            {
                if (waitingToRemove[i] == enemy)
                {
                    removeTimer.RemoveAt(i);
                    waitingToRemove.RemoveAt(i);
                    break;
                }
            }
        }

        if (!attackingQueue.Contains(enemy))
            attackingQueue.Add(enemy);

        if (currentAttacking >= attackingQueue.Count)
            currentAttacking = 0;

        if (attackingQueue.Count > 0 && attackingQueue[currentAttacking] == enemy)
            return true;

        return false;
    }

    public void Update()
    {
        currentTime += Time.deltaTime;

        if (betweenEnemys)
        {
            if(currentTime > timeBetweenEnemys)
            {
                betweenEnemys = false;
            }
            else return;
        }

        if (currentTime > timeForEveryEnemy)
        {
            currentTime = 0;
            currentAttacking++;
            betweenEnemys = true;
        }
        if (currentAttacking >= attackingQueue.Count)
            currentAttacking = 0;

        for (int i = 0; i < removeTimer.Count; i++)
        {
            removeTimer[i] -= Time.deltaTime;
            if (removeTimer[i] < 0)
            {
                attackingQueue.Remove(waitingToRemove[i]);
                waitingToRemove.Remove(waitingToRemove[i]);
                removeTimer.Remove(removeTimer[i]);
            }
        }
    }

}
