using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
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
        currentWave = 0;
        west_and_east_walls_by_wave[0].SetActive(true);
        for (int i = 1; i < west_and_east_walls_by_wave.Length; i++)
        {
            west_and_east_walls_by_wave[i].SetActive(false);
        }
        
    }

    public void SetMoving() {
        current_state = states.moving;
        ui_manager.ShowGoText();
        spawner.Deactivate();

        if (currentWave+1 >= west_and_east_walls_by_wave.Length)
        {
            LevelAndMenuInfo.Instance.menuStartState = MainMenuManager.MenuState.selecting_chapter;
            int winnedLevel = PlayerPrefs.GetInt("level", 1);
            if (LevelAndMenuInfo.Instance.CurrentLevel == winnedLevel && winnedLevel + 1 <= LevelAndMenuInfo.Instance.MAX_LEVEL)
                PlayerPrefs.SetInt("level", winnedLevel + 1);
            SceneManager.LoadScene("MainMenu");
        }
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
        if (current_state == states.moving && currentWave < west_and_east_walls_by_wave.Length-1)
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
                currentWave++;
            }
        }
        
    }
}
