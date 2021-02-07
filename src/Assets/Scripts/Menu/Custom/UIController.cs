using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    public GameObject consolesContainer;
    public GameObject gamesContainer;

    private ConsoleSelect consoles;
    private GameSelect games;

    public bool inputEnabled;
    public Animator animator;

    public Animator drawerAnimator;
    public Button drawerButton;

    public Button enterButton;

    public Animator configAnimator;
    public Button editConfigButton;

    public bool isConfigShowing = false;

    private void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        drawerButton.onClick.AddListener(() =>
        {
            drawerAnimator.SetTrigger("Toggle");
        });
        enterButton.onClick.AddListener(() =>
        {
            animator.SetTrigger("Enter");
        });
        editConfigButton.onClick.AddListener(() =>
        {
            if(isConfigShowing)
            {
                ExitConfig();
            }
            else
            {
                EnterConfig();
            }
        });
        consoles = consolesContainer.GetComponentInChildren<ConsoleSelect>();
        games = gamesContainer.GetComponentInChildren<GameSelect>();

        //ShowConsoles();
    }

    private void Update()
    {
        if (inputEnabled)
        {
            if (Input.GetButtonDown("Cancel"))
            {
                if (gamesContainer.activeSelf)
                {
                    ShowConsoles();
                }
            }
        }
    }

    public void ShowConsoles()
    {
        animator.SetTrigger("ShowConsoles");
        //gamesContainer.SetActive(false);
        //consolesContainer.SetActive(true);
    }

    public void ShowGames(string consoleKey)
    {
        animator.SetTrigger("ShowGames");
        //consolesContainer.SetActive(false);
        //gamesContainer.SetActive(true);
        games.Refresh(consoleKey);
    }

    public void EnableInput()
    {
        inputEnabled = true;
    }
    public void DisableInput()
    {
        inputEnabled = false;
    }

    public void EnterConfig()
    {
        drawerAnimator.SetTrigger("Toggle");
        configAnimator.SetTrigger("Show");
        isConfigShowing = true;
    }

    public void ExitConfig()
    {
        drawerAnimator.SetTrigger("Toggle");
        configAnimator.SetTrigger("Hide");
        isConfigShowing = false;
    }

    private void OnDestroy()
    {
        drawerButton.onClick.RemoveAllListeners();
        enterButton.onClick.RemoveAllListeners();
    }
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
      Application.Quit();
#endif
    }
}