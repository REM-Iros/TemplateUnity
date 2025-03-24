using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

/// <summary>
/// This is a button script that will handle rebinding controls. When it is
/// pressed, a panel will show up asking the player to rebind the control. If the player
/// presses an escape key, it stops the rebind and doesn't save. Otherwise, this will rebind
/// a control. It should also check for duplicates.
/// TODO: Finish implementation because I can't be bothered anymore right now, it's pissed me off too much. 
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

    // This var is the rebind operation to rebind the set binding
    private InputActionRebindingExtensions.RebindingOperation _rebindOp;

    #endregion

    #region UI Vars

    [Tooltip("This is the text that displays the current binding of the kbm control")]
    [SerializeField, Header("UI Variables")]
    private TextMeshProUGUI _kbmBindingText;

    [Tooltip("This is the image that displays the current binding of the gamepad control")]
    [SerializeField]
    private Image _gamePadBindingImage;

    #region Panel Vars

    [Tooltip("This is the panel gameobject that appears when we start rebinding")]
    [SerializeField]
    private GameObject _panel;

    [Tooltip("This is the text component we modify to display the binding")]
    [SerializeField]
    private TextMeshProUGUI _rebindPanelText;

    #endregion

    #endregion

    #endregion

    #region Methods

    /// <summary>
    /// When the button is pressed, we want to rebind the control if possible.
    /// </summary>
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

        // Update the UI to indicate we are ready to rebind
        if (_panel != null && _rebindPanelText != null)
        {
            _panel.SetActive(true);
            _rebindPanelText.text = "Rebinding: " + _actionRef.action.name + " (Press any key)";
        }
        

        // Perform any operations we need to before we rebind
        _rebindOp = _actionRef.action.PerformInteractiveRebinding()
            // Disallow mouse controls
            .WithControlsExcluding("<Mouse>")
            // Enable cancelling via select and escape
            .OnPotentialMatch(operation =>
            {
                if (operation.selectedControl.path == "/Keyboard/escape")
                {
                    operation.Cancel();
                    return;
                }

                // This is simply a test line
                Debug.Log(operation.selectedControl.path);

                if (operation.selectedControl.path == "/Gamepad/select")
                {
                    operation.Cancel();
                    return;
                }
            })
            // On Cancel, we need to reset the operation
            .OnCancel(operation => CancelRebinding())
            // On Complete, we need to cleanup the rebindings
            .OnComplete(operation =>
            {
                if( _panel != null )
                {
                    _panel.SetActive(false);
                }

                CleanupRebinding();
            });

        // Start the rebind
        _rebindOp.Start();
    }

    /// <summary>
    /// Call this method when the rebind is cancelled
    /// </summary>
    private void CancelRebinding()
    {
        // Disable the panel if present and cancel the rebind
        if (_rebindOp != null && _rebindOp.started)
        {
            if (_panel != null)
            {
                _panel.SetActive(false);
            }

            _rebindOp?.Cancel();

            // Reset the operation
            CleanupRebinding();
        }
    }

    /// <summary>
    /// Call this method whenever the rebind ends, either cancel or completion. 
    /// Reset the operation and enable the action.
    /// </summary>
    private void CleanupRebinding()
    {
        // Clean up the operation
        _rebindOp?.Dispose();
        _rebindOp = null;

        _actionRef.action.Enable();
    }

    /// <summary>
    /// This method exists only if the dev wants to include resetting the current
    /// input action, instead of resetting all inputs, needs to be called by another button
    /// </summary>
    public void ResetInputAction()
    {
        ////TODO: Need to get the proper binding index for each of the actions
        // _actionRef.action.RemoveBindingOverride();
    }

    #endregion
}
