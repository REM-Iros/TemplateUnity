using UnityEngine;

/// <summary>
/// This interface is inherited by all UI components that will be used in RPG combat.
/// 
/// REM-i
/// </summary>
public interface RPGIUIInterface
{
    [Tooltip("Called by UI Binder to bind actor to ui component.")]
    void Bind(RPGActor actor);

    [Tooltip("Called by UI Binder to unbind actor to ui component.")]
    void Unbind();
}
