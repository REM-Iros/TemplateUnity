using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// When this button is pressed, resets all the bindings for the specified map.
/// 
/// REM-i
/// </summary>
public class ResetRebindMap : ButtonBase
{
    #region Vars

    // Store the current input action assets
    [SerializeField]
    private InputActionAsset _inputActions;

    // This is the control scheme that will be reset
    [SerializeField]
    private string _targetControlScheme;

    #endregion

    #region Methods

    /// <summary>
    /// On the button being pressed, we call the event.
    /// </summary>
    protected override void OnButtonPressed()
    {
        foreach (InputActionMap map in _inputActions.actionMaps)
        {
            foreach (InputAction action in map.actions)
            {
                // This resets all of the input actions for the map that is masked
                action.RemoveBindingOverride(InputBinding.MaskByGroup(_targetControlScheme));
            }
        }
    }

    #endregion

}
