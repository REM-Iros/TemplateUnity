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

    [Tooltip("This is the InputActionAsset we want to refer to")]
    [SerializeField]
    private InputActionAsset _inputActions;

    [Tooltip("This is the action map name that we want to reset")]
    [SerializeField]
    private string _map;

    [Tooltip("This is the control scheme that will be reset")]
    [SerializeField]
    private string _targetControlScheme;

    #endregion

    #region Methods

    /// <summary>
    /// On the button being pressed, we call the event.
    /// </summary>
    protected override void OnButtonPressed()
    {
        // Go through each action map
        foreach (InputActionMap actionMap in _inputActions.actionMaps)
        {
            // If the action map represents the one we want to reset
            if (actionMap.name == _map)
            {
                // Run through each action and reset the binding if it matches the control scheme
                foreach (InputAction action in actionMap)
                {
                    // This resets all of the input actions for the map that is masked
                    action.RemoveBindingOverride(InputBinding.MaskByGroup(_targetControlScheme));
                }
            }
        }
    }

    #endregion

}
