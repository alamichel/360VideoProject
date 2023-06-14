using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TopMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject itemsButton;
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject quitButton;
    [SerializeField] private SceneTransitionManager sceneTransitionManager;


    void Start()
    {
        panel.SetActive(false);
    }

    public void StartGame()
    {
        sceneTransitionManager.ResetSession();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenPanel()
    {
        panel.SetActive(true);
        itemsButton.SetActive(false);
        startButton.SetActive(false);
        quitButton.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (panel.activeSelf)
            {
                ClosePanel();
            }
        }
    }

    public void ClosePanel()
    {
        panel.SetActive(false);
        itemsButton.SetActive(true);
        startButton.SetActive(true);
        quitButton.SetActive(true);
    }
}
