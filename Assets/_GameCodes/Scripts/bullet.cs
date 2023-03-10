using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    private TriggerParent trigger;
    private DealDamage dealDamage;
    public int damage = 5;
    public float pushForce = 10f;
    public float pushHeight = 1.2f;
    // Start is called before the first frame update
    void Start()
    {
        trigger = GetComponent<TriggerParent>();
        dealDamage = GetComponent<DealDamage>();
    }

    // Update is called once per frame
    void Update()
    {
        if(trigger && trigger.collided)
        {
            dealDamage.Attack(trigger.hitObjects[0], damage, pushHeight, pushForce);
            Destroy(this.gameObject);
        }
    }
}
