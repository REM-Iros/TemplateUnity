using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// This class represents a single player action UI element in the RPG bullet hell segment.
/// 
/// REM-i
/// </summary>
public class PlayerActionUIElement : MonoBehaviour
{
    #region Vars

    [Tooltip("This is the reference to the player input component for subscribing to control scheme change events.")]
    private PlayerInput _playerInput;

    [Tooltip("This is the icon that updates when the control scheme changes, used to determine which sprite to display.")]
    [SerializeField, Header("Control Scheme Icon")]
    private UIBindingIcon _updateUIInput;

    // Fields for UI components
    [Tooltip("The text object to display the action name")]
    [SerializeField, Header("UI Element Components")] 
    private TextMeshProUGUI _actionNameText;

    #endregion

    #region Methods

    /// <summary>
    /// Store reference to Player Input for control scheme detection on initialization.
    /// </summary>
    /// <param name="playerInput"></param>
    public void InitializeUIElement(PlayerInput playerInput)
    {
        _playerInput = playerInput;

        _playerInput.onControlsChanged += _updateUIInput.UpdateUI;
    }

    /// <summary>
    /// On enable, we need to update the ui.
    /// </summary>
    private void OnEnable()
    {
        if (_playerInput == null)
        {
            return;
        }

        _playerInput.onControlsChanged += _updateUIInput.UpdateUI;

        _updateUIInput.UpdateUI(_playerInput);
    }

    /// <summary>
    /// Called when the action menu is activated, updates the text of the action.
    /// </summary>
    /// <param name="actionName"></param>
    public void SetActionName(string actionName)
    {
        _actionNameText.text = actionName;
    }


    /// <summary>
    /// On destroy, unsubscribe from the control scheme change event to prevent memory leaks.
    /// </summary>
    private void OnDisable()
    {
        if (_playerInput == null)
        {
            return;
        }

        _playerInput.onControlsChanged -= _updateUIInput.UpdateUI;
    }

    #endregion
}

