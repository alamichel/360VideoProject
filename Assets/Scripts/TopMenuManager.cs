using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TopMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    void Start()
    {
        panel.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Scenes/2D_TowerBridge");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenPanel()
    {
        panel.SetActive(true);
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
    }
}
