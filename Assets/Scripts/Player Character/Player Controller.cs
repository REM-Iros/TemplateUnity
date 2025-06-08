using UnityEngine;

/// <summary>
/// This is a movement parent script for the player characters. It works like
/// a master controller, with a bunch of smaller scripts acting as a component
/// that this script will check for and then provide if you have set it to be
/// true.
/// 
/// REM-i
/// </summary>
public class PlayerController : MonoBehaviour
{
    #region Vars

    [Tooltip("This is the player controller.")]
    [SerializeField, Header("Input Controller")]
    private PlayerInputController _inputController;

    #region Components

    [Tooltip("This is the movement component script")]
    [SerializeField, Header("Movement Components")]
    private MovementComponentParent _movementController;

    [Tooltip("This is the jumping component script")]
    [SerializeField]
    private JumpingComponentParent _jumpingController;

    [Tooltip("This is the wall detection script")]
    [SerializeField]
    private WallDetection _wallDetection;

    [Tooltip("This is the wall jump component script")]
    [SerializeField]
    private PMWallJump _wallJump;

    #endregion

    #endregion

    #region Methods

    #region Init Methods

    /// <summary>
    /// On awake, we need to subscribe to all the C# events of the player script
    /// </summary>
    private void Awake()
    {
        InitializeComponents();
    }

    /// <summary>
    /// We want to get the components that we need for the script if they aren't already defined
    /// </summary>
    private void InitializeComponents()
    {
        // Check for player input and rigidbody
        if (_inputController == null)
        {
            _inputController = GetComponent<PlayerInputController>();
            Debug.LogError("No Input controller found, script will NOT work!");
            return;
        }

        // Check for movement scripts
        if (_movementController == null)
        {
            Debug.Log("No movement script found, was this intentional?");
            return;
        }
    }

    #region Event Subscription Methods

    /// <summary>
    /// When this object is enabled, the events need to be subscribed/resubbed to
    /// </summary>
    private void OnEnable()
    {
        // Sub to input events
        SubscribeInputEvents();
    }

    /// <summary>
    /// Called on enable, subscribes events to any place that needs them
    /// </summary>
    protected virtual void SubscribeInputEvents()
    {
        // Ensure the input controller isn't null
        if (_inputController == null)
        {
            Debug.LogError("Input controller is null");
            return;
        }

        // Attach the scripts that need to be attached between movement and input
        if (_movementController != null)
        {
            // Set movement
            _inputController.RegisterMovementPerformed(_movementController.GetMovementVector);
            _inputController.RegisterMovementCancelled(_movementController.GetMovementVector);

            // Set dash
            _inputController.RegisterDashPerformed(_movementController.GetDashPressed);
            _inputController.RegisterDashCancelled(_movementController.GetDashReleased);
        }

        // Attach the scripts that need to be attached between jumping and input
        if (_jumpingController != null)
        {
            // Set the jump action
            _inputController.RegisterJumpPerformed(_jumpingController.GetJumpPressed);
            _inputController.RegisterJumpCancelled(_jumpingController.GetJumpReleased);
        }

        // Attach the scripts that need to be attached between movement and wall jumping
        if (_wallJump != null)
        {
            // Set wall jumping
            _inputController.RegisterMovementPerformed(_wallJump.GetMovementVector);
            _inputController.RegisterMovementCancelled(_wallJump.GetMovementVector);
        }
    }

    /// <summary>
    /// When an object is disabled (Or destroyed), unsubs from all events it needs to
    /// </summary>
    private void OnDisable()
    {
        UnsubscribeInputEvents();
    }

    /// <summary>
    /// Called on disable, unsubs events from any place they were subscribed
    /// </summary>
    protected virtual void UnsubscribeInputEvents()
    {
        // Ensure the input controller isn't null
        if (_inputController == null)
        {
            Debug.LogError("Input controller is null");
            return;
        }

        // Attach the scripts that need to be attached between movement and input
        if (_movementController != null)
        {
            // Set movement
            _inputController.UnregisterMovementPerformed(_movementController.GetMovementVector);
            _inputController.UnregisterMovementCancelled(_movementController.GetMovementVector);

            // Set dash
            _inputController.UnregisterDashPerformed(_movementController.GetDashPressed);
            _inputController.UnregisterDashCancelled(_movementController.GetDashReleased);
        }

        // Attach the scripts that need to be attached between jumping and input
        if (_jumpingController != null)
        {
            // Set the jump action
            _inputController.UnregisterJumpPerformed(_jumpingController.GetJumpPressed);
            _inputController.UnregisterJumpCancelled(_jumpingController.GetJumpReleased);
        }

        // Attach the scripts that need to be attached between movement and wall jumping
        if (_wallJump != null)
        {
            // Set wall jumping
            _inputController.UnregisterMovementPerformed(_wallJump.GetMovementVector);
            _inputController.UnregisterMovementCancelled(_wallJump.GetMovementVector);
        }
    }

    #endregion

    #endregion

    #region Movement Methods

    /// <summary>
    /// Fixed update is used for physics calculations with Rigidbody2D
    /// </summary>
    protected virtual void FixedUpdate()
    {
        // Get our wall detection
        if (_wallDetection != null)
        {
            _wallDetection.DetectWalls();
        }

        // If we have a jump controller, we want anything that needs to
        // be executed during the jump to occur here
        if (_jumpingController != null)
        {
            _jumpingController.CheckGround();
            _jumpingController.ExecuteDuringJump();
        }

        // If we have a movement controller, we want to move the player
        // if they are currently ready to be moved
        if (_movementController != null)
        {
            _movementController.MovePlayer();
        }
    }

    #endregion

    #endregion
}
