using UnityEngine;

/// <summary>
/// This class will be used to store the data of an action from the Scriptable Object, and will be referenced for when a character takes an action to
/// preserve scriptable object data.
/// 
/// REM-i
/// </summary>
public class ActionInstance
{
    #region Vars

    [Tooltip("This is the overall action data that the instance references.")]
    public ActionData Data { get; }

    #region Getters

    public string ActionName => Data.actionName;
    public int DamageModifier => Data.damageModifier;
    public int EffectIndex => Data.effectIndex;

    #endregion

    #endregion

    #region Constructor

    /// <summary>
    /// Initialize script called to set the data of the action instance from the scriptable object data.
    /// </summary>
    /// <param name="actionData"></param>
    public ActionInstance(ActionData data)
    {
        // Only construct the instance if the data is not null, otherwise log an error and return
        if (data == null)
        {
            Debug.LogError("ActionInstance initialized with null data!");
            return;
        }

        Data = data;
    }

    #endregion
}
