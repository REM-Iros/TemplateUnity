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
        var audioManager = CreateManager<AudioManager>();
        var saveManager = CreateManager<SaveManager>();
        var dataManager = CreateManager<DataManager>();
        var sceneControlManager = CreateManager<SceneControlManager>();
        var gameStateManager = CreateManager<GameStateManager>();
        var uiFocusManager = CreateManager<UIFocusManager>();

        // Register major game managers that need to persist throughout scenes.
        ServiceLocator.Register(audioManager);
        ServiceLocator.Register(saveManager);
        ServiceLocator.Register(dataManager);
        ServiceLocator.Register(sceneControlManager);
        ServiceLocator.Register(gameStateManager);
        ServiceLocator.Register(uiFocusManager);
    }

    /// <summary>
    /// Method called on startup that creates a manager of type T, adds it to the service locator, and marks it as DontDestroyOnLoad.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    private T CreateManager<T>() where T : MonoBehaviour
    {
        GameObject managerObj = new GameObject(typeof(T).Name);
        T manager = managerObj.AddComponent<T>();
        DontDestroyOnLoad(managerObj);
        return manager;
    }
}
