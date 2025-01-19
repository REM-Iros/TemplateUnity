using UnityEngine;

/// <summary>
/// Bootstrap is called on the init scene, ensures all services are ready to go,
/// and then transitions to the main menu
/// 
/// REM-i
/// </summary>
public class Bootstrap : MonoBehaviour
{
    /// <summary>
    /// On startup, call the initialization, and then move to the main menu
    /// </summary>
    private void Awake()
    {
        // Set up the service locator
        InitServiceLocator();

        // Move onto the main menu
        // TODO: Implement scene transition to main menu
    }

    /// <summary>
    /// Stores all managers that need to go into the service locator.
    /// </summary>
    private void InitServiceLocator()
    {
        // Register major game managers that need to persist throughout scenes.
        ServiceLocator.Register(FindAnyObjectByType<AudioManager>());
        ServiceLocator.Register(FindAnyObjectByType<SaveManager>());
        ServiceLocator.Register(FindAnyObjectByType<DataManager>());
        ServiceLocator.Register(FindAnyObjectByType<SceneControlManager>());
    }
}
