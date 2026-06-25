using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the system that coordinates actions between actors using their time coordinators. This helps
/// determine which actor acts at the time.
/// 
/// REM-i
/// </summary>
public class RPGTimeSystemManager : MonoBehaviour
{
    #region Vars

    [Tooltip("This is the queue of player actors that are ready to attack.")]
    private List<RPGActor> _playerPriorityList;

    [Tooltip("This is the queue of enemy actors that are ready to attack.")]
    private List<RPGActor> _enemyPriorityList;

    [Tooltip("This is the event that fires when the player actor is ready.")]
    public event Action<RPGActor> OnPlayerActorTurnStarted;

    #endregion

    #region Methods

    /// <summary>
    /// Initialize the queues.
    /// </summary>
    public void InitializeQueues()
    {        
        _playerPriorityList = new List<RPGActor>();
        _enemyPriorityList = new List<RPGActor>();
    }

    /// <summary>
    /// Subscribes the time system to actor events.
    /// </summary>
    /// <param name="actor"></param>
    public void SubscribeToActorEvents(RPGActor actor, bool isPlayerActor)
    {
        // Subscribes to the component's events that correlate with actions.
        if (isPlayerActor)
        {
            actor.TimeCoordinator.OnCanAct += SubscribePlayerActor;
            // actor.ActionCoordinator.OnActionFinish += UnsubscribePlayerActor;
            actor.HealthCoordinator.OnKO += UnsubscribePlayerActor;
        }
        else
        {
            actor.TimeCoordinator.OnCanAct += SubscribeEnemyActor;
            // actor.ActionCoordinator.OnActionFinish += UnsubscribeEnemyActor;
            actor.HealthCoordinator.OnKO += UnsubscribeEnemyActor;
        }
    }

    /// <summary>
    /// Adds the actor passed in from the event to the player queue. 
    /// </summary>
    /// <param name="actor"></param>
    private void SubscribePlayerActor(RPGActor actor)
    {
        _playerPriorityList.Add(actor);

        ActivateTopMenu();
    }

    /// <summary>
    /// Called when a player actor is ready, and also when a player actor finishes their action. Checks priority list and activates the top actors menu.
    /// </summary>
    private void ActivateTopMenu()
    {
        // Only needs to run if we have an actor ready to go. If we don't have any actors ready, then we don't need to do anything.
        // This is more of a safety check.
        if (_playerPriorityList.Count <= 0)
        {
            return;
        }

        OnPlayerActorTurnStarted?.Invoke(_playerPriorityList[0]);
    }

    /// <summary>
    /// Removes the specified actor from the player queue.
    /// </summary>
    /// <param name="actor"></param>
    private void UnsubscribePlayerActor(RPGActor actor)
    {
        if (!_playerPriorityList.Contains(actor))
        {
            return;
        }

        _playerPriorityList.Remove(actor);

        ActivateTopMenu();
    }

    /// <summary>
    /// Adds the actor passed in from the event to the enemy queue. 
    /// </summary>
    /// <param name="actor"></param>
    private void SubscribeEnemyActor(RPGActor actor)
    {
        _enemyPriorityList.Add(actor);
    }

    /// <summary>
    /// Removes the specified actor from the enemy queue.
    /// </summary>
    /// <param name="actor"></param>
    private void UnsubscribeEnemyActor(RPGActor actor)
    {
        if (!_enemyPriorityList.Contains(actor))
        {
            return;
        }
        
        _enemyPriorityList.Remove(actor);
    }

    #endregion
}
