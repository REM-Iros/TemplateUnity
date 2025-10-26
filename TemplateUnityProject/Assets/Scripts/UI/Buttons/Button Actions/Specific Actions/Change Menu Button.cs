using UnityEngine;
using UnityEngine.EventSystems;

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

    // Pass the the pointed to obj for the event system
    private EventSystem _eventSys;

    [SerializeField]
    private GameObject _nextObj;

    #endregion

    /// <summary>
    /// Find the event system using the button
    /// </summary>
    protected override void Awake()
    {
        base.Awake();

        _eventSys = FindAnyObjectByType<EventSystem>();
    }

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
            _nextMenu.SetActive(true);
        }

        // Set the next gameobj in the event system
        if (_eventSys != null && _nextObj != null)
        {
            _eventSys.SetSelectedGameObject(_nextObj);
        }
    }
}
