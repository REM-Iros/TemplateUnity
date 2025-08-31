using UnityEngine;

/// <summary>
/// This script is a helper attached to each menu that updates the default obj of each
/// menu so we don't lose focus.
/// 
/// REM-i
/// </summary>
public class MenuUpdateFocus : MonoBehaviour
{
    [Tooltip("Default Gameobject to go to if we lose focus")]
    [SerializeField, Header("Default GameObject")]
    private GameObject _defaultObj;

    /// <summary>
    /// On enable, we want to subscribe the default object to the event focuser
    /// </summary>
    private void OnEnable()
    {
        ServiceLocator.Get<UIFocusManager>().SetDefaultObj(_defaultObj);
    }
}
