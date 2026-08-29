using System;
using UnityEngine;

/// <summary>
/// The action manager handles resolving actions for both the player and the enemy.
/// 
/// REM-i
/// </summary>
public class RPGActionManager : MonoBehaviour
{
    #region Vars

    [Tooltip("This is the current stored player actor for when an action is triggered.")]
    public RPGActor _currPlayerActor;

    [Tooltip("This is the current stored enemy actor for when an action is triggered.")]
    public RPGActor _currEnemyActor;

    [Tooltip("This is the event that is called when the actor.")]
    public event Action<RPGActor, ActionInstance> OnActionSelectedForTargeting;

    #endregion

    #region Methods

    /// <summary>
    /// Called when the Targeting Manager has an actor ready, passes in the current player actor.
    /// </summary>
    /// <param name="actor"></param>
    public void GetCurrentPlayerActor(RPGActor actor)
    {
        _currPlayerActor = actor;
    }
    
    /// <summary>
    /// This is called when the ui calls for an action to be used.
    /// </summary>
    /// <param name="index"></param>
    public void ActivateActionTargettingAtIndex(int index)
    {
        OnActionSelectedForTargeting.Invoke(_currPlayerActor, _currPlayerActor.ActionCoordinator.GetActionAtIndex(index));
    }

    #endregion
}
