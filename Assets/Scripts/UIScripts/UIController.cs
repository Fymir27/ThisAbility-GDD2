using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public enum UIState
    {
        MENU,
        OPTIONS,
        CONTROLS,
        CREDITS,
        GAME,
        PAUSE,
        ENDLEVEL
    }

    public UIState currentUIState = UIState.MENU;

    public List<UIPanel> UIPanels;

    public AudioSource ambientMenu;
    public AudioSource ambientGame;

    UIPanel previousPanel;
    UIPanel currentPanel;
    private void Start()
    {
        previousPanel = UIPanels[(int)currentUIState];
        currentPanel = UIPanels[(int)currentUIState];

        for (int i = 0; i < UIPanels.Count; i++)
            UIPanels[i].Deactivate();

        currentPanel.Activate();
    }

    /// <summary>
    /// Here is a list of int values represented by panels :
    /// 0 - Main Menu
    /// 1 - Settings
    /// 2 - Controls
    /// 3 - Game Screen
    /// 4 - Game Pause
    /// 5 - Game End Screen
    /// </summary>
    /// <param name="newState"></param>
    public void ChangeStateTo(int newState)
    {
        if ((UIState)currentUIState == UIState.MENU && (UIState)newState == UIState.GAME)
        {
            LoadLevel(1);
            ambientMenu.Stop();
            ambientGame.Play();
        }

        if ((UIState)newState == UIState.MENU && !ambientMenu.isPlaying)
        {
            ambientGame.Stop();
            ambientMenu.Play();
        }

        currentUIState = (UIState)newState;
        previousPanel = currentPanel;
        currentPanel = UIPanels[(int)newState];

        previousPanel.Deactivate();
        currentPanel.Activate();

    }
    void LoadLevel(int toLoad)
    {
        SceneManager.LoadScene(toLoad);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    private void Update()
    {

        if (currentUIState == UIState.GAME && Input.GetKeyDown(KeyCode.P))
        {
            ChangeStateTo((int)UIState.PAUSE);
        }
        else if (currentUIState == UIState.PAUSE && Input.GetKeyDown(KeyCode.P))
        {
            ChangeStateTo((int)UIState.GAME);
        }
    }
}
