using UnityEngine;

/// <summary>
/// This is the parent script for all of the movement scripts. All versions of this
/// should only need to implement moving the player and should include anything that
/// it might need that isn't included in the current script. Components will also be checked
/// for to enable more features for the script.
/// 
/// REM-i
/// </summary>
public class PMMovementManager : MonoBehaviour
{
    #region Vars

    [Tooltip("This is the list of movement interface you will end up using!")]
    [SerializeField, Header("Move Components")]
    private InterfaceWrapper<IMovementInterface> _moveComponent;

    [Tooltip("This is the velocity controller that actually enables a player to move.")]
    [SerializeField]
    private PMVelocityController _velocityController;

    // Movement vector for player moving
    private Vector2 _moveVector;

    [Tooltip("This is the wall detector to disable movement against a wall.")]
    [SerializeField, Header("Wall Jump")]
    private WallDetection _wallDetection;

    [Tooltip("This is the wall jump script, it needs to disable the movement for a small amount of time.")]
    [SerializeField]
    private PMWallJump _wallJump;

    #endregion

    #region Methods

    /// <summary>
    /// On awake, we want to check if we even have a component to work with, otherwise,
    /// we need to throw an error because this script either shouldn't be here, or needs
    /// to have the movement component applied. (Editor application should be the standard
    /// because otherwise the order doesn't really work).
    /// </summary>
    private void Awake()
    {
        // If our movement component is null, we need to throw an error
        if (_moveComponent == null)
        {
            Debug.LogError("Movement Manager attached but no move component, player will not be able to move.");
        }
    }

    #region Event Methods

    /*
     * Attach these scripts to the input script from the parent
     */
    public void GetMovementPressed(Vector2 MoveVector)
    {
        ExecuteMove(MoveVector);
    }

    public void GetMovementReleased(Vector2 MoveVector)
    {
        ExecuteMove(MoveVector);
    }

    #endregion

    #region Move Methods

    private void ExecuteMove(Vector2 MoveVector)
    {
        // Check if we even have moves to be able to do
        if (_moveComponent == null)
        {
            Debug.LogError("No move component found");
            return;
        }

        _moveVector = MoveVector;
        
    }

    /// <summary>
    /// Call the movement method in the fixed update to handle physics
    /// </summary>
    private void FixedUpdate()
    {
        // Only run this if we have a move component
        if (_moveComponent == null)
        {
            Debug.LogError("No move component found");
            return;
        }

        // Alter the movement vector if we are against a wall
        if (_wallDetection != null)
        {
            CheckWallMovement();
        }

        _moveComponent.Value.Move(_moveVector);
    }

    /// <summary>
    /// If we are against a wall, stop allowing movement in that direction.
    /// </summary>
    private void CheckWallMovement()
    {
        if (_wallDetection.IsRightWallColliding)
        {
            _moveVector.x = Mathf.Clamp(_moveVector.x, -1, 0);
        }

        if (_wallDetection.IsLeftWallColliding)
        {
            _moveVector.x = Mathf.Clamp(_moveVector.x, 0, 1);
        }
    }

    #endregion

    #endregion
}
