using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationAttack : MonoBehaviour
{
    public Attack melee_attack;
    public SpecialAttack special_attack;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartMeleeAttack()
    {
        melee_attack.Isattacking = true;

    }

    public void EndMeleeAttack()
    {
        melee_attack.Isattacking = false;

    }

    public void StartNextMoveCounter()
    {
        melee_attack.StartNextMoveCounter();
    }

    public void StartSpecialAttack1()
    {
        special_attack.StartAttack();

    }

    public void EndSpecialAttack1()
    {
        special_attack.EndAttack();

    }

    
}
