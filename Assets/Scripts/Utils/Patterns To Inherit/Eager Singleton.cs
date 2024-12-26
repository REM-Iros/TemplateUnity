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

    //START HERE: Finish this when you get a chance
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
    #endregion

    #region Constructors

    protected EagerSingleton() { } //Prevents calling new to create instances

    #endregion
}
