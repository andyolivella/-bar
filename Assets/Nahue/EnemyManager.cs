using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private List<EnemyAI> attackingQueue = new List<EnemyAI>();
    [SerializeField]float timeBetweenAttacks = 5f;
    float currentTime;
    int currentAttacking;

    private void Start()
    {
        currentTime = 0;
        currentAttacking = 0;
    }

    public void RemoveEnemy(EnemyAI enemy) 
    {
        if(attackingQueue.Contains(enemy))
            attackingQueue.Remove(enemy);
    }
    public bool CanAttack(EnemyAI enemy)
    {
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
        if (currentTime > timeBetweenAttacks)
        {
            currentTime = 0;
            currentAttacking++;
        }
        if (currentAttacking >= attackingQueue.Count)
            currentAttacking = 0;
    }

}
