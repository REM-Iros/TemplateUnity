using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using Unity.VisualScripting;

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

    // Store the parent objs of gamepad and mk ui
    [SerializeField]
    private GameObject[] _kbmUI;
    [SerializeField]
    private GameObject[] _gamePadUI;

    // Store kbm action and gamepad actions for when something is pressed
    [SerializeField]
    private InputActionReference _kbmAction;
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
        foreach (var item in _kbmUI)
        {
            item.SetActive(enabled);
        }
    }

    /// <summary>
    /// Enable/disable all ui in the list
    /// </summary>
    /// <param name="enabled"></param>
    private void ToggleAllGamepadUI(bool enabled)
    {
        foreach (var item in _gamePadUI)
        {
            item.SetActive(enabled);
        }
    }

    #endregion
}
