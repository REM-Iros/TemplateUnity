using UnityEngine;

/// <summary>
/// This is a modifier script to the jumping script and moving script, requiring 
/// both movement and jumping to work. This script allows the player to
/// jump from walls repeatedly, resetting jumps for the player.
/// 
/// REM-i
/// </summary>
public class PMWallJump : MonoBehaviour
{
    #region Vars

    [Tooltip("This is the rigidbody we will need for jumping.")]
    [SerializeField, Header("Component Vars")]
    private Rigidbody2D _rb2d;

    // Move vector that gets passed in with the event
    private Vector2 _moveVector;

    #endregion

    #region Methods

    /// <summary>
    /// Make sure wall detection is present on init or the script will break
    /// </summary>
    private void Awake()
    {
        // Check for rigidbody
        if (_rb2d == null)
        {
            _rb2d = GetComponent<Rigidbody2D>();
            Debug.LogError("No rigidbody found, this could be an issue.");
        }
    }

    /// <summary>
    /// Gets the movement vector passed in from the input system
    /// </summary>
    public void GetMovementVector(Vector2 moveVector)
    {
        _moveVector = moveVector;
    }

    /// <summary>
    /// This lowers gravity force for the player when they 
    /// </summary>
    public void ChangeWallGravity()
    {

    }

    /// <summary>
    /// This applies horizontal force in the opposite direction of the current move vector
    /// </summary>
    public void ApplyWallJumpForce()
    {

    }

    #endregion
}
