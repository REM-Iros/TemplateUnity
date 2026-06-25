using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script acts as the go between for the targeting ui and the battle system which provides context to this.
/// The battle manager passes in the actors, and this handles determining who is available for targetting.
/// </summary>
public class RPGTargetingManager : MonoBehaviour
{
    #region Vars

    [Tooltip("This is the battle context obj for the fight. We store it for reference later.")]
    private RPGBattleContext _battleContext;

    [Tooltip("This is the target event we use to reach the target ui.")]
    [SerializeField, Header("Events")]
    private RPGTargetEvent targetEvent;

    [Tooltip ("This is the untarget event we use to reach the target ui.")]
    [SerializeField]
    private RPGUntargetEvent untargetEvent;

    #endregion

    #region Methods

    /// <summary>
    /// On startup, need to pass in a reference to the battle context.
    /// </summary>
    /// <param name="battleContext"></param>
    public void InitializeTargetManager(RPGBattleContext battleContext)
    {
        _battleContext = battleContext;
    }

    /// <summary>
    /// On action run, get the valid targets and enable the UI based on context.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="User"></param>
    /// <param name="targets"></param>
    public void ResolveTarget(RPGActor user, ActionInstance action)
    {
        // Get the targeting strategy
        RPGActionTargetType targetType = action.Data.actionTargetStrategy;

        // Get the valid targets
        List<RPGActor> validTargets = targetType.GetValidTargets(_battleContext, user);

        // Run the target event.
        targetEvent.Raise(validTargets);
    }

    /// <summary>
    /// Called when you back out of targeting an enemy, raises the untarget event to clear targeting.
    /// </summary>
    public void RemoveTargets()
    {
        untargetEvent.Raise();
    }

    #endregion
}
