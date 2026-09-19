using UnityEngine;
using UnityEngine.UI;
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

    [Tooltip("This is the initial selectable item for the menu.")]
    protected abstract GameObject _initialSelectable { get; }

    /// <summary>
    /// Set the current selected game object to the initial selectable item when the menu is entered.
    /// </summary>
    public void OnEnter()
    {
        EventSystem.current.SetSelectedGameObject(_initialSelectable);
    }

    /// <summary>
    /// Clear the event system's current selected game object when the menu is exited.
    /// </summary>
    public void OnExit()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }

    /// <summary>
    /// This method is called when the player navigates the menu, it piggybacks off of the eventsystem to handle input and menu navigation. 
    /// It is passed a Vector2 representing the direction of movement.
    /// </summary>
    /// <param name="move"></param>
    public void OnNavigate(Vector2 move)
    {
        // Get the current selected game object from the event system
        Selectable currSelectable = EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>();

        // If the current selected game object is null, log a warning and return
        if (currSelectable == null)
        {
            Debug.LogWarning("Current selected game object is null, cannot navigate.");
            return;
        }

        // Parse movement vector to determine direction
        Selectable nextSelectable = move.y > 0.5f ? currSelectable.FindSelectableOnUp() :
                                    move.y < -0.5f ? currSelectable.FindSelectableOnDown() :
                                    move.x > 0.5f ? currSelectable.FindSelectableOnRight() :
                                    move.x < -0.5f ? currSelectable.FindSelectableOnLeft() :
                                    null;

        // If the next selectable is not null, set it as the current selected game object
        if (nextSelectable != null)
        {
            EventSystem.current.SetSelectedGameObject(nextSelectable.gameObject);
        }
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
