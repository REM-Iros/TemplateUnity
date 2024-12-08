using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

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

    //Store the events in the editor and then ref them in the dictionary
    [Header("Audio Events"), SerializeField]
    private List<AudioEvents> events;
    private Dictionary<string, AudioEvents> audioEventDict;

    //Dictionary for currently running events
    private Dictionary<string, EventInstance> runningEvents;


    #endregion

    #region Callable Methods

    /// <summary>
    /// Called on awake, initializes the events
    /// </summary>
    private void InitDictionary()
    {
        //Init dictionary
        audioEventDict = new Dictionary<string, AudioEvents>();

        //Go through each event, check if it's in the dict, if not, add it
        foreach (var e in events)
        {
            if(!audioEventDict.ContainsKey(e.name))
            {
                audioEventDict.Add(e.name, e);
            }
            else
            {
                Debug.LogWarning($"Dupe name: {e.name}");
            }
        }
    }

    /// <summary>
    /// Plays audio with parameters if that gets passed in
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="volume"></param>
    /// <param name="parameters"></param>
    public void PlayAudio(string eventName, float volume = 1f, Dictionary<string, float> parameters = null)
    {
        //Try to get the event based on the name, if it works, play a oneshot
        if (audioEventDict.TryGetValue(eventName, out var audioEvent))
        {
            //Make sure the event isn't already playing
            if (runningEvents.ContainsKey(eventName))
            {
                Debug.LogWarning($"{audioEvent.name} is already playing.");
                return;
            }

            //Get the instance for FMOD
            var instance = RuntimeManager.CreateInstance(audioEvent.path);

            //Set volume
            instance.setVolume(volume);

            //Set parameters
            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    //Store the result for parameter setting
                    FMOD.RESULT result = instance.setParameterByName(parameter.Key, parameter.Value);

                    //Check if the result returns an error
                    if(result != FMOD.RESULT.OK)
                    {
                        Debug.LogWarning($"Could not alter parameter {parameter.Key} to value {parameter.Value}");
                    }
                }
            }

            //Play the audio
            instance.start();

            //Add the audio to the events
            runningEvents.Add(eventName, instance);
        }
        //Else display a warning
        else
        {
            Debug.LogWarning($"Audio Event name is not present: {eventName}");
        }
    }

    /// <summary>
    /// Changes currently running event parameters
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="parameters"></param>
    public void ChangeAudioParameters(string eventName, Dictionary<string, float> parameters)
    {
        //Try to get the event based on the name, if it works, play a oneshot
        if (runningEvents.TryGetValue(eventName, out var fmodInstance))
        {
            //Check for each parameter to see if it's in the event
            foreach (var parameter in parameters)
            {
                //Store the result for parameter setting
                FMOD.RESULT result = fmodInstance.setParameterByName(parameter.Key, parameter.Value);

                //Check if the result returns an error
                if (result != FMOD.RESULT.OK)
                {
                    Debug.LogWarning($"Could not alter parameter {parameter.Key} to value {parameter.Value}");
                }
            }
        }
        //Else display a warning
        else
        {
            Debug.LogWarning($"Audio Event name is not present: {eventName}");
        }
    }

    /// <summary>
    /// Stops the currently running audio if it exists
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="allowFadeOut"></param>
    public void StopAudio(string eventName, bool allowFadeOut = true)
    {
        //Try to get the event through the name
        if (runningEvents.TryGetValue (eventName, out var fmodInstance))
        {
            //If the instance exists, stop and free it from mem
            fmodInstance.stop(allowFadeOut ? FMOD.Studio.STOP_MODE.ALLOWFADEOUT : FMOD.Studio.STOP_MODE.IMMEDIATE);
            fmodInstance.release();
            runningEvents.Remove(eventName);
        }
        //Else display a warning
        else
        {
            Debug.LogWarning($"Audio Event name is not present: {eventName}");
        }
    }

    /// <summary>
    /// Pauses running event if it is present
    /// </summary>
    /// <param name="eventName"></param>
    public void PauseAudio(string eventName)
    {
        //Try to get the event through the name
        if (runningEvents.TryGetValue(eventName, out var fmodInstance))
        {
            //If the instance exists, pause it
            fmodInstance.setPaused(true);
        }
        //Else display a warning
        else
        {
            Debug.LogWarning($"Audio Event name is not present: {eventName}");
        }
    }

    /// <summary>
    /// Resumes running event if it is present
    /// </summary>
    /// <param name="eventName"></param>
    public void ResumeAudio(string eventName)
    {
        //Try to get the event through the name
        if (runningEvents.TryGetValue(eventName, out var fmodInstance))
        {
            //If the instance exists, resume it
            fmodInstance.setPaused(false);
        }
        //Else display a warning
        else
        {
            Debug.LogWarning($"Audio Event name is not present: {eventName}");
        }
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
    /// this as the audio manager singleton. Also initializes dictionary.
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

        InitDictionary();
    }
    #endregion
}
