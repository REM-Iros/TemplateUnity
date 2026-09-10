using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// This is a basic menu context interface that inherits from IInputContext and IMenuNavigableContext. It should be used for
/// any context that is a menu context and needs to handle input and menu navigation. It piggybacks off of the eventsystem to handle input and menu navigation. This
/// gets passed in to the Input Router when the menu is needed.
/// 
/// REM-i
/// </summary>
public abstract class IBasicMenuContext : IInputContext, IMenuNavigableContext
{
    [Tooltip("This is the action map name for the menu context.")]
    private const string _actionMapName = "UI";

    public void OnEnter()
    {
        // IDK 100% what I want to do here yet.
    }

    public void OnExit()
    {
        // IDK 100% what I want to do here yet.
    }

    /// <summary>
    /// This method is called when the player navigates the menu, it piggybacks off of the eventsystem to handle input and menu navigation. 
    /// It is passed a Vector2 representing the direction of movement.
    /// </summary>
    /// <param name="move"></param>
    public void OnNavigate(Vector2 move)
    {
        // Get the current selected game object and store it

        // Cancel if its null

        // Parse the move vector to determine the direction of movement and grab selectable components in that direction


    }

    /// <summary>
    /// This method is called when the player confirms a selection in the menu.
    /// </summary>
    public void OnConfirm()
    {
        
    }

    /// <summary>
    /// This method is called when the player cancels a selection in the menu.
    /// </summary>
    public void OnCancel()
    {
        
    }
}
