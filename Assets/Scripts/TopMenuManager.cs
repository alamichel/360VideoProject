using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TopMenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Scenes/2D_TowerBridge");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
