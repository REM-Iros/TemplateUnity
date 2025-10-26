using UnityEngine;

/// <summary>
/// This is a helper script that will be used by the camera manager to
/// set the current target for the camera to follow.
/// 
/// REM-i
/// </summary>
public class CameraTarget : MonoBehaviour
{
    [Tooltip("This is the transform that we want to target.")]
    [SerializeField, Header("Camera Target")]
    private Transform _position;

    /// <summary>
    /// This is the getter for the target
    /// </summary>
    public Transform Position { get { return _position; } }

    /// <summary>
    /// This is the setter for the target
    /// </summary>
    /// <param name="target"></param>
    public void SetTransform(Transform target)
    {
        _position = target;
    }
}
