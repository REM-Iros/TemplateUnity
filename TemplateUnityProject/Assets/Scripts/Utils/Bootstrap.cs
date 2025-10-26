using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Bootstrap is called on the init scene, ensures all services are ready to go,
/// and then transitions to the main menu
/// 
/// REM-i
/// </summary>
public class Bootstrap : MonoBehaviour
{
    // Input actions to insert into the Input Manager
    [SerializeField]
    private InputActionAsset inputActions;

    // Hard code the first scene to go to
    private readonly string _firstScene = "1_Main Menu";

    /// <summary>
    /// On startup, call the initialization, and then move to the main menu
    /// </summary>
    private void Awake()
    {
        // Set up the service locator
        InitServiceLocator();

        // Move onto the main menu
        ServiceLocator.Get<SceneControlManager>().ChangeScene(_firstScene);
    }

    /// <summary>
    /// Stores all managers that need to go into the service locator.
    /// </summary>
    private void InitServiceLocator()
    {
        AudioManager.Instance.Generate();
        SaveManager.Instance.Generate();
        DataManager.Instance.Generate();
        SceneControlManager.Instance.Generate();
        GameStateManager.Instance.Generate();
        UIFocusManager.Instance.Generate();

        // Register major game managers that need to persist throughout scenes.
        ServiceLocator.Register(FindAnyObjectByType<AudioManager>());
        ServiceLocator.Register(FindAnyObjectByType<SaveManager>());
        ServiceLocator.Register(FindAnyObjectByType<DataManager>());
        ServiceLocator.Register(FindAnyObjectByType<SceneControlManager>());
        ServiceLocator.Register(FindAnyObjectByType<GameStateManager>());
        ServiceLocator.Register(FindAnyObjectByType<UIFocusManager>());
    }
}
