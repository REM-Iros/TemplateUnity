using UnityEngine;

/// <summary>
/// This is a generic egaer singleton template class that allows other classes to inherit
/// its basic functions. Eager inits on startup, used for things needed throughout whole
/// games lifecycle
/// 
/// REM-i
/// </summary>
/// <typeparam name="T"></typeparam>
public class EagerSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    #region Vars

    private static T _instance;

    public static T Instance
    {
        get 
        { 
            if (_instance == null)
            {
                Debug.LogError($"Singleton {typeof(T).Name} not initialized");
            }

            return _instance;
        }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Called on startup, sets instance if not present and prevents destruction on load.
    /// </summary>
    protected virtual void Awake()
    {
        if (_instance)
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
            Debug.LogWarning($"Destroyed dupe {typeof(T).Name} singleton");
        }
    }

    #endregion

    #region Constructors

    // Static constructor
    static EagerSingleton()
    {
        // Creates an instance of the singleton on startup
        GameObject obj = new GameObject(typeof(T).Name);
        _instance = obj.AddComponent<T>();
        DontDestroyOnLoad(obj);
    }

    protected EagerSingleton() { } // Prevents calling new to create instances

    #endregion
}
