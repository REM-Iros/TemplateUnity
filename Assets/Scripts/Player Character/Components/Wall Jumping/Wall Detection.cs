using UnityEngine;

/// <summary>
/// This is a script that is used for wall detection. It is a component that will
/// raycast from both sides of the player to detect for walls and if the wall is detected
/// and the player is not grounded, it will allow them to stick to the wall, and jump from
/// it.
/// 
/// REM-i
/// </summary>
public class WallDetection : MonoBehaviour
{
    #region Vars

    [Tooltip("This is the size of the raycast (use a rectangle).")]
    [SerializeField, Header("Ground Check Vars")]
    private Vector2 _raycastRect;

    [Tooltip("This is how far we want the rectangle down before we cast it.")]
    [SerializeField]
    private float _raycastDist;

    [Tooltip("This is the layer mask for the ground check.")]
    [SerializeField]
    private LayerMask _wallLayer;

    // Left wall check
    private bool _isLeftWallColliding;
    public bool IsLeftWallColliding { get { return _isLeftWallColliding; } }

    // Right wall check
    private bool _isRightWallColliding;
    public bool IsRightWallColliding { get { return _isRightWallColliding; } }

    #endregion

    #region Methods

    /// <summary>
    /// A basic catch all to just check if we are actually colliding with something
    /// </summary>
    /// <returns></returns>
    public bool IsColliding()
    {
        return (_isLeftWallColliding || _isRightWallColliding);
    }

    /// <summary>
    /// Checks for if the player is running up against a wall and updates
    /// the bools based on this.
    /// </summary>
    public void DetectWalls()
    {
        // Check if we have transforms and layers set
        if (_raycastRect == null)
        {
            Debug.LogError("Wall detection is null");
            return;
        }

        if (_wallLayer == 0)
        {
            Debug.LogError("Wall Layer is not set");
            return;
        }

        _isLeftWallColliding = Physics2D.OverlapBox(new Vector2(transform.position.x - _raycastDist, transform.position.y), _raycastRect, 0f, _wallLayer);
        _isRightWallColliding = Physics2D.OverlapBox(new Vector2(transform.position.x + _raycastDist, transform.position.y), _raycastRect, 0f, _wallLayer);
    }

    #region Debug Methods

    /// <summary>
    /// Draws the wall check cubes
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        if (_raycastRect == null) return;

        Gizmos.color = Color.blue;

        // Draw both raycasts
        Gizmos.DrawWireCube(new Vector2(transform.position.x + _raycastDist, transform.position.y), _raycastRect);
        Gizmos.DrawWireCube(new Vector2(transform.position.x - _raycastDist, transform.position.y), _raycastRect);
    }

    #endregion

    #endregion
}
