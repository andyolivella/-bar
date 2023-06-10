using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public enum MenuState {
        showing_logos,
        main_menu,
        selecting_chapter
    }

    private MenuState actual_state = MenuState.showing_logos;
    [SerializeField] GameObject logosContainer;
    [SerializeField] List<float> logosDuration;
    [SerializeField] GameObject mainMenuContainer;
    [SerializeField] GameObject continueButton;
    [SerializeField] GameObject chapterSelectContainer;
    [SerializeField] GameObject[] chapterButtons;

    private float logos_timer = 0;
    private int currentLogo = 0;
    // Start is called before the first frame update
    void Start()
    {
        actual_state = LevelAndMenuInfo.Instance.menuStartState;
        if (actual_state == MenuState.showing_logos)
        {
            currentLogo = 0;
            ActivateLogo();
        }
        else if (actual_state == MenuState.main_menu)
        {
            GoToMainMenu();
        }
        else if (actual_state == MenuState.selecting_chapter)
        {
            GoToChapterSelect();
        }
    }

    public void StartGame() 
    {
        SceneManager.LoadScene("Level1");
        LevelAndMenuInfo.Instance.CurrentLevel = 1;
    }

    public void Continue() {
        int maxLevel = PlayerPrefs.GetInt("level", 1);
        SceneManager.LoadScene("Level" + maxLevel);
        LevelAndMenuInfo.Instance.CurrentLevel = maxLevel;
    }

    public void StartSpecificChapter(int chapter)
    {
        SceneManager.LoadScene("Level" + chapter);
        LevelAndMenuInfo.Instance.CurrentLevel = chapter;
    }

    void ActivateLogo()
    {
        mainMenuContainer.SetActive(false);
        chapterSelectContainer.SetActive(false);
        logosContainer.SetActive(true);
        logosContainer.transform.GetChild(currentLogo).gameObject.SetActive(true);
        for (int i = 0; i < logosContainer.transform.childCount; i++)
        {
            if (i != currentLogo)
                logosContainer.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void GoToMainMenu()
    {
        logosContainer.SetActive(false);
        mainMenuContainer.SetActive(true);
        chapterSelectContainer.SetActive(false);

        int maxLevel = PlayerPrefs.GetInt("level", 1);
        if (maxLevel > 1)
            continueButton.SetActive(true);
        else
            continueButton.SetActive(false);
    }

    public void GoToChapterSelect() 
    {
        logosContainer.SetActive(false);
        mainMenuContainer.SetActive(false);
        chapterSelectContainer.SetActive(true);
        int maxLevel = PlayerPrefs.GetInt("level", 1);
        for (int i = 1; i <= chapterButtons.Length; i++)
        {
            chapterButtons[i-1].SetActive(false);
        }
        for (int i = 1; i <= maxLevel; i++)
        {
            chapterButtons[i-1].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (actual_state == MenuState.showing_logos)
        {
            logos_timer += Time.deltaTime;
            if (logos_timer > logosDuration[currentLogo])
            {
                currentLogo++;
                if (currentLogo >= logosDuration.Count)
                {
                    actual_state = MenuState.main_menu;
                    GoToMainMenu();
                    return;
                }
                logos_timer = 0;
                ActivateLogo();

            }
        }
    }
}

