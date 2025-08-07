using UnityEngine;
using UnityEngine.InputSystem;
using System;

/// <summary>
/// This is a component script of the player movement parent, and is functionally
/// the most important. This takes the playerinput component found in UnityEngine.InputSystem
/// and uses the player input component to initialize actions that we can then call to from the parent
/// for it's components.
/// 
/// REM-i
/// </summary>
public class PlayerInputController : MonoBehaviour
{
    #region Vars

    [Tooltip("This is the playerinput component, it is necessary for this script to function.")]
    [SerializeField, Header("Player Input")]
    private PlayerInput _playerInput;

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
    private InputAction _jumpAction;

    #endregion

    #region Input Events

    public event Action<Vector2> OnMovePerformed;
    public event Action<Vector2> OnMoveCancelled;

    public event Action OnAttackPerformed;
    public event Action OnAttackCancelled;

    public event Action OnDashPerformed;
    public event Action OnDashCancelled;

    public event Action OnMenuPerformed;
    public event Action OnMenuCancelled;

    public event Action OnJumpPerformed;
    public event Action OnJumpCancelled;

    #endregion

    #endregion

    #region Methods

    /// <summary>
    /// On awake, we need to initialize the scripts 
    /// </summary>
    private void Awake()
    {
        Init();
    }

    /// <summary>
    /// We want to get the components that we need for the script if they aren't already defined
    /// </summary>
    private void Init()
    {
        // Prevent initialization of input actions if we don't have a player input component.
        if (_playerInput == null)
        {
            _playerInput = GetComponent<PlayerInput>();
            Debug.LogError("Playerinput not assigned, controls will NOT work!");
            return;
        }

        // Need to attach the input actions now
        InitActions();

        // Enable this to dynamically respond to game state changes
        ServiceLocator.Get<GameStateManager>().OnGameStateChanged += HandleGameStateChanged;
    }

    /// <summary>
    /// Called on startup, this initializes all the input actions for the script
    /// for easier reference
    /// </summary>
    private void InitActions()
    {
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

        // Get the jump action from the player input
        if (_playerInput.actions["Jump"] != null)
        {
            _jumpAction = _playerInput.actions["Jump"];
        }
    }

    /// <summary>
    /// This method is called when the game state changes. It updates the input action states based on game state.
    /// </summary>
    /// <param name="gameState"></param>
    private void HandleGameStateChanged(GameState gameState)
    {
        bool isGameplay = gameState == GameState.Playing;

        // Enable or disable input actions based on the game state
        SetInputActionState(_moveAction, isGameplay);
        SetInputActionState(_attackAction, isGameplay);
        SetInputActionState(_dashAction, isGameplay);
        SetInputActionState(_menuAction, isGameplay);
        SetInputActionState(_jumpAction, isGameplay);
    }

    /// <summary>
    /// This script is used to enable/disable input actions based on the game state or other conditions.
    /// </summary>
    /// <param name="action"></param>
    /// <param name="enable"></param>
    private void SetInputActionState(InputAction action, bool enable)
    {
        // Only set the state if the action is not null
        if (action == null)
        {
            Debug.LogError("Input action is null, cannot set state.");
            return;
        }

        // Enable or disable the action based on the enable parameter
        if (enable && !action.enabled)
        {
            action.Enable();
        }
        else if (!enable && action.enabled)
        {
            action.Disable();
        }
    }

    /// <summary>
    /// On enable, we want to sub the actions to the events so that
    /// the player can move.
    /// </summary>
    private void OnEnable()
    {
        // Move
        _moveAction.performed += HandleMovePerformed;
        _moveAction.canceled += HandleMoveCancelled;

        // Attack
        _attackAction.performed += HandleAttackPerformed;
        _attackAction.canceled += HandleAttackCancelled;

        // Dash
        _dashAction.performed += HandleDashPerformed;
        _dashAction.canceled += HandleDashCancelled;

        // Menu
        _menuAction.performed += HandleMenuPerformed;
        _menuAction.canceled += HandleMenuCancelled;

        // Jump
        _jumpAction.performed += HandleJumpPerformed;
        _jumpAction.canceled += HandleJumpCancelled;
    }

