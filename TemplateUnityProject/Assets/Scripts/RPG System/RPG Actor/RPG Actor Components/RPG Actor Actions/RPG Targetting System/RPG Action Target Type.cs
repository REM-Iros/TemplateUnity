using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This scriptable object will hold the data needed to resolve different target types for actions.
/// 
/// REM-i
/// </summary>
[CreateAssetMenu(fileName = "TargetType", menuName = "Scriptable Objects/RPG/Action/TargetType")]
public abstract class RPGActionTargetType : ScriptableObject
{
    // This is the list of actors that are targetable by the attack
    public abstract List<RPGActor> GetValidTargets(RPGBattleContext context, RPGActor user);

    // This indicates whether a player input will be required for selecting a target.
    public abstract bool DoesActionRequireValidation { get; }
}
