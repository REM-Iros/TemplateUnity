using UnityEngine;

/// <summary>
/// This is a component script of the player movement. This specifically handles top down 
/// movement with momentum along with dashing (don't want to build it as a separate script yet).
/// 
/// REM-i
/// </summary>
public class PMTopdownMomentum : MovementComponentParent
{
    #region Vars

    [Tooltip("This is the speed cap for the player.")]
    [SerializeField, Header("Momentum Variables")]
    private float _speedCap;

    #endregion

    #region Movement Methods

    /// <summary>
    /// This is the method that will be called to move the player
    /// </summary>
    public override void MovePlayer()
    {
        base.MovePlayer();

        // Handle movement of player
        if (_rb2d.linearVelocity.magnitude < _speedCap)
        {
            _rb2d.AddForce(_moveVector * ((IsDashingEnabled && _isDashing) ? _dashSpeed : _speed));
        }
        else
        {
            _rb2d.linearVelocity = _moveVector * _speedCap;
        }
    }

    #endregion
}
