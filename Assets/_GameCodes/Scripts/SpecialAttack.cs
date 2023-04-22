using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttack : MonoBehaviour
{
    public GameObject trigger;
    private TriggerParent triggerparent;
    public Animator animator;
    public bool Isattacking;
    private DealDamage dealDamage;

    public float pushForce = 27f;
    public float pushHeight = 1.7f;
    public int damage = 100;
    public float SpecialBarValue = 1;

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
        if (Input.GetButtonDown("Fire2") && SpecialBarValue >= 1)
        {
            SpecialBarValue -= 0.5f;
            animator.SetTrigger("SpecialAttack1");
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

    public void StartAttack()
    {
        Isattacking = true;
        trigger.SetActive(true);
    }

    public void EndAttack()
    { 
        Isattacking = false;
        trigger.SetActive(false);
    }
}
