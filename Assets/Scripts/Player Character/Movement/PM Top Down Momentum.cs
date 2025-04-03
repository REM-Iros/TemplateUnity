using UnityEngine;

/// <summary>
/// This script handles top down 2d movement with momentum. So this is for applying
/// forces specifically to the rigidbody, with the object building up speed until
/// it hits a speed cap, and moving backwards would apply the inverse force on it
/// to slow it down. Moves like spaceship controls.
/// 
/// REM-i
/// </summary>
public class PMTopDownMomentum : PlayerMovementParent
{
    #region Vars

    [Tooltip("This is the speed cap for the player.")]
    [SerializeField, Header("Momentum Vars")]
    private float _speedCap;

    #endregion

    #region Methods

    /// <summary>
    /// This is the method that will move the player character with momentum.
    /// </summary>
    protected override void MovePlayer()
    {
        if (_rb2d.linearVelocity.magnitude < _speedCap)
        {
            _rb2d.AddForce(_moveVector * _speed);
        }
        else
        {
            _rb2d.linearVelocity = _moveVector * _speedCap;
        }
    }

    #endregion
}
