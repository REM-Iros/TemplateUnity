using UnityEngine;

/// <summary>
/// This is a component script of the player movement. This specifically handles side
/// to side movement along with dashing (don't want to build it as a separate script yet).
/// 
/// REM-i
/// </summary>
public class PMSideToSideMovement : MovementComponentParent
{
    [Tooltip("This is the wallcheck component, used for 2D movement.")]
    [SerializeField, Header("Components")]
    private WallDetection _wallDetection;

    #region Movement Methods

    /// <summary>
    /// This is the method that will be called to move the player
    /// </summary>
    public override void MovePlayer()
    {
        base.MovePlayer();

        // Handle movement of the player
        _rb2d.linearVelocityX = _moveVector.x * ((IsDashingEnabled && _isDashing) ? _dashSpeed : _speed);

        // Only run this if we have wall detection and are colliding with something
        if (_wallDetection != null && _wallDetection.IsColliding())
        {
            if (_wallDetection.IsLeftWallColliding && _moveVector.x < 0)
            {
                _rb2d.linearVelocityX = 0;
            }
            else if (_wallDetection.IsRightWallColliding && _moveVector.x > 0)
            {
                _rb2d.linearVelocityX = 0;
            }
        }
    }

    #endregion
}