    /// <summary>
    /// On disable, we want to unsub the actions from the events.
    /// </summary>
    private void OnDisable()
    {
        // Move
        _moveAction.performed -= HandleMovePerformed;
        _moveAction.canceled -= HandleMoveCancelled;

        // Attack
        _attackAction.performed -= HandleAttackPerformed;
        _attackAction.canceled -= HandleAttackCancelled;

        // Dash
        _dashAction.performed -= HandleDashPerformed;
        _dashAction.canceled -= HandleDashCancelled;

        // Menu
        _menuAction.performed -= HandleMenuPerformed;
        _menuAction.canceled -= HandleMenuCancelled;

        // Jump
        _jumpAction.performed -= HandleJumpPerformed;
        _jumpAction.canceled -= HandleJumpCancelled;
    }

    #region Event Invoke Methods

    /*
     * These are the event call methods for the events tied to the input actions.
     * When, for example, the move button is pressed, move.performed will get called
     * which move.performed will then invoke HandleMovePerformed, and etc.
     */

    // Movement methods
    private void HandleMovePerformed(InputAction.CallbackContext context)
    {
        OnMovePerformed?.Invoke(context.ReadValue<Vector2>());
    }

    private void HandleMoveCancelled(InputAction.CallbackContext context)
    {
        OnMoveCancelled?.Invoke(context.ReadValue<Vector2>());
    }

    // Attack methods
    private void HandleAttackPerformed(InputAction.CallbackContext context)
    {
        OnAttackPerformed?.Invoke();
    }

    private void HandleAttackCancelled(InputAction.CallbackContext context)
    {
        OnAttackCancelled?.Invoke();
    }

    // Dash methods
    private void HandleDashPerformed(InputAction.CallbackContext context)
    {
        OnDashPerformed?.Invoke();
    }

    private void HandleDashCancelled(InputAction.CallbackContext context)
    {
        OnDashCancelled?.Invoke();
    }

    // Menu methods
    private void HandleMenuPerformed(InputAction.CallbackContext context)
    {
        OnMenuPerformed?.Invoke();
    }

    private void HandleMenuCancelled(InputAction.CallbackContext context)
    {
        OnMenuCancelled?.Invoke();
    }

    // Jump methods
    private void HandleJumpPerformed(InputAction.CallbackContext context)
    {
        OnJumpPerformed?.Invoke();
    }

    private void HandleJumpCancelled(InputAction.CallbackContext context)
    {
        OnJumpCancelled?.Invoke();
    }

    #endregion

    #region Sub/Unsub Methods

    /*
     *  All these methods are used by the Parent controller to subscribe scripts to components
     */

    //Movement action
    public void RegisterMovementPerformed(Action<Vector2> callback) => OnMovePerformed += callback;
    public void UnregisterMovementPerformed(Action<Vector2> callback) => OnMovePerformed -= callback;
    public void RegisterMovementCancelled(Action<Vector2> callback) => OnMoveCancelled += callback;
    public void UnregisterMovementCancelled(Action<Vector2> callback) => OnMoveCancelled -= callback;

    // Attack action
    public void RegisterAttackPerformed(Action callback) => OnAttackPerformed += callback;
    public void UnregisterAttackPerformed(Action callback) => OnAttackPerformed -= callback;
    public void RegisterAttackCancelled(Action callback) => OnAttackCancelled += callback;
    public void UnregisterAttackCancelled(Action callback) => OnAttackCancelled -= callback;

    // Dash action
    public void RegisterDashPerformed(Action callback) => OnDashPerformed += callback;
    public void UnregisterDashPerformed(Action callback) => OnDashPerformed -= callback;
    public void RegisterDashCancelled(Action callback) => OnDashCancelled += callback;
    public void UnregisterDashCancelled(Action callback) => OnDashCancelled -= callback;

    // Menu action
    public void RegisterMenuPerformed(Action callback) => OnMenuPerformed += callback;
    public void UnregisterMenuPerformed(Action callback) => OnMenuPerformed -= callback;
    public void RegisterMenuCancelled(Action callback) => OnMenuCancelled += callback;
    public void UnregisterMenuCancelled(Action callback) => OnMenuCancelled -= callback;

    // Jump action
    public void RegisterJumpPerformed(Action callback) => OnJumpPerformed += callback;
    public void UnregisterJumpPerformed(Action callback) => OnJumpPerformed -= callback;
    public void RegisterJumpCancelled(Action callback) => OnJumpCancelled += callback;
    public void UnregisterJumpCancelled(Action callback) => OnJumpCancelled -= callback;

    #endregion

    #endregion
}
