using UnityEngine;

/// <summary>
/// This interface will be used for any unique movement behavior we want to use.
/// 
/// REM-i
/// </summary>
public interface IMovementInterface
{
    // This is the actual movement that gets implemented
    VelocityRequest Move(Vector2 input);
}