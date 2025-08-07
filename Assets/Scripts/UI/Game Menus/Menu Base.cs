using UnityEngine;

/// <summary>
/// This script is the base class for all menu scripts in the game.
/// All menus should inherit from this class to ensure consistency.
/// 
/// REM-i
/// </summary>
public abstract class MenuBase : MonoBehaviour
{
    [Tooltip("This is the menu gameobject, it is necessary for this script to function.")]
    [SerializeField, Header("Menu GameObject")]
    private GameObject _menuGameObject;

    /// <summary>
    /// On opening the menu, this method will be called.
    /// </summary>
    public virtual void Open()
    {
        _menuGameObject.SetActive(true);
    }

    /// <summary>
    /// On closing the menu, this method will be called.
    /// </summary>
    public virtual void Close()
    {
        _menuGameObject.SetActive(false);

        // Set the game state to playing
        ServiceLocator.Get<GameStateManager>().SetGameState(GameState.Playing);
    }
}
