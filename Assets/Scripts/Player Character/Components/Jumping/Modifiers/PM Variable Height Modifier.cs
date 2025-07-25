using UnityEngine;

/// <summary>
/// This is a modifier for jumping that allows the player to hold
/// the jump button to gain more height.
/// 
/// REM-i
/// </summary>
public class PMVariableHeightModifier : MonoBehaviour, IJumpReleaseListener
{
    #region Vars

    [Tooltip("This is rigidbody we need to apply force to the object.")]
    [SerializeField, Header("Rigidbody")]
    private Rigidbody2D _rb2d;

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

    #region Methods

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
    public void NotifyJumping()
    {
        // Change the gravity scale
        _rb2d.gravityScale = _jumpGravityScale;

        // Set the hold time to max
        _holdTime = _maxHoldTime;

    }

    /// <summary>
    /// While jump is being held, continue to apply force until either it is released
    /// or the timer is up.
    /// </summary>
    private void Update()
    {
        // Make sure we only run this while we have hold time
        if (_holdTime <= 0)
        {
            _rb2d.gravityScale = _baseGravityScale;
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
    public void JumpRelease()
    {
        _holdTime = 0;

        _rb2d.gravityScale = _baseGravityScale;
    }

    #endregion

    #endregion
}
