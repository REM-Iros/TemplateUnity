using UnityEngine;

/// <summary>
/// This is a scriptable object for the AudioManager that stores the name, path, and default
/// volume for events. The AudioManager stores these in a dictionary to reference them later.
/// 
/// REM-i
/// </summary>
[CreateAssetMenu(fileName = "New Audio Event", menuName = "Audio/Audio Event")]
public class AudioEvents : ScriptableObject
{
    public string eventName;            //The event name that is referenced
    public string path;                 //The file path
    public float defaultVol = 1.0f;     //The default volume for the event
}
