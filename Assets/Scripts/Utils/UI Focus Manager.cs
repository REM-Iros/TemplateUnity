using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// This is a fix to the event system that prevents the event system from losing
/// focus on changes to the current input device. I'm throwing it into the service locator
/// because it seems to be the most scalable solution to this problem.
/// 
/// REM-i
/// </summary>
public class UIFocusManager : EagerSingleton<UIFocusManager>
{
    // Event System reference
    private EventSystem _currEventSystem;

    // Default gameobject
    private GameObject _defaultObj;

    // Last selected gameobject
    private GameObject _lastSelectedObj;

    #region Methods

    /// <summary>
    /// Attaches the event system to this device
    /// </summary>
    /// <param name="es"></param>
    public void AttachEventSystem(EventSystem es, GameObject defaultObj)
    {
        // Set event system
        _currEventSystem = es;

        // Set the default obj
        _defaultObj = defaultObj;

        // If the default obj isn't null, we set the selected game object to the default
        if (_defaultObj == null)
        {
            return;
        }

        _currEventSystem.SetSelectedGameObject(_defaultObj);
    }

    /// <summary>
    /// Called on menu transitions to set the default
    /// </summary>
    /// <param name="defaultObj"></param>
    public void SetDefaultObj(GameObject defaultObj)
    {
        _defaultObj = defaultObj;

        // Only run this if we have an event system
        if (_currEventSystem == null)
        {
            return;
        }

        // Set the selected obj
        _currEventSystem.SetSelectedGameObject(defaultObj);
    }

    /// <summary>
    /// We need to check for if focus is lost on the event system and if it does,
    /// reset the focus.
    /// </summary>
    private void Update()
    {
        // Only run the current event system
        if (_currEventSystem == null)
        {
            return;
        }

        // Only set the last selected obj if the current obj isn't null and isn't already picked out
        if (_currEventSystem.currentSelectedGameObject != null 
            && _currEventSystem.currentSelectedGameObject != _lastSelectedObj)
        {
            _lastSelectedObj = _currEventSystem.currentSelectedGameObject;
        }

        // If we lose focus, we need to refocus
        if (_currEventSystem.currentSelectedGameObject == null)
        {
            _currEventSystem.SetSelectedGameObject(_lastSelectedObj ?? _defaultObj);
        }
    }

    #endregion
}
