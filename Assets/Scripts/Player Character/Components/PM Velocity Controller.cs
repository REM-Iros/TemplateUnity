using UnityEngine;

/// <summary>
/// Since we will likely have multiple movement components that will all want to affect the
/// velocity of the rigidbody, this script will be used to control the velocity. It prevents certain
/// components from completely overwriting things, allowing for more complex movement systems and blending
/// of movement styles. We will also have an override system in place to allow for certain components to run critical
/// methods that need to run before others, such as wall jumps or dashes.
/// 
/// REM-i
/// </summary>
public class PMVelocityController : MonoBehaviour
{
    #region Vars

    [Tooltip("This is the rigidbody we will be controlling the velocity of.")]
    [SerializeField, Header("Rigidbody")]
    private Rigidbody2D _rb2d;

    // This is the cumulative velocity that will be calculated each frame
    private Vector2 _cumulativeVelocity;

    // This is the override velocity. This will be used to override the cumulative velocity if needed
    private Vector2 _overrideVelocity;

    // This is the override active flag. If this is true, the override velocity will be used instead of the cumulative velocity
    private bool _isOverrideActive;

    #endregion

    #region Methods

    /// <summary>
    /// On awake, we want to check if we have a rigidbody to work with, otherwise, the script won't run.
    /// </summary>
    private void Awake()
    {
        // Check for rigidbody
        if (_rb2d == null)
        {
            Debug.LogError("No Rigidbody2D found, script will not work.");
        }
    }

    #region Velocity Methods

    /// <summary>
    /// Adds a velocity to the cumulative velocity. This will be used to calculate the final velocity.
    /// </summary>
    /// <param name="velocity"></param>
    public void AddToCumulativeVelocity(Vector2 velocity)
    {
        // Add the velocity to the cumulative velocity
        _cumulativeVelocity += velocity;
    }

    /// <summary>
    /// Adds a velocity to the override velocity. This will override the cumulative velocity until the override is removed.
    /// </summary>
    /// <param name="velocity"></param>
    public void AddOverrideVelocity(Vector2 velocity)
    {
        // If the override is already active, then we don't want to add a new force
        if (_isOverrideActive)
        {
            return;
        }

        // Set the override velocity and activate the override
        _overrideVelocity = velocity;
        _isOverrideActive = true;
    }

    /// <summary>
    /// Turns off the override enabling cumulative velocity to be used again.
    /// </summary>
    public void EndOverride()
    {
        // End the override by setting the active flag to false
        _isOverrideActive = false;
    }

    /// <summary>
    /// FixedUpdate is called at a fixed interval and is used for physics calculations.
    /// </summary>
    private void FixedUpdate()
    {
        HandleVelocityApplication();
    }

    /// <summary>
    /// Handles the application of the velocity to the rigidbody.
    /// </summary>
    private void HandleVelocityApplication()
    {
        // Only run if we have a rigidbody
        if (_rb2d == null)
        {
            Debug.LogError("No Rigidbody2D found, script will not work.");
            return;
        }

        // Reset the cumulative velocity to zero before applying new forces
        if (_isOverrideActive)
        {
            // If the override is active, apply the override velocity
            _rb2d.linearVelocity = _overrideVelocity;
        }
        else
        {
            // If the override is not active, apply the cumulative velocity
            _rb2d.linearVelocity = _cumulativeVelocity;
        }

        // Reset both velocities after applying them
        _overrideVelocity = Vector2.zero;
        _cumulativeVelocity = Vector2.zero;
    }

    #endregion

    #endregion
}
