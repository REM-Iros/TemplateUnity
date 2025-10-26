using UnityEngine;

/// <summary>
/// This is a camera behavior interface for allowing the user
/// to implement different camera behaviors.
/// 
/// REM-i
/// </summary>
public interface ICameraBehavior
{
    Vector2 GetTargetPosition(Vector2 position, Transform target);
}
