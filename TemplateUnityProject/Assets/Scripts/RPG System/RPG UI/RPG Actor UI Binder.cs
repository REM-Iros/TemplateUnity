using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// This is the binder script that passes in an actor and binds UI components to each event.
/// 
/// REM-i
/// </summary>
public class RPGActorUIBinder : MonoBehaviour
{
    #region Vars

    [Tooltip("This is the actor to bind the ui to. Passed in by UI Manager.")]
    private RPGActor _actor;

    [Tooltip("This is the list of UI Interface components on the prefab.")]
    private List<RPGIUIInterface> _uiComponents = new List<RPGIUIInterface>();

    #endregion

    #region Methods

    /// <summary>
    /// This gets the actor passed in from the UI Manager and binds all UI on the prefab to the actor.
    /// </summary>
    /// <param name="actor"></param>
    public void Bind(RPGActor actor)
    {
        // Bind the actor
        _actor = actor;

        // Get all the components on this gameobject that use IUIInterface and cast to list
        _uiComponents = GetComponentsInChildren<RPGIUIInterface>().ToList();

        foreach (RPGIUIInterface component in _uiComponents)
        {
            component.Bind(_actor);
        }
    }
    
    /// <summary>
    /// This unbinds the actor from all ui events.
    /// </summary>
    public void Unbind()
    {
        _actor = null;

        foreach (RPGIUIInterface component in _uiComponents)
        {
            component.Unbind();
        }
    }

    #endregion
}
