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
    public override void Move(Vector2 input)
    {
        base.Move(input);

        _rb2d.linearVelocity = input.normalized * _speed;
    }
}
