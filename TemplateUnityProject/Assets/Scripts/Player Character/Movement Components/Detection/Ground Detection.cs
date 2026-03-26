using System;
using UnityEngine;

/// <summary>
/// This is a script that is used for ground detection. It is a component that will
/// raycast below the player to check if they are grounded, and if they are, will set
/// a bool that can be polled by other methods that need it.
/// 
/// REM-i
/// </summary>
public class GroundDetection : MonoBehaviour
{
    #region Vars

    #region Raycast Vars

    [Tooltip("This is the size of the raycast (use a rectangle).")]
    [SerializeField, Header("Raycast Vars")]
    private Vector2 _raycastRect;

    [Tooltip("This is how far we want the rectangle down before we cast it.")]
    [SerializeField]
    private float _raycastDist;

    [Tooltip("This is the layer mask for the ground check.")]
    [SerializeField]
    private LayerMask _groundLayer;

    #endregion

    // Check if we are grounded
    private bool _isGrounded;
    public bool IsGrounded { get { return _isGrounded; } }

    // This is what we check if we are grounded.
    private bool _groundedCurrently;

    // This event fires when the grounded state changes notifying all listeners.
    public event Action<bool> OnGroundedStateChange;

    #endregion

    #region Methods

    // Update will check if the player is grounded, and if not, will set for how long
    // the player hasn't been grounded.
    void Update()
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

        _groundedCurrently = Physics2D.OverlapBox(new Vector2(transform.position.x, transform.position.y - _raycastDist), _raycastRect, 0f, _groundLayer);

        // Fire off event to notify subscribers about state change
        if (_groundedCurrently != _isGrounded)
        {
            _isGrounded = _groundedCurrently;

            OnGroundedStateChange?.Invoke(_isGrounded);
        }
    }

    #region Debug Methods

    /// <summary>
    /// Draws the ground check cubes
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        if (_raycastRect == null) return;

        Gizmos.color = Color.green;

        // Draw raycast
        Gizmos.DrawWireCube(new Vector2(transform.position.x, transform.position.y - _raycastDist), _raycastRect);
    }

    #endregion

    #endregion


}
