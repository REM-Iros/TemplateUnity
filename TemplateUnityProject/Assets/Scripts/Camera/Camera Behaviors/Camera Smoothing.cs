using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// This script handles smoothing for the camera, gradually moving the camera
/// towards the player instead of aggressively tracking the player. This
/// should be the last method called in the camera manager to enable 
/// smoothing of movement.
/// 
/// REM-i
/// </summary>
public class CameraSmoothing : MonoBehaviour, ICameraBehavior
{
    #region Vars

    [Tooltip("This is the speed the camera smoothes.")]
    [SerializeField, Header("Smoothing Vars")]
    private float _smoothSpeed;

    [Tooltip("This is the default camera offset during gameplay.")]
    [SerializeField]
    private Vector2 _offset;

    // Private velocity for use with smoothdamp
    private Vector2 _velocity = Vector2.zero;

    #endregion

    #region Methods

    /// <summary>
    /// Implementation of get target position, tracks the target and smooths camera movements
    /// towards the target using SmoothDamp.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public Vector2 GetTargetPosition(Vector2 position, Transform target)
    {
        // Don't move if no target
        if(target == null)
        {
            return position;
        }

        // Don't move the position if a new position isn't passed in.
        if (position == (Vector2)target.position)
        {
            return position;
        }

        // Set the offset
        Vector2 desiredPosition = (Vector2)target.position + _offset;

        // Calc the smoothing
        Vector2 smoothedPosition = Vector2.SmoothDamp(position, desiredPosition, ref _velocity, _smoothSpeed);

        // Return the smoothed position
        return smoothedPosition;
    }

    #endregion
}
