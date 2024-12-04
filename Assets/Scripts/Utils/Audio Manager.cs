using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

/// <summary>
/// Basic audio manager to be used throughout a game. A library of all of the sound effects and
/// audio will be stored, and then the game objects will make a call to the audio manager to play
/// those sound effects.
/// 
/// REM-i
/// </summary>
public class AudioManager<T> : MonoBehaviour where T : MonoBehaviour
{
    #region Vars

    #endregion

    #region Callable Methods

    /// <summary>
    /// This calls the runtime manager to play a oneshot of an audio event.
    /// </summary>
    /// <param name="sound"></param>
    /// <param name="pos"></param>
    public void PlayOneShotAudio(EventReference sound, Vector3 pos)
    {
        RuntimeManager.PlayOneShot(sound, pos);
    }

    #endregion

    #region Singleton Stuff

    //Instance variable that stores manager as an instance
    private static T _instance;

    //Lock to prevent multithread issues
    private static readonly object _lock = new object();

    //Create instance that we can call from other scripts
    public static T Instance
    {
        get
        {
            //Lock down manager so others can use it
            lock (_lock)
            {
                if (_instance == null)
                {
                    //Look for already instantiated instance
                    _instance = FindAnyObjectByType<T>();

                    //If no instance, then create a new one
                    if (_instance == null)
                    {
                        GameObject singletonObject = new GameObject(typeof(T).Name);
                        _instance = singletonObject.AddComponent<T>();

                        //Make manager persistant
                        DontDestroyOnLoad(singletonObject);
                    }
                }
                return _instance;
            }
        }
    }

    /// <summary>
    /// Awake method to check if there is an audiomanager already present, if not, set
    /// this as the audio manager singleton.
    /// </summary>
    protected virtual void Awake()
    {
        //Check for instance
        if (_instance == null)
        {
            //Set instance if null
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        //Otherwise we have a dupe and destroy it
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }
    #endregion
}
