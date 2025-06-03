using UnityEngine;

/// <summary>
/// This is a component of the Jump Parent, and allows the player
/// to hold the jump button to get longer jumps, or shorter if they
/// just tap it.
/// 
/// REM-i
/// </summary>
public class PMVariableHeightJump : JumpingComponentParent
{
    #region Variable Height Vars

    [Tooltip("This is the force that will be applied over time as the jump is held.")]
    [SerializeField, Header("Variable Height Vars")]
    private float _jumpHeldForce;

    [Tooltip("This is the length of time that the jump can be held for more heigth")]
    [SerializeField]
    private float _maxHoldTime;

    // This is the current hold time
    private float _holdTime = 0f;

    #region Gravity Scale Vars

    [Tooltip("This is the lower gravity scale that is applied when jumping.")]
    [SerializeField, Header("Gravity Scale Vars")]
    private float _jumpGravityScale;

    [Tooltip("This is the gravity the player will always have.")]
    [SerializeField]
    private float _baseGravityScale;

    #endregion

    #endregion

    /// <summary>
    /// We want to init the object's gravity at the base gravity scale
    /// </summary>
    private void Start()
    {
        _rb2d.gravityScale = _baseGravityScale;
    }

    #region Jumping Methods

    /// <summary>
    /// Inherit the base and implement a basic jump.
    /// </summary>
    protected override void ApplyJumpForce()
    {
        // Reset the linear velocity of y
        _rb2d.linearVelocityY = 0f;

        // Change the gravity scale
        _rb2d.gravityScale = _jumpGravityScale;

        // Apply force to the player to get them to jump
        _rb2d.AddForceY(_jumpForce, ForceMode2D.Impulse);

        // Set the hold time to max
        _holdTime = _maxHoldTime;

    }

    /// <summary>
    /// While jump is being held, continue to apply force until either it is released
    /// or the timer is up.
    /// </summary>
    public override void ExecuteDuringJump()
    {
        // Make sure we only run this while jump is held
        if(!_isJumpHeld)
        {
            return;
        }

        // Stop granting extra height if we have no more hold time
        if (_holdTime <= 0f)
        {
            return;
        }

        // Decrement the time
        _holdTime -= Time.deltaTime;

        // Apply a weaker force to keep the player moving through the air
        _rb2d.AddForceY(_jumpHeldForce);
    }

    /// <summary>
    /// Reset the jump held script and gravity
    /// </summary>
    protected override void OnJumpReleased()
    {
        _isJumpHeld = false;

        _rb2d.gravityScale = _baseGravityScale;
    }

    #endregion
}
