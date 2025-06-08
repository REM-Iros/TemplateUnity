using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// This is the parent script for all of the jumping scripts. All versions of this
/// should only need to implement jumping the player and should include anything that
/// it might need that isn't included in the current script. Components will also be checked
/// for to enable more features for the script.
/// 
/// NOTE: Jumping is not compatible for top down movement... you'll need to implement a different
/// script for that.
/// 
/// REM-i
/// </summary>
public class JumpingComponentParent : MonoBehaviour
{
    #region Vars

    [Tooltip("This is the rigidbody we need for movement.")]
    [SerializeField]
    protected Rigidbody2D _rb2d;

    #region Jump Vars

    [Tooltip("This is the jump force for the player.")]
    [SerializeField, Header("Jump Vars")]
    protected float _jumpForce;

    protected bool _isJumpHeld = false;

    #endregion

    #region Ground Checking Vars

    [Tooltip("This is the size of the raycast (use a rectangle).")]
    [SerializeField, Header("Ground Check Vars")]
    private Vector2 _raycastRect;

    [Tooltip("This is how far we want the rectangle down before we cast it.")]
    [SerializeField]
    private float _raycastDist;

    [Tooltip("This is the layer mask for the ground check.")]
    [SerializeField]
    private LayerMask _groundLayer;

    // Check if we are grounded
    private bool _isGrounded;
    public bool IsGrounded { get { return _isGrounded; } }

    [Space(5)]

    #endregion

    #region Component Vars

    [Tooltip("This stores the coyote time modifier script.")]
    [SerializeField, Header("Components")]
    private PMCoyoteTimeModifier _coyoteTimeController;

    [Tooltip("This stores the wall jump modifier script.")]
    [SerializeField]
    private PMWallJump _wallJumpController;

    #endregion

    #endregion

    #region Methods

    #region Startup Methods

    /// <summary>
    /// On startup, we need to initialize
    /// </summary>
    private void Awake()
    {
        InitDependencies();
    }

    /// <summary>
    /// Finds the components necessary for this script to operate
    /// </summary>
    private void InitDependencies()
    {
        // Check for player input and rigidbody
        if (_rb2d == null)
        {
            _rb2d = GetComponent<Rigidbody2D>();
            Debug.LogError("No Rigidbody2D found, script will NOT work!");
            return;
        }
    }

    #endregion

    #region Event Methods

    /*
     * Attach these scripts to the input script from the parent
     */
    public void GetJumpPressed()
    {
        // This is where you will activate the jumping for the player
        JumpPlayer();
    }

    public void GetJumpReleased()
    {
        // This will only really be needed for variable height
        OnJumpReleased();
    }

    #endregion

    #region Jumping Methods

    /// <summary>
    /// This is the method that will be called to apply force to make
    /// the player jump. Will need to be implemented in different variants.
    /// </summary>
    private void JumpPlayer()
    {
        // Only call this if the rigidbody is not null
        if (_rb2d == null)
        {
            Debug.LogError("Rigidbody2D is null");
            return;
        }

        // Only jump if we pass all checks
        if (!_isGrounded)
        {
            return;
        }

        ApplyJumpForce();

        // Apply the wall jump force if we have a wall jump script
        if (_wallJumpController != null)
        {
            _wallJumpController.ApplyWallJumpForce();
        }

        _isJumpHeld = true;
    }

    /// <summary>
    /// This script checks to make sure the raycast is hitting to allow
    /// the player to jump. We will also need to check for components when
    /// we call this.
    /// </summary>
    public void CheckGround()
    {
        // Check if we have transforms and layers set
        if (_raycastRect == null)
        {
            Debug.LogError("Ground Check Transform is null");
            return;
        }

        if (_groundLayer == 0)
        {
            Debug.LogError("Ground Layer is not set");
            return;
        }

        _isGrounded = Physics2D.OverlapBox(new Vector2(transform.position.x, transform.position.y - _raycastDist), _raycastRect, 0f, _groundLayer);

        // If we have coyote time, check if we are in the grace period
        if (_coyoteTimeController != null)
        {
            _isGrounded = (_coyoteTimeController.HandleCoyoteTime(_isGrounded) > 0);
        }
    }

    /// <summary>
    /// This is the script the children variants will inherit and override
    /// to provide functionality.
    /// </summary>
    protected virtual void ApplyJumpForce()
    {
        // Implement functionality in child scripts here
    }

    /// <summary>
    /// This is the script that will run while the jump button is held,
    /// needs to be overridden to provide functionality.
    /// </summary>
    public virtual void ExecuteDuringJump()
    {
        // Implement functionality in child scripts here
    }

    /// <summary>
    /// This is the script that will run when the jump button is released.
    /// Needs to be overridden to provide functionality.
    /// </summary>
    protected virtual void OnJumpReleased()
    {
        // Implement functionality in child scripts here
    }

    #endregion

    #region Debug Methods

    /// <summary>
    /// Draws the ground check cube
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        if (_raycastRect == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector2(transform.position.x, transform.position.y - _raycastDist), _raycastRect);
    }

    #endregion

    #endregion
}
