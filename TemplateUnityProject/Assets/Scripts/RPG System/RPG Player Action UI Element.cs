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

    // Store the player input for action icon changing based on control schemes
    private PlayerInput _playerInput;
    private string _currentControlScheme;

    // Fields for UI components
    [Tooltip("The text object to display the action name")]
    [SerializeField, Header("UI Element Components")] 
    private TextMeshProUGUI _actionNameText;

    [Tooltip("The control icon to display")]
    [SerializeField]
    private Image _actionControlIcon;

    //Store the sprites used for each control scheme
    [Tooltip("This sprite is for KBM controls")]
    [SerializeField, Header("Control Scheme Sprites")]
    private Sprite _keyboardMouseIcon;

    [Tooltip("This sprite is for PS controls")]
    [SerializeField]
    private Sprite _psIcon;

    [Tooltip("This sprite is for Xbox controls and default if gamepad is detected but not ps or xbox")]
    [SerializeField]
    private Sprite _xboxIcon;

    #endregion

    #region Methods

    /// <summary>
    /// Store reference to Player Input for control scheme detection on initialization.
    /// </summary>
    /// <param name="playerInput"></param>
    public void Init(PlayerInput playerInput)
    {
         _playerInput = playerInput;
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
    /// Band-aid method for updating the control icon based on current control scheme. This should
    /// be done through an input manager in the future, but for now this will suffice.
    /// </summary>
    private void Update()
    {
        if (_playerInput == null)
        {
            return;
        }
        
        // If the scheme has changed, update the icons
        if (_currentControlScheme == _playerInput.currentControlScheme)
        {
            return;
        }
    
        // Update the current control scheme
        _currentControlScheme = _playerInput.currentControlScheme;

        // Update icon based on control scheme
        switch (_currentControlScheme)
        {
            case "Keyboard&Mouse":
                _actionControlIcon.sprite = _keyboardMouseIcon;
                break;
            case "PS Controller":
                _actionControlIcon.sprite = _psIcon;
                break;
            default:
                _actionControlIcon.sprite = _xboxIcon;
                break;
        }

    }

    #endregion
}
