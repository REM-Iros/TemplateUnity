using UnityEngine;

/// <summary>
/// This movement variant handles cardinal movement for top-down 2D.
/// 
/// REM-i
/// </summary>
public class PMCardinalMovement : MovementComponentParent
{
    /// <summary>
    /// This handles the four way movement.
    /// </summary>
    /// <param name="input"></param>
    public override VelocityRequest Move(Vector2 input)
    {
        return new VelocityRequest(input.normalized * _speed, VelocityPriority.Normal, "Movement");
    }
}
