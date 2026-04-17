using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This coordinator script holds the action data for the actors. It will be responsible for the data management of the actions.
/// 
/// REM-i
/// </summary>
public class RPGActorActionCoordinator : MonoBehaviour
{
    #region Vars

    [Tooltip("This is the list of action instances that the actor can take. It should be filled by the actor on initialization.")]
    private List<ActionInstance> _actions;

    [Tooltip("This is a public getter for the list of actions.")]
    public int ActionCount => _actions.Count;

    #endregion

    #region Methods

    /// <summary>
    /// On init, create the new list.
    /// </summary>
    public void InitializeActionCoordinator()
    {
        _actions = new List<ActionInstance>();
    }

    /// <summary>
    /// This takes in the passed action data and creates an action instance from it then adds it to the list of actions. This is how the actor will be able to add actions to its list of actions.
    /// </summary>
    /// <param name="data"></param>
    public void AddAction(ActionData data)
    {
        _actions.Add(new ActionInstance(data));
    }

    /// <summary>
    /// Public get method for getting an action at an instance.
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public ActionInstance GetActionAtIndex(int index)
    {
        if (index < 0 || index >= _actions.Count)
        {
            Debug.LogError($"Index {index} is out of range for actions list.");
            return null;
        }

        return _actions[index];
    }

    #endregion
}
