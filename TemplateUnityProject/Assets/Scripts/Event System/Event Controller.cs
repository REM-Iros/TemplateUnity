using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is responsible for managing game events and interactions in something like an RPG game.
/// It handles event triggers, dialogues, and other interactive elements. Each event controller is responsible
/// for an event.
/// 
/// REM-i
/// </summary>
public class EventController : MonoBehaviour
{
    [Tooltip("This is the event to run.")]
    [SerializeField, Header("Event Scriptable Object")]
    private GameEvent _gameEvent;

    [Tooltip("This is the trigger that will start the event.")]
    [SerializeField, Header("Event Trigger")]
    private InterfaceWrapper<IEventTrigger> _eventTrigger;


    /// <summary>
    /// On startup, grab the trigger component and initialize it with this controller.
    /// </summary>
    private void Awake()
    {
        // Ensure we have a trigger assigned, otherwise we don't run
        if (_eventTrigger == null)
        {
            Debug.LogWarning("No event trigger assigned to the controller.");
            return;
        }

        // Initialize the trigger with this controller
        _eventTrigger.Value.Initialize(this);
    }

    /// <summary>
    /// The trigger will call this method to start the event.
    /// </summary>
    public void TriggerEvent()
    {
        StartCoroutine(RunEvent());
    }

    /// <summary>
    /// Executes the game event.
    /// </summary>
    /// <returns></returns>
    private IEnumerator RunEvent()
    {
        // Only run the event if it exists
        if (_gameEvent != null)
        {
            // Set the context for the event
            EventContext context = new()
            {
                player = GameObject.FindWithTag("Player"),
                camera = Camera.main,
                eventController = this
            };

            // Execute each action in the event
            foreach (var action in _gameEvent.actions)
            {
                // Run the action and get a tracker for its completion
                var trackedCoroutine = StartTrackedCoroutine(action.Execute(context));

                // If the action is not set to run in parallel, wait for it to complete
                if (!action.runInParallel)
                {
                    yield return new WaitUntil(() => trackedCoroutine.IsDone);
                }
            }
        }
        else
        {
            Debug.LogWarning("No event assigned to the controller.");
        }
    }

    #region Coroutine Tracking

    /// <summary>
    /// Starts a coroutine and returns a tracker to monitor its completion.
    /// </summary>
    /// <param name="coroutine"></param>
    /// <returns></returns>
    public CoroutineTracker StartTrackedCoroutine(IEnumerator coroutine)
    {
        var tracker = new CoroutineTracker();
        StartCoroutine(tracker.Run(coroutine));
        return tracker;
    }

    #endregion
}
