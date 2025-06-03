using UnityEngine;

/// <summary>
/// This is a modifier script that goes with the jumping component. It
/// allows the player "coyote time" as it is called, where the player 
/// has a slight grace period of stepping off of the ledge where they
/// get to jump still.
/// 
/// REM-i
/// </summary>
public class PMCoyoteTimeModifier : MonoBehaviour
{
    #region Vars

    // Coyote time is the grace period for the player to jump after leaving a platform
    [Tooltip("This is the coyote time for the player.")]
    [SerializeField, Header("Coyote Time Vars")]
    private float _coyoteTimeMax;

    // This is the current amount of expended coyote time for the player
    private float _coyoteTimeCurrent;

    #endregion

    #region Methods

    /// <summary>
    /// This method will handle the coyote time for the player. Time stays at max if the player is grounded
    /// and decrements when the player is airborne
    /// </summary>
    public float HandleCoyoteTime(bool _isGrounded)
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

        return _coyoteTimeCurrent;
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
}
