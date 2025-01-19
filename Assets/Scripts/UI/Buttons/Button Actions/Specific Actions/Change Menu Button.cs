using UnityEngine;

/// <summary>
/// This script closes the current menu that you have opened and
/// activate another menu.
/// 
/// REM-i
/// </summary>
public class ChangeMenuButton : ButtonBase
{
    #region Vars

    // Store the current menu and the next menu
    [SerializeField]
    private GameObject _currMenu;

    [SerializeField]
    private GameObject _nextMenu;

    #endregion

    /// <summary>
    /// When the button is pressed, close the current menu and open the next
    /// </summary>
    protected override void OnButtonPressed()
    {
        // Set the curr menu to be off
        if (_currMenu != null)
        {
            _currMenu.SetActive(false);
        }

        // Set the next menu to be on
        if (_nextMenu != null)
        {
            _nextMenu.SetActive(false);
        }
    }
}
