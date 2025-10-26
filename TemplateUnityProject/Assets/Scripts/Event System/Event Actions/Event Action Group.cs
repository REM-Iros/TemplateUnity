using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This scriptable object serves as a container for grouping multiple event actions together.
/// 
/// REM-i
/// </summary>
[CreateAssetMenu(menuName = "Event/Event Actions/EventActionGroup")]
public class EventActionGroup : EventAction
{
    [Tooltip("This is the list of actions that make up this event action group.")]
    [SerializeField, Header("Actions in group.")]
    private List<EventAction> actions;

    /// <summary>
    /// Override of the Execute method to run all actions in the group sequentially.
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public override IEnumerator Execute(EventContext context)
    {
        // Get the event controller from the context
        var controller = context.eventController;

        // Get a list to track currently running actions that will cause stoppage.
        var currNonParallelActions = new List<CoroutineTracker>();

        foreach (var action in actions)
        {
            var coroutine = controller.StartTrackedCoroutine(action.Execute(context));

            // If we want the action to block further actions until it completes, track it.
            if (!action.runInParallel)
            {
                currNonParallelActions.Add(coroutine);
            }
        }

        // Wait for all non-parallel actions to complete before finishing this group.
        if (currNonParallelActions.Count > 0)
        {
            yield return new WaitUntil(() => currNonParallelActions.TrueForAll(h => h.IsDone));
        }
    }
}
