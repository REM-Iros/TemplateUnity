using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Because there isn't a great way to link the target icons to the target manager, we use this event instead.
/// 
/// REM-i
/// </summary>
[CreateAssetMenu(menuName = "Events/RPG/Actor List Channel")]
public class RPGTargetEvent : ScriptableObject
{
    [Tooltip("This is the actual event we are using.")]
    public event Action<IReadOnlyList<RPGActor>> OnEventRaised;

    [Tooltip("This is what we use to invoke the event.")]
    public void Raise(IReadOnlyList<RPGActor> list) => OnEventRaised?.Invoke(list);
}
