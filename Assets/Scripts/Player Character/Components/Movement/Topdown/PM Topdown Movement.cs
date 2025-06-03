using UnityEngine;

/// <summary>
/// This is a component script of the player movement. This specifically handles top down 
/// movement along with dashing (don't want to build it as a separate script yet).
/// 
/// REM-i
/// </summary>
public class PMTopdownMovement : MovementComponentParent
{
    #region Methods

    #region Movement Methods

    /// <summary>
    /// This is the method that will be called to move the player
    /// </summary>
    public override void MovePlayer()
    {
        base.MovePlayer();

        // Handle movement of the player
        _rb2d.linearVelocity = _moveVector * ((IsDashingEnabled && _isDashing) ? _dashSpeed : _speed);
    }

    #endregion

    #endregion
}
