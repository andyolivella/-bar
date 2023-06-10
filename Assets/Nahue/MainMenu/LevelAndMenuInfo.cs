using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelAndMenuInfo : MonoBehaviour
{
    public int MAX_LEVEL = 3;

    private static LevelAndMenuInfo instance = null;
    public static LevelAndMenuInfo Instance
    {
        get {
            if (instance == null)
            {
                GameObject GO = new GameObject();
                instance = GO.AddComponent<LevelAndMenuInfo>();
                DontDestroyOnLoad(GO);
            }
            return instance;
        }
    }

    public int CurrentLevel = 1;
    public MainMenuManager.MenuState menuStartState = MainMenuManager.MenuState.showing_logos;
}
