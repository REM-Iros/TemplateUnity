using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Because there isn't a great way to link the target icons to the target manager, we use this event instead. This one
/// deactivates the target icon, however.
/// 
/// REM-i
/// </summary>
public class RPGUntargetEvent : ScriptableObject
{
    [Tooltip("This is the actual event we are using.")]
    public event Action OnEventRaised;

    [Tooltip("This is what we use to invoke the event.")]
    public void Raise() => OnEventRaised?.Invoke();
}
