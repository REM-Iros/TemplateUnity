using UnityEngine;

/// <summary>
/// This is a generic lazy singleton template class that allows other classes to inherit
/// its basic functions. Lazy only inits when it is called for the first time.
/// 
/// REM-i
/// </summary>
/// <typeparam name="T"></typeparam>
public class LazySingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    #region Vars

    private static T _instance;
    private static readonly object _instanceLock = new();

    // Peristence determiner, true means persist, false means it only exists for a scene, set in editor
    [SerializeField]
    private bool Persistent { get; set; } = true;

    // We use lazy initialization here so that it only instantiates on first call,
    // and then use the lock for thread safety to prevent initialization calling
    // more than once
    public static T Instance
    {
        get
        {
            lock (_instanceLock)
            {
                if (_instance == null)
                {
                    _instance = FindAnyObjectByType<T>();

                    if (_instance == null)
                    {
                        GameObject singletonObj = new GameObject(typeof(T).FullName);
                        _instance = singletonObj.AddComponent<T>();

                        // Only set don't destroy on load when persistent
                        LazySingleton<T> singleton = _instance as LazySingleton<T>;
                        if (singleton != null && singleton.Persistent)
                        {
                            DontDestroyOnLoad(singletonObj);
                        }
                    }
                }

                return _instance;
            }
        }
    }

    #endregion

    #region Methods

    /// <summary>
    /// On awake, checks if we have an instance, if we do, destroy, otherwise assign this as the singleton
    /// </summary>
    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;

            // Only set don't destroy on load if persistant
            if (Persistent)
            {
                DontDestroyOnLoad(_instance);
            }
        }
        else if (_instance != this)
        {
            Debug.LogWarning($"Dupe singleton of {typeof(T).Name} attempted to create");
            Destroy(gameObject);
        }
    }

    #endregion

    #region Constructors

    protected LazySingleton() { } // Prevent instantiation through "new"

    #endregion
}
