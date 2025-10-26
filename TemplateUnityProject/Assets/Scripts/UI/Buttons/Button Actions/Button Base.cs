using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This is a basic button template that avoids editor work
/// where you can define custom button scripts that override the
/// OnButtonPressed method to perform events that it needs to. This
/// hopefully saves some time working with buttons so you don't need
/// to change the same button multiple times.
/// 
/// REM-i
/// </summary>
public class ButtonBase : MonoBehaviour
{
    #region Vars

    // Store the button variable
    private Button _button;

    #endregion

    #region Methods

    /// <summary>
    /// On awake, we store the button component and add the button press listener
    /// </summary>
    protected virtual void Awake()
    {
        // Check for the button
        if (!TryGetComponent<Button>(out _button))
        {
            return;
        }

        _button.onClick.AddListener(OnButtonPressed);
    }

    /// <summary>
    /// On button pressed performs a specific event
    /// </summary>
    protected virtual void OnButtonPressed()
    {
        Debug.Log("Button Pressed event from Button Base");
    }

    /// <summary>
    /// On destroy cleans up the event references
    /// </summary>
    private void OnDestroy()
    {
        // Remove the listener when object destroyed
        if (_button != null)
        {
            _button.onClick.RemoveListener(OnButtonPressed);
        }
    }

    #endregion
}
