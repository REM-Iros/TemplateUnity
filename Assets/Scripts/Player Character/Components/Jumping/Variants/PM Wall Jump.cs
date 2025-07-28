using UnityEngine;

/// <summary>
/// This is a modifier script to the jumping script and moving script, requiring 
/// both movement and jumping to work. This script allows the player to
/// jump from walls repeatedly, resetting jumps for the player.
/// 
/// REM-i
/// </summary>
public class PMWallJump : JumpingComponentParent
{
    #region Vars

    [Tooltip("This is the wall detection script we will need to check for.")]
    [SerializeField, Header("Wall Detection Component")]
    private WallDetection _wallDetection;

    [Tooltip("This is the horizontal force applied to push the player from the wall.")]
    [SerializeField, Header("Wall Jump Force")]
    private float _wallJumpForce;

    // This is the timer for the velocity override
    [SerializeField, Header("Velocity Override Timer")]
    private const float _velocityOverrideTimer = 0.2f;
    private float _velocityOverrideTimerCurrent = 0f;

    #endregion

    #region Methods

    /// <summary>
    /// Make sure wall detection is present on init or the script will break
    /// </summary>
    private void Awake()
    {
        // Check for rigidbody
        if (_wallDetection == null)
        {
            Debug.LogError("No wall detection found, script will not work.");
        }
    }

    #region Jump Methods

    /// <summary>
    /// Player needs to be against a wall to be able to jump.
    /// </summary>
    /// <returns></returns>
    protected override bool CheckForCanJump()
    {
        return _wallDetection.IsColliding();
    }

    /// <summary>
    /// Applies a force in the opposite direction of the wall and upwards
    /// </summary>
    protected override void ApplyJumpForce()
    {
        // Reset linear velocity
        _rb2d.linearVelocityY = 0;

        //Apply force to the player opposite of the wall and upwards
        _rb2d.AddForce(new Vector2(_wallDetection.IsLeftWallColliding ? _wallJumpForce : -_wallJumpForce, _jumpForce), ForceMode2D.Impulse);
    }

    #region Override Timer

    /// <summary>
    /// A timer before regular movement can take over again
    /// </summary>
    private void Update()
    {
        // Only run the override timer if the override is active
        if (_velocityOverrideTimerCurrent > 0)
        {
            RunMomentumOverrideTimer();
        }
    }

    private void RunMomentumOverrideTimer()
    {
        // Decrease the timer
        _velocityOverrideTimerCurrent -= Time.deltaTime;

        // If the timer is done, reset the override
        if (_velocityOverrideTimerCurrent <= 0)
        {
            

            _velocityOverrideTimerCurrent = 0f;
        }
    }

    #endregion

    #endregion

    #endregion
}
