using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script functionally just updates the UI if the actor is targeted.
/// 
/// REM-i
/// </summary>
public class RPGActorTargetUI : MonoBehaviour, RPGIUIInterface
{
    #region Vars

    [Tooltip("This is the actor we are bound to.")]
    private RPGActor _actor;

    [Tooltip("This is the targeting gameobject that appears above the actor.")]
    [SerializeField, Header("Target Object")]
    private GameObject _targetObj;

    [Tooltip("This is the event that the target ui subscribes to.")]
    [SerializeField, Header("Events")]
    private RPGTargetEvent targetEvent;

    [Tooltip("This is the untarget event we want to subscribe to.")]
    [SerializeField]
    private RPGUntargetEvent untargetEvent;

    #endregion

    #region Methods

    /// <summary>
    /// Bind this ui to the actor, and move this object to that position.
    /// </summary>
    /// <param name="actor"></param>
    public void Bind(RPGActor actor)
    {
        // Bind the actor
        _actor = actor;

        // Set this UI's position to the target transform.
        _targetObj.transform.position = actor.ActorTargetTransform.position;

        // Subscribe to the events
        targetEvent.OnEventRaised += ActivateTarget;
        untargetEvent.OnEventRaised += DeactivateTarget;
    }

    /// <summary>
    /// Unbind the ui to the actor and unsubscribe from the event.
    /// </summary>
    public void Unbind()
    {
        // Unbind the actor
        _actor = null;

        // Unsub from the event
        targetEvent.OnEventRaised -= ActivateTarget;
        untargetEvent.OnEventRaised -= DeactivateTarget;
    }

    /// <summary>
    /// This method is called by the event and sets the object to activate if it's in the list of valuable targets.
    /// </summary>
    /// <param name="targets"></param>
    public void ActivateTarget(IReadOnlyList<RPGActor> targets)
    {
        // Run through each actor and if it matches this actor, activate the target.
        foreach (RPGActor actor in targets)
        {
            if (actor == _actor)
            {
                _targetObj?.SetActive(true);
            }
        }
    }

    /// <summary>
    /// This method is called by the event and sets the object to deactivate.
    /// </summary>
    /// <param name="targets"></param>
    public void DeactivateTarget()
    {
        _targetObj?.SetActive(false);
    }

    /// <summary>
    /// We need this method here as a fallback in case of the scene ending. Because we are using a
    /// SO event, it won't clear itself up until you manually do it.
    /// </summary>
    public void OnDestroy()
    {
        Unbind();
    }

    #endregion
}
