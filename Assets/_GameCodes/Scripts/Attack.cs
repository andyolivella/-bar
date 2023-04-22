using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{



    public bool Isattacking;
    public GameObject hitbox;
    public Animator animator;
    public AudioClip attacksound;

    public int damage = 10;

    public float pushForce = 27f; 
    public float pushHeight = 1.7f;

    private SpecialAttack special;
    private float attackCounter;
    //correct animation layers
    //setup boolean in the throwing script
    //
    private DealDamage dealDamage;
    private TriggerParent attackTrigger;

    // Start is called before the first frame update
    void Start()
    {
        attackCounter = 0;
        if (!hitbox) {Debug.Log ("no hay hitbox"); }

        Isattacking = false;
        dealDamage = GetComponent<DealDamage>();
        attackTrigger = hitbox.GetComponent<TriggerParent>();
        special = GetComponent<SpecialAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("Attack");
        }

        if(Isattacking)
        {
            if(attackTrigger && attackTrigger.colliding && attackTrigger.hitObjects != null && attackTrigger.hitObjects.Count > 0)
            { 
                foreach(GameObject hitGObj in attackTrigger.hitObjects)
                {
                    if(hitGObj && hitGObj.GetComponent<Health>())
                    dealDamage.Attack(hitGObj, damage, pushHeight, pushForce);
                    special.SpecialBarValue += 0.5f;
                }
            }
        }
    }


}
