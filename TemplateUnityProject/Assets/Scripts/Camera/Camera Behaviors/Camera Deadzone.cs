using UnityEngine;

/// <summary>
/// This script handles deadzone behavior for the camera, only moving
/// the camera when the player gets out of the deadzone.
/// 
/// REM-i
/// </summary>
public class CameraDeadzone : MonoBehaviour, ICameraBehavior
{
    #region Vars

    [Tooltip("This is the deadzone x value for the camera to work with.")]
    [SerializeField, Header("Deadzone Vars")]
    private float _xDeadzone;

    [Tooltip("This is the deadzone y value for the camera to work with.")]
    [SerializeField] 
    private float _yDeadzone;

    #endregion

    #region Methods

    /// <summary>
    /// This will move the camera to follow the player when they are out of the deadzone.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public Vector2 GetTargetPosition(Vector2 position, Transform target)
    {
        // The distance we need to travel to reach destination
        float newX = position.x;
        float newY = position.y;

        // Distance between each point
        float xDist = position.x - target.position.x;
        float yDist = position.y - target.position.y;

        // Check if the x distance is greater than the x threshold or less than the x threshold
        if (Mathf.Abs(xDist) > _xDeadzone )
        {
            newX = target.position.x - Mathf.Sign(xDist) * _xDeadzone;
        }

        // Check if the x distance is greater than the x threshold or less than the x threshold
        if (Mathf.Abs(yDist) > _yDeadzone)
        {
            newY = target.position.y - Mathf.Sign(yDist) * _yDeadzone;
        }

        return new Vector2(newX, newY);
    }

    #endregion
}
