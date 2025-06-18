using UnityEngine;

/// <summary>
/// This is a basic jumping script that inherits from the jumping parent.
/// Nothing fancy, just implements the jump script when jump is pressed, and checks
/// for modifiers that it needs to apply.
/// 
/// REM-i
/// </summary>
public class PMRegularJump : JumpingComponentParent
{
    #region Vars

    [Tooltip("Ground detection needed to determine if we can jump.")]
    [SerializeField, Header("Ground Detection Component")]
    private GroundDetection _groundDetection;

    // This is subbed to the ground detection event.
    private bool _grounded;

    [Tooltip("Coyote Time modifier to allow for more forgiving jumps.")]
    [SerializeField, Header("Modifiers")]
    private PMCoyoteTimeModifier _coyoteTimeModifier;

    [Tooltip("Variable Height modifier to allow for more precise jumping.")]
    [SerializeField]
    private PMVariableHeightModifier _variableHeightModifier;

    #endregion

    #region Jumping Methods

    /// <summary>
    /// Check for ground detection, and if it is present and coyote time isn't, sub to the
    /// method.
    /// </summary>
    private void Awake()
    {
        if (_groundDetection == null)
        {
            Debug.LogError("Ground detection not present, regular jump will not work.");
            return;
        }
    }

    #region Event Methods

    /*
     * On enable or disable, we need to sub/unsub appropriate event
     */
    private void OnEnable()
    {
        // If coyote time is present, it is responsible for polling ground detection.
        if (_coyoteTimeModifier != null)
        {
            return;
        }

        _groundDetection.OnGroundedStateChange += OnGroundStateChange;
    }

    private void OnDisable()
    {
        // If coyote time is present, it is responsible for polling ground detection.
        if (_coyoteTimeModifier != null)
        {
            return;
        }

        _groundDetection.OnGroundedStateChange -= OnGroundStateChange;
    }

    /// <summary>
    /// This method subs to the event of the ground detection to determine when we are grounded or not.
    /// </summary>
    /// <param name="currentState"></param>
    private void OnGroundStateChange(bool currentState)
    {
        _grounded = currentState;
    }

    #endregion

    /// <summary>
    /// Check for if we can jump based on if we are grounded or if coyote time is available.
    /// </summary>
    /// <returns></returns>
    protected override bool CheckForCanJump()
    {
        // Don't run if we are missing ground detection
        if (_groundDetection == null)
        {
            Debug.LogError("Ground detection not present, regular jump will be skipped.");
            return false;
        }

        // If coyote time is not present, just check if we are grounded
        if (_coyoteTimeModifier == null)
        {
            return _grounded;
        }

        return _coyoteTimeModifier.WithinCoyoteTimeThreshold();
    }

    /// <summary>
    /// Inherit the base and implement a basic jump.
    /// </summary>
    protected override void ApplyJumpForce()
    {
        // Reset linear velocity
        _rb2d.linearVelocityY = 0;

        // Apply force to the player to get them to jump
        _rb2d.AddForceY(_jumpForce, ForceMode2D.Impulse);

        // Notify variable height if it is present
        if (_variableHeightModifier != null)
        {
            _variableHeightModifier.NotifyJumping();
        }
    }

    #endregion
}
