using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [System.Serializable]
    public class NextMode {
        public string nextModeName;
        public float time;
    }

    [System.Serializable]
    public class AttackMode {
        public string name;
        public string animation;
        public string animationTrigger;
        public List<NextMode> nextModes;
    }

    public Dictionary<string, AttackMode> attackModesDictionary;

    [HideInInspector]public bool Isattacking;
    public GameObject hitbox;
    public Animator animator;
    public AudioClip attacksound;

    public int damage = 10;

    public float pushForce = 27f; 
    public float pushHeight = 1.7f;

    private SpecialAttack special;
    //correct animation layers
    //setup boolean in the throwing script
    //
    private DealDamage dealDamage;
    private TriggerParent attackTrigger;

    public float attackTimeCounter;
    [SerializeField] string idleAnimation = "SLOW PILL";
    [HideInInspector] PlayerMove playerMove;
    public List<AttackMode> attackModes;
    string currentAttack;
    int currentNextModeCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        attackModesDictionary = new Dictionary<string, AttackMode>();
        for (int i = 0; i < attackModes.Count; i++)
        {
            attackModesDictionary.Add(attackModes[i].name, attackModes[i]);
        }

        currentAttack = attackModes[0].name;
        currentNextModeCounter = -1;
        if (!hitbox) {Debug.Log ("no hay hitbox"); }

        Isattacking = false;
        dealDamage = GetComponent<DealDamage>();
        attackTrigger = hitbox.GetComponent<TriggerParent>();
        special = GetComponent<SpecialAttack>();
        playerMove = GetComponent<PlayerMove>();
    }

    public void StartNextMoveCounter() {
        attackTimeCounter = 0;
        currentNextModeCounter = 0;
    }

    private string GetNextModeName()
    {
        return attackModesDictionary[currentAttack].nextModes[currentNextModeCounter].nextModeName;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (!playerMove.IsGrounded())
            {
                animator.SetTrigger("Air Attack");
                currentAttack = attackModes[0].name;
                attackTimeCounter = -1;
            }
            else {
                if(currentNextModeCounter != -1)
                {
                    currentAttack = GetNextModeName();
                    animator.SetTrigger(attackModesDictionary[currentAttack].animationTrigger);
                    currentNextModeCounter = -1;
                }
                else if (currentAttack == attackModes[0].name && animator.GetCurrentAnimatorStateInfo(2).IsName(idleAnimation))
                {
                    animator.SetTrigger(attackModesDictionary[currentAttack].animationTrigger);
                    currentNextModeCounter = -1;
                }

                /*  if (attackCounter == 0)
                  {
                      animator.SetTrigger("Attack");
                      attackCounter = 1;
                      attackTimeCounter = -1;
                  }
                  else if (attackCounter == 1 && animator.GetCurrentAnimatorStateInfo(2).IsName(melee1Animation) && attackTimeCounter > animator.GetCurrentAnimatorClipInfo(2)[0].clip.length * minComboAnimationPercent)
                  {
                      animator.SetTrigger("Attack 2");
                      attackCounter = 2;
                      attackTimeCounter = -1;
                  }
                  else if (attackCounter == 2 && animator.GetCurrentAnimatorStateInfo(2).IsName(melee2Animation) && attackTimeCounter > animator.GetCurrentAnimatorClipInfo(2)[0].clip.length * minComboAnimationPercent)
                  {
                      animator.SetTrigger("Attack 3");
                      attackCounter = 0;
                      attackTimeCounter = -1;
                  }*/
            }
            
        }

        if (currentNextModeCounter != -1)
        { 
            attackTimeCounter += Time.deltaTime;
            if (attackTimeCounter > 0 && attackTimeCounter > attackModesDictionary[currentAttack].nextModes[currentNextModeCounter].time)
            {
                currentNextModeCounter++;
                attackTimeCounter = 0;
                if (currentNextModeCounter >= attackModesDictionary[currentAttack].nextModes.Count)
                { 
                    currentNextModeCounter = -1;
                    currentAttack = attackModes[0].name;
                }
            }
        }

        /*
        if (animator.GetCurrentAnimatorStateInfo(2).IsName(idleAnimation))
        {
            currentAttack = attackModes[0].name;
            attackTimeCounter = -1;
        }
        */

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
