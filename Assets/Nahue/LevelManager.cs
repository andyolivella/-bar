using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LevelManager : MonoBehaviour
{
    [SerializeField] UIManager ui_manager;
    // Start is called before the first frame update
    void Start()
    {
        ui_manager.HideGoText();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
