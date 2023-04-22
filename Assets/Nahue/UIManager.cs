using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [SerializeField]Image healthImg;
    [SerializeField] Image specialImg;
    Health health;
    private int maxHealth;
    SpecialAttack special;
    // Start is called before the first frame update
    void Start()
    {
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        maxHealth = health.DefaultHealth;
        special = GameObject.FindGameObjectWithTag("Player").GetComponent<SpecialAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        healthImg.fillAmount = (float)health.currentHealth / (float)maxHealth;
        specialImg.fillAmount = (float)special.SpecialBarValue;
    }
}
