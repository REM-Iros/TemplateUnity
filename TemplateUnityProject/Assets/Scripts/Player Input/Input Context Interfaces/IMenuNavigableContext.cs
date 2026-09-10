using UnityEngine;

/// <summary>
/// This interface is used to mark a context as a menu navigable context.
/// </summary>
public interface IMenuNavigableContext
{
    /// <summary>
    /// This method is called when the player navigates the menu, it is passed a Vector2 representing the direction of movement.
    /// </summary>
    /// <param name="move"></param>
    void OnNavigate(Vector2 move);

    /// <summary>
    /// This method is called when the player confirms a selection in the menu.
    /// </summary>
    void OnConfirm();

    /// <summary>
    /// This method is called when the player cancels a selection in the menu.
    /// </summary>
    void OnCancel();
}
