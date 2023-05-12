using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{



    [HideInInspector]public bool Isattacking;
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

    private float attackTimeCounter;
    [SerializeField] float minComboAnimationPercent = 0.7f;
    [SerializeField] string idleAnimation = "SLOW PILL";
    [SerializeField] string melee1Animation = "melee";
    [SerializeField] string melee2Animation = "melee 2";
    [SerializeField] string melee3Animation = "melee 3";
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
            if (attackCounter == 0)
            {
                animator.SetTrigger("Attack");
                attackCounter = 1;
                attackTimeCounter = 0;
            }
            else if (attackCounter == 1 && animator.GetCurrentAnimatorStateInfo(2).IsName(melee1Animation)  && attackTimeCounter > animator.GetCurrentAnimatorClipInfo(2)[0].clip.length*minComboAnimationPercent)
            {
                animator.SetTrigger("Attack 2");
                attackCounter = 2;
                attackTimeCounter = 0;
            }
            else if (attackCounter == 2 && animator.GetCurrentAnimatorStateInfo(2).IsName(melee2Animation) && attackTimeCounter > animator.GetCurrentAnimatorClipInfo(2)[0].clip.length * minComboAnimationPercent)
            {
                animator.SetTrigger("Attack 3");
                attackCounter = 0;
                attackTimeCounter = 0;
            }
        }

        attackTimeCounter += Time.deltaTime;

        if (animator.GetCurrentAnimatorStateInfo(2).IsName(idleAnimation))
        {
            attackCounter = 0;
            attackTimeCounter = 0;
        }

        if(Isattacking)
        {
            if(attackTrigger && attackTrigger.colliding && attackTrigger.hitObjects != null && attackTrigger.hitObjects.Count > 0)
            { 
                foreach(GameObject hitGObj in attackTrigger.hitObjects)
                {
                    if (hitGObj && hitGObj.GetComponent<Health>())
                    { 
                        dealDamage.Attack(hitGObj, damage, pushHeight, pushForce);
                        special.SpecialBarValue += 0.5f;
                    }
                }
            }
        }
    }


}
