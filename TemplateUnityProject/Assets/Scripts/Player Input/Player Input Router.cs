using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This is a refactor of the player input controller. It acts as a go between for the new Input System and other scripts,
/// and will attach itself to the player input registry to be called by other methods. The input router has a stack of input context
/// interface using scripts, and these scripts inherit smaller context interface objects that handle very standard gameplay behaviors.
/// These are fairly normal things like movement, dodging, or menu navigation, confirm, cancel. These scripts would be things like the 
/// 2D movement manager which subscribes to the stack having a movement input interface.
/// 
/// REM-i
/// </summary>
public class PlayerInputRouter : MonoBehaviour
{
    #region Vars

    [Tooltip("This is the playerinput component for use within the router.")]
    [SerializeField, Header("Player Input")]
    private PlayerInput _input;

    [Tooltip("This is the stack of input contexts in the router.")]
    private Stack<IInputContext> _contextStack;

    [Tooltip("This is the string for the current map.")]
    private string _currentInputMap;

    #endregion

    #region Methods

    /// <summary>
    /// On awake, we need to check for player input, as well as subscribe to the player input.
    /// </summary>
    private void Awake()
    {
        // Validate input is attached before waking.
        if (_input == null)
        {
            Debug.LogError("Missing player input, input router will not work.");
            return;
        }

        // Sub to the action triggered event.
        _input.onActionTriggered += HandleAction;

        // Generate the stack
        _contextStack = new Stack<IInputContext>();
    }

    /// <summary>
    /// This is the method called to handle all actions based on the current input context.
    /// </summary>
    /// <param name="context"></param>
    private void HandleAction(InputAction.CallbackContext context)
    {
        // Context stack is empty
        if (_contextStack.Count == 0)
        {
            return;
        }

        // Get the context stack at the top.
        IInputContext top = _contextStack.Peek();

        // Based on the action being performed,
        switch(context.action.name)
        {
            // TODO: Extend this for the basic contexts I will need.
            case "Movement" when top is IInputContext:
                break;
            default:
                Debug.LogError("No action corresponding to found in the menu.");
                break;
        }
    }

    /// <summary>
    /// This is the stack push method, calls exit if there is a current context
    /// on, and then enters the context being pushed in.
    /// </summary>
    /// <param name="context"></param>
    public void Push(IInputContext context)
    {
        // If there is something in the stack, we need to exit.
        if (_contextStack.Count > 0)
        {
            _contextStack.Peek().OnExit();
        }

        // Push to the stack and enter. 
        _contextStack.Push(context);
        _currentInputMap = context.ActionMapName;
        _input.SwitchCurrentActionMap(_currentInputMap);
        _contextStack.Peek().OnEnter();
    }

    /// <summary>
    /// This is the stack pop method, calls exit for the context, 
    /// and enters the next thing on the stack if it exists.
    /// </summary>
    public void Pop()
    {
        // Get the top context off the stack.
        _contextStack.Peek().OnExit();
        _contextStack.Pop();

        // Check if there is anything left in the stack.
        if (_contextStack.Count == 0)
        {
            return;
        }

        // Run the enter method
        _contextStack.Peek().OnEnter();
        _currentInputMap = _contextStack.Peek().ActionMapName;
        _input.SwitchCurrentActionMap(_currentInputMap);
    }
    
    /// <summary>
    /// This method is called to completely clear the stack, used for when you need to escape from context.
    /// This is a little early on, but I foresee needing this if you say change scenes abruptly. Maybe won't be needed,
    /// but IDK.
    /// (This doesn't trigger the context exit... tbd on if it needs to.)
    /// </summary>
    public void Clear()
    {
        // If the context stack isn't empty, we need to clear it, otherwise ignore it.
        if (_contextStack.Count == 0)
        {
            return;
        }

        _contextStack.Clear();
    }

    #endregion
}
