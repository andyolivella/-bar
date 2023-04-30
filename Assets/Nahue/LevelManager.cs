using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LevelManager : MonoBehaviour
{
    [SerializeField] UIManager ui_manager;
    private enum states {
        moving,
        fighting
    }

    private states current_state = states.fighting;
    private int currentWave = 0;
    
    [SerializeField]Camera game_camera;

    [SerializeField]EnemySpawner spawner;
    [SerializeField] GameObject[] west_and_east_walls_by_wave;
    [SerializeField] float move_velocity = 1;

    // Start is called before the first frame update
    void Start()
    {
        SetFighting();
        west_and_east_walls_by_wave[currentWave + 1].SetActive(false);
    }

    public void SetMoving() {
        current_state = states.moving;
        ui_manager.ShowGoText();
        spawner.Deactivate();
    }

    public void SetFighting()
    {
        current_state = states.fighting;
        ui_manager.HideGoText();
        spawner.Activate();

    }

    // Update is called once per frame
    void Update()
    {
        if (current_state == states.moving)
        {
            Vector3 pos = west_and_east_walls_by_wave[currentWave].transform.position;
            pos.z -= Time.deltaTime * move_velocity;
            west_and_east_walls_by_wave[currentWave].transform.position = pos;

            Vector3 campos = game_camera.transform.position;
            campos.z -= Time.deltaTime * move_velocity;
            game_camera.transform.position = campos;

            if (west_and_east_walls_by_wave[currentWave].transform.position.z <= west_and_east_walls_by_wave[currentWave + 1].transform.position.z)
            { 
                SetFighting();
                west_and_east_walls_by_wave[currentWave + 1].SetActive(true);
            }
        }
    }
}
