using UnityEngine;

/// <summary>
/// This is an input context interface. Anything that ever needs to be added to the input router stack will implement this.
/// 
/// REM-i
/// </summary>
public interface IInputContext
{
    private const string _actionMapName = "";

    // Store a reference to the action map name.
    public string ActionMapName => _actionMapName;

    // On enter and on exit methods to keep in mind.
    public void OnEnter();
    public void OnExit();
}
