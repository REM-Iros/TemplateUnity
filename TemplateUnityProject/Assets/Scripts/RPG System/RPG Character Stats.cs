using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the scriptable object that holds the RPG character stats. This is used for both player and
/// enemy stats, and will be referenced by the RPGActor class.
/// 
/// REM-i
/// </summary>
[CreateAssetMenu(fileName = "RPGCharacterStats", menuName = "Scriptable Objects/RPGCharacterStats")]
public class RPGCharacterStats : ScriptableObject
{
    [Tooltip("This is the unique ID for the character.")]
    public string characterID;

    [Tooltip("This is the name of the character.")]
    public string characterName;

    [Tooltip("This is the headshot of the character.")]
    public Sprite characterHeadshot;

    [Tooltip("This is the full body image of the character.")]
    public Sprite characterFullBodyImage;

    [Tooltip("This is the stat block for the character")]
    public RPGStats baseStats;

    // Holy hack, but without setting up other things, this will work for now. 
    public List<ActionData> actionData;
}
