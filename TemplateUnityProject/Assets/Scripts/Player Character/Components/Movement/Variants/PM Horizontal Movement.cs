using UnityEngine;

/// <summary>
/// This movement variant handles horiontal movement for side-to-side 2D.
/// 
/// REM-i
/// </summary>
public class PMHorizontalMovement : MovementComponentParent
{
    /// <summary>
    /// This handles the two way movement.
    /// </summary>
    /// <param name="input"></param>
    public override VelocityRequest Move(Vector2 input)
    {
        return new VelocityRequest(input * _speed, VelocityPriority.Normal, "Movement");
    }
}
