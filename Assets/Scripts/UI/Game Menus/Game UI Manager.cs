using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script handles the UI menus during gameplay. Since menus are generally handled with event systems,
/// this really only needs to handle the UI elements that are not part of the main menu.
/// 
/// REM-i
/// </summary>
public class GameUIManager : MonoBehaviour
{
    #region Vars

    [Tooltip("This is the player input controller, it is necessary for this script to function.")]
    [SerializeField, Header("Input Controller")]
    private PlayerInputController _inputController;

    private Dictionary<string, MenuBase> _menus = new Dictionary<string, MenuBase>();

    #endregion

    #region Methods

    /// <summary>
    /// This method initializes the GameUIManager and sets up the menu dictionary.
    /// </summary>
    private void Awake()
    {
        // Check if the input controller is assigned, if not, find it in the scene.
        if (_inputController == null)
        {
            _inputController = FindAnyObjectByType<PlayerInputController>();
            
            if (_inputController == null)
            {
                Debug.LogError("No Input controller found, script will NOT work!");
                return;
            }
        }

        // Register all menus in this manager.
        RegisterAllMenus();

        // Subscribe to the input controller events
        InitActions();
    }

    /// <summary>
    /// This method registers all menus that inherit menubase in the GameUIManager.
    /// </summary>
    private void RegisterAllMenus()
    {
        // Find all MenuBase components that are children and register them.
        MenuBase[] menus = GetComponentsInChildren<MenuBase>(true);

        // Iterate through each menu and add it to the dictionary.
        foreach (var menu in menus)
        {
            // Store the menu in the dictionary using its type name as the key.
            string key = menu.GetType().Name;

            // Check if the key already exists in the dictionary to avoid duplicates.
            if (!_menus.ContainsKey(key))
            {
                _menus.Add(key, menu);
            }
            else
            {
                Debug.LogWarning($"Menu with key {key} already exists in the dictionary. Skipping registration.");
            }
        }
        
    }

    /// <summary>
    /// This method initializes the input actions for the game UI manager.
    /// </summary>
    private void InitActions()
    {
        _inputController.RegisterMenuPerformed(OpenPauseMenu);
    }

    /// <summary>
    /// This method toggles the pause menu on.
    /// </summary>
    private void OpenPauseMenu()
    {
        // Check if the pause menu is registered in the dictionary.
        if (!_menus.TryGetValue(nameof(PauseMenu), out MenuBase pauseMenu))
        {
            Debug.LogError("PauseMenu is not registered in the GameUIManager.");
            return;
        }

        pauseMenu.Open();
    }

    /// <summary>
    /// This method closes the pause menu if it is currently open.
    /// </summary>
    public void ClosePauseMenu()
    {
        // Check if the pause menu is registered in the dictionary.
        if (!_menus.TryGetValue(nameof(PauseMenu), out MenuBase pauseMenu))
        {
            Debug.LogError("PauseMenu is not registered in the GameUIManager.");
            return;
        }

        pauseMenu.Close();
    }

    #endregion
}
