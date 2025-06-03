using UnityEngine;

/// <summary>
/// This is a basic jumping script that inherits from the jumping parent.
/// Nothing fancy, just implements the jump script when jump is pressed.
/// 
/// REM-i
/// </summary>
public class PMRegularJump : JumpingComponentParent
{
    #region Jumping Methods

    /// <summary>
    /// Inherit the base and implement a basic jump.
    /// </summary>
    protected override void ApplyJumpForce()
    {
        // Reset linear velocity
        _rb2d.linearVelocityY = 0;

        //Apply force to the player to get them to jump
        _rb2d.AddForceY(_jumpForce, ForceMode2D.Impulse);
    }

    #endregion
}
