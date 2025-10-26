using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is responsible for defining game events in something like an RPG game.
/// It should store a list of event actions, and be used by an EventController.
/// 
/// REM-i
/// </summary>
[CreateAssetMenu(menuName = "Event")]
public class GameEvent : ScriptableObject
{
    // List of actions that make up this event
    public List<EventAction> actions;

    /// <summary>
    /// This is called by the event controller to play the events.
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public IEnumerator Play(EventContext context)
    {
        foreach (var action in actions)
        {
            yield return action.Execute(context);
        }
    }
}
