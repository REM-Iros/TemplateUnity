using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This is a movement parent script for the player characters. It handles basic
/// functions that will be needed between every script.
/// 
/// REM-i
/// </summary>
public class PlayerMovementParent : MonoBehaviour
{
    #region Vars

    [Tooltip("This is the player controller.")]
    [SerializeField, Header("Input Controller")]
    protected PlayerInput _playerInput;

    [Space(5)]

    [Tooltip("This is the basic speed variable.")]
    [SerializeField, Header("Movement Vars")]
    protected float _speed;

    [Tooltip("This is the basic speed variable.")]
    [SerializeField]
    protected float _dashSpeed;

    [Tooltip("This is the rigidbody we need for movement.")]
    [SerializeField]
    protected Rigidbody2D _rb2d;

    // This is the 2D vector that movement will be passed into
    protected Vector2 _moveVector;
    // This is the boolean that will be used to check if the player is dashing
    protected bool _isDashing;

    #region Input Actions

    /*
    This should be a list of all of the input actions for the map that
    you plan to use. Not going to define them all with comments because
    they are self-explanatory. 
    */

    private InputAction _moveAction;
    private InputAction _attackAction;
    private InputAction _dashAction;
    private InputAction _menuAction;

    #endregion

    #endregion

    #region Methods

    #region Init and Destroy Methods

    /// <summary>
    /// On awake, we need to subscribe to all the C# events of the player script
    /// </summary>
    protected virtual void Awake()
    {
        InitializeComponents();
        InitializeInputActions();

        // Ensure the player input isn't null
        if (_playerInput == null)
        {
            Debug.LogError("Player Input is null");
            return;
        }

        SubscribeToStartupEvents();
    }

    /// <summary>
    /// We want to get the components that we need for the script if they aren't already defined
    /// </summary>
    protected virtual void InitializeComponents()
    {
        // Check for player input and rigidbody
        if (_playerInput == null)
        {
            _playerInput = GetComponent<PlayerInput>();
        }
        if (_rb2d == null)
        {
            _rb2d = GetComponent<Rigidbody2D>();
        }
    }

    /// <summary>
    /// Called on startup, this initializes all the input actions for the script
    /// for easier reference
    /// </summary>
    protected virtual void InitializeInputActions()
    {
        // Ensure the player input isn't null
        if (_playerInput == null)
        {
            Debug.LogError("Player Input is null");
            return;
        }

        // Get the actions from the player input
        if (_playerInput.actions["Movement"] != null)
        {
            _moveAction = _playerInput.actions["Movement"];
        }

        // Get the attack action from the player input
        if (_playerInput.actions["Attack"] != null)
        {
            _attackAction = _playerInput.actions["Attack"];
        }

        // Get the dash action from the player input
        if (_playerInput.actions["Dash"] != null)
        {
            _dashAction = _playerInput.actions["Dash"];
        }

        // Get the menu action from the player input
        if (_playerInput.actions["Menu"] != null)
        {
            _menuAction = _playerInput.actions["Menu"];
        }
    }

    /// <summary>
    /// Called on startup, subscribes to all the events for the player input controller
    /// as well as any events that need to be subscribed to elsewhere.
    /// </summary>
    protected virtual void SubscribeToStartupEvents()
    {
        // In the docs, you want to pass in both a performed and a cancelled event for context
        // Sub to the move events
        if (_moveAction != null)
        {
            _moveAction.performed += MoveMethod;
            _moveAction.canceled += MoveMethod;
        }

        // For toggleable actions, you just want to pass in one method to one, and either another to another or don't need to check for cancelled
        // Sub to the attack events
        if (_attackAction != null)
        {
            _attackAction.performed += AttackMethod;
        }

        // Sub to the dash events
        if (_dashAction != null)
        {
            _dashAction.performed += DashMethod;
            _dashAction.canceled += EndDashMethod;
        }

        // Sub to the menu events
        if (_menuAction != null)
        {
            _menuAction.performed += MenuMethod;
        }
    }

    /// <summary>
    /// Called when the object is destroyed, unsubscribes from all events
    /// </summary>
    protected virtual void OnDestroy()
    {
        // Ensure the player input isn't null
        if (_playerInput == null)
        {
            Debug.LogError("Player Input is null");
            return;
        }

        UnsubscribeToStartupEvents();
    }

    /// <summary>
    /// Called on destroy, unsubscribes to all the events for the player input controller
    /// as well as any events that need to be subscribed to elsewhere.
    /// </summary>
    protected virtual void UnsubscribeToStartupEvents()
    {
        // Unsub to the move events
        if (_moveAction != null)
        {
            _moveAction.performed -= MoveMethod;
            _moveAction.canceled -= MoveMethod;
        }

        // Unsub to the attack events
        if (_attackAction != null)
        {
            _attackAction.performed -= AttackMethod;
        }

        // Unsub to the dash events
        if (_dashAction != null)
        {
            _dashAction.performed -= DashMethod;
            _dashAction.canceled -= EndDashMethod;
        }

        // Unsub to the menu events
        if (_menuAction != null)
        {
            _menuAction.performed -= MenuMethod;
        }
    }

    #endregion

    #region Input Methods 

    /// <summary>
    /// This is the method called when the player moves
    /// </summary>
    /// <param name="context"></param>
    protected virtual void MoveMethod(InputAction.CallbackContext context)
    {
        // Update the move vector
        _moveVector = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// Method called when the attack button is pressed to trigger an attack
    /// </summary>
    /// <param name="context"></param>
    protected virtual void AttackMethod(InputAction.CallbackContext context)
    {
        // Attack method
    }

    /// <summary>
    /// Method called when the dash button is pressed
    /// </summary>
    /// <param name="context"></param>
    protected virtual void DashMethod(InputAction.CallbackContext context)
    {
        // Update the dashing boolean
        _isDashing = true;
    }

    /// <summary>
    /// Method called when the dash button is no longer pressed
    /// </summary>
    /// <param name="context"></param>
    protected virtual void EndDashMethod(InputAction.CallbackContext context)
    {
        // Update the dashing boolean
        _isDashing = false;
    }

    /// <summary>
    /// Method called when the menu button is pressed to pause the game
    /// and open the menu
    /// </summary>
    /// <param name="context"></param>
    protected virtual void MenuMethod(InputAction.CallbackContext context)
    {
        // Menu method
    }

    #endregion

    #region Movement Methods

    /// <summary>
    /// Fixed update is used for physics calculations with Rigidbody2D
    /// </summary>
    protected virtual void FixedUpdate()
    {
        MovePlayer();
    }

    /// <summary>
    /// This is the method that will be called to move the player
    /// </summary>
    protected virtual void MovePlayer()
    {
        // Only call this if the rigidbody is not null
        if (_rb2d == null)
        {
            Debug.LogError("Rigidbody2D is null");
            return;
        }

        // Move the player based on whether the player is dashing
        if (!_isDashing)
        {
            _rb2d.linearVelocity = _moveVector * _speed;
        }
        else
        {
            _rb2d.linearVelocity = _moveVector * _dashSpeed;
        }
    }

    #endregion

    #endregion
}
