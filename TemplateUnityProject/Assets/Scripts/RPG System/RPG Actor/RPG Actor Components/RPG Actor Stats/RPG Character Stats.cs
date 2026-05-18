using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the scriptable object that holds the RPG character stats. This is used for both player and
/// enemy stats, and will be referenced by the RPGActor class.
/// 
/// REM-i
/// </summary>
[CreateAssetMenu(fileName = "RPGCharacterStats", menuName = "Scriptable Objects/RPG/RPGCharacterStats")]
public class CharacterStats : ScriptableObject
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
    public Stats baseStats;

    // In clarification, this is a hack for now because most games have characters learning skills over
    // time or based on equipment or level or such. For now, if you just need to test skills working, this
    // will work, but I don't know how this would look currently, and it would likely vary based upon rpg
    // implementation.
    public List<ActionData> actionData;
}
