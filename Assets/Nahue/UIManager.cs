using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    [SerializeField]Image healthImg;
    [SerializeField] Image specialImg;
    Health health;
    private int maxHealth;
    SpecialAttack special;
    [SerializeField] GameObject GO_gameobj;
    // Start is called before the first frame update
    void Start()
    {
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        maxHealth = health.DefaultHealth;
        special = GameObject.FindGameObjectWithTag("Player").GetComponent<SpecialAttack>();
    }

    public void HideGoText()
    {
        GO_gameobj.SetActive(false);
    
    }

    // Update is called once per frame
    void Update()
    {
        healthImg.fillAmount = (float)health.currentHealth / (float)maxHealth;
        specialImg.fillAmount = (float)special.SpecialBarValue;
    }
}
