using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// <c>SceneTransitionManager</c> contains the basic logic for transitioning between gameplay scenes.
/// </summary>
public class SceneTransitionManager : MonoBehaviour
{
    // The animation that plays when we are switching scenes.
    [SerializeField] private Animator transition;
    // The delay after triggering the transition animation.
    [SerializeField] private float transitionTime = 3.0f;

    /// <summary>
    /// Flag to know if we are currently changing scenes.
    /// </summary>
    public bool IsTransitioning { get; private set; } = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeSceneTo("TowerBridge");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeSceneTo("CrystalShowerFalls");
        }
    }

    /// <summary>
    /// Smoothly transitions from the current scene to the requested scene.
    /// </summary>
    /// <param name="sceneName">
    /// The name of the scene that we should switch to, as listed in the project's build settings.
    /// </param>
    public void ChangeSceneTo(string sceneName) => StartCoroutine(LoadScene(sceneName));

    /// <summary>
    /// Loads the requested scene into the game.
    /// </summary>
    /// <param name="sceneName">
    /// The name of the scene that we should switch to, as listed in the project's build settings.
    /// </param>
    /// <returns></returns>
    IEnumerator LoadScene(string sceneName)
    {
        IsTransitioning = true;

        // Allow time for the transition animation to play.
        transition.SetTrigger("ChangeScene");
        yield return new WaitForSeconds(transitionTime);

        // Finish switching scenes.
        SceneManager.LoadScene(sceneName);
        IsTransitioning = false;
    }
}

