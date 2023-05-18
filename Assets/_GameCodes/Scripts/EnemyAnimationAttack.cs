using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationAttack : MonoBehaviour
{
    [SerializeField] EnemyAI enemyAI;
    [SerializeField] Shoot enemyShoot;

    public void StartAttack()
    {
        enemyAI.dealingDamage = true;
    }

    public void EndAttack()
    {
        enemyAI.dealingDamage = false;
    }

    public void StartShoot() 
    {
        enemyShoot.shoot();
    }

}
