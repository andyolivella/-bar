using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MenuGameData : MonoBehaviour
{
    [MenuItem("GameData/Erase Level Progress")]
    // Start is called before the first frame update
    public static void EraseProgress()
    {
        PlayerPrefs.SetInt("level", 1);
    }

    [MenuItem("GameData/Set Level 1 Winned")]
    // Update is called once per frame
    public static void Level1Winned()
    {
        PlayerPrefs.SetInt("level", 2);
    }

    [MenuItem("GameData/Set Level 2 Winned")]
    // Update is called once per frame
    public static void Level2Winned()
    {
        PlayerPrefs.SetInt("level", 3);
    }
}
