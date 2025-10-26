using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This god-forsaken name comes from not wanting to hide the
/// scenemanager that is native to Unity. The scene control manager
/// is just a more superflous name for scene manager. It is an eager
/// singleton that gets added to the service locator, and then the
/// service locator gets this to transition scenes and such.
/// 
/// REM-i
/// </summary>
public class SceneControlManager : EagerSingleton<SceneControlManager>
{
    /// <summary>
    /// Passes in a string that determines where to go for
    /// the next scene.
    /// </summary>
    public void ChangeScene(string sceneName)
    {
        // Check if scene is null
        if (sceneName == null)
        {
            Debug.LogWarning("Scene passed into Scene Manager is null.");
            return;
        }

        // Check if scene is present
        if (SceneManager.GetSceneByName(sceneName) == null)
        {
            Debug.LogWarning($"Scene of {sceneName} either does not exist or is not present in the build");
            return;
        }

        // Load the scene asynchronously.
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
