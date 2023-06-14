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
    // The main camera for the game.
    [SerializeField] private CameraController cameraController;
    // The delay after triggering the transition animation.
    [SerializeField] private float transitionTime = 1.0f;

    /// <summary>
    /// Flag to know if we are currently changing scenes.
    /// </summary>
    public bool IsTransitioning { get; private set; } = false;

    void Awake() => StartCoroutine(InitialSceneSetup());

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
    public void ChangeSceneTo(string sceneName)
    {
        // Don't allow direct loading of menu, since that should already exist.
        if (sceneName != "TopMenu")
            StartCoroutine(LoadScene(sceneName));
    }

    /// <summary>
    /// Loads the requested scene into the game.
    /// </summary>
    /// <param name="sceneName">
    /// The name of the scene that we should switch to, as listed in the project's build settings.
    /// </param>
    /// <returns></returns>
    private IEnumerator LoadScene(string sceneName)
    {
        // Allow time for the transition animation to play.
        IsTransitioning = true;
        transition.SetTrigger("UnloadScene");
        yield return new WaitForSeconds(transitionTime);

        // Remove scene that we had added to the game.
        if (SceneManager.GetActiveScene().name != "TopMenu")
        {
            Debug.Log($"Unloaded Scene : {SceneManager.GetActiveScene().name}");
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
        }

        // Additively load scene we want to see.
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);

        // Allow time for scene to load, then mark it as the active scene.
        yield return null;
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));

        // Finish switching scenes.
        cameraController.ResetCameraView();
        transition.SetTrigger("LoadScene");
        Debug.Log($"Loaded Scene : {SceneManager.GetActiveScene().name}");
        IsTransitioning = false;
    }

    /// <summary>
    /// Sets up initial configuration for game scenes.
    /// </summary>
    private IEnumerator InitialSceneSetup()
    {
        // First load base scene (i.e. menu) to game.
        if (SceneManager.GetActiveScene().name != "TopMenu")
            SceneManager.LoadScene("TopMenu", LoadSceneMode.Single);
        Debug.Log($"Initial Scene : {SceneManager.GetActiveScene().name}");

        // Additively load the first scene for the game.
        SceneManager.LoadScene("TowerBridge", LoadSceneMode.Additive);

        // Allow time for scene to load, then mark it as the active scene.
        yield return null;
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("TowerBridge"));
        Debug.Log($"Active Scene : {SceneManager.GetActiveScene().name}");
    }

    /// <summary>
    /// Restarts the game from the beginning.
    /// </summary>
    public void ResetSession()
    {
        // Loading menu scene again will clear out data from previous session.
        SceneManager.LoadScene("TopMenu", LoadSceneMode.Single);
    }
}

