using UnityEngine;

/// <summary>
/// This scriptable object will hold the data for an action that can be taken by the player in the RPG Bullet Hell segments. This specifically handles
/// attacks.
/// 
/// REM-i
/// </summary>
[CreateAssetMenu(fileName = "ActionData", menuName = "Scriptable Objects/ActionData")]
public class ActionData : ScriptableObject
{
    public string actionName;
    public int damageModifier;
    public int effectIndex;
}
