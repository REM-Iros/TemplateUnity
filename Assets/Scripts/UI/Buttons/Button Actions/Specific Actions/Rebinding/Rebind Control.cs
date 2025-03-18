using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

/// <summary>
/// This is a button script that will handle rebinding controls. When it is
/// pressed, a panel will show up asking the player to rebind the control. If the player
/// presses an escape key, it stops the rebind and doesn't save. Otherwise, this will rebind
/// a control. It should also check for duplicates.
/// 
/// REM-i
/// </summary>
public class RebindControl : ButtonBase
{
    #region Vars

    #region Action Vars

    [Tooltip("This is the action to be referenced for rebinding")]
    [SerializeField, Header("Input Action Variables")]
    private InputActionReference _actionRef;

    #endregion

    #region UI Vars

    [Tooltip("This is the panel gameobject that appears when we start rebinding")]
    [SerializeField, Header("UI Variables")]
    private GameObject _panel;

    [Tooltip("This is the text component we modify to display the binding")]
    [SerializeField]
    private TextMeshProUGUI _rebindText;

    #endregion

    #endregion

    #region Methods

    protected override void OnButtonPressed()
    {
        // Check that we actually have an action and throw an error if not
        if (_actionRef == null || _actionRef.action == null)
        {
            Debug.LogError("Action references a null action, cannot rebind.");
            return;
        }

        // Disable the current action
        _actionRef.action.Disable();


    }

    #endregion
}
