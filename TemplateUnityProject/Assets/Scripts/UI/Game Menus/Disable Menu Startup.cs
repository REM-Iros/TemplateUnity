using UnityEngine;

/// <summary>
/// This script is used to hide a pause menu after the game starts. 
/// 
/// REM-i
/// </summary>
public class DisableMenuStartup : MonoBehaviour
{
    [Tooltip("This is the menu that will be hidden on startup.")]
    [SerializeField, Header("Menu GameObject")]
    private GameObject _menuToHide;

    /// <summary>
    /// On awake, we want to disable the menu so that it does not show up at the start of the game.
    /// </summary>
    private void Awake()
    {
        _menuToHide.SetActive(false);
    }
}
