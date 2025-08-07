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
    private PMMovementManager _movementController;

    [Tooltip("This is the jumping component script")]
    [SerializeField]
    private PMJumpManager _jumpManager;

    #endregion

    // This is the data manager, used to store player data
    private DataManager _dataManager;

    #endregion

    #region Methods

    #region Init Methods

    /// <summary>
    /// On awake, we need to subscribe to all the C# events of the player script
    /// </summary>
    private void Awake()
    {
        _dataManager = ServiceLocator.Get<DataManager>();

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
            _inputController.RegisterMovementPerformed(_movementController.GetMovementPressed);
            _inputController.RegisterMovementCancelled(_movementController.GetMovementReleased);

        }

        // Attach the scripts that need to be attached between jumping and input
        if (_jumpManager != null)
        {
            // Set the jump action
            // With us using a manager now, we just need to sub the manager's get jump pressed and released
            _inputController.RegisterJumpPerformed(_jumpManager.GetJumpPressed);
            _inputController.RegisterJumpCancelled(_jumpManager.GetJumpReleased);
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
            _inputController.UnregisterMovementPerformed(_movementController.GetMovementPressed);
            _inputController.UnregisterMovementCancelled(_movementController.GetMovementReleased);
        }

        // Attach the scripts that need to be attached between jumping and input
        if (_jumpManager != null)
        {
            // Set the jump action
            _inputController.UnregisterJumpPerformed(_jumpManager.GetJumpPressed);
            _inputController.UnregisterJumpCancelled(_jumpManager.GetJumpReleased);
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
        // Update the datamanager's current position
        if (_dataManager != null)
        {
            _dataManager.GameData.currPlayerPosition = transform.position;
        }
    }

    #endregion

    #endregion
}
