using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the system that coordinates actions between actors using their time coordinators. This helps
/// determine which actor acts at the time.
/// 
/// REM-i
/// </summary>
public class RPGTimeSystemCoordinator : MonoBehaviour
{
    #region Vars

    [Tooltip("This is the list of player actors that are ready to attack.")]
    private List<RPGActor> _playerPriorityList;

    [Tooltip("This is the list of enemy actors that are ready to attack.")]
    private List<RPGActor> _enemyPriorityList;

    [Tooltip("This is the event that fires when the player actor is ready.")]
    public event Action<RPGActor> NotifyPlayerActorUI;

    #endregion

    #region Methods

    /// <summary>
    /// Subscribes the time system to actor events.
    /// </summary>
    /// <param name="actor"></param>
    public void SubscribeToActorEvents(RPGActor actor, bool isPlayerActor)
    {
        actor.OnActorActionAvailable += (isPlayerActor) ? SubscribePlayerActor : SubscribeEnemyActor;
        actor.OnActorActionFinish += (isPlayerActor) ? SubscribePlayerActor : SubscribeEnemyActor;
        actor.OnActorKO += (isPlayerActor) ? UnsubscribePlayerActor : UnsubscribeEnemyActor;
    }

    /// <summary>
    /// Adds the actor passed in from the event to the player list. 
    /// </summary>
    /// <param name="actor"></param>
    private void SubscribePlayerActor(RPGActor actor)
    {
        _playerPriorityList.Add(actor);

        ActivateTopMenu();
    }

    /// <summary>
    /// Called when a player actor is ready.
    /// </summary>
    private void ActivateTopMenu()
    {
        if (_playerPriorityList.Count <= 0)
        {
            return;
        }


    }

    /// <summary>
    /// Removes the specified actor from the player list.
    /// </summary>
    /// <param name="actor"></param>
    private void UnsubscribePlayerActor(RPGActor actor)
    {
        if (!_playerPriorityList.Contains(actor))
        {
            return;
        }

        _playerPriorityList.Remove(actor);
    }

    /// <summary>
    /// Adds the actor passed in from the event to the player list. 
    /// </summary>
    /// <param name="actor"></param>
    private void SubscribeEnemyActor(RPGActor actor)
    {
        _enemyPriorityList.Add(actor);
    }

    /// <summary>
    /// Removes the specified actor from the player list.
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
