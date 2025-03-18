using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This is a basic script that swaps visibility of game objects
/// based on which input device is being used. If M&K, use M&K, and
/// if Gamepad, then gamepad. Hides the objects 
/// 
/// REM-i
/// </summary>
public class SwapInputDeviceUI : MonoBehaviour
{
    #region Vars

    [Tooltip("This is the gameobject that shows the keyboard ui")]
    [SerializeField]
    private GameObject _kbmUI;

    [Tooltip("This is the gameobject that shows the gamepad ui")]
    [SerializeField]
    private GameObject _gamePadUI;

    [Tooltip("This is the action reference for the any key pressed action")]
    [SerializeField]
    private InputActionReference _kbmAction;
    [Tooltip("This is the action reference for the any gamepad button pressed action")]
    [SerializeField]
    private InputActionReference _gamepadAction;

    #endregion

    #region Methods

    /// <summary>
    /// On enable, sub the two actions to the events
    /// </summary>
    private void OnEnable()
    {
        _kbmAction.action.performed += EnableKBMUI;
        _gamepadAction.action.performed += EnableGPadUI;
    }

    /// <summary>
    /// On disable, unsub the two actions
    /// </summary>
    private void OnDisable()
    {
        _kbmAction.action.performed -= EnableKBMUI;
        _gamepadAction.action.performed -= EnableGPadUI;
    }

    /// <summary>
    /// On a keyboard key being pressed, enables kbm ui and disables gamepad ui
    /// </summary>
    private void EnableKBMUI(InputAction.CallbackContext context)
    {
        ToggleAllKBMUI(true);
        ToggleAllGamepadUI(false);
    }

    /// <summary>
    /// On a gamepad button press, enables gamepad ui and disables keyboard ui
    /// </summary>
    /// <param name="context"></param>
    private void EnableGPadUI(InputAction.CallbackContext context)
    {
        ToggleAllKBMUI(false);
        ToggleAllGamepadUI(true);
    }

    /// <summary>
    /// Enable/disable all ui in the list
    /// </summary>
    /// <param name="enabled"></param>
    private void ToggleAllKBMUI(bool enabled)
    {
        _kbmUI.SetActive(enabled);
    }

    /// <summary>
    /// Enable/disable all ui in the list
    /// </summary>
    /// <param name="enabled"></param>
    private void ToggleAllGamepadUI(bool enabled)
    {
        _gamePadUI.SetActive(enabled);
    }

    #endregion
}
