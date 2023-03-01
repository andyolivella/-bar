using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttack : MonoBehaviour
{
    public GameObject trigger;
    private TriggerParent triggerparent;
    public Animator animator;
    private bool Isattacking;
    private float attackCounter;
    public float attackDuration = 5;
    private DealDamage dealDamage;

    public float pushForce = 27f;
    public float pushHeight = 1.7f;
    public int damage = 100;


    // Start is called before the first frame update
    void Start()
    {
        Isattacking = false;
        triggerparent = trigger.GetComponent<TriggerParent>();
        dealDamage = GetComponent<DealDamage>();
        trigger.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            animator.SetTrigger("Attack");
            attackCounter = attackDuration;
            Isattacking = true;
            trigger.SetActive(true);
        }

        if (attackCounter > 0)
            attackCounter -= Time.deltaTime;
        if (attackCounter < 0)
        { 
            Isattacking = false;
            trigger.SetActive(false);
        }

        if (Isattacking && trigger.activeInHierarchy && triggerparent.collided && triggerparent.hitObjects.Count > 0)
        {
            foreach (GameObject hitGObj in triggerparent.hitObjects)
            {
                if (hitGObj && hitGObj.GetComponent<Health>())
                    dealDamage.Attack(hitGObj, damage, pushHeight, pushForce);
            }
        }
    }
}
