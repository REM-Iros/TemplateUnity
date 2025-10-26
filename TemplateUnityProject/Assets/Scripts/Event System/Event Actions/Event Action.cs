using System.Collections;
using UnityEngine;

/// <summary>
/// This scriptable object serves as a base class for defining various event actions in the game, for
/// use in the event system.
/// 
/// REM-i
/// </summary>
public abstract class EventAction : ScriptableObject
{
    // This bool allows for Event Actions to run in parallel with other actions.
    public bool runInParallel = false;

    // This method will be inherited and used by derived classes to execute specific event actions.
    public abstract IEnumerator Execute(EventContext context);
}
