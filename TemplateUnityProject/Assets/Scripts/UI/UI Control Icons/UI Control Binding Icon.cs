using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/// <summary>
/// This is a component image class that updates it's UI based on the action assigned to it. It is used for any UI element that needs to update based on the button
/// assigned to it, which may also change based on the input scheme or if a button is rebound.
/// 
/// REM-i
/// </summary>
public class UIBindingIcon : MonoBehaviour
{
    #region Vars

    [Tooltip("This is the action reference for the input action that this UI element is displaying.")]
    [SerializeField]
    private InputActionReference _actionReference;

    [Tooltip("This is the image that will be displayed for the input action.")]
    [SerializeField]
    private Image _actionImage;


    private InputAction _action;

    #endregion

    #region Methods

    /// <summary>
    /// Awake validates if action image is present.
    /// </summary>
    private void Awake()
    {
        if (_actionImage == null)
        {
            Debug.LogError("Action image is not assigned in the inspector.");
            return;
        }
    }

    /// <summary>
    /// This method is called by UI to update the UI element with the correct icon for the correct input action and device.
    /// </summary>
    /// <param name="input"></param>
    public void UpdateUI(PlayerInput input)
    {
        _action ??= _actionReference.action;

        if (_action == null)
        {
            Debug.LogWarning($"Action with name {_actionReference.action.name} not found in PlayerInput actions.");
            return;
        }

        string controlScheme = input.currentControlScheme;

        int bindingIndex = _action.GetBindingIndex(InputBinding.MaskByGroup(controlScheme));

        if (bindingIndex == -1)
        {
            Debug.LogWarning($"No binding found for action {_action.name} in control scheme {controlScheme}.");
            return;
        }

        var binding = _action.bindings[bindingIndex];

        if (binding.isComposite)
        {
            return;
        }

        string controlPath = binding.effectivePath;

        var sprite = UIControlIconDatabase.Instance.GetIcon(controlScheme, controlPath);

        if (sprite == null)
        {
            Debug.LogWarning($"No icon found for control scheme {controlScheme} and control path {controlPath}.");
        }

        _actionImage.sprite = sprite;
    }

    #endregion
}
