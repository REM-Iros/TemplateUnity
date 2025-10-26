using System.Collections;

/// <summary>
/// This script is used to track coroutines in Unity, specifically for managing event actions.
/// 
/// REM-i
/// </summary>
public class CoroutineTracker
{
    public bool IsDone { get; private set; }

    /// <summary>
    /// This method runs a coroutine and tracks its completion status.
    /// </summary>
    /// <param name="coroutine"></param>
    /// <returns></returns>
    public IEnumerator Run(IEnumerator coroutine)
    {
        IsDone = false;
        yield return coroutine;

        // Return true when done.
        IsDone = true;
    }
}
