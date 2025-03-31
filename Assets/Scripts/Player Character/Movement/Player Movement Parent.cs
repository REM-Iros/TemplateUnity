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

    [Tooltip("This is the rigidbody we need for movement.")]
    [SerializeField]
    protected Rigidbody2D _rb2d;

    // This is the 2D vector that movement will be passed into
    protected Vector2 _moveVector;

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

    /// <summary>
    /// On awake, we need to subscribe to all the C# events of the player script
    /// </summary>
    protected virtual void Awake()
    {
        InitializeInputActions();
        SubscribeToStartupEvents();
    }

    /// <summary>
    /// Called on startup, this initializes all the input actions for the script
    /// for easier reference
    /// </summary>
    protected virtual void InitializeInputActions()
    {
        _moveAction = _playerInput.actions["Movement"];
        _attackAction = _playerInput.actions["Attack"];
        _dashAction = _playerInput.actions["Dash"];
        _menuAction = _playerInput.actions["Menu"];
    }

    /// <summary>
    /// Called on startup, subscribes to all the events for the player input controller
    /// as well as any events that need to be subscribed to elsewhere.
    /// </summary>
    protected virtual void SubscribeToStartupEvents()
    {
        // In the docs, you want to pass in both a performed and a cancelled event for context
        // moveAction.performed += MoveMethod;
        // moveAction.canceled += MoveMethod;

        // For toggleable actions, you just want to pass in one method to one, and either another to another or don't need to check for cancelled
        // _attackAction.performed += AttackMethod;

        // dashAction.performed += DashMethod;
        // dashAction.canceled += DashMethod;

        // _menuAction.performed += MenuMethod;
    }

    /// <summary>
    /// Called when the object is destroyed, unsubscribes from all events
    /// </summary>
    protected virtual void OnDestroy()
    {
        UnsubscribeToStartupEvents();
    }

    /// <summary>
    /// Called on destroy, unsubscribes to all the events for the player input controller
    /// as well as any events that need to be subscribed to elsewhere.
    /// </summary>
    protected virtual void UnsubscribeToStartupEvents()
    {
        // moveAction.performed -= MoveMethod;
        // moveAction.canceled -= MoveMethod;

        // _attackAction.performed -= AttackMethod;

        // dashAction.performed -= DashMethod;
        // dashAction.canceled -= DashMethod;

        // _menuAction.performed -= MenuMethod;
    }

    #endregion
}
