using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This script handles platformer 2d movement. So this is for applying gravity to the
/// player, as well as handling basic 2d movement. Player should move left and right, be
/// able to double jump and dash, as well as wall jump. Player should also be able to fall through
/// platforms if they are holding down.
/// 
/// REM-i
/// </summary>
public class PMPlatformer : PlayerController
{
    /*

    #region Vars

    #region Input Vars

    // This is the input action for the jump
    private InputAction _jumpAction;

    #endregion

    #region Jump Vars

    [Tooltip("This is the jump force for the player.")]
    [SerializeField, Header("Jump Vars")]
    private float _jumpForce;

    [Tooltip("This is the length of time that the jump can be held for to increase height.")]
    [SerializeField]
    private float _jumpHoldTime;

    [Tooltip("This is the jump force for the double jump.")]
    [SerializeField]
    private float _doubleJumpForce;

    // Check if we can double jump
    private bool _canDoubleJump;

    #endregion

    #region Ground Checking Vars

    [Tooltip("This is the transform for the ground check.")]
    [SerializeField, Header("Ground Check Vars")]
    private Transform _groundCheckTransform;

    [Tooltip("This is the radius of the ground check.")]
    [SerializeField]
    private Vector2 _groundCheckDimensions;

    [Tooltip("This is the layer mask for the ground check.")]
    [SerializeField]
    private LayerMask _groundLayer;

    // Check if we are grounded
    private bool _isGrounded;

    #endregion

    #region Coyote Time Vars

    // Coyote time is the grace period for the player to jump after leaving a platform
    [Tooltip("This is the coyote time for the player.")]
    [SerializeField, Header("Coyote Time Vars")]
    private float _coyoteTimeMax;

    // This is the current amount of expended coyote time for the player
    private float _coyoteTimeCurrent;

    #endregion

    #region Wall Jump Vars

    [Tooltip("This is the transform that checks if we are against a wall.")]
    [SerializeField, Header("Wall Jump Vars")]
    private Transform _wallCheckTransform;

    [Tooltip("This is the radius of the wall check.")]
    [SerializeField]
    private Vector2 _wallCheckDimensions;

    [Tooltip("This is the layer mask for the wall check.")]
    [SerializeField]
    private LayerMask _wallLayer;

    // Check if we are against a wall
    private bool _isCollidingWithWall;

    [Tooltip("This is the wall jump force for the player.")]
    [SerializeField]
    private Vector2 _wallJumpForce;

    // Check if we are wall jumping
    private bool _isWallJumping;

    #region Wall Slide Vars

    [Tooltip("This is the wall slide speed for the player.")]
    [SerializeField]
    private float _wallSlideSpeed;

    // Check if we are wall sliding
    private bool _isWallSliding;

    #endregion

    #endregion

    #endregion

    #region Methods

    #region Init and Destroy Methods

    /// <summary>
    /// Unsubscribe the new actions from the events when the player is destroyed
    /// </summary>
    protected override void OnDestroy()
    {
        base.OnDestroy();

        // Unsub to the dash events
        if (_jumpAction != null)
        {
            _jumpAction.performed -= JumpMethod;
            _jumpAction.canceled -= EndJumpMethod;
        }
    }

    #endregion

    #region Input Methods

    /// <summary>
    /// This is the method that will be called to jump the player. This will check if the player is grounded,
    /// on a wall, or in the air. If the player is grounded, they will jump without expending double jump, and
    /// if they are not grounded, they will double jump. If the player is on a wall, they will jump off of the wall.
    /// </summary>
    /// <param name="context"></param>
    private void JumpMethod(InputAction.CallbackContext context)
    {
        // First, we check if we are wall sliding
        if (_isWallSliding)
        {
            // First, we reset the linear velocity of the player
            _rb2d.linearVelocityY = 0;

            // Jump from the wall (reset double jump)
            _rb2d.AddForce(_wallJumpForce, ForceMode2D.Impulse);

            // Set the double jump to true
            _canDoubleJump = true;
        }
        // Next, we want to check if we are within coyote time to call the first jump
        else if (_coyoteTimeCurrent > 0)
        {
            // First, we reset the linear velocity of the player
            _rb2d.linearVelocityY = 0;

            // Jump if we are grounded
            _rb2d.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);

            // Set coyote time to be off
            _coyoteTimeCurrent = 0;
        }
        // Finally, we want to check if we are in the air and can double jump,
        // and if so, we will double jump and set the jump to false
        else if (_canDoubleJump)
        {
            // First, we reset the linear velocity of the player
            _rb2d.linearVelocityY = 0;

            // Double jump if we are not grounded and can double jump
            _rb2d.AddForce(Vector2.up * _doubleJumpForce, ForceMode2D.Impulse);
            _canDoubleJump = false;
        }
    }

    private void EndJumpMethod(InputAction.CallbackContext context)
    {
        // Update the player jumping
    }

    #endregion

    #region Movement Methods

    /// <summary>
    /// This is the method that will be called to move the player
    /// </summary>
    protected override void MovePlayer()
    {
        // Only call this if the rigidbody is not null
        if (_rb2d == null)
        {
            Debug.LogError("Rigidbody2D is null");
            return;
        }

        // We want to stop applying linear velocity force if we are colliding with a wall as it
        // messes with wall sliding
        if (!_isCollidingWithWall)
        {
            // Move the player based on whether the player is dashing
            if (_isDashing)
            {
                _rb2d.linearVelocityX = _moveVector.x * _dashSpeed;
            }
            else
            {
                _rb2d.linearVelocityX = _moveVector.x * _speed;
            }
        }
        
        // Set the player's rotation
        if (_moveVector.x > 0)
        {
            _rb2d.transform.localRotation = Quaternion.Euler(0, 0, 0);
            _wallJumpForce.x = -1 * Mathf.Abs(_wallJumpForce.x);
        }
        else if (_moveVector.x < 0)
        {
            _rb2d.transform.localRotation = Quaternion.Euler(0, 180, 0);
            _wallJumpForce.x = Mathf.Abs(_wallJumpForce.x);
        }
    }

    /// <summary>
    /// Fixed update will handle all of the special movement parts we need to deal with like jumping,
    /// coyote time, wall jumping etc
    /// </summary>
    protected override void FixedUpdate()
    {
        // Check if we are grounded and against a wall
        CheckGrounded();
        CheckWallCollision();

        // We need to handle coyote time and wall sliding next
        HandleCoyoteTime();
        HandleWallSlide();

        // Then we handle player inputs
        MovePlayer();
        JumpPlayer();
    }

    #region Check Collision Methods

    /// <summary>
    /// This script will check if we are grounded when called and set the _isGrounded variable
    /// </summary>
    private void CheckGrounded()
    {
        // Check if we have transforms and layers set
        if (_groundCheckTransform == null)
        {
            Debug.LogError("Ground Check Transform is null");
            return;
        }

        if (_groundLayer == 0)
        {
            Debug.LogError("Ground Layer is not set");
            return;
        }

        _isGrounded = Physics2D.OverlapBox(_groundCheckTransform.position, _groundCheckDimensions, 0f, _groundLayer);

        // If we are grounded, we need to reset the double jump
        if (_isGrounded)
        {
            _canDoubleJump = true;
        }
    }

    /// <summary>
    /// This script will check if we are colliding with a wall when called and set the _isCollidingWithWall variable
    /// </summary>
    private void CheckWallCollision()
    {
        // Check if we have transforms and layers set
        if (_wallCheckTransform == null)
        {
            Debug.LogError("Wall Check Transform is null");
            return;
        }

        if (_wallLayer == 0)
        {
            Debug.LogError("Wall Layer is not set");
            return;
        }

        _isCollidingWithWall = Physics2D.OverlapBox(_wallCheckTransform.position, _wallCheckDimensions, 0f, _wallLayer);
    }

    #endregion

    #region Coyote Time Methods

    /// <summary>
    /// This method will handle the coyote time for the player. Time stays at max if the player is grounded
    /// and decrements when the player is airborne
    /// </summary>
    private void HandleCoyoteTime()
    {
        // Reset the coyote time if we are grounded
        if (_isGrounded)
        {
            _coyoteTimeCurrent = _coyoteTimeMax;
        }
        // Otherwise decrement coyote time
        else
        {
            DecrementCoyoteTime();
        }
    }

    /// <summary>
    /// This is called when the player is considered not grounded. Will decrement the coyote time
    /// available until it is 0 and the player can no longer double jump.
    /// </summary>
    private void DecrementCoyoteTime()
    {
        if (_coyoteTimeCurrent > 0)
        {
            _coyoteTimeCurrent -= Time.deltaTime;
        }
    }

    #endregion

    #region Wall Slide Methods

    /// <summary>
    /// This method will handle the wall jump for the player. The player will be able to jump off of walls
    /// while they are sliding on them.
    /// </summary>
    private void HandleWallSlide()
    {
        // If we are colliding with a wall and not grounded
        if (_isCollidingWithWall && !_isGrounded && _rb2d.linearVelocityY <= 0)
        {
            _isWallSliding = true;

            // We need to slow the player's y velocity to slow them down
            _rb2d.linearVelocityY = -_wallSlideSpeed;
        }
        else
        {
            // Otherwise set it to false
            _isWallSliding = false;
        }
    }

    #endregion

    #region Jump Methods

    /// <summary>
    /// This method will handle the jump for the player. The player will be able to jump off of walls and holding
    /// jump will allow the player to jump higher. The player will also be able to double jump.
    /// </summary>
    private void JumpPlayer()
    {

    }

    #endregion

    #endregion

    #endregion

    #region Gizmos

    void OnDrawGizmosSelected()
    {
        if (_wallCheckTransform == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_wallCheckTransform.position, _wallCheckDimensions);

        Gizmos.DrawWireCube(_groundCheckTransform.position, _groundCheckDimensions);
    }

    #endregion

    */
}
