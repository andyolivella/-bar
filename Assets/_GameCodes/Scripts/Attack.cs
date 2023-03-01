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

    public float attackDuration = 2;

    public float pushForce = 27f; 
    public float pushHeight = 1.7f;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("Attack");
            attackCounter = attackDuration;
            Isattacking = true;
        }

        if(Isattacking)
        {
            attackCounter -= Time.deltaTime;
            
            if(attackTrigger && attackTrigger.colliding && attackTrigger.hitObjects != null && attackTrigger.hitObjects.Count > 0)
            { 
                foreach(GameObject hitGObj in attackTrigger.hitObjects)
                {
                    if(hitGObj && hitGObj.GetComponent<Health>())
                    dealDamage.Attack(hitGObj, damage, pushHeight, pushForce);
                }
            }
            
            if (attackCounter < 0)
                Isattacking = false;

        }
    }
}
