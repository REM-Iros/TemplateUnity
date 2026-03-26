using Unity.VisualScripting;
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

    [Tooltip("Ground detection to poll when we are no longer grounded.")]
    [SerializeField]
    private GroundDetection _groundDetection;

    // Coyote time is the grace period for the player to jump after leaving a platform
    [Tooltip("This is the coyote time for the player.")]
    [SerializeField, Header("Coyote Time Vars")]
    private float _coyoteTimeMax;

    // This is the last recorded time the player was grounded
    private float _lastGroundedTime;

    // This bool is used to detect when grounded state change occurs
    private bool _grounded;

    #endregion

    #region Methods

    /// <summary>
    /// Check if coyote time is present and if it isn't, then we throw an error.
    /// </summary>
    private void Awake()
    {
        if (_groundDetection == null)
        {
            Debug.LogError("Ground detection not found for coyote time, will not work.");
            return;
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
    private void OnGroundStateChange(bool currentState)
    {
        _grounded = currentState;

        // Store the current time as the last time we were grounded
        if (!_grounded)
        {
            _lastGroundedTime = Time.time;
        }
    }

    #endregion

    /// <summary>
    /// Checks the current coyote time against the threshold and if we are within it, returns true.
    /// </summary>
    /// <returns></returns>
    public bool WithinCoyoteTimeThreshold()
    {
        // If we are grounded, don't bother checking
        if (_grounded)
        {
            return true;
        }
        // If we aren't, compare last time player touched the ground to current time to see if it is under the threshold
        else
        {
            return Time.time - _lastGroundedTime <= _coyoteTimeMax;
        }
    }

    #endregion
}
