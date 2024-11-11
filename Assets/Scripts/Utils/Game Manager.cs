using UnityEngine;
/// <summary>
/// Basic gamemanager script. This will load on startup, and will provide basic features
/// needed to get the game running.
/// 
/// REM-i
/// </summary>
/// <typeparam name="T"></typeparam>
public class GameManager<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    private static readonly object _lock = new object();

    /// <summary>
    /// Create an instance that we can reference in script
    /// </summary>
    public static T Instance
    {
        get
        {
            //Lock to prevent multithread issues
            lock (_lock)
            {
                //If the instance isn't set, find it
                if (_instance == null)
                {
                    _instance = FindAnyObjectByType<T>();

                    //If we can't find one, create this as the manager singleton
                    if (_instance == null)
                    {
                        GameObject singletonObject = new GameObject(typeof(T).Name);
                        _instance = singletonObject.AddComponent<T>();

                        //Set this manager as persistant
                        DontDestroyOnLoad(singletonObject);
                    }
                }
                return _instance;
            }
        }
    }

    /// <summary>
    /// Called on startup, 
    /// </summary>
    protected virtual void Awake()
    {
        // Ensure there is only one instance of the Singleton
        if (_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject); // Optional
        }
        else if (_instance != this)
        {
            Destroy(gameObject); // Destroy duplicate instance
        }
    }
}
