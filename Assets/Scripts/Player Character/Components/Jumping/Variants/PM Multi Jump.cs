using UnityEngine;

/// <summary>
/// This is the jump variant that allows players to double/triple/multi jump
/// after a regular jump and gets reset on wall jumps.
/// 
/// REM-i
/// </summary>
public class PMMultiJump : JumpingComponentParent
{
    #region Vars

    [Tooltip("Ground detection needed to determine if we can jump.")]
    [SerializeField, Header("Ground Detection Component")]
    private GroundDetection _groundDetection;

    #region Multi-jump Vars

    [Tooltip("This is the max amount of jumps the players gets after the ground jump.")]
    [SerializeField, Header("Multi Jump Vars")]
    private int _maxExtraJumps;

    //This is the current amount of extra jumps they've used
    private int _currentExtraJumps;

    #endregion

    [Tooltip("Variable Height modifier to allow for more precise jumping.")]
    [SerializeField, Header("Modifiers")]
    private PMVariableHeightModifier _variableHeightModifier;

    #endregion

    #region Methods

    /// <summary>
    /// Check for necessary components to function.
    /// </summary>
    private void Awake()
    {
        if (_groundDetection == null)
        {
            Debug.LogError("Ground detection not found for multi jumps, jumps will not regenerate.");
        }
    }

    #region Event Methods

    /*
     * On enable or disable, we need to sub/unsub appropriate event
     */
    private void OnEnable()
    {
        _groundDetection.OnGroundedStateChange += OnGroundStateChange;
    }

    private void OnDisable()
    {
        _groundDetection.OnGroundedStateChange -= OnGroundStateChange;
    }

    /// <summary>
    /// This method subs to the event of the ground detection to determine when we are grounded or not.
    /// </summary>
    /// <param name="currentState"></param>
    private void OnGroundStateChange(bool isGrounded)
    {
        if (isGrounded == true)
        {
            _currentExtraJumps = 0;
        }
    }

    #endregion

    /// <summary>
    /// If our current jumps is less than our max jumps, we can jump again.
    /// </summary>
    /// <returns></returns>
    protected override bool CheckForCanJump()
    {
        return _currentExtraJumps < _maxExtraJumps;
    }

    /// <summary>
    /// Apply the jump force and if we want variable height, notify that.
    /// </summary>
    public override VelocityRequest Jump()
    {
        // Increment our current jumps
        _currentExtraJumps += 1;

        return new VelocityRequest(new Vector2(0, _jumpForce), VelocityPriority.Normal, "Jump");

        /*
        // Notify variable height if it is present
        if (_variableHeightModifier != null)
        {
            _variableHeightModifier.NotifyJumping();
        }
        */
    }

    #endregion
}
